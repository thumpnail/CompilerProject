namespace NanoScript.Parser.AstNodes;

//    //class
//    | 'pub'? 'class' identifier type_decl? '{' statement* '}'
public class StructDeclarationStatement : IStatement {
	public bool isPublic;
	public IdentifierExpression? identifier;
	public TypeDeclarationStatement? typeDeclarationStatement;
	public List<IStatement> statements = new();

	public  string GenCS() {
		return "//TODO: StructDeclarationStatement:\n";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}