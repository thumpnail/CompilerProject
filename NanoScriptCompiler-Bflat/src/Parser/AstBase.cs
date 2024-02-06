using System.Numerics;
using System.Text;
using pipetest;
using static NanoScript.Program.Token;

namespace NanoScript.Parser;

public abstract class Node {
    //public string currentFileName { get; set; }
    //public Dictionary<string,StringBuilder> ModuleFiles = new Dictionary<string,StringBuilder>();

    /*public StringBuilder currentFile
    {
        get
        {
            if (ModuleFiles.ContainsKey(currentFileName))
                return ModuleFiles[currentFileName];
            else return null;
        }
    }*/
    public string source { get; set; }

    public abstract string TranspileToC();
    public abstract string TranspileToBflat();
    public abstract string TranspileToByteCode();
    
    public int[] Compile() {
        return new int[0];
    }
}

public abstract class Statement : Node {
    public override string TranspileToC() {
        return "<EMPTYNODE>";
    }
    public override string TranspileToBflat() {
        return "<EMPTYNODE>";
    }
    public override string TranspileToByteCode() {
        return "<EMPTYNODE>";
    }
}

public abstract class Expression : Node {
    public override string TranspileToC() {
        return "<EMPTYNODE>";
    }
    public override string TranspileToBflat() {
        return "<EMPTYNODE>";
    }
    public override string TranspileToByteCode() {
        return "<EMPTYNODE>";
    }
}