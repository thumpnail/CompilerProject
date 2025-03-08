namespace NanoScript.Parser.AstNodes;

//    | ('true'|'false')
public class BooleanExpression : IExpression {
	public bool value;
	public  string ToString() {
		return value.ToString();
	}
	public  string GenCS() {
		return $"{this}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}