using System.Reflection.Metadata;

using NanoScript.Parser.AstNodes;

using Parseus.Parser.Implicit;
using Parseus.Parser.Common;

// ReSharper disable VariableHidesOuterVariable
namespace NanoScript.Parser;

// Token-Konstanten für alle Token-Namen
public static class Tokens {
	public const string NONE = "NONE";
	public const string PUB = "PUB";
	public const string MOD = "MOD";
	public const string RETURN = "RETURN";
	public const string ENUM = "ENUM";
	public const string GOTO = "GOTO";
	public const string DEF = "DEF";
	public const string STRUCT = "STRUCT";
	public const string CLASS = "CLASS";
	public const string INTERFACE = "INTERFACE";
	public const string IMPORT = "IMPORT";
	public const string AS = "AS";
	public const string FROM = "FROM";
	public const string LET = "LET";
	public const string VAR = "VAR";
	public const string CONST = "CONST";
	public const string IF = "IF";
	public const string ELSE = "ELSE";
	public const string SWITCH = "SWITCH";
	public const string BREAK = "BREAK";
	public const string DEFAULT = "DEFAULT";
	public const string CONTINUE = "CONTINUE";
	public const string EXPORT = "EXPORT";
	public const string FNC = "FNC";
	public const string MATCH = "MATCH";
	public const string TRUE = "TRUE";
	public const string FALSE = "FALSE";
	public const string IS = "IS";
	public const string FOR = "FOR";
	public const string IN = "IN";
	public const string LEFTBRACE = "LEFTBRACE";
	public const string RIGHTBRACE = "RIGHTBRACE";
	public const string DOT = "DOT";
	public const string DOUBLELEFT = "DOUBLELEFT";
	public const string DOUBLERIGHT = "DOUBLERIGHT";
	public const string PLUSEQUALS = "PLUSEQUALS";
	public const string MINUSEQUALS = "MINUSEQUALS";
	public const string TIMESEQUALS = "TIMESEQUALS";
	public const string SLASHEQUALS = "SLASHEQUALS";
	public const string DOUBLEPLUS = "DOUBLEPLUS";
	public const string DOUBLEMINUS = "DOUBLEMINUS";
	public const string DOUBLECOLON = "DOUBLECOLON";
	public const string COLON = "COLON";
	public const string SEMICOLON = "SEMICOLON";
	public const string LEFTPAREN = "LEFTPAREN";
	public const string COMMA = "COMMA";
	public const string RIGHTPAREN = "RIGHTPAREN";
	public const string GREATER = "GREATER";
	public const string LEFTBRACKET = "LEFTBRACKET";
	public const string RIGHTBRACKET = "RIGHTBRACKET";
	public const string DOUBLEBRACES = "DOUBLEBRACES";
	public const string DOUBLEARROWRIGHT = "DOUBLEARROWRIGHT";
	public const string DOUBLEPIPE = "DOUBLEPIPE";
	public const string PIPE = "PIPE";
	public const string DOUBLEAND = "DOUBLEAND";
	public const string AND = "AND";
	public const string PLUS = "PLUS";
	public const string MINUS = "MINUS";
	public const string SLASH = "SLASH";
	public const string DOUBLESTAR = "DOUBLESTAR";
	public const string STAR = "STAR";
	public const string PERCENT = "PERCENT";
	public const string DOUBLEEQUAL = "DOUBLEEQUAL";
	public const string NOTEQUAL = "NOTEQUAL";

	public const string LESSEQUAL = "LESSEQUAL";

	//public const string DOUBLELEFT = "DOUBLELEFT";
	public const string LESS = "LESS";
	public const string GREATEREQUALS = "GREATEREQUALS";
	public const string EQUAL = "EQUAL";
	public const string DOUBLEDOT = "DOUBLEDOT";
	public const string CIRCUMFLEX = "CIRCUMFLEX";
	public const string TILDE = "TILDE";
	public const string EXLEMATIONMARK = "EXLEMATIONMARK";
	public const string EOL = "EOL";
	public const string IDENTIFIER = "IDENTIFIER";

	public const string STRING = "STRING";

	//public const string STRING = "STRING";
	public const string NUMBER = "NUMBER";
	public const string NULL = "NULL";
}

public class NanoScriptParser : BaseParser {
	const string ANY = "[.]";
	const string STRING = $"\"{ANY}\"";
	const string WORD = "[a-zA-Z_][a-zA-Z0-9_]*";
	const string IDENTIFIER = $"[\\.]?{WORD}([\\.]{WORD})*([\\:]{WORD})?";
	const string DIGIT = "[0-9]";
	const string NUMBER = $"{DIGIT}+(\\.{DIGIT}+)?";

	private static readonly Parseus.Lexer.RegExBased.Lexer lexer = new Parseus.Lexer.RegExBased.Lexer()
		.Skippable(Tokens.NONE, @"\/\/.*")
		//Keywords
		.Child(Tokens.PUB, "pub")
		.Child(Tokens.MOD, "mod")
		.Child(Tokens.RETURN, "return")
		.Child(Tokens.ENUM, "enum")
		.Child(Tokens.GOTO, "goto")
		.Child(Tokens.DEF, "def")
		.Child(Tokens.STRUCT, "struct")
		.Child(Tokens.CLASS, "class")
		.Child(Tokens.INTERFACE, "interface")
		//.child(Tokens.UNION, "union")
		//.child(Tokens.ASSERT, "assert")
		.Child(Tokens.IMPORT, "import")
		.Child(Tokens.AS, "as")
		.Child(Tokens.FROM, "from")
		.Child(Tokens.LET, "let")
		.Child(Tokens.VAR, "var")
		.Child(Tokens.CONST, "const")
		.Child(Tokens.IF, "if")
		.Child(Tokens.ELSE, "else")
		.Child(Tokens.SWITCH, "switch")
		.Child(Tokens.BREAK, "break")
		.Child(Tokens.DEFAULT, "default")
		.Child(Tokens.CONTINUE, "continue")
		.Child(Tokens.EXPORT, "export")
		.Child(Tokens.FNC, "fnc")
		.Child(Tokens.MATCH, "match")
		.Child(Tokens.TRUE, "true")
		.Child(Tokens.FALSE, "false")
		.Child(Tokens.IS, "is")
		.Child(Tokens.FOR, "for")
		.Child(Tokens.IN, "in")
		// Operators
		.Child(Tokens.LEFTBRACE, "\\{")
		.Child(Tokens.RIGHTBRACE, "\\}")
		.Child(Tokens.DOT, "\\.")
		.Child(Tokens.DOUBLELEFT, "<<")
		.Child(Tokens.DOUBLERIGHT, ">>")
		.Child(Tokens.PLUSEQUALS, "\\+=")
		.Child(Tokens.MINUSEQUALS, "-=")
		.Child(Tokens.TIMESEQUALS, "\\*=")
		.Child(Tokens.SLASHEQUALS, "/=")
		.Child(Tokens.DOUBLEPLUS, "\\+\\+")
		.Child(Tokens.DOUBLEMINUS, "--")
		.Child(Tokens.DOUBLECOLON, "::")
		.Child(Tokens.COLON, ":")
		.Child(Tokens.SEMICOLON, ";")
		.Child(Tokens.LEFTPAREN, "\\(")
		.Child(Tokens.COMMA, ",")
		.Child(Tokens.RIGHTPAREN, "\\)")
		//.child(Tokens.NONE, " ")
		.Child(Tokens.GREATER, ">")
		.Child(Tokens.LEFTBRACKET, "\\[")
		.Child(Tokens.RIGHTBRACKET, "\\]")
		.Child(Tokens.DOUBLEBRACES, "\\{\\}")
		.Child(Tokens.DOUBLEARROWRIGHT, "=>")
		.Child(Tokens.DOUBLEPIPE, "\\|\\|")
		.Child(Tokens.PIPE, "\\|")
		.Child(Tokens.DOUBLEAND, "\\&\\&")
		.Child(Tokens.AND, "\\&")
		.Child(Tokens.PLUS, "\\+")
		.Child(Tokens.MINUS, "-")
		.Child(Tokens.SLASH, "/")
		.Child(Tokens.DOUBLESTAR, "\\*\\*")
		.Child(Tokens.STAR, "\\*")
		.Child(Tokens.PERCENT, "%")
		.Child(Tokens.DOUBLEEQUAL, "==")
		.Child(Tokens.NOTEQUAL, "\\!=")
		.Child(Tokens.LESSEQUAL, "<=")
		.Child(Tokens.DOUBLELEFT, "<<")
		.Child(Tokens.LESS, "<")
		.Child(Tokens.GREATEREQUALS, ">=")
		.Child(Tokens.EQUAL, "=")
		.Child(Tokens.DOUBLEDOT, "\\.\\.")
		.Child(Tokens.CIRCUMFLEX, "^")
		.Child(Tokens.TILDE, "\\~")
		.Child(Tokens.EXLEMATIONMARK, "\\!")
		// regex
		.Skippable(Tokens.EOL, Environment.NewLine)
		.Child(Tokens.IDENTIFIER, IDENTIFIER)
		.Child(Tokens.STRING, "\"" + @"(\\.|[^" + "\"" + @"\\])*" + "\"")
		.Child(Tokens.STRING, @"'(\\.|[^'\\])*'")
		.Child(Tokens.NUMBER, @"-?(0[xX][0-9a-fA-F]+|\d*[.]\d+([eE][+-]?\d+)?|\d+([.]\d*)?([eE][+-]?\d+)?)");

	public override ProgramStatement Parse(string src) {
		var lexResult = lexer.Lex(src);
		var context = new BasicAParserContext(lexResult.result.ToArray());
		var state = new CancellationState();
		return ProgramParser.Parse(new BaseParserContext(context, state));
	}

	private static readonly Parser<ProgramStatement> ProgramParser = new((c, self) => {
		Node(c, ModuleStatementParser, v => self.moduleStatements.Add(v));
	});

	private static readonly Parser<ModuleStatement> ModuleStatementParser = new((c, self) => {
		Token(c, Tokens.MOD);
		Node(c, IdentifierParser, v => self.moduleName = v);
		RepeatOpt(c, c => {
			Node(c, ImportStatementParser, v => self.importStatements.Add(v));
		});
		Opt(c, c => {
			Literal(c, "{", t => { self.hasBody = t; });
		});
		RepeatOpt(c, c => {
			Node(c, StatementParser, v => self.statements.Add(v));
		});
		if (self.hasBody) {
			Literal(c, "}");
		}
	});

	private static readonly Parser<ImportStatement> ImportStatementParser = new((c, self) => {
		Token(c, Tokens.IMPORT);
		//TODO: Alt broken? seems like it only tries the first option
		Alt(c, [
			c => {
				Token(c, Tokens.STRING, t => { self.importString = t; });
				Opt(c, c => {
					Literal(c, Tokens.AS, t => { self.isAs = t; });
					Node(c, IdentifierParser, v => self.Identifier = v);
				});
			},
			c => {
				Node(c, IdentifierParser, v => self.Identifier = v);
				Opt(c, c => {
					Literal(c, Tokens.FROM, t => { self.isFrom = t; });
					Token(c, Tokens.STRING, t => { self.importString = t; });
				});
			}
		]);
	});

	private static readonly Parser<IStatement> StatementParser = new((c, self) => {
		Alt(c, [
			c => {
				Node(c, VariableDeclarationParser, v => self = v);
			},
			c => {
				Node(c, FunctionDeclarationParser, v => self = v);
			},
			c => {
				Node(c, ReturnStatementParser, v => self = v);
			},
			c => {
				Node(c, BreakStatementParser, v => self = v);
			},
			c => {
				Node(c, ContinueStatementParser, v => self = v);
			},
			c => {
				Node(c, ClassDeclarationParser, v => self = v);
			},
			c => {
				Node(c, StructDeclarationParser, v => self = v);
			},
			c => {
				Node(c, EnumDeclarationParser, v => self = v);
			},
			c => {
				Node(c, InterfaceDeclarationParser, v => self = v);
			},
			c => {
				Node(c, UnionDeclarationParser, v => self = v);
			}
		]);
	});

	private static readonly Parser<VariableDeclarationStatement> VariableDeclarationParser = new((c, self) => {
		Opt(c, c => {
			Literal(c, Tokens.PUB, t => { self.isPublic = t; });
		});
		Alt(c, [
			c => Token(c, Tokens.LET, t => { self.prefix = t; }),
			c => Token(c, Tokens.VAR, t => { self.prefix = t; }),
			c => Token(c, Tokens.CONST, t => { self.prefix = t; })
		]);
		Node(c, IdentifierParser, v => self.Identifier = v);
		Opt(c, c => {
			Token(c, Tokens.COLON);
			Node(c, TypeDeclarationParser, v => self.typeDeclarationStatement = v);
		});
		Opt(c, c => {
			Token(c, Tokens.EQUAL);
			Node(c, ExpressionParser, v => self.exp = v);
		});
	});

	private static readonly Parser<FunctionDeclarationStatement> FunctionDeclarationParser = new((c, self) => {
		Opt(c, c => {
			Literal(c, Tokens.EXPORT, t => { self.isExport = t; });
		});
		Opt(c, c => {
			Literal(c, Tokens.PUB, t => { self.isPublic = t; });
		});
		Literal(c, Tokens.FNC);
		Node(c, IdentifierParser, v => self.identifier = v);
		Literal(c, "(");
		Opt(c, c => {
			Repeat(c, c => {
				Node(c, ParameterParser, v => self.parameters.Add(v));
				Opt(c, c => Token(c, Tokens.COMMA));
			});
		});
		Literal(c, ")");
		Opt(c, c => {
			Literal(c, Tokens.COLON);
			Node(c, TypeDeclarationParser, v => self.returnType = v);
		});
		Literal(c, "{");
		Repeat(c, c => {
			Node(c, StatementParser, v => self.statements.Add(v));
		});
		Literal(c, "}");
	});

	private static readonly Parser<AssignmentStatement> AssignmentStatementParser = new((c, self) => {
		Opt(c, c => Literal(c, Tokens.DOT, t => { self.isSelf = t; }));
		Node(c, IdentifierParser, v => self.identifier = v);
		Opt(c, c => Node(c, TypeDeclarationParser, v => self.typeDeclarationStatement = v));
		Alt(c, [
			c => Token(c, Tokens.EQUAL, (s => self.assignmentType = AssignmentType.equal)),
			c => Token(c, Tokens.PLUSEQUALS, (s => self.assignmentType = AssignmentType.add)),
			c => Token(c, Tokens.DOUBLERIGHT, (s => self.assignmentType = AssignmentType.pop)),
			c => Token(c, Tokens.DOUBLELEFT, (s => self.assignmentType = AssignmentType.push)),
			c => Token(c, Tokens.PLUSEQUALS, (s => self.assignmentType = AssignmentType.add)),
			c => Token(c, Tokens.MINUSEQUALS, (s => self.assignmentType = AssignmentType.sub)),
			c => Token(c, Tokens.TIMESEQUALS, (s => self.assignmentType = AssignmentType.mul)),
			c => Token(c, Tokens.SLASHEQUALS, (s => self.assignmentType = AssignmentType.div))
		]);
		Node(c, ExpressionParser, v => self.exp = v);
	});

	private static readonly Parser<ReturnStatement> ReturnStatementParser = new((c, self) => {
		Token(c, Tokens.RETURN);
		Opt(c, c => Node(c, ExpressionParser, v => self.exp = v));
	});

	private static readonly Parser<ConditionalStatement> ConditionalStatementParser = new((c, self) => {
		Token(c, Tokens.IF);
		Node(c, SubConditionalStatementParser, v => self.ifConditionalStatement = v);
		Literal(c, "{");
		Repeat(c, c => Node(c, StatementParser, v => self.ifConditionalStatement.statements.Add(v)));
		Literal(c, "}");
		Repeat(c, c => {
			Alt(c, [
				c => {
					Opt(c, c => {
						Token(c, Tokens.ELSE);
						Token(c, Tokens.IF);
						Node(c, SubConditionalStatementParser, v => self.elseIfConditionalStatements.Add(v));
						Literal(c, "{");
						Repeat(c, c => {
							Node(c, StatementParser, v => {
								if (self.elseIfConditionalStatements is null) {
									self.elseIfConditionalStatements = new List<SubConditionalStatement>();
								}

								self.elseIfConditionalStatements.Add(new());
								self.elseIfConditionalStatements.Last().statements.Add(v);
							});
						});
						Literal(c, "}");
					});
				},
				c => {
					Token(c, Tokens.ELSE);
					Literal(c, "{");
					Repeat(c, c => Node(c, StatementParser, v => self.elseConditionalStatement.statements.Add(v)));
					Literal(c, "}");
				}
			]);
		});
	});

	// TODO: Fix this parser to match the SubConditionalStatement structure
	private static Parser<SubConditionalStatement> SubConditionalStatementParser = new((c, self) => {
		Node(c, ExpressionParser, v => self.exp = v);
		/*Opt(c, c => {
			Token(c, Tokens.IS);
			Node(c, IdentifierParser, v => self.exp = v);
		});*/
	});

	private static readonly Parser<SwitchStatement> SwitchStatementParser = new((c, self) => {
		Token(c, Tokens.SWITCH);
		Node(c, ExpressionParser, v => self.Expression = v);
		Literal(c, "{");
		Repeat(c, c => {
			Node(c, SubSwitchStatementParser, v => self.subSwitchStatements.Add(v));
		});
		Opt(c, c => {
			Token(c, Tokens.DEFAULT);
			Literal(c, ":");
			Repeat(c, c => Node(c, StatementParser, v => self.defSubSwitchStatement = (SubSwitchStatement?)v));
		});
		Literal(c, "}");
	});

	private static readonly Parser<SubSwitchStatement> SubSwitchStatementParser = new((c, self) => {
		Node(c, IdentifierParser, v => self.identifier = v);
		Literal(c, ":");
		Repeat(c, c => Node(c, StatementParser, v => self.statements.Add(v)));
		Opt(c, c => Literal(c, Tokens.BREAK, t => { self.isBreak = t; }));
	});

	private static readonly Parser<IdentifierExpression> IdentifierParser = new((c, self) => {
		Token(c, Tokens.IDENTIFIER, t => { self.identifier = t; });
	});

	private static readonly Parser<TypeDeclarationStatement> TypeDeclarationParser = new((c, self) => {
		Node(c, IdentifierParser, v => self.identifier = v);
	});

	private static readonly Parser<IExpression> ExpressionParser = new((c, self) => {
		Alt(c, [
			c => {
				Node(c, TermParser, v => self = v);
				Repeat(c, c => {
					BinaryOperatorType op = BinaryOperatorType.none;
					Alt(c, [
						c => {
							Token(c, Tokens.PLUS);
							op = BinaryOperatorType.add;
						},
						c => {
							Token(c, Tokens.MINUS);
							op = BinaryOperatorType.sub;
						},
						c => {
							Token(c, Tokens.DOUBLEAND);
							op = BinaryOperatorType.and;
						},
						c => {
							Token(c, Tokens.DOUBLEPIPE);
							op = BinaryOperatorType.or;
						}
					]);
					Node(c, TermParser, v => self = new BinaryExpression(self, op, v));
				});
			},
			c => {
				Literal(c, "[");
				Node(c, ListExpressionParser, v => self = new ArrayCreationExpression { expressions = v });
				Literal(c, "]");
				//}, c => {
				//	Node(c, IdentifierParser, v => self = v);
				//	Literal(c, "{");
				//	Node(c, ListExpressionParser, v => self = new InstanceInitializationExpression { identifier = self, expressions = v });
				//	Literal(c, "}");
			},
			c => {
				Token(c, Tokens.FNC);
				Literal(c, "(");
				Node(c, ParameterDeclListParser, v => self = new FunctionDeclarationExpression { parameters = v });
				Literal(c, ")");
				Node(c, TypeDeclarationParser, v => ((FunctionDeclarationExpression)self).typeDeclarationStatement = v);
				Literal(c, "{");
				Repeat(c, c => Node(c, StatementParser, v => ((FunctionDeclarationExpression)self).statements.Add(v)));
				Literal(c, "}");
			}
		]);
	});

	private static readonly Parser<IExpression> TermParser = new((c, self) => {
		Node(c, FactorParser, v => self = v);
		Repeat(c, c => {
			BinaryOperatorType op = BinaryOperatorType.none;
			Alt(c, [
				c => {
					Token(c, Tokens.STAR);
					op = BinaryOperatorType.mul;
				},
				c => {
					Token(c, Tokens.SLASH);
					op = BinaryOperatorType.div;
				},
				c => {
					Token(c, Tokens.PERCENT);
					op = BinaryOperatorType.mod;
				},
				c => {
					Token(c, Tokens.DOUBLEEQUAL);
					op = BinaryOperatorType.equals;
				},
				c => {
					Token(c, Tokens.NOTEQUAL);
					op = BinaryOperatorType.notEquals;
				},
				c => {
					Token(c, Tokens.LESS);
					op = BinaryOperatorType.less;
				},
				c => {
					Token(c, Tokens.GREATER);
					op = BinaryOperatorType.greater;
				},
				c => {
					Token(c, Tokens.LESSEQUAL);
					op = BinaryOperatorType.lessEquals;
				},
				c => {
					Token(c, Tokens.GREATEREQUALS);
					op = BinaryOperatorType.greaterEquals;
				}
			]);
			Node(c, FactorParser, v => self = new BinaryExpression(self, op, v));
		});
	});

	private static readonly Parser<IExpression> FactorParser = new((c, self) => {
		Node(c, UnaryParser, v => self = v);
		Repeat(c, c => {
			Token(c, Tokens.DOUBLESTAR);
			Node(c, UnaryParser, v => self = new BinaryExpression(self, BinaryOperatorType.pow, v));
		});
	});

	private static readonly Parser<IExpression> UnaryParser = new((c, self) => {
		Opt(c, c => {
			Alt(c, [
				c => Token(c, Tokens.DOUBLEPLUS),
				c => Token(c, Tokens.DOUBLEMINUS),
				c => Token(c, Tokens.EXLEMATIONMARK)
			]);
		});
		Node(c, BaseParser, v => self = v);
		Opt(c, c => {
			Alt(c,[
					c => Token(c, Tokens.DOUBLEPLUS),
					c => Token(c, Tokens.DOUBLEMINUS)
				]
			);
		});
	});

	private static readonly Parser<IExpression> BaseParser = new((c, self) => {
		Alt(c, [
			c => {
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
				Literal(c, "(");
				Node(c, ExpressionParser, v => self = v);
				Literal(c, ")");
			}, c => {
				Node(c, ArrayCreationParser, v => self = v);
			}
		]);
	});

	private static readonly Parser<IExpression> LiteralParser = new((c, self) => {
		Alt(c, [
			c => {
				Token(c, Tokens.NUMBER, t => {
					self = new NumberExpression {
						number = new Func<INumber>(() => {
							if (t.Contains('.')) {
								return new FloatExpression(t);
							}

							return new IntegerExpression(t);
						}).Invoke()
					};
				});
			}, c => {
				Token(c, Tokens.STRING, t => {
					self = new StringExpression { str = t };
				});
			}, c => {
				Token(c, Tokens.TRUE, t => {
					self = new BooleanExpression { value = true };
				});
			}, c => {
				Token(c, Tokens.FALSE, t => {
					self = new BooleanExpression { value = false };
				});
			}
		]);
	});

	private static readonly Parser<IExpression> ArrayIndexingParser = new((c, self) => {
		Node(c, IdentifierParser, v => self = v);
		Literal(c, "[");
		Node(c, ExpressionParser, v => self = new ArrayIndexingExpression { identifier = self, index = v });
		Literal(c, "]");
	});

	private static readonly Parser<IExpression> FunctionCallParser = new((c, self) => {
		Node(c, IdentifierParser, v => self = v);
		Literal(c, "(");
		Opt(c, c => {
			Node(c, ExpressionListParser, v => self = new FunctionCallExpression { identifier = self, parameters = v });
		});
		Literal(c, ")");
	});

	private static readonly Parser<IExpression> TypeConversionParser = new((c, self) => {
		Literal(c, "(");
		var typeId = null as IdentifierExpression;
		Node(c, IdentifierParser, v => typeId = v);
		Literal(c, ")");
		Node(c, ExpressionParser, v => self = new TypeConversionExpression { identifier = typeId, exp = v });
	});

	private static readonly Parser<IExpression> ArrayCreationParser = new((c, self) => {
		Literal(c, "[");
		Opt(c, c => {
			Node(c, ListExpressionParser, v => self = new ArrayCreationExpression { expressions = v });
		});
		Literal(c, "]");
	});

	private static readonly Parser<List<IExpression>> ListExpressionParser = new((c, self) => {
		Repeat(c, c => {
			Node(c, ExpressionParser, v => self.Add(v));
			Opt(c, c => Literal(c, ","));
		});
	});

	private static readonly Parser<IStatement> BreakStatementParser = new((c, self) => {
		Token(c, Tokens.BREAK);
		self = new BreakContinueStatement() {
			ControlFlowModifierType = ControlFlowModifierType.@break
		};
	});

	private static readonly Parser<IStatement> ContinueStatementParser = new((c, self) => {
		Token(c, Tokens.CONTINUE);
		self = new BreakContinueStatement() {
			ControlFlowModifierType = ControlFlowModifierType.@continue
		};
	});

	// private static readonly Parser<IStatement> AssertionStatementParser = new((c, self) => {
	// 	Token(c, Tokens.AS);
	// 	Node(c, ExpressionParser, v => self = new AssertionStatement { Expression = v });
	// });

	// private static readonly Parser<IStatement> ErrorStatementParser = new((c, self) => {
	// 	Token(c, Tokens.Error);
	// 	Node(c, ExpressionParser, v => self = new ErrorStatement { Expression = v });
	// });

	private static readonly Parser<ClassDeclarationStatement> ClassDeclarationParser = new((c, self) => {
		Opt(c, c => Literal(c, Tokens.PUB, t => {
			self.isPublic = t;
		}));
		Token(c, Tokens.CLASS);
		Node(c, IdentifierParser, v => self.identifier = v);
		Literal(c, "{");
		Repeat(c, c => {
			Node(c, StatementParser, v => self.statements.Add(v));
		});
		Literal(c, "}");
	});

	private static readonly Parser<StructDeclarationStatement> StructDeclarationParser = new((c, self) => {
		Opt(c, c => Literal(c, Tokens.PUB, t => {
			self.isPublic = t;
		}));
		Token(c, Tokens.STRUCT);
		Node(c, IdentifierParser, v => self.identifier = v);
		Opt(c, c => Node(c, TypeDeclarationParser, v => self.typeDeclarationStatement = v));
		Literal(c, "{");
		Repeat(c, c => {
			Node(c, StatementParser, v => self.statements.Add(v));
		});
		Literal(c, "}");
	});

	private static readonly Parser<EnumDeclarationStatement> EnumDeclarationParser = new((c, self) => {
		Opt(c, c => Literal(c, Tokens.PUB, t => {
			self.isPublic = t;
		}));
		Token(c, Tokens.ENUM);
		Node(c, IdentifierParser, v => self.identifier = v);
		Opt(c, c => {
			Token(c, Tokens.COLON);
			Node(c, TypeDeclarationParser, v => self.typeDeclarationStatement = v);
		});
		Literal(c, "{");
		Repeat(c, c => {
			Node(c, EnumValueParser, v => self.enumValueDeclarations.Add(v));
			Opt(c, c => Token(c, Tokens.COMMA));
		});
		Literal(c, "}");
	});

	private static readonly Parser<EnumValueDeclaration> EnumValueParser = new((c, self) => {
		Node(c, IdentifierParser, v => self.identifier = v);
		Opt(c, c => {
			Token(c, Tokens.COLON);
			Node(c, ExpressionParser, v => self.exp = v);
			self.type = EnumValueType.none;
		});
	});

	private static readonly Parser<InterfaceStatement> InterfaceDeclarationParser = new((c, self) => {
		//Opt(c, c => Token(c, Tokens.PUB));
		Token(c, Tokens.INTERFACE);
		Node(c, IdentifierParser, v => self.identifier = v);
		Literal(c, "{");
		Repeat(c, c => {
			Node(c, StatementParser, v => self.statements.Add(v));
		});
		Literal(c, "}");
	});

	private static readonly Parser<UnionStatement> UnionDeclarationParser = new((c, self) => {
		//Opt(c, c => Token(c, Tokens.PUB));
		Token(c, Tokens.NULL);
		Node(c, IdentifierParser, v => self.identifier = v);
		Literal(c, "{");
		Repeat(c, c => {
			Node(c, StatementParser, v => self.statements.Add(v));
		});
		Literal(c, "}");
	});

	private static readonly Parser<List<ParameterDeclaration>> ParameterDeclListParser = new((c, self) => {
		Repeat(c, c => {
			Node(c, ParameterParser, v => self.Add(v));
			Opt(c, c => Token(c, Tokens.COMMA));
		});
	});

	private static readonly Parser<ExpressionList> ParameterListParser = new((c, self) => {
		Repeat(c, c => {
			Node(c, ParameterParser, v => self.expressions.Add(v));
			Opt(c, c => Token(c, Tokens.COMMA));
		});
	});

	private static readonly Parser<ParameterDeclaration> ParameterParser = new((c, self) => {
		Node(c, IdentifierParser, v => self.identifier = v);
		Opt(c, c => Node(c, TypeDeclarationParser, v => self.typeDeclarationStatement = v));
	});

	private static readonly Parser<FunctionCallStatement> FunctionCallStatementParser = new((c, self) => {
		Node(c, IdentifierParser, v => self.identifier = v);
		Literal(c, "(");
		Opt(c, c => Node(c, ExpressionListParser, v => self.parameters = v));
		Literal(c, ")");
	});

	private static readonly Parser<ExpressionList> ExpressionListParser = new((c, self) => {
		Repeat(c, c => {
			Node(c, ExpressionParser, v => self.expressions.Add(v));
			Opt(c, c => Token(c, Tokens.COMMA));
		});
	});
}