using Newtonsoft.Json;

using YamlDotNet.Serialization;
namespace TinyScript;

class Program {
	static void Main(string[] args) {
		var path = "../../../../../CompilerProject/NanoScript.Tiny/Grammars/test2.nano";
		
		var file = File.ReadAllText(path);
		//Console.WriteLine("Source:\n"+file);
		
		var parser = new TinyScriptParser();
		var ast = parser.Parse(file);

		//var code = new CodeGenerator(ast).Build();
		
		//File.WriteAllText("D:\\fried\\OneDrive\\Dokumente\\Code\\Rider-Projects\\CompilerProject\\NanoScript\\sample\\NanoScript.csx","");
		
		// print ast as Json
		var yaml = new SerializerBuilder()
			.ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
			.Build();
		Console.WriteLine(yaml.Serialize(ast));
		Console.WriteLine();
		var res = string.Join("\n", ast.modules.Select(m => m.Print()));
		res = ParenthesesUtils.StringParenthesesResolver(res);
		Console.WriteLine(res);
	}
}