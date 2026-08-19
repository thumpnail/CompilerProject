using System.Xml;
using System.Xml.Serialization;

using NanoScript.Parser;
using NanoScript.Parser.AstNodes;

//using YamlDotNet;
namespace NanoScript {
	// Mid Tier Change Comment Yes
    public static class Program {
        
        public static void Main(string[] args) {
	        //File.ReadAllText(@"D:\fried\OneDrive\Dokumente\Code\Rider-Projects\CompilerProject\NanoScript\test1.nano")
	        //    "let somename = \"Hello World\"" + Environment.NewLine +
	        //    "//"+"this is a comment" + Environment.NewLine +
	        //    "let somenum = 12.21" + Environment.NewLine
	        //File.ReadAllText("./../../../test.nano")
	        var ns = new NanoScript();
	        //ns.RunFile(@"D:\fried\OneDrive\Dokumente\Code\Rider-Projects\CompilerProject\NanoScript\sample\fnc_expression.nano");
	        //ns.RunFile(@"D:\fried\OneDrive\Dokumente\Code\Rider-Projects\CompilerProject\NanoScript\sample\expressions.nano");
			
			
			var newParser = new NanoScriptParser();
			var res = 
				//newParser.Parse(File.ReadAllText(@"C:\Users\fried\OneDrive\Dokumente\Code\Rider-Projects\CompilerProject\NanoScript\sample\test2.nano"));
				newParser.Parse(File.ReadAllText(@"../../../../../CompilerProject/NanoScript/sample/test2.nano"));
			
			//Console.WriteLine(new YamlDotNet.Serialization.Serializer().Serialize(res));
			Console.WriteLine(res.GenCS());
		}
    }
}