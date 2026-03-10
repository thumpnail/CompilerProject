using NanoScript.Parser;

using Parseus.Parser.Implicit;

using NanoScript.Parser.AstNodes;

using Parseus.Parser.Common;

using static NanoScript.Token;

using Parseus.Parser.ObjectBased;

using YamlDotNet.Core.Tokens;

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

public class TinyScriptParser : BaseParser {
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
		.Child(Tokens.COLON, ":")
		.Child(Tokens.EQL, "==")
		.Child(Tokens.NEQ, "!=")
		.Child(Tokens.LSS, "<")
		.Child(Tokens.GTR, ">")
		.Child(Tokens.LEQ, "<=")
		.Child(Tokens.GEQ, ">=")
		.Child(Tokens.MOD, "%")
		.Child(Tokens.POW, "\\*\\*")
		.Child(Tokens.AND, "\\&\\&")
		.Child(Tokens.OR, "\\|\\|")
		.Child(Tokens.XOR, "^^")
		.Child(Tokens.NOT, "!")
		.Child(Tokens.SHL, "<<")
		.Child(Tokens.SHR, ">>")
		.Child(Tokens.PLUS, "\\+")
		.Child(Tokens.MINUS, "-")
		.Child(Tokens.STAR, "\\*")
		.Child(Tokens.SLASH, "/")
		//Literals
		.Child(Tokens.NULL, "null")
		.Child(Tokens.TRUE, "true")
		.Child(Tokens.FALSE, "false")
		
		// regex
		.Skippable(Tokens.NONE, @"\s+")
		.Skippable(Tokens.NONE, @"#.*")
		.Skippable(Tokens.EOL, Environment.NewLine)
		.Child(Tokens.IDENTIFIER, IDENTIFIER)
		.Child(Tokens.STRING, "\"" + @"(\\.|[^" + "\"" + @"\\])*" + "\"")
		.Child(Tokens.STRING, @"'(\\.|[^'\\])*'")
		.Child(Tokens.NUMBER, @"-?(0[xX][0-9a-fA-F]+|\d*[.]\d+([eE][+-]?\d+)?|\d+([.]\d*)?([eE][+-]?\d+)?)");

	public override Script Parse(string src) {
		var lexResult = lexer.Lex(src);
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

	public interface IStatement;
	public class CStatement {
		public IStatement Statement;
	}
	private static readonly Parser<CStatement> StatementParser = new((c, self) => {
		Alt(c,
			c => {
				Node(c, DefinitionParser, d => {
					self.Statement = d;
				});
			},
			c => {
				Node(c, VariableDefinitionParser, v => {
					self.Statement = v;
				});
			},
			c => {
				Node(c, FunctionDefinitionParser, f => {
					self.Statement = f;
				});
			},
			c => {
				Node(c, SetStatementParser, s => {
					self.Statement = s;
				});
			}
		);
	});

	public class DefinitionStatement() : IStatement {
		public string? Identifier;
		public Literal? Value;
	}
	private static readonly Parser<DefinitionStatement> DefinitionParser = new((c, self) => {
		Token(c, Tokens.DEF, out _);
		Token(c, Tokens.IDENTIFIER, out self.Identifier);
		Opt(c, c => {
			Node(c, ValueParser, out self.Value);
		});
	});
	
	public class VariableDefinitionStatement() : IStatement {
		public string? Identifier;
		public Literal? Value;
	}
	private static readonly Parser<VariableDefinitionStatement> VariableDefinitionParser = new((c, self) => {
		Token(c, Tokens.LET, out _);
		Token(c, Tokens.IDENTIFIER, out self.Identifier);
		Opt(c, c => {
			Node(c, ValueParser, out self.Value);
		});
	});
	
	public class SetStatement() : IStatement {
		public string? Identifier;
		public Literal? Value;
	}
	private static readonly Parser<SetStatement> SetStatementParser = new((c, self) => {
		Token(c, Tokens.SET, out _);
		Token(c, Tokens.IDENTIFIER, out self.Identifier);
		Node(c, ValueParser, out self.Value);
	});
	
	public class FunctionDefinitionStatement() : IStatement {
		public string? Identifier;
		public List<string> Parameters = new();
		public List<CStatement> Body = new();
	}
	private static readonly Parser<FunctionDefinitionStatement> FunctionDefinitionParser = new((c, self) => {
		Token(c, Tokens.FNC, out _);
		Token(c, Tokens.IDENTIFIER, out self.Identifier);
		RepeatOpt(c, c => {
			Token(c, Tokens.IDENTIFIER, p => {
				self.Parameters.Add(p);
			});
		});
		Token(c, Tokens.COLON, out _);
		//body
		RepeatOpt(c, c => {
			Node(c, StatementParser, s => {
				self.Body.Add(s);
			});
		});
		Token(c, Tokens.RET, out _);
		RepeatOpt(c, c => {
			
		});
	});

	public interface IExpression;
	public class CExpression() {
		public IExpression Expression;
	}

	public class Literal {
		public bool IsNull;
		public string? BoolValue;
		public string? NumberValue;
		public string? StringValue;
		public string? IdentifierValue;
	}
	private static readonly Parser<Literal> ValueParser = new((c, self) => {
		Alt(c, c => {
			Literal(c, Tokens.NULL, out self.IsNull);
		}, c => {
			Token(c, Tokens.TRUE, out self.BoolValue);
		}, c => {
			Token(c, Tokens.FALSE, out self.BoolValue);
		}, c => {
			Token(c, Tokens.NUMBER, out self.NumberValue);
		}, c => {
			Token(c, Tokens.STRING, out self.StringValue);
		}, c => {
			Token(c, Tokens.IDENTIFIER, out self.IdentifierValue);
		});
	});
}