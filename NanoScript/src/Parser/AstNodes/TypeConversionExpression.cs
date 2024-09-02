namespace NanoScript.Parser.AstNodes;

//    //Property Access Expressions: Expressions used to access properties of objects or structures.
//    //Assignment Expressions: Expressions that assign values to variables or locations.
//    //Type Conversion Expressions: Expressions that convert values between different data types.
//    | '(' identifier ')' exp
public class TypeConversionExpression : IExpression {
	public IdentifierExpression? identifier;
	public IExpression? exp;
	public string GenCS() {
		return $"({identifier?.GenCS()}){exp?.GenCS()}";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}