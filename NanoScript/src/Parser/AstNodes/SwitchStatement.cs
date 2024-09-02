namespace NanoScript.Parser.AstNodes;

//TODO: Switch identifier with expression
//    | 'switch' exp '{' (identifier ':' statement* 'break'?)* 'default' ':' statement* 'break' '}'
public class SwitchStatement : IStatement {
	public IExpression? exp;
	public List<SubSwitchStatement> subSwitchStatements = new();
	//default
	public SubSwitchStatement? defSubSwitchStatement;
	public string GenCS() {
		var res = new StringBuilder();
		res.AppendLine("//TODO: SwitchStatement:");
		res.AppendLine(defSubSwitchStatement?.GenCS());
		foreach (var item in subSwitchStatements) {
			res.AppendLine(item.GenCS());
		}
		return res.ToString();
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}