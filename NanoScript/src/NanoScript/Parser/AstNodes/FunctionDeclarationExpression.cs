namespace NanoScript.Parser.AstNodes;

//    //Function Creation Expression
//    | 'fnc' '(' (identifier type_decl? (',' identifier type_decl? )* )? ')' type_decl? '{' statement* '}'
public class FunctionDeclarationExpression : IExpression {
	public List<ParameterDeclaration> parameters = new();
	public TypeDeclarationStatement? typeDeclarationStatement;
	public List<IStatement> statements = new();
	public  string GenCS() {
		// ((Func<string>)item.GenBflat).Invoke(); <- this is valid c#
		var paras = string.Join(',', parameters.Select(x=>x.GenCS()));
		var block = string.Join(',', statements.Select(x=>x.GenCS()));
		return $"({paras}) => {{\n{block}\n}}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}