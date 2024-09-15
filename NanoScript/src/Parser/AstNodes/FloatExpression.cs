namespace NanoScript.Parser.AstNodes;

public class FloatExpression : INumber {
	public double double_number;
	public string RAW;
	public FloatExpression(string val) {
		this.double_number = double.Parse(val);
		this.RAW = val;
	}

	public  string GetRawValue() {
		return RAW;
	}
}