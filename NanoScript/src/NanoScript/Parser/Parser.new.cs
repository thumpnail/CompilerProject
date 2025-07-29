using System.Reflection.Metadata;

using NanoScript.Parser.AstNodes;
using Parseus.Parser.Implicit;
using Parseus.Lexer;
using Parseus.Parser.Common;

namespace NanoScript.Parser;

public class NanoScriptParser : BaseParser {
    private static readonly Parseus.Lexer.Lexer lexer = new Parseus.Lexer.Lexer()
        // Keywords
        .Child("mod", "mod")
        .Child("import", "import")
        .Child("as", "as")
        .Child("from", "from")
        .Child("pub", "pub")
        .Child("let", "let")
        .Child("var", "var")
        .Child("const", "const")
        .Child("class", "class")
        .Child("struct", "struct")
        .Child("enum", "enum")
        .Child("interface", "interface")
        .Child("union", "union")
        .Child("fnc", "fnc")
        .Child("return", "return")
        .Child("if", "if")
        .Child("else", "else")
        .Child("switch", "switch")
        .Child("default", "default")
        .Child("break", "break")
        .Child("continue", "continue")
        .Child("assert", "assert")
        .Child("error", "error")
        // Symbols
        .Child("colon", ":")
        .Child("semicolon", ";")
        .Child("comma", ",")
        .Child("dot", "\\.")
        .Child("lbrace", "\\{")
        .Child("rbrace", "\\}")
        .Child("lparen", "\\(")
        .Child("rparen", "\\)")
        .Child("lbracket", "\\[")
        .Child("rbracket", "\\]")
        .Child("equal", "=")
        .Child("plusequals", "\\+=")
        .Child("minusequals", "\\-=")
        .Child("timeequals", "\\*=")
        .Child("divideequals", "/=")
        // Literals
        .Child("string", "\"[^\"]*\"", "'[^']*'")
        .Child("number", "\\d+(\\.\\d+)?")
        // Identifiers
        .Child("identifier", "[a-zA-Z_][a-zA-Z0-9_]*")
        // Whitespace and comments
        .Skippable("whitespace", "\\s+")
        .Skippable("comment_single", "//[^\\n]*")
        .Skippable("comment_multi", "/\\*[\\s\\S]*?\\*/");

    public override ProgramStatement Parse(string src) {
        var lexResult = lexer.Lex(src);
        var context = new BasicAParserContext(lexResult.result.ToArray());
        var state = new CancellationState();
        return ProgramParser.Parse(new BaseParserContext(context, state));
    }

    private static readonly Parser<ProgramStatement> ProgramParser = new((c, self) => {
        Repeat(c, c => {
            Node(c, ModuleStatementParser, v => self.moduleStatements.Add(v));
        });
    });

    private static readonly Parser<ModuleStatement> ModuleStatementParser = new((c, self) => {
        Token(c, "mod", out _);
        Node(c, IdentifierParser, v => self.moduleName = v);
        Repeat(c, c => {
            Node(c, ImportStatementParser, v => self.importStatements.Add(v));
        });
        Opt(c, c => {
            Literal(c, "{", out _);
            Repeat(c, c => {
                Node(c, StatementParser, v => self.statements.Add(v));
            });
            Literal(c, "}", out _);
        });
    });

    private static readonly Parser<ImportStatement> ImportStatementParser = new((c, self) => {
        Token(c, "import", out _);
        Alt(c, c => {
            Token(c, "string", out self.importString);
            Opt(c, c => {
                Token(c, "as", out _);
                Node(c, IdentifierParser, v => self.alias = v);
            });
        }, c => {
            Node(c, IdentifierParser, v => self.importName = v);
            Opt(c, c => {
                Token(c, "from", out _);
                Token(c, "string", out self.importString);
            });
        });
    });

    private static readonly Parser<IStatement> StatementParser = new((c, self) => {
        Alt(c, c => {
            Node(c, VariableDeclarationParser, v => self = v);
        }, c => {
            Node(c, FunctionDeclarationParser, v => self = v);
        }, c => {
            Node(c, ReturnStatementParser, v => self = v);
        }, c => {
            Node(c, BreakStatementParser, v => self = v);
        }, c => {
            Node(c, ContinueStatementParser, v => self = v);
        }, c => {
            Node(c, AssertionStatementParser, v => self = v);
        }, c => {
            Node(c, ErrorStatementParser, v => self = v);
        }, c => {
            Node(c, ClassDeclarationParser, v => self = v);
        }, c => {
            Node(c, StructDeclarationParser, v => self = v);
        }, c => {
            Node(c, EnumDeclarationParser, v => self = v);
        }, c => {
            Node(c, InterfaceDeclarationParser, v => self = v);
        }, c => {
            Node(c, UnionDeclarationParser, v => self = v);
        });
    });

    private static readonly Parser<VariableDeclarationStatement> VariableDeclarationParser = new((c, self) => {
        Opt(c, c => Token(c, "pub", out self.isPublic));
        Alt(c, c => Token(c, "let", out self.prefix), c => Token(c, "var", out self.prefix), c => Token(c, "const", out self.prefix));
        Node(c, IdentifierParser, v => self.identifier = v);
        Opt(c, c => {
            Token(c, "colon", out _);
            Node(c, TypeDeclarationParser, v => self.type = v);
        });
        Opt(c, c => {
            Token(c, "equal", out _);
            Node(c, ExpressionParser, v => self.value = v);
        });
    });

    private static readonly Parser<FunctionDeclarationStatement> FunctionDeclarationParser = new((c, self) => {
        Opt(c, c => Token(c, "export", out self.isExport));
        Opt(c, c => Token(c, "pub", out self.isPublic));
        Token(c, "fnc", out _);
        Node(c, IdentifierParser, v => self.identifier = v);
        Literal(c, "(", out _);
        Opt(c, c => {
            Repeat(c, c => {
                Node(c, ParameterParser, v => self.parameters.Add(v));
                Opt(c, c => Token(c, "comma", out _));
            });
        });
        Literal(c, ")", out _);
        Opt(c, c => {
            Token(c, "colon", out _);
            Node(c, TypeDeclarationParser, v => self.returnType = v);
        });
        Literal(c, "{", out _);
        Repeat(c, c => {
            Node(c, StatementParser, v => self.statements.Add(v));
        });
        Literal(c, "}", out _);
    });

    private static readonly Parser<AssignmentStatement> AssignmentStatementParser = new((c, self) => {
        Opt(c, c => Token(c, "dot", out self.isSelf));
        Node(c, IdentifierParser, v => self.identifier = v);
        Opt(c, c => Node(c, TypeDeclarationParser, v => self.type = v));
        Alt(c, c => Token(c, "equal", out self.assignmentType),
            c => Token(c, "doubleleft", out self.assignmentType),
            c => Token(c, "doubleright", out self.assignmentType),
            c => Token(c, "plusequals", out self.assignmentType),
            c => Token(c, "minusequals", out self.assignmentType),
            c => Token(c, "timeequals", out self.assignmentType),
            c => Token(c, "divideequals", out self.assignmentType));
        Node(c, ExpressionParser, v => self.value = v);
    });

    private static readonly Parser<ReturnStatement> ReturnStatementParser = new((c, self) => {
        Token(c, "return", out _);
        Opt(c, c => Node(c, ExpressionParser, v => self.expression = v));
    });

    private static readonly Parser<ConditionalStatement> ConditionalStatementParser = new((c, self) => {
        Token(c, "if", out _);
        Node(c, ExpressionParser, v => self.condition = v);
        Literal(c, "{", out _);
        Repeat(c, c => Node(c, StatementParser, v => self.ifStatements.Add(v)));
        Literal(c, "}", out _);
        Repeat(c, c => {
            Token(c, "else", out _);
            Opt(c, c => {
                Token(c, "if", out _);
                Node(c, ExpressionParser, v => self.elseIfConditions.Add(v));
                Literal(c, "{", out _);
                Repeat(c, c => Node(c, StatementParser, v => self.elseIfStatements.Add(v)));
                Literal(c, "}", out _);
            });
        });
        Opt(c, c => {
            Token(c, "else", out _);
            Literal(c, "{", out _);
            Repeat(c, c => Node(c, StatementParser, v => self.elseStatements.Add(v)));
            Literal(c, "}", out _);
        });
    });

    private static readonly Parser<SwitchStatement> SwitchStatementParser = new((c, self) => {
        Token(c, "switch", out _);
        Node(c, ExpressionParser, v => self.expression = v);
        Literal(c, "{", out _);
        Repeat(c, c => {
            Node(c, SubSwitchStatementParser, v => self.cases.Add(v));
        });
        Opt(c, c => {
            Token(c, "default", out _);
            Literal(c, ":", out _);
            Repeat(c, c => Node(c, StatementParser, v => self.defaultStatements.Add(v)));
        });
        Literal(c, "}", out _);
    });

    private static readonly Parser<SubSwitchStatement> SubSwitchStatementParser = new((c, self) => {
        Node(c, IdentifierParser, v => self.identifier = v);
        Literal(c, ":", out _);
        Repeat(c, c => Node(c, StatementParser, v => self.statements.Add(v)));
        Opt(c, c => Token(c, "break", out self.isBreak));
    });

    private static readonly Parser<IdentifierExpression> IdentifierParser = new((c, self) => {
        Token(c, "identifier", out self.name);
    });

    private static readonly Parser<TypeDeclaration> TypeDeclarationParser = new((c, self) => {
        Token(c, "identifier", out self.typeName);
    });

    private static readonly Parser<IExpression> ExpressionParser = new((c, self) => {
        Alt(c, c => {
            Node(c, TermParser, v => self = v);
            Repeat(c, c => {
                Alt(c, c => Token(c, "plus", out _), c => Token(c, "minus", out _), c => Token(c, "doubleand", out _), c => Token(c, "doubleor", out _));
                Node(c, TermParser, v => self = new BinaryExpression(self, v));
            });
        }, c => {
            Literal(c, "[", out _);
            Node(c, ListExpressionParser, v => self = new ArrayCreationExpression { expressions = v });
            Literal(c, "]", out _);
        }, c => {
            Node(c, IdentifierParser, v => self = v);
            Literal(c, "{", out _);
            Node(c, ListExpressionParser, v => self = new InstanceInitializationExpression { identifier = self, expressions = v });
            Literal(c, "}", out _);
        }, c => {
            Token(c, "fnc", out _);
            Literal(c, "(", out _);
            Node(c, ParameterListParser, v => self = new FunctionDeclarationExpression { parameters = v });
            Literal(c, ")", out _);
            Node(c, TypeDeclarationParser, v => ((FunctionDeclarationExpression)self).typeDeclarationStatement = v);
            Literal(c, "{", out _);
            Repeat(c, c => Node(c, StatementParser, v => ((FunctionDeclarationExpression)self).statements.Add(v)));
            Literal(c, "}", out _);
        });
    });

    private static readonly Parser<IExpression> TermParser = new((c, self) => {
        Node(c, FactorParser, v => self = v);
        Repeat(c, c => {
            Alt(c, c => Token(c, "star", out _), c => Token(c, "slash", out _), c => Token(c, "percent", out _), c => Token(c, "doubleequals", out _), c => Token(c, "notequals", out _), c => Token(c, "less", out _), c => Token(c, "greater", out _), c => Token(c, "lessequal", out _), c => Token(c, "greaterequal", out _));
            Node(c, FactorParser, v => self = new BinaryExpression(self, v));
        });
    });

    private static readonly Parser<IExpression> FactorParser = new((c, self) => {
        Node(c, UnaryParser, v => self = v);
        Repeat(c, c => {
            Token(c, "doublestar", out _);
            Node(c, UnaryParser, v => self = new BinaryExpression(self, v));
        });
    });

    private static readonly Parser<IExpression> UnaryParser = new((c, self) => {
        Opt(c, c => {
            Alt(c, c => Token(c, "doubleplus", out _), c => Token(c, "doubleminus", out _), c => Token(c, "exclamationmark", out _));
        });
        Node(c, BaseParser, v => self = v);
        Opt(c, c => {
            Alt(c, c => Token(c, "doubleplus", out _), c => Token(c, "doubleminus", out _));
        });
    });

    private static readonly Parser<IExpression> BaseParser = new((c, self) => {
        Alt(c, c => {
            Node(c, LiteralParser, v => self = v);
        }, c => {
            Node(c, IdentifierParser, v => self = v);
        }, c => {
            Node(c, ArrayIndexingParser, v => self = v);
        }, c => {
            Node(c, FunctionCallParser, v => self = v);
        }, c => {
            Node(c, TypeConversionParser, v => self = v);
        }, c => {
            Literal(c, "(", out _);
            Node(c, ExpressionParser, v => self = v);
            Literal(c, ")", out _);
        }, c => {
            Node(c, ArrayCreationParser, v => self = v);
        });
    });

    private static readonly Parser<IExpression> LiteralParser = new((c, self) => {
        Alt(c, c => {
            Token(c, "number", out var value);
            self = new NumberExpression { Value = value };
        }, c => {
            Token(c, "string", out var value);
            self = new StringExpression { Value = value };
        }, c => {
            Token(c, "true", out _);
            self = new BooleanExpression { Value = true };
        }, c => {
            Token(c, "false", out _);
            self = new BooleanExpression { Value = false };
        });
    });

    private static readonly Parser<IExpression> ArrayIndexingParser = new((c, self) => {
        Node(c, IdentifierParser, v => self = v);
        Literal(c, "[", out _);
        Node(c, ExpressionParser, v => self = new ArrayIndexingExpression { identifier = self, index = v });
        Literal(c, "]", out _);
    });

    private static readonly Parser<IExpression> FunctionCallParser = new((c, self) => {
        Node(c, IdentifierParser, v => self = v);
        Literal(c, "(", out _);
        Opt(c, c => {
            Node(c, ExpressionListParser, v => self = new FunctionCallExpression { identifier = self, parameters = v });
        });
        Literal(c, ")", out _);
    });

    private static readonly Parser<IExpression> TypeConversionParser = new((c, self) => {
        Literal(c, "(", out _);
        Node(c, IdentifierParser, v => self = v);
        Literal(c, ")", out _);
        Node(c, ExpressionParser, v => self = new TypeConversionExpression { identifier = self, expression = v });
    });

    private static readonly Parser<IExpression> ArrayCreationParser = new((c, self) => {
        Literal(c, "[", out _);
        Opt(c, c => {
            Node(c, ListExpressionParser, v => self = new ArrayCreationExpression { expressions = v });
        });
        Literal(c, "]", out _);
    });

    private static readonly Parser<List<IExpression>> ListExpressionParser = new((c, self) => {
        Repeat(c, c => {
            Node(c, ExpressionParser, v => self.Add(v));
            Opt(c, c => Literal(c, ",", out _));
        });
    });

    private static readonly Parser<IStatement> BreakStatementParser = new((c, self) => {
        Token(c, "break", out _);
        self = new BreakStatement();
    });

    private static readonly Parser<IStatement> ContinueStatementParser = new((c, self) => {
        Token(c, "continue", out _);
        self = new ContinueStatement();
    });

    private static readonly Parser<IStatement> AssertionStatementParser = new((c, self) => {
        Token(c, "assert", out _);
        Node(c, ExpressionParser, v => self = new AssertionStatement { Expression = v });
    });

    private static readonly Parser<IStatement> ErrorStatementParser = new((c, self) => {
        Token(c, "error", out _);
        Node(c, ExpressionParser, v => self = new ErrorStatement { Expression = v });
    });

    private static readonly Parser<ClassDeclarationStatement> ClassDeclarationParser = new((c, self) => {
        Opt(c, c => Token(c, "pub", out self.isPublic));
        Token(c, "class", out _);
        Node(c, IdentifierParser, v => self.identifier = v);
        Literal(c, "{", out _);
        Repeat(c, c => {
            Node(c, StatementParser, v => self.statements.Add(v));
        });
        Literal(c, "}", out _);
    });

    private static readonly Parser<StructDeclarationStatement> StructDeclarationParser = new((c, self) => {
        Opt(c, c => Token(c, "pub", out self.isPublic));
        Token(c, "struct", out _);
        Node(c, IdentifierParser, v => self.identifier = v);
        Opt(c, c => Node(c, TypeDeclarationParser, v => self.type = v));
        Literal(c, "{", out _);
        Repeat(c, c => {
            Node(c, StatementParser, v => self.statements.Add(v));
        });
        Literal(c, "}", out _);
    });

    private static readonly Parser<EnumDeclarationStatement> EnumDeclarationParser = new((c, self) => {
        Opt(c, c => Token(c, "pub", out self.isPublic));
        Token(c, "enum", out _);
        Node(c, IdentifierParser, v => self.identifier = v);
        Opt(c, c => {
            Token(c, "colon", out _);
            Node(c, TypeDeclarationParser, v => self.type = v);
        });
        Literal(c, "{", out _);
        Repeat(c, c => {
            Node(c, EnumValueParser, v => self.enumValues.Add(v));
            Opt(c, c => Token(c, "comma", out _));
        });
        Literal(c, "}", out _);
    });

    private static readonly Parser<EnumValue> EnumValueParser = new((c, self) => {
        Node(c, IdentifierParser, v => self.identifier = v);
        Opt(c, c => {
            Token(c, "colon", out _);
            Node(c, ExpressionParser, v => self.value = v);
        });
    });

    private static readonly Parser<InterfaceDeclarationStatement> InterfaceDeclarationParser = new((c, self) => {
        Opt(c, c => Token(c, "pub", out self.isPublic));
        Token(c, "interface", out _);
        Node(c, IdentifierParser, v => self.identifier = v);
        Literal(c, "{", out _);
        Repeat(c, c => {
            Node(c, StatementParser, v => self.statements.Add(v));
        });
        Literal(c, "}", out _);
    });

    private static readonly Parser<UnionDeclarationStatement> UnionDeclarationParser = new((c, self) => {
        Opt(c, c => Token(c, "pub", out self.isPublic));
        Token(c, "union", out _);
        Node(c, IdentifierParser, v => self.identifier = v);
        Literal(c, "{", out _);
        Repeat(c, c => {
            Node(c, StatementParser, v => self.statements.Add(v));
        });
        Literal(c, "}", out _);
    });

    private static readonly Parser<ParameterList> ParameterListParser = new((c, self) => {
        Repeat(c, c => {
            Node(c, ParameterParser, v => self.parameters.Add(v));
            Opt(c, c => Token(c, "comma", out _));
        });
    });

    private static readonly Parser<Parameter> ParameterParser = new((c, self) => {
        Node(c, IdentifierParser, v => self.identifier = v);
        Opt(c, c => Node(c, TypeDeclarationParser, v => self.type = v));
    });

    private static readonly Parser<FunctionCallStatement> FunctionCallStatementParser = new((c, self) => {
        Node(c, IdentifierParser, v => self.identifier = v);
        Literal(c, "(", out _);
        Opt(c, c => Node(c, ExpressionListParser, v => self.parameters = v));
        Literal(c, ")", out _);
    });

    private static readonly Parser<ExpressionList> ExpressionListParser = new((c, self) => {
        Repeat(c, c => {
            Node(c, ExpressionParser, v => self.expressions.Add(v));
            Opt(c, c => Token(c, "comma", out _));
        });
    });
}
