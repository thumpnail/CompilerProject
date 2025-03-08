using System.Diagnostics.CodeAnalysis;
namespace NanoScript.Parser.AstNodes;

//    //Function or Method Calls: Expressions that invoke functions or methods.
//    | identifier '(' (exp (',' exp)*)? ')'
public class FunctionCallExpression : IExpression {
	public IExpression? identifier;
	public ExpressionList parameters = new();
	public  string GenCS() {
		var pList = new List<string>();
		foreach (var item in parameters.expressions) {
			pList.Add($"{item.GenCS()}");
		}
		var paras = string.Join(", ", pList.ToArray());
		return $"{FunctionNameResolver.ResolveFunctionName(identifier?.GenCS() ?? throw new())}({paras})";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
}