namespace NanoScript.Parser.AstNodes;

// ImportStatement: 'import' string ('as' identifier)? | 'import' identifier 'from' string;
public class ImportStatement : IStatement {
	public IdentifierExpression Identifier= new();
	public string importString = "";
	public bool isAs;

	public bool isFrom;

	public  string GenCS() {
		var res = new StringBuilder();
		if (isAs) {
			if (importString.EndsWith(".nano")) {
				//TODO: Namepace Magic
			} else {
				//Guessing that it is a c# namepace
				res.Append($"using {Identifier.GenCS()} = {importString.Substring(1, importString.Length - 2)};");
			}
		} else if (isFrom) {
			if (importString.EndsWith(".nano")) {
				//TODO: Namepace Magic
			} else {
				//Guessing that it is a c# namepace
				res.Append($"using {Identifier.GenCS()} = {importString.Substring(1, importString.Length - 2)};");
			}
		} else {
			res.Append($"using {importString};");
		}
		// TODO: parse nano files
		return $"{res}\n";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		throw new NotImplementedException();
	}
}