using Parseus.Parser.Implicit;
using Parseus.Parser.Common;

public static class Tokens {
	public const string NONE = "NONE";

	public const string DEF = "DEF";
	public const string FNC = "FNC";
	public const string RET = "RET";
	public const string IFF = "IFF";
	public const string ELF = "ELF";
	public const string ELS = "ELS";
	public const string EXT = "EXT";
	public const string WHL = "WHL";
	public const string FOR = "FOR";
	public const string LET = "LET";
	public const string INC = "INC";
	public const string TBL = "TBL";
	public const string PCK = "PCK";
	public const string ERR = "ERR";
	public const string CLL = "CLL";
	public const string SET = "SET";

	public const string EQL = "EQL";
	public const string NEQ = "NEQ";
	public const string LSS = "LSS";
	public const string GTR = "GTR";
	public const string LEQ = "LEQ";
	public const string GEQ = "GEQ";
	public const string MOD = "MOD";
	public const string POW = "POW";
	public const string AND = "AND";
	public const string OR = "OR";
	public const string XOR = "XOR";
	public const string NOT = "NOT";
	public const string SHL = "SHL";
	public const string SHR = "SHR";

	public const string PLUS = "PLUS";
	public const string MINUS = "MINUS";
	public const string STAR = "STAR";
	public const string SLASH = "SLASH";
	public const string PERCENT = "PERCENT";
	public const string COLON = "COLON";

	public const string NULL = "NULL";
	public const string TRUE = "TRUE";
	public const string FALSE = "FALSE";
	public const string IDENTIFIER = "IDENTIFIER";
	public const string STRING = "STRING";
	public const string NUMBER = "NUMBER";
	public const string EOL = "EOL";
}

public partial class TinyScriptParser : BaseParser {
	const string ANY = "[.]";
	const string STRING = $"\"{ANY}\"";
	const string WORD = "[a-zA-Z_][a-zA-Z0-9_]*";
	const string IDENTIFIER = $"[\\.]?{WORD}([\\.]{WORD})*([\\:]{WORD})?";
	const string DIGIT = "[0-9]";
	const string NUMBER = $"{DIGIT}+(\\.{DIGIT}+)?";
	private static readonly Parseus.Lexer.Lexer lexer = new Parseus.Lexer.Lexer()
		.Skippable(Tokens.NONE, @"\/\/.*")
		//Keywords
		.Child(Tokens.DEF, "def")
		.Child(Tokens.FNC, "fnc")
		.Child(Tokens.RET, "ret")
		.Child(Tokens.IFF, "iff")
		.Child(Tokens.ELF, "elf")
		.Child(Tokens.ELS, "els")
		.Child(Tokens.EXT, "ext")
		.Child(Tokens.WHL, "whl")
		.Child(Tokens.FOR, "for")
		.Child(Tokens.LET, "let")
		.Child(Tokens.INC, "inc")
		.Child(Tokens.TBL, "tbl")
		.Child(Tokens.PCK, "pck")
		.Child(Tokens.ERR, "err")
		.Child(Tokens.CLL, "cll")
		.Child(Tokens.SET, "set")

		// Operators
		.Child(Tokens.COLON, "\\:")
		.Child(Tokens.EQL, "\\=\\=")
		.Child(Tokens.NEQ, "\\!\\=")
		.Child(Tokens.LSS, "\\<")
		.Child(Tokens.GTR, "\\>")
		.Child(Tokens.LEQ, "\\<\\=")
		.Child(Tokens.GEQ, "\\>\\=")
		.Child(Tokens.MOD, "\\%")
		.Child(Tokens.POW, "\\*\\*")
		.Child(Tokens.AND, "AND")
		.Child(Tokens.OR, "OR")
		.Child(Tokens.XOR, "\\^\\^")
		.Child(Tokens.NOT, "\\!")
		.Child(Tokens.SHL, "\\<\\<")
		.Child(Tokens.SHR, "\\>\\>")
		.Child(Tokens.PLUS, "\\+")
		.Child(Tokens.MINUS, "\\-")
		.Child(Tokens.STAR, "\\*")
		.Child(Tokens.SLASH, "\\/")
		//Literals
		.Child(Tokens.NULL, "null")
		.Child(Tokens.TRUE, "true")
		.Child(Tokens.FALSE, "false")

		// regex
		.Skippable(Tokens.NONE, @"\s+")
		.Skippable(Tokens.NONE, "#.*")
		.Skippable(Tokens.EOL, Environment.NewLine)
		.Child(Tokens.IDENTIFIER, IDENTIFIER)
		.Child(Tokens.STRING, "\"" + @"(\\.|[^" + "\"" + @"\\])*" + "\"")
		.Child(Tokens.STRING, @"'(\\.|[^'\\])*'")
		.Child(Tokens.NUMBER, @"-?(0[xX][0-9a-fA-F]+|\d*[.]\d+([eE][+-]?\d+)?|\d+([.]\d*)?([eE][+-]?\d+)?)");

	public override Script Parse(string src) {
		var lexResult = lexer.Lex(src);
		lexResult.result.ForEach(t => {
			Console.WriteLine($"{t.Token} - {t.Value}");
		});
		var context = new BasicAParserContext(lexResult.result.ToArray());
		var state = new CancellationState();
		return ScriptParser.Parse(new BaseParserContext(context, state));
	}

	public class Script() {
		public List<CStatement> statements = new();
	}

	private static readonly Parser<Script> ScriptParser = new((c, self) => {
		Repeat(c, c => {
			Node(c, StatementParser, s => {
				self.statements.Add(s);
			});
		});
	});

	public interface IStatement {
		public string print();
	}

	public class CStatement {
		public IStatement Statement;
	}

	private static readonly Parser<CStatement> StatementParser = new((c, self) => {
		Alt(c,
			c => Node(c, DefinitionParser, d => { self.Statement = d; }),
			c => Node(c, VariableDefinitionParser, v => { self.Statement = v; }),
			c => Node(c, FunctionDefinitionParser, f => { self.Statement = f; }),
			c => Node(c, SetStatementParser, s => { self.Statement = s; }),
			c => Node(c, CallStatementParser, s => { self.Statement = s; }),
			c => Node(c, WhileStatementParser, s => { self.Statement = s; })
		);
	});

	public class DefinitionStatement() : IStatement {
		public string? Identifier;
		public CExpression? Value;
		public string print() {
			return $"def {Identifier} {(Value != null ? $"= {Value.Expression.print()}" : "")}";
		}
	}

	private static readonly Parser<DefinitionStatement> DefinitionParser = new((c, self) => {
		Token(c, Tokens.DEF);
		Token(c, Tokens.IDENTIFIER, t => { self.Identifier = t; });
		Opt(c, c => {
			Node(c, LogicalExpressionParser, l => { self.Value = l; });
		});
	});

	public class VariableDefinitionStatement() : IStatement {
		public string? Identifier;
		public CExpression? Value;
		public string print() {
			return $"let {Identifier} {(Value != null ? $"= {Value.Expression.print()}" : "")}";
		}
	}

	private static readonly Parser<VariableDefinitionStatement> VariableDefinitionParser = new((c, self) => {
		Token(c, Tokens.LET);
		Token(c, Tokens.IDENTIFIER, t => { self.Identifier = t; });
		Opt(c, c => {
			Node(c, LogicalExpressionParser, e => { self.Value = e; });
		});
	});

	public class SetStatement() : IStatement {
		public string? Identifier;
		public CExpression? Value;
		public string print() {
			return $"set {Identifier} {(Value != null ? $"= {Value.Expression.print()}" : "")}";
		}
	}

	private static readonly Parser<SetStatement> SetStatementParser = new((c, self) => {
		Token(c, Tokens.SET);
		Token(c, Tokens.IDENTIFIER, t => { self.Identifier = t; });
		Node(c, LogicalExpressionParser, t => { self.Value = t; });
	});

	public class CallStatement() : IStatement {
		public string? Identifier;
		public List<CExpression> Parameters = new();

		// public CExpression? Value;
		public string print() {
			return $"call {Identifier} ({string.Join(",", Parameters.Select(p => p.Expression.print()))})";
		}
	}

	private static readonly Parser<CallStatement> CallStatementParser = new((c, self) => {
		Token(c, Tokens.CLL);
		Token(c, Tokens.IDENTIFIER, t => { self.Identifier = t; });
		RepeatOpt(c, c => {
			Node(c, LogicalExpressionParser, t => { self.Parameters.Add(t); });
		});
	});

	public class FunctionDefinitionStatement() : IStatement {
		public string? Identifier;
		public List<string> Parameters = new();
		public List<CStatement> Body = new();
		public CExpression? ReturnExpression;
		public string print() {
			return $"fnc {Identifier}({string.Join(", ", Parameters)}) {{\n{string.Join("\n", Body.Select(s => s.Statement.print()))}\n}}";
		}
	}

	private static readonly Parser<FunctionDefinitionStatement> FunctionDefinitionParser = new((c, self) => {
		Token(c, Tokens.FNC);
		Token(c, Tokens.IDENTIFIER, t => { self.Identifier = t; });
		RepeatOpt(c, c => {
			Token(c, Tokens.IDENTIFIER, p => {
				self.Parameters.Add(p);
			});
		});
		Token(c, Tokens.COLON);
		//body
		RepeatOpt(c, c => {
			Node(c, StatementParser, s => {
				self.Body.Add(s);
			});
		});
		Token(c, Tokens.RET);
		Opt(c, c => {
			Node(c, LogicalExpressionParser, r => self.ReturnExpression = r);
		});
	});

	public class WhileStatement() : IStatement {
		public CExpression? Condition;
		public List<CStatement> Body = new();

		// public CExpression? Value;
		public string print() {
			return $"while ({Condition?.Expression.print() ?? "null"}) {{\n{string.Join("\n", Body.Select(s => s.Statement.print()))}\n}}";
		}
	}

	private static readonly Parser<WhileStatement> WhileStatementParser = new((c, self) => {
		Token(c, Tokens.WHL);
		Node(c, LogicalExpressionParser, t => { self.Condition = t; });
		Token(c, Tokens.COLON);
		RepeatOpt(c, c => {
			Node(c, StatementParser, t => { self.Body.Add(t); });
		});
		Token(c, Tokens.EXT);
	});
}