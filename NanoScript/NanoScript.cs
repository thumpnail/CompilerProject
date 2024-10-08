using System.Xml.Serialization;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NanoScript.Helper;
using NanoScript.Parser;
using Newtonsoft.Json;
using NanoScript.Lexer;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using NanoScript.Parser.AstNodes;
namespace NanoScript;

public class NanoScript {
	private readonly string pathref = Directory.GetCurrentDirectory() + "\\..\\..\\..\\";
	public static readonly string CWD = Directory.GetCurrentDirectory() + "\\";
	private static readonly string stdlibpath = Path.Combine(CWD, "lib");
	public NanoScript() {}
	public void RunFile(string path) {
		if (File.Exists(path)) {
			RunString(File.ReadAllText(path));
		} else {
			Console.WriteLine("File not found: " + path);
		}
	}
	public void RunString(string input) {
		Lexer<Token> lexer = CreateLexer();
		LexerResult<Token> lexerResult = lexer.Lex(input);
		Compile_CS(lexerResult);
	}
	public void RunDictionary(string path) {
		Lexer<Token> lexer = CreateLexer();
		//Get all files inside given directory that end with .nano
		Directory.GetFiles(path, "*.nano", SearchOption.AllDirectories);
	}
	private void Compile_CS(LexerResult<Token> lexerResult) {
		using (StreamWriter writer = new($"{pathref}output_tokens.txt")) {
			lexerResult.result.ForEach(element => {
				writer.WriteLine($"({element.token}, {element.Value}):({element.index}, {element.length})");
			});
			writer.Close();
		}

		ParserContext ctx = new ParserContext(lexerResult);
		ProgramStatement res = new Parser.Parser(ctx).Parse();

		//try {
		//	var xml = res.ToXml();
		//} catch (Exception e) {e.ToString().ToConsole();}
		
		var json = JsonConvert.SerializeObject(res, Formatting.Indented);
		json.ToFile($"{pathref}output_ast.json");
		json.ToXml().ToFile($"{pathref}output_ast.xml");
		
		
		string code = res.GenCS().Trim();
		code.ToConsole();
		code.ToFile($"{pathref}output_bflat.csx");
		"Code Written to file".ToConsole();

		FormatGeneratedFile();
	}
	private void FormatGeneratedFile() {
		//format the generated file
		string code2;
		using (StreamReader sr = new StreamReader($"{pathref}output_bflat.csx")) {
			code2 = sr.ReadToEnd();
		}
		SyntaxTree tree = CSharpSyntaxTree.ParseText(code2);
		SyntaxNode root = tree.GetRoot();
		AdhocWorkspace workspace = new AdhocWorkspace();
		OptionSet options = workspace.Options;
		SyntaxNode formattedRoot = Formatter.Format(root, workspace, options);
		using (StreamWriter sw = new StreamWriter($"{pathref}output_bflat.csx", Encoding.Default, new FileStreamOptions() {
			       Access = FileAccess.Write,
			       Mode = FileMode.Create
		       })) {
			sw.WriteLine(formattedRoot.ToFullString());
		}
	}
	public static Lexer<Token> CreateLexer() {
		const string ANY = "[.]";
		const string STRING = $"\"{ANY}\"";
		const string WORD = "[a-zA-Z_][a-zA-Z0-9_]*";
		const string IDENTIFIER = $"[\\.]?{WORD}([\\.]{WORD})*([\\:]{WORD})?";
		const string DIGIT = "[0-9]";
		const string NUMBER = $"{DIGIT}+(\\.{DIGIT}+)?";

		Lexer<Token> lexer =
			new Lexer<Token>()
				.skipable(Token.NONE, @"\/\/.*")
				//Keywords
				.child(Token.PUB, "pub")
				.child(Token.MOD, "mod")
				.child(Token.RETURN, "return")
				.child(Token.ENUM, "enum")
				.child(Token.ERROR, "error")
				.child(Token.GOTO, "goto")
				.child(Token.DEF, "def")
				//.child(Token.TYPE, "type")
				.child(Token.STRUCT, "struct")
				.child(Token.CLASS, "class")
				.child(Token.INTERFACE, "interface")
				.child(Token.UNION, "union")
				.child(Token.ASSERT, "assert")
				.child(Token.IMPORT, "import")
				.child(Token.AS, "as")
				.child(Token.FROM, "from")
				.child(Token.LET, "let")
				.child(Token.VAR, "var")
				.child(Token.CONST, "const")
				.child(Token.IF, "if")
				.child(Token.ELSE, "else")
				.child(Token.SWITCH, "switch")
				.child(Token.BREAK, "break")
				.child(Token.DEFAULT, "default")
				.child(Token.CONTINUE, "continue")
				.child(Token.EXPORT, "export")
				.child(Token.FNC, "fnc")
				.child(Token.MATCH, "match")
				.child(Token.TRUE, "true")
				.child(Token.FALSE, "false")
				.child(Token.IS, "is")
				//.child(Token.SIZE, "size")
				.child(Token.FOR, "for")
				.child(Token.IN, "in")
				// Operators
				.child(Token.LEFTBRACE, "\\{")
				.child(Token.RIGHTBRACE, "\\}")
				.child(Token.DOT, "\\.")
				.child(Token.DOUBLELEFT, "<<")
				.child(Token.DOUBLERIGHT, ">>")
				.child(Token.PLUSEQUALS, "\\+=")
				.child(Token.MINUSEQUALS, "-=")
				.child(Token.TIMESEQUALS, "\\*=")
				.child(Token.SLASHEQUALS, "/=")
				.child(Token.DOUBLEPLUS, "\\+\\+")
				.child(Token.DOUBLEMINUS, "--")
				.child(Token.DOUBLECOLON, "::")
				.child(Token.COLON, ":")
				.child(Token.SEMICOLON, ";")
				.child(Token.LEFTPAREN, "\\(")
				.child(Token.COMMA, ",")
				.child(Token.RIGHTPAREN, "\\)")
				//.child(Token.NONE, " ")
				.child(Token.GREATER, ">")
				.child(Token.LEFTBRACKET, "\\[")
				.child(Token.RIGHTBRACKET, "\\]")
				.child(Token.DOUBLEBRACES, "\\{\\}")
				.child(Token.DOUBLEARROWRIGHT, "=>")
				.child(Token.DOUBLEPIPE, "\\|\\|")
				.child(Token.PIPE, "\\|")
				.child(Token.DOUBLEAND, "\\&\\&")
				.child(Token.AND, "\\&")
				.child(Token.PLUS, "\\+")
				.child(Token.MINUS, "-")
				.child(Token.SLASH, "/")
				.child(Token.DOUBLESTAR, "\\*\\*")
				.child(Token.STAR, "\\*")
				.child(Token.PERCENT, "%")
				.child(Token.DOUBLEEQUAL, "==")
				.child(Token.NOTEQUAL, "\\!=")
				.child(Token.LESSEQUAL, "<=")
				.child(Token.DOUBLELEFT, "<<")
				.child(Token.LESS, "<")
				.child(Token.GREATEREQUALS, ">=")
				.child(Token.EQUAL, "=")
				.child(Token.DOUBLEDOT, "\\.\\.")
				.child(Token.CIRCUMFLEX, "^")
				.child(Token.TILDE, "\\~")
				.child(Token.EXLEMATIONMARK, "\\!")
				// regex
				.skipable(Token.EOL, Environment.NewLine)
				.child(Token.IDENTIFIER, IDENTIFIER)
				.child(Token.STRING, "\"" + @"(\\.|[^" + "\"" + @"\\])*" + "\"")
				.child(Token.STRING, @"'(\\.|[^'\\])*'")
				.child(Token.NUMBER, @"-?(0[xX][0-9a-fA-F]+|\d*[.]\d+([eE][+-]?\d+)?|\d+([.]\d*)?([eE][+-]?\d+)?)");
		return lexer;
	}
}