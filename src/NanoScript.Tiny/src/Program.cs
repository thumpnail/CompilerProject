using Newtonsoft.Json;

using Parseus.Parser.BasicParser.SBNF;

using YamlDotNet.Serialization;
namespace TinyScript;

class Program {
	static void Main(string[] args) {
		var path = "../../../../../CompilerProject/NanoScript/sample/test2.nano";
		
		var file = File.ReadAllText(path);
		Console.WriteLine("Source:\n"+file);
		
		var parser = new TinyScriptParser();
		var ast = parser.Parse(file);

		//var code = new CodeGenerator(ast).Build();
		
		//File.WriteAllText("D:\\fried\\OneDrive\\Dokumente\\Code\\Rider-Projects\\CompilerProject\\NanoScript\\sample\\NanoScript.csx","");
		
		// print ast as json
		var yaml = new SerializerBuilder()
			.ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
			.Build();
		Console.WriteLine(yaml.Serialize(ast));
		Console.WriteLine();
		var res = string.Join("\n", ast.modules.Select(m => m.print()));
		res = ParenthesesUtils.StringParenthesesResolver(res);
		Console.WriteLine(res);
	}
}