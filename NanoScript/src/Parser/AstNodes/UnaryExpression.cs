namespace NanoScript.Parser.AstNodes;

//    //Unary Expressions: Expressions involving a single operand (e.g., negation, logical NOT).
//    | '!' exp
//    | '-' exp
//    | '++' exp
//    | exp '++'
//    | '--' exp
//    | exp'--'
public enum UnaryOperatorType {
	none,
	not, // !
	//neg, // -
	inc, // ++
	dec, // --
}

public class UnaryExpression : IExpression {
	public bool isBefore;
	public IExpression? exp;
	public UnaryOperatorType operatorType;
	public string GenCS() {
		if(isBefore)
			return $"{operatorType} {exp?.GenCS()}";
		return $"{exp?.GenCS()} {operatorType}";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}