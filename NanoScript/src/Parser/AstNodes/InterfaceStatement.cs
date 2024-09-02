namespace NanoScript.Parser.AstNodes;

//    //interface
//    | 'interface' identifier type_decl? '{' statement '}'
public class InterfaceStatement : IStatement {
	public IdentifierExpression? identifier;
	public TypeDeclarationStatement? typeDeclarationStatement;
	public List<IStatement> statements = new ();

	public string GenCS() {
		return "//TODO: InterfaceStatement:\n";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}