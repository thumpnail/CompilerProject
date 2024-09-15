namespace NanoScript.Parser.AstNodes;

//    //Pointer or Reference Expressions: Expressions that deal with memory addresses (in languages like C and C++).
//    | '&' exp
//    | '*' exp
public class PointerOrReferenceExpression : IExpression {
	public bool isPointer;
	public IExpression? exp;
	public bool isReference {
		get { return !isPointer; }
		set { isPointer = !value; }
	}
	public  string GenCS() {
		return $"{(isPointer ? "*" : "&")}{exp?.GenCS()}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}