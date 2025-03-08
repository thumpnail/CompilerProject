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
	public  string GenCS() {
		if(isBefore)
			return $"{operatorType switch {
				UnaryOperatorType.dec => "--",
				UnaryOperatorType.inc => "++",
				UnaryOperatorType.not => "!",
				UnaryOperatorType.none => "",
				_ => throw new ArgumentOutOfRangeException() }} {exp?.GenCS()}";
		return $"{exp?.GenCS()} {operatorType switch {
			UnaryOperatorType.dec => "--",
			UnaryOperatorType.inc => "++",
			UnaryOperatorType.not => throw new ArgumentOutOfRangeException(),
			UnaryOperatorType.none => "",
			_ => throw new ArgumentOutOfRangeException() 
			 }}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}