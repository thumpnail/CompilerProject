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
        
        //TODO: Parse Unary1
        //TODO: Parse Literal
        //TODO: Parse Unary2
        // Returns a Expression or UnaryExpression if a value(num,str,bool) is found
        Expression exp;
        if ((exp = TryParseUnaryExpression(out var leftOp, out var current_tk, out var current_str, out var rightOp)) is not null) {
            res = exp;
        }

        if (ctx.Peek_tk(IDENTIFIER) && ctx.PeekNext_tk(1) == LEFTPAREN) {
            //exp = ParseCallExpression();
        }
        
        if (ctx.Peek_tk(IDENTIFIER) && ctx.PeekNext_tk(1) == LEFTBRACKET) {
            //exp = ParseArrayIndexingExpression();
        }
        
        //TODO: Parse ArrayDeclaration
        if (ctx.Peek_tk(LEFTBRACKET)) {
            
        }
        //TODO: Parse Create Binary Expression(+ Unary)
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
    private Expression? ParseArithmeticExpression() {
        Expression res = null;
        switch (ctx.Peek_tk()) {
            
        }
        return null;
    }
    private ArrayCreationExpression? ParseArrayCreationExpression() {
        ctx.CreateFrame();
        var res = new ArrayCreationExpression();
        if (ctx.Peek_tk(NanoScript.Program.Token.LEFTBRACKET)) {

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