namespace NanoScript.Parser.AstNodes;

//    // Control Flow Statements: Statements for altering the flow of execution (e.g., break, continue, goto - though this is less common).
//    | 'break' | 'continue'
public enum ControlFlowModifierType {
	@break,
	@continue
}

public class BreakContinueStatement : IStatement {
	public ControlFlowModifierType ControlFlowModifierType;
	public string GenCS() {
		return "//TODO: BreakContinueStatement:";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}