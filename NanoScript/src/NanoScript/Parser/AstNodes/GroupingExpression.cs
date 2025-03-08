namespace NanoScript.Parser.AstNodes;

//    //Grouping Expression:
//    | '(' exp ')'
public class GroupingExpression : IExpression {
	public IExpression? exp;
	public  string GenCS() {
		return $"({exp?.GenCS()})";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}