namespace NanoScript.Parser.AstNodes;

// // Function Call Statement
// | identifier '(' (exp (',' exp)*)? ')'
public class FunctionCallStatement : IStatement {
	public IdentifierExpression? identifier;
	public ExpressionList parameters = new();

	public  string GenCS() {
		var res = $"{FunctionNameResolver.ResolveFunctionName(identifier?.GenCS()??throw new())}(";
		for (var i = 0; i < parameters.expressions.Count; i++) {
			res += parameters.expressions[i].GenCS();
			if (i < parameters.expressions.Count - 1)
				res += ", ";
		}
		return $"{res});";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}