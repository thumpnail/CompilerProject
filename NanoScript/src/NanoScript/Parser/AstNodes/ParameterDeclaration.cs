namespace NanoScript.Parser.AstNodes;

public class ParameterDeclaration : IExpression {
	public IdentifierExpression? identifier;
	public TypeDeclarationStatement? typeDeclarationStatement;

	public  string GenCS() {
		var res = new StringBuilder();
		if (typeDeclarationStatement is not null) res.Append($"{typeDeclarationStatement.GenCS()} ");
		else res.Append("object ");
		res.Append($"{identifier?.GenCS()}");
		return $"{res}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}