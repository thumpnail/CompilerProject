namespace NanoScript.Parser;

public static class FunctionNameResolver {
	private static Dictionary<string, string> dotnetFunctionNameLookup = new Dictionary<string, string>() {
		{ "println", "System.Console.WriteLine" },
		{ "print", "System.Console.Write" },
		{ "type", "typeof" },
		{ "assert", "System.Diagnostics.Debug.Assert" },
		{ "size", "sizeof" },
		{ "str", "String.Parse" },
	};
	public static string ResolveFunctionName(string functionName) {
		return dotnetFunctionNameLookup.ContainsKey(functionName) ? dotnetFunctionNameLookup[functionName] : functionName;
	}
}