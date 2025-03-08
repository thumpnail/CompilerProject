namespace NanoScript.Parser.AstNodes;

//    //class
//    | 'pub'? 'class' identifier type_decl? '{' statement '}'
public class ClassDeclarationStatement : IStatement {
	public bool isPublic;
	public IdentifierExpression? identifier;
	public TypeDeclarationStatement? typeDeclarationStatement;
	public List<IStatement> statements = new();

	public  string GenCS() {
		var res = new StringBuilder();
		if (isPublic) res.Append("public ");
		res.Append($"class {identifier?.GenCS()}");
		if (typeDeclarationStatement != null)
			res.Append(typeDeclarationStatement.GenCS());
		res.Append(" {\n");
		foreach (var item in statements) {
			res.Append(item.GenCS());
		}
		res.Append("}\n");
		return $"{res}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}