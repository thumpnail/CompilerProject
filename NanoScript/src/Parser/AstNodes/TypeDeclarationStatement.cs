namespace NanoScript.Parser.AstNodes;
//    // Macro and Preprocessor Directives: Special statements used for compile-time code generation (in C and C++).
//    | '#include' '<' string '>'
//    // Concurrency and Synchronization Statements: Statements for managing multi-threading and synchronization.
//    ;
// 
// type_decl: ':' ('[' (exp)? ']'|'{}')? identifier;
public enum TypeDeclarationType {
	value,
	array,
	table
}

public class TypeDeclarationStatement : IStatement {
	public IdentifierExpression? identifier;
	public TypeDeclarationType typeDeclarationType;
	public IExpression? exp;

	public string GenCS() {
		var res = new StringBuilder();
		if (typeDeclarationType == TypeDeclarationType.array) {
			res.Append($"{identifier?.GenCS()}");
			res.Append("[");
			if (exp != null) res.Append(exp.GenCS());
			res.Append("]");
		} else if (typeDeclarationType == TypeDeclarationType.table) {
			res.Append($"dynamic ");
		} else if (identifier is null) {
			res.Append($"object ");
		} else {
			res.Append($"{identifier.GenCS()} ");
		}
		return res.ToString();
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}