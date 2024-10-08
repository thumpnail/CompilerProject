namespace NanoScript.Parser;

public interface INode {
	//public string source { get; set; } = "";

    public string GenCS();
    public List<int> TranspileToByteCode();
    public List<string> TranspileToAsm();
    public string ToXml();
    public string NAME  { get=>this.GetType().Name; }
    
    public int[] Compile() {
        return new int[0];
    }
}

public interface IStatement : INode {}

public interface IExpression : INode {}