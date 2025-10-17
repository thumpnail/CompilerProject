namespace NanoScript.Parser;

public abstract class INode {
	//public string source { get; set; } = "";

	public string GenCS() { return string.Empty;}
    public List<int> TranspileToByteCode() { return []; }
    public List<string> TranspileToAsm() { return []; }
    public string NAME  { get=>this.GetType().Name; }
    
    public int[] Compile() {
        return new int[0];
    }
    public string ToLua() {
	    return "";
    }
}

public class IStatement : INode {}

public class IExpression : INode {}