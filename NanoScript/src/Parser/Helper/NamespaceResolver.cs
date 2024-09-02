namespace NanoScript.Parser;

public static class NamespaceResolver {
	private static Dictionary<string, string> namespaces = new Dictionary<string, string>() {
		{ "system", "System" },
	};
	public static string ResolveNamespaces(string moduleName) {
		return namespaces.ContainsKey(moduleName) ? namespaces[moduleName] : $"module_{moduleName}";
	}
}