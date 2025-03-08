using Newtonsoft.Json;

using Parseus.ParserV3;
namespace TinyScript;

class Program {
	static void Main(string[] args) {
		var path = "D:\\fried\\OneDrive\\Dokumente\\Code\\Rider-Projects\\CompilerProject\\NanoScript\\sample\\NanoScript.sbnf";
		
		var file = File.ReadAllText(path);
		
		var parser = new SbnfParser(file);
		var ast = parser.Parse();

		var code = new CodeGenerator(ast).Build();
		
		File.WriteAllText("D:\\fried\\OneDrive\\Dokumente\\Code\\Rider-Projects\\CompilerProject\\NanoScript\\sample\\NanoScript.csx", code);
		
		// print ast as json
		Console.WriteLine(JsonConvert.SerializeObject(ast, Formatting.Indented));
	}
}