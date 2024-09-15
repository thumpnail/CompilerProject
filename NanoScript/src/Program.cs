

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
	        ns.RunFile(@"D:\fried\OneDrive\Dokumente\Code\Rider-Projects\CompilerProject\NanoScript\sample\main.nano");
	        //ns.RunFile(@"C:\Users\fenex\OneDrive\Dokumente\Code\Rider-Projects\CompilerProject\NanoScript\sample\main.nano");
        }
    }
}