namespace NanoScript.Parser.AstNodes;

//    | string
public class StringExpression : IExpression {
	public string str = "";
	public string ToString() {
		return str;
	}
	public string GenCS() {
		return $"{this}";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}