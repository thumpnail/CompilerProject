namespace NanoScript.Parser.AstNodes;

//    // Enum
//    | 'enum' identifier? type_decl? '{' enum_declariation* '}'
public class EnumDeclarationStatement : IStatement {
	public TypeDeclarationStatement? typeDeclarationStatement;
	public IdentifierExpression? identifier;
	public List<EnumValueDeclaration> enumValueDeclarations = new();
	// public Expression exp;
	public bool isPublic;

	public  string GenCS() {
		var res = $"{(isPublic ? "public" : "")} enum {identifier?.GenCS()} {typeDeclarationStatement?.GenCS()} {{\n";
		foreach (var item in enumValueDeclarations) {
			res += $"{item.GenCS()}\n";
		}
		res += "}\n";
		return res;
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}