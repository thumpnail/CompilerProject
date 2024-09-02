using System.Diagnostics.CodeAnalysis;
namespace NanoScript.Parser.AstNodes;

//    //Function or Method Calls: Expressions that invoke functions or methods.
//    | identifier '(' (exp (',' exp)*)? ')'
public class FunctionCallExpression : IExpression {
	public IExpression? identifier;
	public ExpressionList parameters = new();
	public string GenCS() {
		var paras = "";
		foreach (var item in parameters.expressions) {
			paras += $"{item.GenCS()},";
		}
		paras.Remove(paras.Length-2);
		return $"{FunctionNameResolver.ResolveFunctionName(identifier?.GenCS() ?? throw new())}({paras})";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}