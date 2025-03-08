namespace NanoScript.Parser.AstNodes;

//    | string
public class StringExpression : IExpression {
	public string str = "";
	public new string ToString() {
		return str;
	}
	public  string GenCS() {
		return $"{ToString()}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}