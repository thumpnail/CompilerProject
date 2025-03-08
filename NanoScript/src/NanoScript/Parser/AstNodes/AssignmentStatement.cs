namespace NanoScript.Parser.AstNodes;

//    // assignment Statement
//    | '.'? identifier type_decl? ('=' exp | '<<' exp | '>>' exp | '+=' exp | '-=' exp | '*=' exp | '/=' exp)?
public enum AssignmentType {
	none,
	equal,
	push,
	pop,
	add,
	sub,
	mul,
	div
}

public class AssignmentStatement : IStatement {
	public bool isSelf = false;
	public IdentifierExpression? identifier;
	public TypeDeclarationStatement? typeDeclarationStatement;
	public AssignmentType? assignmentType;
	public IExpression? exp;
	public  string GenCS() {
		var res = $"{typeDeclarationStatement?.GenCS()} {(isSelf ? "this." : "")}{identifier?.GenCS()} {assignmentType switch {
			AssignmentType.equal => "=",
			AssignmentType.add => "+=",
			AssignmentType.sub => "-=",
			AssignmentType.mul => "*=",
			AssignmentType.div => "/=",
			AssignmentType.push => "<<",
			AssignmentType.pop => ">>",
			AssignmentType.none => "",
			_ => ""
		}} {exp?.GenCS()};";
		return $"{res};";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
}