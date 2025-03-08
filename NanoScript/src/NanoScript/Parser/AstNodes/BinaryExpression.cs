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
    public int Precedence { get => GetPredency(operatorType); }
    public BinaryExpression(IExpression left, BinaryOperatorType operatorType, IExpression right) {
        this.left = left;
        this.right = right;
        this.operatorType = operatorType;
    }
    public  string GenCS() {
	    var res = "";
	    if (operatorType == BinaryOperatorType.pow) {
		    res += $"Math.Pow({left.GenCS()}, {right.GenCS()})";
	    } else
		    res += $"{left.GenCS()} {GetBinOp(operatorType)} {right.GenCS()}";
	    switch (Precedence) {
		    case 4: 
			case 3: res = "(" + res + ")"; break;
			case 2:
			case 1:
			case 0:
			case -1:
            case -2:
            case -3:
			default: break;
	    }
        return $"{res}";
    }
    private int GetPredency(BinaryOperatorType binaryOperatorType) {
	    return operatorType switch {
		    BinaryOperatorType.pow => 4,             // Exponentiation has high precedence
		    BinaryOperatorType.mul => 3,             // Multiplication, Division, Modulus
		    BinaryOperatorType.div => 3,
		    BinaryOperatorType.mod => 3,
		    BinaryOperatorType.add => 2,             // Addition and Subtraction
		    BinaryOperatorType.sub => 2,
		    BinaryOperatorType.shl => 1,             // Bitwise shift operators
		    BinaryOperatorType.shr => 1,
		    BinaryOperatorType.and => 1,             // Bitwise AND
		    BinaryOperatorType.or => 1,              // Bitwise OR
		    BinaryOperatorType.xor => 1,             // Bitwise XOR
		    BinaryOperatorType.equals => 0,          // Comparison Operators (==, !=, <, >, <=, >=)
		    BinaryOperatorType.notEquals => 0,
		    BinaryOperatorType.lessEquals => 0,
		    BinaryOperatorType.less => 0,
		    BinaryOperatorType.greaterEquals => 0,
		    BinaryOperatorType.greater => 0,
		    BinaryOperatorType.doubleAnd => -1,      // Logical AND (&&)
		    BinaryOperatorType.doubleOr => -2,       // Logical OR (||)
		    BinaryOperatorType.not => -3,            // Logical NOT
		    BinaryOperatorType.none => 0,
		    _ => throw new ArgumentOutOfRangeException()
	    };
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
    public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public  List<string> TranspileToAsm() {throw new NotImplementedException();}
    public string ToXml() {
	    throw new NotImplementedException();
    }
}