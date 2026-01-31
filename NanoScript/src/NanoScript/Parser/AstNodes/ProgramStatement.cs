namespace NanoScript.Parser.AstNodes;

// program: module_statement*;
public class ProgramStatement : IStatement {
    public List<ModuleStatement> moduleStatements = new();
    public string GenCS() {
        var res = new StringBuilder();
        res.AppendLine("//TODO: Program:");
        foreach (var item in moduleStatements) {
            res.Append(item.GenCS());
        }
        return $"{res}";
    }
    public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public  List<string> TranspileToAsm() {throw new NotImplementedException();}
    public string ToXml() {
	    throw new NotImplementedException();
    }
}






//    ; ANOMALY
//      Ylamona.AiE