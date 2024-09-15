namespace NanoScript.Parser.AstNodes;

public class NumberExpression : IExpression {
	public INumber? number;
	public  string GenCS() {
		return $"{number?.GetRawValue()??"<numberconversionerror>"}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}