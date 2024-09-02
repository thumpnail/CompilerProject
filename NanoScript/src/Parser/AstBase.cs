namespace NanoScript.Parser;

public interface INode {
	//public string source { get; set; } = "";

    public string GenCS();
    public List<int> TranspileToByteCode();
    public List<string> TranspileToAsm();
    
    public int[] Compile() {
        return new int[0];
    }
}

public interface IStatement : INode {}

public interface IExpression : INode {}