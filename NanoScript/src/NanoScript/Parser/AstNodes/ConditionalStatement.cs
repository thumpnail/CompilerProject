namespace NanoScript.Parser.AstNodes;

//    // Conditional Statements: Statements that perform different actions based on conditions (e.g., if, else, switch).
//    | 'if' exp '{' statement* '}' ('else' 'if' '{' statement* '}')* ('else' '{' statement* '}')?
public class ConditionalStatement : IStatement {
	public SubConditionalStatement? ifConditionalStatement;
	public List<SubConditionalStatement> elseIfConditionalStatements = new();
	public SubConditionalStatement? elseConditionalStatement;
	public  string GenCS() {
		var res = $"{ifConditionalStatement?.GenCS()}";
		foreach (var statement in elseIfConditionalStatements) {
			res += $"{statement.GenCS()}";
		}
		res += $"{elseConditionalStatement?.GenCS()}";
		return res;
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}