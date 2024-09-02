namespace NanoScript.Parser.AstNodes;

// 
// exp
//    // 'match' '(' exp ')' '{' exp '=>' exp ('|' exp '=>' exp)* '}'
//    //Literal Expressions: Constants representing specific values (e.g., numbers, strings, boolean values).
//    : identifier
//    //Member Access Expressions: Expressions to access properties or methods of objects or structures.
//    | '.'? identifier ('.' identifier)* (':' identifier)?
public class IdentifierExpression : IExpression {
	public string? identifier;
	public List<string> identifiers = new List<string>();
	public bool isExtension;
	public string lastIdentifier = "";
	public bool isSelf;
	public string GenCS() {
		var res = new StringBuilder();
		if (isSelf)
			res.Append("this.");

		res.Append(identifier);

		foreach (var VARIABLE in identifiers) {
			res.Append($".{VARIABLE}");
		}

		if (isExtension) {
			res.Append($".{lastIdentifier}");
		}

		return $"{res}";
	}
	public List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public List<string> TranspileToAsm() {throw new NotImplementedException();}
}