using Newtonsoft.Json;

using Parseus.Parser.BasicParser.SBNF;
namespace TinyScript;

class Program {
	static void Main(string[] args) {
		var path = "/home/fenex/RiderProjects/CompilerProject/NanoScript.Tiny/Grammars/test1.nano";
		
		var file = File.ReadAllText(path);
		
		var parser = new TinyScriptParser();
		var ast = parser.Parse(file);

		//var code = new CodeGenerator(ast).Build();
		
		//File.WriteAllText("D:\\fried\\OneDrive\\Dokumente\\Code\\Rider-Projects\\CompilerProject\\NanoScript\\sample\\NanoScript.csx","");
		
		// print ast as json
		Console.WriteLine(new YamlDotNet.Serialization.Serializer().Serialize(ast));
	}
}