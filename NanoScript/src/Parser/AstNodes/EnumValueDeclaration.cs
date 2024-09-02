namespace NanoScript.Parser.AstNodes;
public enum EnumValueType {
	none,
	simple,
	tuple,
	block
}

//    |(identifier ('=' exp | '(' (identifier type_decl? (',' identifier type_decl? )* )? ')' | '{'  '}') )
public class EnumValueDeclaration : IStatement {
	public IdentifierExpression? identifier;
	public IExpression? exp;
	public EnumValueType type;
	//public List<ParameterDeclaration> parameters;

	public string GenCS() {
		return $"{identifier?.GenCS()} {(((exp is null? "":"= ")+exp?.GenCS()))},";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}