namespace NanoScript.Parser;

public static class FunctionNameResolver {
	private static Dictionary<string, string> functionNameLookup = new Dictionary<string, string>() {
		{ "println", "System.Console.WriteLine" },
		{ "print", "System.Console.Write" },
	};
	public static string ResolveFunctionName(string functionName) {
		return functionNameLookup.ContainsKey(functionName) ? functionNameLookup[functionName] : functionName;
	}
}