namespace NanoScript.Parser.AstNodes;

//    // Label Statements: Statements that mark a specific point in the code for jumping or referencing (e.g., break to label).
//    | '::' identifier
public class LabelStatement : IStatement {
	public IdentifierExpression? identifier;

	public string GenCS() {
		return "//TODO: LabelStatement:\n";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}