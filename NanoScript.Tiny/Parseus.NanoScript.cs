using NanoScript.Parser;

using Parseus.Parser.Implicit;

using NanoScript.Parser.AstNodes;

using static NanoScript.Token;

using Parseus.Parser.ObjectBased;

using YamlDotNet.Core.Tokens;

public class NanoScriptParser : BaseParser {
	public record NanoProgram {
		public List<NanoInstruction> Instructions = new();
	}

	public record BaseInstruction;

	public record NanoInstruction {
		public BaseInstruction BaseInstruction;
	}
	public record NanoAssignInstruction: BaseInstruction {
		public string Identifier;
		public List<NanoValue> Values;
	}

	public record BaseValue;
	public record NanoValue {
		public BaseValue Value;
	}

	public override NanoProgram Parse(string src) {
		return new();
	}
	// program         = { statement } ;
	private static Parser<NanoProgram> NanoProgramParser = new((c, self) => {
		Repeat(c, c => {
			Node(c, NanoInstructionParser, v => self.Instructions.Add(v));
		});
	});
	private static Parser<NanoInstruction> NanoInstructionParser = new((c, self) => {
		Alt(c, 
			c => Node(c, NanoAssignInstructionParser, v => self.BaseInstruction = v)
		);
	});
	private static Parser<NanoAssignInstruction> NanoAssignInstructionParser = new((c, self) => {
		Literal(c, "let", out _);
		Token(c, "identifier", out self.Identifier);
		Repeat(c, c => Node(c, NanoValueParser, v => self.Values.Add(v)));
	});
	private static Parser<NanoValue> NanoValueParser = new((c, self) => {
		Alt(c, c => {
			
		});
	});
}