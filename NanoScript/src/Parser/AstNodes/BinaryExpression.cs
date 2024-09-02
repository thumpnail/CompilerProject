namespace NanoScript.Parser.AstNodes;

//    //Arithmetic Expressions: Expressions that perform mathematical operations (e.g., addition, subtraction).
//    | exp '+' exp
//    | exp '-' exp
//    | exp '/' exp
//    | exp '*' exp
//    //Grouping Expression:
//    | '(' exp ')'
//    //Logical Expressions: Expressions that involve logical operations (e.g., AND, OR).
//    | exp '||' exp
//    | exp '&&' exp
//    //Comparison Expressions: Expressions that compare values (e.g., greater than, equal to).
//    | exp '==' exp
//    | exp '!=' exp
//    | exp '<=' exp
//    | exp '<' exp
//    | exp '>=' exp
//    | exp '>' exp
//    //Bitwise Expressions: Expressions that manipulate individual bits within values.
//    | exp '&' exp
//    | exp '|' exp
//    | exp '^' exp
//    | exp '<<' exp
//    | exp '>>' exp
//    | exp '~' exp
public enum BinaryOperatorType {
    add, // +
    sub, // -
    div, // /
    mul, // *
    mod, // %
    pow, // **

    doubleOr, // ||
    doubleAnd, // &&

    equals, // ==
    notEquals, // !=
    lessEquals, // <=
    less, // <
    greaterEquals, // >=
    greater, // >

    and, // &
    or, // |
    xor, // ^
    shl, // <<
    shr, // >>
    not, // ~
    none
}

public class BinaryExpression : IExpression {
    public IExpression left;
    public IExpression right;
    public BinaryOperatorType operatorType = BinaryOperatorType.none;
    public BinaryExpression(IExpression left, BinaryOperatorType operatorType, IExpression right) {
        this.left = left;
        this.right = right;
        this.operatorType = operatorType;
    }
    public string GenCS() {
        return $"{left.GenCS()} {GetBinOp(operatorType)} {right.GenCS()}";
    }
    public string GetBinOp(BinaryOperatorType type) {
        return type switch {
            BinaryOperatorType.add => "+",
            BinaryOperatorType.sub => "-",
            BinaryOperatorType.div => "/",
            BinaryOperatorType.mul => "*",
            BinaryOperatorType.mod => "%",
            BinaryOperatorType.pow => "**",
            BinaryOperatorType.doubleOr => "||",
            BinaryOperatorType.doubleAnd => "&&",
            BinaryOperatorType.equals => "==",
            BinaryOperatorType.notEquals => "!=",
            BinaryOperatorType.lessEquals => "<=",
            BinaryOperatorType.less => "<",
            BinaryOperatorType.greaterEquals => ">=",
            BinaryOperatorType.greater => ">",
            BinaryOperatorType.and => "&",
            BinaryOperatorType.or => "|",
            BinaryOperatorType.xor => "^",
            BinaryOperatorType.shl => "<<",
            BinaryOperatorType.shr => ">>",
            BinaryOperatorType.not => "~",
            _ => type.ToString()
        };
    }
    public List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public List<string> TranspileToAsm() {throw new NotImplementedException();}
}