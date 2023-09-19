using static NanoScript.Program.Token;

namespace NanoScript.Parser;

public partial class Parser {
    // Expression Parser Recursive
    //      exp <op> exp
    //      '(' exp ')'
    //      exp '(' exp ')'
    //      exp '[' exp ']'
    //      exp '.' identifier
    //      '(' identifier ')' exp
    //    // 'match' '(' exp ')' '{' exp '=>' exp ('|' exp '=>' exp)* '}'
    private Expression? ParseFullExpression() {
        Expression res = null;
        //TODO: bound check
        if (!ctx.boundCheck(ctx.idx)) {
            return null;
        }
        //Expression exp;
        res = ParseExpression();

        return res;
    }
    //<literal> := <number> | <string> | <boolean>
    private Expression? ParseLiteralExpression() {
        ctx.CreateFrame();
        Expression res = null;
        if (ctx.Peek_tk(NUMBER) || ctx.Peek_tk(STRING) || ctx.Peek_tk(TRUE) || ctx.Peek_tk(FALSE)) {

        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<arrayIndexing> := <identifier> '[' <exp> ']'
    private Expression? ParseArrayIndexingExpression() {
        ctx.CreateFrame();
        Expression res = null;
        if (ctx.Peek_tk(IDENTIFIER) && ctx.PeekNext_tk(1) == LEFTBRACKET) {

        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<functionCall> := <identifier> '(' <list> ')'
    private Expression? ParseCallExpression() {
        ctx.CreateFrame();
        Expression res = null;
        if (ctx.Peek_tk(IDENTIFIER) && ctx.PeekNext_tk(1) == LEFTPAREN) {
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<typeConversion> := '(' <identifier> ')' <exp>
    private Expression? ParseTypeConversionExpression() {
        ctx.CreateFrame();
        Expression res = null;
        if (ctx.Peek_tk(LEFTPAREN) && ctx.PeekNext_tk(IDENTIFIER) && ctx.PeekNext_tk(2) == RIGHTPAREN) {
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<base> := <literal> | <identifier> | <arrayIndexing> | <functionCall> | <typeConversion> | '(' <exp> ')'
    private Expression? ParseBaseExpression() {
        ctx.CreateFrame();
        Expression res = null;
        if (ctx.Peek_tk(IDENTIFIER)) {
            //functionCall
            if (ctx.PeekNext_tk(LEFTPAREN)) {
                res = ParseCallExpression();
            }
            //arrayIndexing
            else if (ctx.PeekNext_tk(LEFTBRACKET)) {
                res = ParseArrayIndexingExpression();
            }
            //identifier
            else {
                res = ParseIdentifierExpression();
            }
        } else if (ctx.Peek_tk(NUMBER)) {
            res = ParseNumberExpression();
        } else if (ctx.Peek_tk(STRING)) {
            res = ParseStringExpression();
        } else if (ctx.Peek_tk(TRUE, FALSE)) {
            res = ParseBooleanExpression();
        } else if (ctx.Peek_tk(LEFTPAREN)) {
            ctx.Consume_tk(LEFTPAREN);
            res = ParseExpression();
            ctx.Consume_tk(RIGHTPAREN);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<power> := ['++'|'--'|'!'|'~'] <base> ['++'|'--']
    private Expression? ParsePowerExpression() {
        ctx.CreateFrame();
        Expression res = null;
        Expression exp = null;
        UnaryOperatorType leftOp = 0;
        UnaryOperatorType rightOp = 0;

        //get left op
        if ((ctx.Peek_tk(DOUBLEPLUS) || ctx.Peek_tk(DOUBLEMINUS) ||
             ctx.Peek_tk(EXLEMATIONMARK) /*|| ctx.Peek_tk(MINUS)*/))
            leftOp = GetUnaryOp(ctx.Consume_tk());

        //get base
        if ((exp = ParseBaseExpression()) != null) {
            res = exp;
        } else {
            ctx.PopFrame();
            return null;
        }

        //get right op
        if ((ctx.Peek_tk(DOUBLEPLUS) || ctx.Peek_tk(DOUBLEMINUS)) /*&& !tkIsOperator()*/)
            rightOp = GetUnaryOp(ctx.Consume_tk());

        //Merging
        if (leftOp is not 0 && rightOp is not 0) {
            return new UnaryExpression() {
                exp = new UnaryExpression() { exp = res, isBefore = false, operatorType = rightOp }, isBefore = true,
                operatorType = leftOp
            };
        } else if (leftOp is not 0) {
            return new UnaryExpression() { exp = res, isBefore = true, operatorType = leftOp };
        } else if (rightOp is not 0) {
            return new UnaryExpression() { exp = res, isBefore = false, operatorType = rightOp };
        }
        ctx.ClearFrame();
        return res;
    }
    //<factor> := <power> { '**' <power> }
    private Expression? ParseFactorExpression() {
        var op = BinaryOperatorType.none;
        ctx.CreateFrame();
        Expression res = null;
        Expression exp = null;

        if ((exp = ParsePowerExpression()) is not null) {
            while ((op = GetBinOp(ctx.Peek_tk())) == BinaryOperatorType.pow) {
                ctx.Consume_tk();
                res = new BinaryExpression() {
                    left = exp,
                    operatorType = op,
                    right = ParsePowerExpression()
                };
            }
            if (op != BinaryOperatorType.pow) res = exp;
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<term> := <factor> { ('*' | '/' | '%' | '==' | '!=' | '<' | '>' | '<=' | '>=') <factor> }
    private Expression? ParseTermExpression() {
        BinaryOperatorType op = BinaryOperatorType.none;
        ctx.CreateFrame();
        Expression res = null;
        Expression exp = null;
        if ((exp = ParseFactorExpression()) is not null) {
            while ((op = GetBinOp(ctx.Peek_tk())) != BinaryOperatorType.none) {
                ctx.Consume_tk();
                res = new BinaryExpression() {
                    left = exp,
                    operatorType = op,
                    right = ParseFactorExpression()
                };
            }
            // if(op == BinaryOperatorType.none) {
            //     res = exp;
            // }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<exp> := <term> { ('+' | '-' | '&&' | '||') <term> } | '[' <list> ']' | '{' <list> '}'
    private Expression? ParseExpression() {
        ctx.CreateFrame();
        Expression res = null;
        Expression exp = null;
        if (ctx.Peek_tk(LEFTBRACKET)) {
            ctx.Consume_tk(LEFTBRACKET);
            if ((exp = ParseListExpression()) is not null) {
                res = new ArrayCreationExpression() {
                    expressions = ((ExpressionList)exp).expressions
                };
            }
        } else if (ctx.Peek_tk(LEFTBRACE)) {
            ctx.Consume_tk(LEFTBRACE);
            if ((exp = ParseListExpression()) is not null) {
                //TODO: res = Table;
                throw new NotImplementedException();
                res = new ArrayCreationExpression() {
                    expressions = ((ExpressionList)exp).expressions
                };
            }
        } else if ((exp = ParseTermExpression()) is not null) {
            BinaryOperatorType op = BinaryOperatorType.none;
            while ((op = GetBinOp(ctx.Peek_tk())) != BinaryOperatorType.none) {
                ctx.Consume_tk();
                res = new BinaryExpression() {
                    left = exp,
                    operatorType = op,
                    right = ParseTermExpression()
                };
            }
            if (op == BinaryOperatorType.none) res = exp;
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<list> := <exp> {',' <exp>}
    private Expression? ParseListExpression() {
        ExpressionList expLi = null;
        ctx.CreateFrame();
        Expression res = null;
        Expression exp = null;
        if ((exp = ParseExpression()) is not null) {
            expLi.expressions.Add(exp);
            while (ctx.Consume_tk(COMMA)) {
                if ((exp = ParseExpression()) is not null) {
                    expLi.expressions.Add(exp);
                }
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }


    public Expression? TryParseUnaryExpression(out UnaryOperatorType leftOp, out NanoScript.Program.Token current_tk,
        out string current_str, out UnaryOperatorType rightOp) {

        leftOp = 0;
        rightOp = 0;

        if ((ctx.Peek_tk(DOUBLEPLUS) || ctx.Peek_tk(DOUBLEMINUS) ||
             ctx.Peek_tk(EXLEMATIONMARK) /*|| ctx.Peek_tk(MINUS)*/))
            leftOp = ctx.Consume_tk() switch {
                //MINUS => UnaryOperatorType.neg,
                DOUBLEPLUS => UnaryOperatorType.inc,
                EXLEMATIONMARK => UnaryOperatorType.not,
                DOUBLEMINUS => UnaryOperatorType.dec,
                _ => 0
            };
        //Parse Based on Tokentype
        Expression res;
        current_tk = ctx.Peek_tk();
        current_str = ctx.Peek();
        switch (ctx.Peek_tk()) {
            case IDENTIFIER:
                res = ParseIdentifierExpression();
                break;
            case NUMBER:
                res = ParseNumberExpression();
                break;
            case STRING:
                res = ParseStringExpression();
                break;
            case TRUE:
            case FALSE:
                res = ParseBooleanExpression();
                break;
            default:
                res = null;
                break;
        }
        if ((ctx.Peek_tk(DOUBLEPLUS) || ctx.Peek_tk(DOUBLEMINUS)) && !tkIsOperator(current_tk))
            rightOp = ctx.Consume_tk() switch {
                DOUBLEPLUS => UnaryOperatorType.inc,
                DOUBLEMINUS => UnaryOperatorType.dec,
            };
        if (leftOp is not 0 && rightOp is not 0) {
            return new UnaryExpression() {
                exp = new UnaryExpression() { exp = res, isBefore = false, operatorType = rightOp }, isBefore = true,
                operatorType = leftOp
            };
        } else if (leftOp is not 0) {
            return new UnaryExpression() { exp = res, isBefore = true, operatorType = leftOp };
        } else if (rightOp is not 0) {
            return new UnaryExpression() { exp = res, isBefore = false, operatorType = rightOp };
        }
        return res;
    }
    // '[' [ <exp> {',' <exp>} ] ']'
    private ArrayCreationExpression? ParseArrayCreationExpression() {
        ctx.CreateFrame();
        var res = new ArrayCreationExpression();
        if (ctx.Consume_tk(LEFTBRACKET)) {
            //Parse List Expression
            Expression exp = null;
            while ((exp = ParseExpression()) is not null) {
                res.expressions.Add(exp);
                if (!ctx.Peek_tk(COMMA) && !ctx.Peek_tk(RIGHTBRACKET)) break;
            }
            ctx.Consume_tk(RIGHTBRACKET);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private bool tkIsOperator(NanoScript.Program.Token peekTk) {
        return (int)peekTk >= (int)NanoScript.Program.Token.LEFTBRACE &&
               (int)peekTk <= (int)NanoScript.Program.Token.DOUBLEMINUS;
    }
    private UnaryOperatorType GetUnaryOp(NanoScript.Program.Token peekTk) {
        return peekTk switch {
            EXLEMATIONMARK => UnaryOperatorType.not,
            DOUBLEPLUS => UnaryOperatorType.inc,
            DOUBLEMINUS => UnaryOperatorType.dec,
            _ => 0
        };
    }
    private BinaryOperatorType GetBinOp(NanoScript.Program.Token peekTk) {
        return peekTk switch {
            PLUS => BinaryOperatorType.add,
            MINUS => BinaryOperatorType.sub,
            STAR => BinaryOperatorType.mul,
            SLASH => BinaryOperatorType.div,
            PERCENT => BinaryOperatorType.mod,
            DOUBLESTAR => BinaryOperatorType.pow,

            TILDE => BinaryOperatorType.not,
            CIRCUMFLEX => BinaryOperatorType.xor,
            PIPE => BinaryOperatorType.or,
            AND => BinaryOperatorType.and,
            DOUBLEPIPE => BinaryOperatorType.doubleOr,
            DOUBLEAND => BinaryOperatorType.doubleAnd,

            DOUBLEEQUAL => BinaryOperatorType.equals,
            NOTEQUAL => BinaryOperatorType.notEquals,
            GREATER => BinaryOperatorType.greater,
            LESS => BinaryOperatorType.less,
            GREATEREQUALS => BinaryOperatorType.greaterEquals,
            LESSEQUAL => BinaryOperatorType.lessEquals,

            DOUBLERIGHT => BinaryOperatorType.shr,
            DOUBLELEFT => BinaryOperatorType.shl,

            _ => BinaryOperatorType.none
        };
    }
    private bool tkIsValue(NanoScript.Program.Token peekTk) {
        return (int)peekTk >= (int)NanoScript.Program.Token.IDENTIFIER &&
               (int)peekTk <= (int)NanoScript.Program.Token.FALSE;
    }
    private bool tkIsKeyword(NanoScript.Program.Token peekTk) {
        return (int)peekTk >= (int)NanoScript.Program.Token.PUB && (int)peekTk <= (int)NanoScript.Program.Token.IN;
    }
    private NumberExpression? ParseNumberExpression() {
        ctx.CreateFrame();
        var res = new NumberExpression();
        if (ctx.Peek_tk(NanoScript.Program.Token.NUMBER)) {
            var tmp = ctx.Consume();
            res.number = tmp.Contains('.') ? new FloatExpression(tmp) : new IntegerExpression(tmp);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private StringExpression? ParseStringExpression() {
        ctx.CreateFrame();
        var res = new StringExpression();
        if (ctx.Peek_tk(NanoScript.Program.Token.STRING)) {
            res.str = ctx.Consume();
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private BooleanExpression? ParseBooleanExpression() {
        ctx.CreateFrame();
        var res = new BooleanExpression();
        if (ctx.Peek_tk(NanoScript.Program.Token.TRUE) || ctx.Peek_tk(NanoScript.Program.Token.FALSE)) {
            res.value = ctx.Consume_tk(NanoScript.Program.Token.TRUE);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //identifier: WORD ('.' WORD)* (':' WORD);
    private IdentifierExpression ParseIdentifierExpression() {
        ctx.CreateFrame();
        var res = new IdentifierExpression();
        if (ctx.Peek_tk(NanoScript.Program.Token.IDENTIFIER) || (ctx.Peek_tk(NanoScript.Program.Token.DOT) &&
                                                                 ctx.PeekNext_tk(NanoScript.Program.Token
                                                                     .IDENTIFIER))) {
            if (ctx.Consume_tk(NanoScript.Program.Token.DOT)) res.isSelf = true;
            res.identifier = ctx.Consume();
            while (ctx.Consume_tk(NanoScript.Program.Token.DOT)) {
                res.identifiers.Add(ctx.Consume());
            }
            if (ctx.Consume_tk(NanoScript.Program.Token.COLON)) {
                res.isExtension = true;
                res.lastIdentifier = ctx.Consume();
            }

        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
}