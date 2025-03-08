namespace NanoScript.Parser.AstNodes;

//    | 'goto' identifier
public class GotoStatement : IStatement {
	public IdentifierExpression? identifier;

	public  string GenCS() {
		return "//TODO: GotoStatement:\n";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}