namespace NanoScript.Parser.AstNodes;

//    // Error Handling: Statements for handling errors or exceptions (e.g., try-catch, throw).
//    | 'error' exp
public class ErrorStatement : IStatement {
	public IExpression? exp;

	public  string GenCS() {
		return "//TODO: ErrorStatement:\n";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}
