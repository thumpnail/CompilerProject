namespace NanoScript.Parser.AstNodes;

///   // internal functions
///   | 'type' '(' exp ')'
///   | 'size' '(' exp ')'
///   | 'str' '(' exp ')'
///   | 'len' '(' exp ')'
///   | 'print' '(' exp ')'
///   | 'println' '(' exp ')'
public class InternalFunctionCallExpression : IExpression {
	public string functionName = String.Empty;
	public List<IExpression> parameters = new();
	public string GenCS() {
		var paras = "";
		foreach(var item in parameters) {
			paras += item.GenCS()+",";
		}
		paras.Remove(paras.Length-2);
		return $"{functionName}()";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}