namespace NanoScript.Parser.AstNodes;

//    // Function and Method Declaration: Statements that invoke functions or methods.
//    | 'export'? 'pub'? 'fnc' '.'? identifier'('(identifier type_decl? (',' identifier type_decl? )* )?')' ':' type_decl '{' statement* '}'
public class FunctionDeclarationStatement : IStatement {
    public bool isExport;
    public bool isPublic;
    public bool isSelf;
    public IdentifierExpression? identifier;
    public List<ParameterDeclaration> parameters = new();
    public List<IStatement> statements = new();
    public TypeDeclarationStatement? returnType;
    public  string GenCS() {
        var res = new StringBuilder();
        if (isExport) res.Append("export ");
        if (isPublic) res.Append("public ");
        if (!isSelf) res.Append("static ");
        if (returnType != null) res.Append($"{returnType.GenCS()} ");
        else res.Append("void ");

        if (identifier is not null && identifier.isSelf) {
            res.Append($"{identifier.lastIdentifier} ");
        } else if(identifier is not null) {
            res.Append($"{identifier.GenCS()}");
        }

        res.Append("(");
        if (identifier is not null && identifier.isSelf) {
            res.Append($"this {identifier.identifier}");
            foreach (var VARIABLE in identifier.identifiers) {
                res.Append($".{VARIABLE}");
            }
        }
        //todo: better way for this?
        if(parameters is not null)
        	for (int i = 0; i < parameters.Count; i++) {
	    	    var item = parameters[i];
	    	    res.Append(item.GenCS());
	    	    if (i < parameters.Count - 1) {
			        res.Append(", ");
	    	    }
        	}
        res.Append(") {\n");
        if (statements.Count > 0)
            for (int i = 0; i < statements.Count; i++) {
                var item = statements[i];
                res.AppendLine(item.GenCS());
            }
        res.Append("\n}");
        return $"{res}\n";
    }
    public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public  List<string> TranspileToAsm() {throw new NotImplementedException();}
    public string ToXml() {
	    throw new NotImplementedException();
    }
}