using System;
using System.Linq;

using Parseus.Parser.Common;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

using YamlDotNet.Serialization;

class Program {
	static void Main(string[] args) {
		var inputs = new[] {
			"let a true OR true",
			"let a true OR true OR false",
			"let a true AND false OR true",
		};
		var parser = new TinyScriptParser();
		var yaml = new SerializerBuilder()
			.ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
			.Build();
		foreach (var src in inputs) {
			Console.WriteLine("--- INPUT: " + src);
			Console.WriteLine("Chars:");
			for (int i = 0; i < src.Length; i++) {
				Console.WriteLine($"  [{i}] '{src[i]}' (U+{(int)src[i]:X4})");
			}
			var res = parser.Parse(src);
			Console.WriteLine($"Parsed OK: ({string.Join(",", res.statements.Select(s=>s.Statement.print()))})");
			Console.WriteLine(yaml.Serialize(res));
		}
	}
}

