namespace NanoScript.Parser.AstNodes;

public class ExpressionList : IExpression {
	public List<IExpression> expressions = new();
	public string GenCS() {
		var res = "";
		for (int i = 0; i < expressions.Count(); i++) {
			res += expressions[i].GenCS();
			if(i < expressions.Count()) res += ",";
		}
		return res;
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}