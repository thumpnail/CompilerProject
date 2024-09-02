namespace NanoScript.Parser.AstNodes;

//    // Declaration Statements: Statements for defining types, structures, classes, and interfaces.
//    | ('def'|'type') identifier ('=' exp)?
public enum DeclarationType {
	def,
	type
}

public class DeclarationStatement : IStatement {
	public DeclarationType declarationType;
	public IdentifierExpression? identifier;
	public IExpression? exp;
	public string GenCS() {
		return "//TODO: DeclarationStatement:\n";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}