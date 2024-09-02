namespace NanoScript.Parser.AstNodes;

//    //Lambda or Anonymous Function Expressions: Expressions that define inline functions.
//    | '(' exp ')' '=>' '{' statement* '}'
//    //Regex or String Matching Expressions: Expressions used for pattern matching in strings.
//    //Type Check Expressions: Expressions used to check the type of an object.
//    | exp 'is' identifier
//    | exp 'as' identifier
public class TypeCheckExpression : IExpression {
	public IExpression? exp;
	public bool isAs;

	public bool isIs {
		get { return !isAs; }
		set { isAs = !value; }
	}
	public IdentifierExpression? identifier;
	public string GenCS() {
		return $"{exp?.GenCS()} {(isAs ? "as" : "is")} {identifier?.GenCS()}";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}