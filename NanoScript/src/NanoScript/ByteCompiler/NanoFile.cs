using NanoScript.Parser;
namespace NanoScript.Compiler;

public class NanoFile {
	//Head(FunctionTable, etc)
	public List<int> Head;
	public List<NanoFile> imports;
	public Dictionary<int, int> FunctionTable;
	//Init(Globals, cwd, etc)
	public Dictionary<int, object> Inits;
	//DB
	public List<(int length, string value)> DB;
	//Code
	public List<int> Code;
	//Foot

	public NanoFile() {}
	public NanoFile(List<int> head, List<NanoFile> imports, Dictionary<int, int> functionTable, Dictionary<int, object> inits, List<(int length, string value)> db, List<int> code) {
		Head = head;
		this.imports = imports;
		FunctionTable = functionTable;
		Inits = inits;
		DB = db;
		Code = code;
	}
	public NanoBinaryFile GetNanoBinaryFile() {
		NanoBinaryFile nbin = new NanoBinaryFile();
		nbin.head = Head
			.Select(x => x.ToByteArray())
			.SelectMany(x => x)
			.ToArray();
		return nbin;
	}
}

public class NanoBinaryFile {
	public byte[] head;
	public (byte, byte)[] functionTable;
	public byte[] inits;
	public (byte, byte)[] db;
	public byte[] code;
	public void WriteBinaryFile() {
		var bin = new List<byte>();
		bin.AddRange(head);
		foreach ((byte, byte) tuple in functionTable) {
			bin.Add(tuple.Item1);
			bin.Add(tuple.Item2);
		}
		bin.AddRange(inits);
		foreach ((byte, byte) tuple in db) {
			bin.Add(tuple.Item1);
			bin.Add(tuple.Item2);
		}
		bin.AddRange(code);
		File.WriteAllBytes("code.bin", bin.ToArray());
	}
}