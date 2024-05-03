using Parseus.Lexer;
using NanoScript.Parser;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Win32.SafeHandles;
using NanoScriptCompiler_Bflat.Helper;

namespace NanoScript {
    public static class Program {
        public enum Token {
            NONE, EOL, IDENTIFIER, STRING, NUMBER, @TRUE, @FALSE,
            @PUB, @MOD, @RETURN, @ENUM, @ERROR, @GOTO, @DEF, @TYPE, @STRUCT,
            @CLASS, @INTERFACE, @UNION, @ASSERT, @IMPORT, @AS, @FROM, @LET, @VAR, @CONST, @IF, @ELSE, @SWITCH, @BREAK,
            @DEFAULT, @CONTINUE, @EXPORT, @FNC, @MATCH, @IS, @SIZE, @STR, @FOR, @IN,

            LEFTBRACE, RIGHTBRACE, DOT, DOUBLELEFT, DOUBLERIGHT, PLUSEQUALS, MINUSEQUALS, TIMESEQUALS, SLASHEQUALS,
            COLON, EQUAL, SEMICOLON, LEFTPAREN, COMMA, RIGHTPAREN, DOUBLECOLON, GREATER, LEFTBRACKET, RIGHTBRACKET,
            DOUBLEBRACES, DOUBLEARROWRIGHT, PIPE, AND, PLUS, MINUS, PERCENT, SLASH, DOUBLESTAR, STAR, DOUBLEPIPE, DOUBLEAND, DOUBLEEQUAL,
            NOTEQUAL, LESSEQUAL, LESS, GREATEREQUALS, DOUBLEDOT, CIRCUMFLEX, TILDE, EXLEMATIONMARK, DOUBLEPLUS, DOUBLEMINUS
        }

        public static void Main(string[] args) {
            var lexer = CreateLexer();
            var lexerResult =
                lexer.Lex(
                    //    "let somename = \"Hello World\"" + Environment.NewLine +
                    //    "//"+"this is a comment" + Environment.NewLine +
                    //    "let somenum = 12.21" + Environment.NewLine
                    //File.ReadAllText("./../../../test.nano")
                    File.ReadAllText(@"D:\fried\OneDrive\Dokumente\Code\Rider-Projects\CompilerProject\NanoScriptCompiler-Bflat\test.nano")
                );

            using (StreamWriter writer = new StreamWriter("output_tokens.txt")) {
                lexerResult.result.ForEach(element => {
                    writer.WriteLine($"({element.token}, {element.Value}):({element.index}, {element.length})");
                });
                writer.Close();
            }

            var ctx = new ParserContext(lexerResult.result);
            var res = new Parser.Parser(ctx).Parse();
            using (StreamWriter writer = new StreamWriter("output_ast.json")) {
                writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(res, Formatting.Indented));
                writer.Close();
            }
            var code = res.GenBflat();
            code.ToConsole();
            code.ToFile("output_bflat.csx");
        }
        public static Lexer<Token> CreateLexer() {
            const string ANY = "[.]";
            const string STRING = $"\"{ANY}\"";
            const string WORD = "[a-zA-Z_][a-zA-Z0-9_]*";
            const string IDENTIFIER = $"{WORD}(.{WORD})*(:{WORD})";
            const string DIGIT = "[0-9]";
            const string DECIMAL = $"({DIGIT}*).{DIGIT}*";
            const string NUMBER = $"{DIGIT}*{DECIMAL}?";

            var lexer =
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
                    .child(Token.TYPE, "type")
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
                    .child(Token.SIZE, "size")
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
                    .child(Token.IDENTIFIER, WORD)
                    .child(Token.STRING, @"'(\\.|[^'\\])*'", "\"" + @"(\\.|[^" + "\"" + @"\\])*" + "\"")
                    .child(Token.NUMBER, @"-?(0[xX][0-9a-fA-F]+|\d*[.]\d+([eE][+-]?\d+)?|\d+([.]\d*)?([eE][+-]?\d+)?)");
            return lexer;
        }
    }
}