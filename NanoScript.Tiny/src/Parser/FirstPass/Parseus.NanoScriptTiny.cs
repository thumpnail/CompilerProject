using System.Text;

using Parseus.Lexer;
using Parseus.Lexer.RegExBased;
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
	public const string OPENPAREN = "OPENPAREN";
	public const string CLOSEPAREN = "CLOSEPAREN";
	public const string OPENBRACK = "OPENBRACK";
	public const string CLOSEBRACK = "CLOSEBRACK";
}

public interface IPrintable {
	string Print();
}

public partial class TinyScriptParser : BaseParser {
	private static readonly Lexer Lexer = new Lexer()
		.Skippable(Tokens.NONE, @"//(.*?)\r?\n")
		//Keywords
		.Child(Tokens.MOD, "mod")
		.Child(Tokens.DEF, "def")
		.Child(Tokens.FNC, "fnc")
		.Child(Tokens.RET, "ret")
		.Child(Tokens.IFF, "iff")
		.Child(Tokens.ELF, "elf")
		.Child(Tokens.ELS, "els")
		.Child(Tokens.EXT, "ext")
		.Child(Tokens.WHL, "whl")
		//.Child(Tokens.FOR, "for")
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
		.Child(Tokens.OPENPAREN, "\\(")
		.Child(Tokens.CLOSEPAREN, "\\)")
		.Child(Tokens.OPENPAREN, "\\{")
		.Child(Tokens.CLOSEPAREN, "\\}")
		//Literals
		.Child(Tokens.NULL, "null")
		.Child(Tokens.TRUE, "true")
		.Child(Tokens.FALSE, "false")

		// regex
		.Skippable(Tokens.NONE, @"\s+")
		.Skippable(Tokens.NONE, "#.*")
		.Skippable(Tokens.EOL, Environment.NewLine)
		.Child(Tokens.IDENTIFIER, REGEX_IDENTIFIER)
		.Child(Tokens.STRING, REGEX_STRING2)
		.Child(Tokens.STRING, REGEX_STRING3)
		.Child(Tokens.NUMBER, REGEX_NUMBER2);

	private class TinyScriptContext(LexerResult res) : BasicAParserContext(res) {
		public int BodyDepth = 0;
	}

	public override Script Parse(string src) {
		var lexResult = Lexer.Lex(src);
		var context = new TinyScriptContext(lexResult) {SourceCode = src};
		var state = new CancellationState();
		return ScriptParser.Parse(new BaseParserContext(context, state));
	}

	public class Script() : IPrintable {
		public List<ModuleDeclaration> modules = new();

		public string Print() {
			var sb = new StringBuilder();
			sb.AppendLine($"Mod Count: {modules.Count}");
			sb.AppendLine("Modules:");
			foreach (var item in modules) {
				sb.AppendLine(item.Print());
			}

			return sb.ToString();
		}
	}

	private static readonly Parser<Script> ScriptParser = new((c, self) => {
		RepeatOpt(c, c => Node(c, ModuleDeclarationParser, mod => self.modules.Add(mod)));
		Console.WriteLine($"Modules: {self.modules.Count}");
	});

	public class ModuleDeclaration() : IStatement, IPrintable {
		public string name;

		// definitions inside module
		public List<DefinitionStatement> Definitions = new();

		// functions inside module
		public List<FunctionDefinitionStatement> Functions = new();

		// first-class Statements inside module
		public List<CStatement> Statements = new();
		public List<ImportStatement> Imports = new();

		public string Print() {
			var sb = new StringBuilder();
			sb.AppendLine($"Module: {name}");

			sb.AppendLine("# Imports");
			foreach (var import in Imports) {
				sb.AppendLine(import.Print());
			}

			sb.AppendLine("# Definitions");
			foreach (var def in Definitions) {
				sb.AppendLine(def.Print());
			}

			sb.AppendLine("# Functions");
			foreach (var func in Functions) {
				sb.AppendLine(func.Print());
			}

			sb.AppendLine("# Statements");
			foreach (var statement in Statements) {
				sb.AppendLine(statement.Statement.Print());
			}

			return sb.ToString();
		}
	}

	public static readonly Parser<ModuleDeclaration> ModuleDeclarationParser = new((c, self) => {
		Token(c, Tokens.MOD);
		Token(c, Tokens.IDENTIFIER, t => self.name = t);
		// statements
		RepeatOpt(c, c => {
			Alt(c, [
				c => Node(c, DefinitionParser, d => self.Definitions.Add(d)),
				c => Node(c, ImportStatementParser, i => { self.Imports.Add(i); }),
				c => Node(c, FunctionDefinitionParser, f => self.Functions.Add(f)),
				c => Node(c, StatementParser, s => self.Statements.Add(s))
			], errorCallback: r => {
				var diagPack = GetLineNumberFromContinuosString(c.Context.SourceCode, c.Context.PeekToken());
				Console.WriteLine($"{CreateReportLine(c, diagPack, "only 'def', 'inc', 'fnc', 'let', 'set', 'call', 'whl', 'if' and 'ret' allowed inside bodies.")}");
			});
		});
	});

	public interface IStatement : IPrintable;

	public class CStatement {
		public IStatement Statement;
	}

	private static readonly Parser<CStatement> StatementParser = new((c, self) => {
		if ((c.Context is TinyScriptContext { BodyDepth: > 0 }))
			Alt(c, [
				//c => Node(c, DefinitionParser, d => { self.Statement = d; }),
				//c => Node(c, ImportStatementParser, s => { self.Statement = s; }),
				//c => Node(c, FunctionDefinitionParser, f => { self.Statement = f; }),
				c => Node(c, VariableDefinitionParser, v => { self.Statement = v; }),
				c => Node(c, SetStatementParser, s => { self.Statement = s; }),
				c => Node(c, CallStatementParser, s => { self.Statement = s; }),
				c => Node(c, WhileStatementParser, s => { self.Statement = s; }),
				c => Node(c, IfStatementParser, s => { self.Statement = s; }),
				//c => Node(c, ImportStatementParser, s => { self.Statement = s; }),
				c => Node(c, ReturnStatmentParser, s => { self.Statement = s; })
			], errorCallback: r => {
				var diagPack = GetLineNumberFromContinuosString(c.Context.SourceCode, c.Context.PeekToken());
				Console.WriteLine($"{CreateReportLine(c, diagPack, "only 'def', 'inc', 'fnc', 'let', 'set', 'call', 'whl', 'if' and 'ret' allowed inside bodies.")}");
			});
		else
			Alt(c, [
				c => Node(c, VariableDefinitionParser, v => { self.Statement = v; }),
				c => Node(c, SetStatementParser, s => { self.Statement = s; }),
				c => Node(c, CallStatementParser, s => { self.Statement = s; }),
				c => Node(c, WhileStatementParser, s => { self.Statement = s; }),
				c => Node(c, IfStatementParser, s => { self.Statement = s; })
			], errorCallback: r => {
				var diagPack = GetLineNumberFromContinuosString(c.Context.SourceCode, c.Context.PeekToken());
				Console.WriteLine(CreateReportLine(c, diagPack, $"only 'fnc', 'def', 'let', 'set', 'call', 'whl' and 'if' allowed inside the module."));
			});
	});

	public class ReturnStatement : IStatement, IPrintable {
		public CExpression? Value;

		public string Print() {
			var sb = new StringBuilder();
			sb.AppendLine($"(return {Value.Expression.Print()})");
			return sb.ToString();
		}
	}

	private static readonly Parser<ReturnStatement> ReturnStatmentParser = new((c, self) => {
		Token(c, Tokens.RET);
		Opt(c, c => {
			Node(c, LogicalExpressionParser, e => { self.Value = (e); });
		});
	});

	public class DefinitionStatement() : IStatement, IPrintable {
		public string? Identifier;
		public CExpression? Value;

		public string Print() {
			var sb = new StringBuilder();
			sb.AppendLine($"(def {Identifier} {Value.Expression.Print()})");
			return sb.ToString();
		}
	}

	private static readonly Parser<DefinitionStatement> DefinitionParser = new((c, self) => {
		Token(c, Tokens.DEF);
		Token(c, Tokens.IDENTIFIER, t => { self.Identifier = t; });
		Opt(c, c => {
			Node(c, LogicalExpressionParser, e => { self.Value = (e); });
		});
	});

	public class VariableDefinitionStatement() : IStatement, IPrintable {
		public string? Identifier;
		public List<CExpression>? Values = [];
		public bool isArray => Values.Count > 1;

		public string Print() {
			var sb = new StringBuilder();
			sb.Append($"(let {Identifier} ({string.Join(',', Values.Select(x => x.Expression.Print()))}))");
			return sb.ToString();
		}
	}

	private static readonly Parser<VariableDefinitionStatement> VariableDefinitionParser = new((c, self) => {
		Token(c, Tokens.LET);
		Token(c, Tokens.IDENTIFIER, t => { self.Identifier = t; });
		RepeatOpt(c, c => {
			Node(c, LogicalExpressionParser, e => { self.Values?.Add(e); });
		});
	});

	public class SetStatement() : IStatement, IPrintable {
		public string? Identifier;
		public CExpression? Value;

		public string Print() {
			var sb = new StringBuilder();
			sb.AppendLine($"(set {Identifier} {Value.Expression.Print()})");
			return sb.ToString();
		}
	}

	private static readonly Parser<SetStatement> SetStatementParser = new((c, self) => {
		Token(c, Tokens.SET);
		Token(c, Tokens.IDENTIFIER, t => { self.Identifier = t; });
		Opt(c, c => Node(c, LogicalExpressionParser, t => { self.Value = t; }));
	});

	public class CallStatement() : IStatement, IPrintable {
		public string? Identifier;
		public List<CExpression> Parameters = new();

		public string Print() {
			var sb = new StringBuilder();
			sb.Append($"(let {Identifier} ({string.Join(',', Parameters.Select(x => x.Expression.Print()))}))");
			return sb.ToString();
		}
	}

	private static readonly Parser<CallStatement> CallStatementParser = new((c, self) => {
		Token(c, Tokens.CLL);
		Token(c, Tokens.IDENTIFIER, t => { self.Identifier = t; });
		RepeatOpt(c, c => {
			Node(c, LogicalExpressionParser, t => { self.Parameters.Add(t); });
		});
	});

	public class FunctionDefinitionStatement() : IStatement, IPrintable {
		public string? FuncName;
		public List<string> Parameters = new();
		public List<CStatement> Body = new();

		public string Print() {
			var sb = new StringBuilder();
			sb.Append($"(func {FuncName}");
			foreach (var item in Parameters) {
				sb.Append($"(param {item})");
			}

			sb.AppendLine("");
			foreach (var item in Body) {
				sb.Append($"\t{item.Statement.Print()}");
			}

			sb.AppendLine(")");
			return sb.ToString();
		}
	}

	private static readonly Parser<FunctionDefinitionStatement> FunctionDefinitionParser = new((c, self) => {
		Token(c, Tokens.FNC);
		Token(c, Tokens.IDENTIFIER, t => { self.FuncName = t; });
		RepeatOpt(c, c => {
			Token(c, Tokens.IDENTIFIER, p => {
				self.Parameters.Add(p);
			});
		});
		Token(c, Tokens.COLON);
		//body
		((c.Context as TinyScriptContext)!).BodyDepth++;
		RepeatOpt(c, c => {
			if (!c.Context.PeekToken().Value.Equals("ext")) {
				Node(c, StatementParser, s => {
					self.Body.Add(s);
				});
			} else {
				c.State.Flag("found ext");
			}
		});
		Token(c, Tokens.EXT);
		((c.Context as TinyScriptContext)!).BodyDepth--;
	});

	public class WhileStatement : IStatement, IPrintable {
		public CExpression? Condition;
		public List<CStatement> Body = new();

		public string Print() {
			var sb = new StringBuilder();
			sb.Append($"(loop {Condition} {string.Join(',', Body.Select(x => x.Statement.Print()))})");
			return sb.ToString();
		}
	}

	private static readonly Parser<WhileStatement> WhileStatementParser = new((c, self) => {
		Token(c, Tokens.WHL);
		Node(c, LogicalExpressionParser, t => { self.Condition = t; });
		Token(c, Tokens.COLON);
		((c.Context as TinyScriptContext)!).BodyDepth++;
		RepeatOpt(c, c => {
			Node(c, StatementParser, t => { self.Body.Add(t); });
		});
		Token(c, Tokens.EXT);
		((c.Context as TinyScriptContext)!).BodyDepth--;
	});

	public class IfStatement : IStatement, IPrintable {
		// Has one less than bodies
		public List<CExpression> Conditions = [];
		public List<List<CStatement>> Bodys = [];

		public string Print() {
			var sb = new StringBuilder();
			for (int i = 0; i < Conditions.Count; i++) {
				sb.Append(
					$"(${(i == 0 ? "if" : "elseif")} {Conditions[i].Expression.Print()} {string.Join(',', Bodys[i].Select(x => x.Statement.Print()))})");
			}

			if (Conditions.Count < Bodys.Count) {
				sb.Append($"(else {string.Join(',', Bodys.Last().Select(x => x.Statement.Print()))})");
			}

			return sb.ToString();
		}
	}

	// "iff" condition ":" { statement } [ "elf" condition ":" { statement } ] [ "els" ":" { statement } ] "ext"
	private static readonly Parser<IfStatement> IfStatementParser = new((c, self) => {
		Token(c, Tokens.IFF);
		Node(c, LogicalExpressionParser, t => { self.Conditions.Add(t); });
		Token(c, Tokens.COLON);
		self.Bodys.Add([]);
		((c.Context as TinyScriptContext)!).BodyDepth++;
		RepeatOpt(c, c => {
			Node(c, StatementParser, t => { self.Bodys.Last().Add(t); });
		});
		((c.Context as TinyScriptContext)!).BodyDepth--;
		RepeatOpt(c, c => {
			Token(c, Tokens.ELS);
			Node(c, LogicalExpressionParser, t => { self.Conditions.Add(t); });
			Token(c, Tokens.COLON);
			self.Bodys.Add([]);
			((c.Context as TinyScriptContext)!).BodyDepth++;
			RepeatOpt(c, c => {
				Node(c, StatementParser, t => { self.Bodys.Last().Add(t); });
			});
			((c.Context as TinyScriptContext)!).BodyDepth--;
		});
		Opt(c, c => {
			Token(c, Tokens.ELS);
			Token(c, Tokens.COLON);
			self.Bodys.Add([]);
			((c.Context as TinyScriptContext)!).BodyDepth++;
			RepeatOpt(c, c => {
				Node(c, StatementParser, t => { self.Bodys.Last().Add(t); });
			});
			((c.Context as TinyScriptContext)!).BodyDepth--;
		});
		Token(c, Tokens.EXT);
	});

	public class ImportStatement : IStatement, IPrintable {
		public AtomExpression Atom = new();

		public string Print() {
			var sb = new StringBuilder();
			sb.AppendLine($"(import {Atom.Print()})");
			return sb.ToString();
		}
	}

	private static readonly Parser<ImportStatement> ImportStatementParser = new((c, self) => {
		Token(c, Tokens.INC);
		Token(c, Tokens.IDENTIFIER, r => self.Atom = new AtomExpression { IdentifierValue = r });
	});
}