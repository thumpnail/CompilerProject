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

    public abstract string GenBflat();
    public abstract List<int> TranspileToByteCode();
    public abstract List<string> TranspileToAsm();
    
    public int[] Compile() {
        return new int[0];
    }
}

public abstract class Statement : Node {}

public abstract class Expression : Node {}