namespace NanoScript.Parser.AstNodes;

//    // Expression Statements: Statements that evaluate and execute expressions.
//    // Input/Output Statements: Statements for interacting with the console or other input/output streams.
//    // Memory Management: Statements for memory allocation and deallocation (e.g., malloc, free, new, delete).
//    // Assertion Statements: Statements for specifying conditions that must be true at certain points in the program.
//    | 'assert' exp
public class AssertionStatement : IStatement {
	public IExpression? exp;

	public string GenCS() {
		return "//TODO: AssertionStatement:\n";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}