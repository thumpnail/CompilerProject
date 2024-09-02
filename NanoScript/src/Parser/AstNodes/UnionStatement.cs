namespace NanoScript.Parser.AstNodes;

//    // union
//    | 'union' type_decl? '{' statement* '}'
public class UnionStatement : IStatement {
	public IdentifierExpression? identifier;
	public TypeDeclarationStatement? typeDeclarationStatement; //???
	public List<IStatement> statements = new();

	public string GenCS() {
		return "//TODO: UnionStatement:\n";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}