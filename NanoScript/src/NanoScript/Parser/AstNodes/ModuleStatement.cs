namespace NanoScript.Parser.AstNodes;

// 
// ModuleStatement: 'mod' identifier ImportStatement* ( '{' statement* '}' | statement* )?;
public class ModuleStatement : IStatement {
	public IdentifierExpression? moduleName;
	public List<ImportStatement> importStatements = new();
	public bool isSubModule;
	public List<IStatement> statements = new();
	public  string GenCS() {
		var res = new StringBuilder();
		foreach (var importStatement in importStatements) {
			res.Append(importStatement.GenCS());
		}
		res.Append($"namespace module_{moduleName?.GenCS()} {{\n");
		foreach (var statement in statements) {
			res.Append(statement.GenCS());
		}
		res.Append("\n}");
		return $"{res}";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}