namespace NanoScript.Parser.AstNodes;

public class SubConditionalStatement : IStatement {
	public bool isElse;
	public bool isIf;
	public IExpression? exp;
	public List<IStatement> statements = new();
	public  string GenCS() {
		var res = $"{(isElse ? "else" : "")} {(isIf ? $"if({exp?.GenCS()})" : "")} {{";
		foreach (var statement in statements) {
			res += $"{statement.GenCS()}";
		}
		res += "\n}";
		return $"{res}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}