namespace NanoScript.Parser.AstNodes;

//    | number
public struct IntegerExpression : INumber {
	public Int128 int_number;
	public string RAW;
	public IntegerExpression(string val) {
		this.int_number = Int128.Parse(val);
		this.RAW = val;
	}

	public string GetRawValue() {
		return RAW;
	}
}