namespace NanoScript.Parser.AstNodes;

//    // Looping Statements: Statements that repeat a block of code multiple times (e.g., for, while, do-while).
//    | 'for' (identifier 'in' identifier | identifier '=' exp ';' exp ';' exp | exp) '{' statement* '}'
public enum ForType {
	While,
	For,
	ForIn
}

public class ForStatement : IStatement {
	public IdentifierExpression? elementIdentifier;
	public IdentifierExpression? listIdentifier;

	public ForType type;

	public IExpression? exp_def;
	public IExpression? exp_cond;
	public IExpression? exp_incr;

	public List<IStatement> statements = new();

	public string GenCS() {
		var res = "";
		if (type == ForType.For) {
			res = $"for(int {exp_def?.GenCS()}; {exp_cond?.GenCS()}; {exp_incr?.GenCS()}) {{\n";
		} else if (type == ForType.While) {
			res = $"while({(exp_cond is null? "true" : exp_cond.GenCS())}) {{\n";
		} else if (type == ForType.ForIn) {
			res = $"foreach(var {elementIdentifier?.GenCS()} in {listIdentifier?.GenCS()}) {{\n";
		}
		foreach (var statement in statements) {
			res += $"{statement.GenCS()};\n";
		}
		res += "}";
		return res;
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}