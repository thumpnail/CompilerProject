namespace NanoScript.Parser.AstNodes;

//    //Tuple Creation Expression:
//    | '(' exp (',' exp) ')'
public class TupleCreationExpression : IExpression {
	public IExpression? exp;
	private List<IExpression> expressions = new();
	public string GenCS() {
		var res = new StringBuilder();
		List<string> explist = new();
		foreach(var item in expressions) {
			explist.Add($"{item.GenCS()}");
		}
		res.Append(string.Join(',', explist));
		return $"({res})";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}