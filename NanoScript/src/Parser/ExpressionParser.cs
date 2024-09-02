using NanoScript.Parser.AstNodes;
using static NanoScript.Token;

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
    private IExpression? ParseFullExpression() {
        IExpression res;
        if (!ctx.boundCheck(ctx.idx)) {
            return null;
        }
        //Expression exp;
        res = ParseExpression()??throw new NullReferenceException();
        return res;
    }
    //<literal> := <number> | <string> | <boolean> | <identifier> | <functionCall> | <arrayIndexing>
    private IExpression? ParseLiteralExpression() {
        ctx.CreateFrame();
        IExpression res;
        switch (ctx.Peek_tk()) {
            case IDENTIFIER:
                res = ParseIdentifierExpression();
                break;
            case NUMBER:
                res = ParseNumberExpression()??throw new NullReferenceException();
                break;
            case STRING:
                res = ParseStringExpression()??throw new NullReferenceException();
                break;
            case TRUE:
            case FALSE:
                res = ParseBooleanExpression()??throw new NullReferenceException();
                break;
            default:
                ctx.PopFrame();
                return null;
        }
        
        ctx.ClearFrame();
        return res;
    }
    //<arrayIndexing> := <identifier> '[' <exp> ']'
    //FIX: Might not get parsed correctly
    private ArrayIndexingExpression? ParseArrayIndexingExpression() {
        ctx.CreateFrame();
        ArrayIndexingExpression res;
        if (ctx.Peek_tk(IDENTIFIER) && ctx.PeekNext_tk(1) == LEFTBRACKET) {
            //TODO: Implement Array Indexing
            var ident = ParseIdentifierExpression();
            ctx.Consume_tk(LEFTBRACKET);
            var index = ParseExpression();
            ctx.Consume_tk(RIGHTBRACKET);
            res = new() {
                identifier = ident, index = index
            };
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<functionCall> := <identifier> '(' <list> ')'
    private FunctionCallExpression? ParseFunctionCallExpression() {
        ctx.CreateFrame();
        FunctionCallExpression res;
        if (ctx.Peek_tk(IDENTIFIER) && ctx.PeekNext_tk(1) == LEFTPAREN) {
            //TODO: Implement Function Call
            var identifier = ParseIdentifierExpression();
            ctx.Consume_tk(LEFTPAREN);
            var expressionList = ParseListExpression();
            ctx.Consume_tk(RIGHTPAREN);
            res = new FunctionCallExpression {
	            identifier = identifier, parameters = expressionList ?? new()
            };
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<typeConversion> := '(' <identifier> ')' <exp>
    private TypeConversionExpression? ParseTypeConversionExpression() {
        ctx.CreateFrame();
        TypeConversionExpression res = new();
        if (ctx.Peek_tk(LEFTPAREN) && ctx.PeekNext_tk(IDENTIFIER) && ctx.PeekNext_tk(2) == RIGHTPAREN) {
	        
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<base> := <literal> | <identifier> | <arrayIndexing> | <functionCall> | <typeConversion> | '(' <exp> ')'
    private IExpression? ParseBaseExpression() {
        ctx.CreateFrame();
        IExpression res = null;
        if (ctx.Peek_tk(IDENTIFIER)) {
            //FunctionCallExpression
            FunctionCallExpression? functionCallExpression;
            ArrayIndexingExpression? arrayIndexingExpression;
            if (ctx.PeekNext_tk(LEFTPAREN) && (functionCallExpression = ParseFunctionCallExpression()) is not null) {
	            res = functionCallExpression;
            }
            //arrayIndexing
            else if (ctx.PeekNext_tk(LEFTBRACKET) && (arrayIndexingExpression = ParseArrayIndexingExpression()) is not null) {
	            res = arrayIndexingExpression;
            }
            //identifier
            else {
                res = ParseIdentifierExpression();
            }
        } else if (ctx.Peek_tk(NUMBER)) {
            res = ParseNumberExpression() ?? throw new NullReferenceException();
        } else if (ctx.Peek_tk(STRING)) {
            res = ParseStringExpression() ?? throw new NullReferenceException();
        } else if (ctx.Peek_tk(TRUE, FALSE)) {
            res = ParseBooleanExpression() ?? throw new NullReferenceException();
        } else if (ctx.Peek_tk(LEFTPAREN)) {
            //TODO: Implement Expression Grouping
            ctx.Consume_tk(LEFTPAREN);
            res = ParseExpression() ?? throw new NullReferenceException();
            ctx.Consume_tk(RIGHTPAREN);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<power> := ['++'|'--'|'!'|'~'] <base> ['++'|'--']
    private IExpression? ParsePowerExpression() {
        ctx.CreateFrame();
        IExpression res = null;
        IExpression exp = null;
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
    private IExpression? ParseFactorExpression() {
        var op = BinaryOperatorType.none;
        ctx.CreateFrame();
        IExpression res;
        IExpression exp;

        if ((exp = ParsePowerExpression()) is not null) {
            if ((op = GetBinOp(ctx.Peek_tk())) == BinaryOperatorType.pow) {
                ctx.Consume_tk();
                res = new BinaryExpression(exp, op, ParseFactorExpression());
            } else {
                res = exp;
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<term> := <factor> { ('*' | '/' | '%' | '==' | '!=' | '<' | '>' | '<=' | '>=') <factor> }
    private IExpression? ParseTermExpression() {
        BinaryOperatorType op;
        ctx.CreateFrame();
        IExpression res;
        IExpression exp;
        if ((exp = ParseFactorExpression()) is not null) {
            if ((op = GetBinOp(ctx.Peek_tk())) != BinaryOperatorType.none) {
                ctx.Consume_tk();
                res = new BinaryExpression(exp, op, ParseTermExpression());
            } else {
                res = exp;
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<exp> := <term> { ('+' | '-' | '&&' | '||') <term> } | '[' <list> ']' | identifier '{' <list> '}'
    private IExpression? ParseExpression() {
        ctx.CreateFrame();
        IExpression? res = null;
        IExpression? exp;
        if (ctx.Peek_tk(LEFTBRACKET)) {
            ctx.Consume_tk(LEFTBRACKET);
            if ((exp = ParseListExpression()) is not null) {
                res = new ArrayCreationExpression() {
                    expressions = ((ExpressionList)exp).expressions
                };
            }
        } else if (ctx.Peek_tk(IDENTIFIER) && ctx.PeekNext_tk(LEFTBRACE)) {
            var ident = ParseIdentifierExpression();
            ctx.Consume_tk(LEFTBRACE);
            if ((exp = ParseListExpression()) is not null) {
                res = new InstanceInitializationExpression() {
                    identifier = ident,
                    expressions = ((ExpressionList)exp).expressions
                };
            }
            ctx.Consume_tk(RIGHTBRACE);
        } else if ((exp = ParseTermExpression()) is not null) {
            BinaryOperatorType? op;
            if ((op = GetBinOp(ctx.Peek_tk())) == BinaryOperatorType.add || op == BinaryOperatorType.sub ||
                op == BinaryOperatorType.and || op == BinaryOperatorType.or) {
                ctx.Consume_tk(); //consume operator
                res = new BinaryExpression(exp, op ?? throw new NullReferenceException(), ParseExpression());
            } else {
                res = exp;
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //<list> := <exp> {',' <exp>}
    private ExpressionList? ParseListExpression() {
        ExpressionList expLi = new();
        ctx.CreateFrame();
        IExpression? exp = null;
        if (ctx.PeekNext_tk(COLON)) {
            IExpression? indexExpr;
            IExpression? valueExpr;
            //custom index array workaround
            while ((indexExpr = ParseLiteralExpression()) is not null && ctx.Consume_tk(COLON) && (valueExpr = ParseExpression())is not null) {
                ctx.Consume_tk(COMMA);
                expLi.expressions.Add(new IndexExpression(indexExpr, valueExpr));
            }
        } else if ((exp = ParseExpression()) is not null) {
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
        return expLi;
    }


    public IExpression? TryParseUnaryExpression(out UnaryOperatorType leftOp, out Token current_tk,
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
        IExpression? res;
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
                _ => throw new ArgumentOutOfRangeException()
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
            IExpression? exp;
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
    private bool tkIsOperator(Token peekTk) {
        return (int)peekTk >= (int)Token.LEFTBRACE &&
               (int)peekTk <= (int)Token.DOUBLEMINUS;
    }
    private UnaryOperatorType GetUnaryOp(Token peekTk) {
        return peekTk switch {
            EXLEMATIONMARK => UnaryOperatorType.not,
            DOUBLEPLUS => UnaryOperatorType.inc,
            DOUBLEMINUS => UnaryOperatorType.dec,
            _ => 0
        };
    }
    private BinaryOperatorType GetBinOp(Token peekTk) {
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
    private bool tkIsValue(Token peekTk) {
        return (int)peekTk >= (int)Token.IDENTIFIER &&
               (int)peekTk <= (int)Token.FALSE;
    }
    private bool tkIsKeyword(Token peekTk) {
        return (int)peekTk >= (int)Token.PUB && (int)peekTk <= (int)Token.IN;
    }
    private NumberExpression? ParseNumberExpression() {
        ctx.CreateFrame();
        var res = new NumberExpression();
        if (ctx.Peek_tk(Token.NUMBER)) {
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
        if (ctx.Peek_tk(Token.STRING)) {
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
        if (ctx.Peek_tk(Token.TRUE) || ctx.Peek_tk(Token.FALSE)) {
            res.value = ctx.Consume_tk(Token.TRUE);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //identifier: ('.')? WORD ('.' WORD)* (':' WORD)?;
    private IdentifierExpression? ParseIdentifierExpression() {
        ctx.CreateFrame();
        var res = new IdentifierExpression();
        if (ctx.Peek_tk(IDENTIFIER)) {
	        var ident = ctx.Consume() ?? throw new Exception("Expected IDENTIFIER");
	        if (ident.Contains(':')) {
		        var split1 = ident.Split(':')[1];
		        res.lastIdentifier = split1;
	        }
	        if (ident.Contains('.')) {
		        var split = ident.Split('.');
		        res.identifier = split[0];
		        for (int index = 1; index < split.Length; index++) {
			        var item = split[index];
			        res.identifiers.Add(item);
		        }
	        } else {
		        res.identifier = ident;
	        }
	        //if (ctx.Peek_tk(Token.IDENTIFIER) || (ctx.Peek_tk(Token.DOT) && ctx.PeekNext_tk(Token.IDENTIFIER))) {
	        //    if (ctx.Consume_tk(Token.DOT)) res.isSelf = true;
	        //    res.identifier = ctx.Consume();
	        //    while (ctx.Consume_tk(Token.DOT)) {
	        //        res.identifiers.Add(ctx.Consume());
	        //    }
	        //    if (ctx.Consume_tk(Token.DOUBLECOLON)) {
	        //        res.isExtension = true;
	        //        res.lastIdentifier = ctx.Consume();
	        //    }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
}