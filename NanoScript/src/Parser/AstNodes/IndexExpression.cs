namespace NanoScript.Parser.AstNodes;

public class IndexExpression : IExpression {
	public IExpression? index;
	public IExpression? identifier;
	public IndexExpression(IExpression index, IExpression identifier) {
		this.index = index;
		this.identifier = identifier;
	}
	public string GenCS() {
		return $"{identifier?.GenCS()}[{index?.GenCS()}]";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}