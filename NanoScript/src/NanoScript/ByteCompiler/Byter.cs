namespace NanoScript.Compiler;

public static class Byter {
	public static void Op_Add(this NanoFile file, int value1, int value2) {
		file.Code.Add((int)Opcode.Add);
		file.Code.Add(value1);
		file.Code.Add(value2);
	}
}