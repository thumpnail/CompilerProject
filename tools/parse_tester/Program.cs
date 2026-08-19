using System;
using System.Linq;

using Parseus.Parser.Common;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

using YamlDotNet.Serialization;

class Program {
    static void Main(string[] args) {
        var src = "let a true AND false OR true";
        var parser = new TinyScriptParser();
        var res = parser.Parse(src);
		// check if the code can be (logically) replicated
        Console.WriteLine($"Parsed OK: ({string.Join(",", res.statements.Select(s=>s.Statement.print()))})");
		// print AST
		var yaml = new SerializerBuilder()
			.ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
			.Build();
		Console.WriteLine(yaml.Serialize(res));
	}
}

