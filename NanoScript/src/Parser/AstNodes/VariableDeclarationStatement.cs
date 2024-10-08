namespace NanoScript.Parser.AstNodes;
// 
// statement
//    // Variable Declaration and Assignment: Statements to declare and assign values to variables.
//    : 'pub'? ('let'|'var'|'const')? '.'? identifier type_decl? ('=' exp)?
//[Parser("VariableDeclarationStatement","pub?","let|var|const?")]
public class VariableDeclarationStatement : IStatement {
	public bool isPublic = false;
	public string prefix = ""; //'let'|'var'|'const'
	public bool isSelf = false;
	public IdentifierExpression Identifier = new();
	public TypeDeclarationStatement? typeDeclarationStatement;
	public bool isAssign = false;
	public IExpression? exp;
	public  string GenCS() {
		var res = new StringBuilder();
		res.AppendLine("//TODO: VariableDeclarationStatement:");
		res.Append((isPublic ? "public " : ""));
		if(typeDeclarationStatement != null)
			res.Append(prefix switch {
				"let" => "readonly " + typeDeclarationStatement?.GenCS() + " ",
				"const" => "const " + typeDeclarationStatement?.GenCS() + " ",
				"var" => typeDeclarationStatement?.GenCS() + " ",
				_ => typeDeclarationStatement?.GenCS() + " "
			});
		else
			res.Append(prefix switch {
				"let" => "readonly object " + typeDeclarationStatement?.GenCS() + " ",
				"const" => "const object " + typeDeclarationStatement?.GenCS() + " ",
				"var" => "object "+typeDeclarationStatement?.GenCS() + " ",
				_ => typeDeclarationStatement?.GenCS() + " "
			});
		res.Append(isSelf ? "this." : "");
		res.Append(Identifier.GenCS());
		res.Append(isAssign ? " = " + exp?.GenCS() : "");
		res.AppendLine(";");
		return $"{res.ToString().Trim()}\n";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		return $"<VariableDeclarationStatement isPublic=\"{isPublic}\" prefix=\"{prefix}\" isSelf=\"{isSelf}\" isAssign=\"{isAssign}\">" +
		       $"{Identifier.ToXml()}" +
		       $"{typeDeclarationStatement?.ToXml()}" +
		       $"{exp?.ToXml()}" +
		       $"</VariableDeclarationStatement>";
	}
}