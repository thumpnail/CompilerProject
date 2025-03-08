namespace NanoScript.Parser.AstNodes;

//(identifier ':' statement* 'break'?)*
public class SubSwitchStatement : IStatement {
	public IdentifierExpression? identifier;
	public List<IStatement> statements = new();
	public bool isBreak = false;
	public bool isDefault = false;
	public  string GenCS() {
		var res = new StringBuilder();
		res.AppendLine("//TODO: SubSwitchStatement:");
		foreach (var item in statements) {
			res.AppendLine(item.GenCS());
		}
		return res.ToString();
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}