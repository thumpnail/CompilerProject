namespace NanoScript.Compiler;

public class NanoFile {
	//Head(FunctionTable, etc)
	public List<int> Head;
	public List<NanoFile> imports;
	public Dictionary<int, int> FunctionTable;
	//Init(Globals, cwd, etc)
	public Dictionary<int,object> Inits;
	//DB
	public List<(int length,string value)> DB;
	//Code
	public List<int> Code;
	//Foot
}

public class NanoBinaryFile {
	public byte[] head;
	public (byte, byte)[] functionTable;
	public byte[] inits;
	public (byte,byte)[] db;
	public byte[] code;
	public void WriteBinaryFile() {
		
	}
}