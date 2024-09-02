namespace NanoScript.Parser.AstNodes;

//    // Return Statements: Statements that return values from functions or methods.
//    | 'return' exp
public class ReturnStatement : IStatement {
	public IExpression? exp;

	public string GenCS() {
		return $"return {exp?.GenCS()};";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}