namespace NanoScript.Parser.AstNodes;

//    //Array Creation Expression
//    | identifier '{' ( (exp (',' exp)*)? '}'
public class InstanceInitializationExpression : IExpression {
	public IdentifierExpression? identifier;
	public List<IExpression> expressions = new();
	public  string GenCS() {
		return "InstanceInitializationExpression:";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}