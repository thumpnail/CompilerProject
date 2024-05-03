using static NanoScript.Program.Token;

namespace NanoScript.Parser;

public partial class Parser {
    private ModuleStatement ParseModuleStatement() {
        ctx.CreateFrame();
        var res = new ModuleStatement();
        if (ctx.Consume_tk(NanoScript.Program.Token.MOD)) {
            res.moduleName = ParseIdentifierExpression();
            res.importStatements = ParseImportStatements();
            if (ctx.Peek_tk(NanoScript.Program.Token.LEFTBRACE)) {
                ctx.Consume_tk(NanoScript.Program.Token.LEFTBRACE);
                res.statements = ParseStatements();
                ctx.Consume_tk(NanoScript.Program.Token.RIGHTBRACE);
            } else {
                res.statements = ParseStatements();
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private List<ModuleStatement> ParseModuleStatements() {
        var res = new List<ModuleStatement>();
        ModuleStatement tmp;
        while ((tmp = ParseModuleStatement()) != null) {
            res.Add(tmp);
        }
        return res;
    }
    private List<Statement> ParseStatements(NanoScript.Program.Token delimiter = NONE) {
        var res = new List<Statement>();
        if (delimiter == NONE) {
            Statement tmp;
            while ((tmp = ParseStatement()) != null) {
                res.Add(tmp);
            }
        } else {
            Statement tmp;
            while (!ctx.Peek_tk(delimiter) && (tmp = ParseStatement()) != null) {
                res.Add(tmp);
            }
        }
        return res;
    }
    private List<ImportStatement> ParseImportStatements() {
        var res = new List<ImportStatement>();
        ImportStatement tmp;
        while ((tmp = ParseImportStatement()) != null) {
            res.Add(tmp);
        }
        return res;
    }
    private ImportStatement ParseImportStatement() {
        ctx.CreateFrame();
        var res = new ImportStatement();
        if (ctx.Consume_tk(NanoScript.Program.Token.IMPORT)) {
            res.importString = ctx.Consume();
            //TODO: Add from functionality
            if (ctx.Peek_tk(NanoScript.Program.Token.AS)) {
                res.isAs = ctx.Consume_tk(NanoScript.Program.Token.AS);
                res.identifier = res.isAs ? ParseIdentifierExpression() : null;
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private Statement ParseStatement() {
        switch (ctx.Peek_tk()) {
            case NanoScript.Program.Token.PUB:
                //TODO: add keywordless varible statement
                switch (ctx.PeekNext_tk()) {
                    case NanoScript.Program.Token.VAR:
                    case NanoScript.Program.Token.CONST:
                    case NanoScript.Program.Token.LET:
                        return ParseVariableDeclarationStatement();
                    case NanoScript.Program.Token.CLASS:
                        return ParseClassDeclarationStatement();
                    case NanoScript.Program.Token.ENUM:
                        return ParseEnumDeclarationStatement();
                    case NanoScript.Program.Token.STRUCT:
                        return ParseStructDeclarationStatement();
                    case NanoScript.Program.Token.INTERFACE:
                        return ParseInterfaceStatement();
                    case NanoScript.Program.Token.UNION:
                        return ParseUnionStatement();
                    case NanoScript.Program.Token.FNC:
                        return ParseFunctionDeclarationStatement();
                    default:
                        Console.WriteLine($"Parser ERROR[0/2]: {ctx.Peek_tk()}:{ctx.Peek()}, {ctx.PeekNext_tk()}:{ctx.PeekNext()}");
                        return null;
                }
                break;
            case NanoScript.Program.Token.VAR:
            case NanoScript.Program.Token.CONST:
            case NanoScript.Program.Token.LET:
                return ParseVariableDeclarationStatement();
            case NanoScript.Program.Token.CLASS:
                return ParseClassDeclarationStatement();
            case NanoScript.Program.Token.ENUM:
                return ParseEnumDeclarationStatement();
            case NanoScript.Program.Token.UNION:
                return ParseUnionStatement();
            case NanoScript.Program.Token.FNC:
                return ParseFunctionDeclarationStatement();
            case NanoScript.Program.Token.DOT:
                switch (ctx.PeekNext_tk()) {
                    case NanoScript.Program.Token.IDENTIFIER:
                        switch (ctx.PeekNext_tk(2)) {
                            case NanoScript.Program.Token.EQUAL:
                                return ParseAssignmentStatement();
                            case NanoScript.Program.Token.LEFTPAREN:
                                return ParseFunctionCallStatement();
                            default: 
                                Console.WriteLine($"Parser ERROR[1/3]: {ctx.Peek_tk()}:{ctx.Peek()}, {ctx.PeekNext_tk()}:{ctx.PeekNext()}, {ctx.PeekNext_tk(2)}:{ctx.PeekNext(2)}");
                                return null;
                        }
                    default:
                        Console.WriteLine($"Parser ERROR[1/2]: {ctx.Peek_tk()}:{ctx.Peek()}, {ctx.PeekNext_tk()}:{ctx.PeekNext()}");
                        return null;
                }
            case NanoScript.Program.Token.IDENTIFIER:
                switch (ctx.PeekNext_tk()) {
                    case NanoScript.Program.Token.EQUAL:
                        return ParseAssignmentStatement();
                    case NanoScript.Program.Token.LEFTPAREN:
                        return ParseFunctionCallStatement();
                    default: 
                        Console.WriteLine($"Parser ERROR[2/2]: {ctx.Peek_tk()}:{ctx.Peek()}, {ctx.PeekNext_tk()}:{ctx.PeekNext()}");
                        return null;
                }
            case NanoScript.Program.Token.RETURN:
                return ParseReturnStatement();
            case NanoScript.Program.Token.IF:
            //case NanoScript.Program.Token.ELSE:
                return ParseConditionalStatement();
            case NanoScript.Program.Token.FOR:
                return ParseForStatement();
            case NanoScript.Program.Token.BREAK:
            case NanoScript.Program.Token.CONTINUE:
                return ParseBreakContinueStatement();
            case NanoScript.Program.Token.DEF:
            case NanoScript.Program.Token.ASSERT:
                return ParseAssertionStatement();
            case NanoScript.Program.Token.ERROR:
                return ParseErrorStatement();
            case NanoScript.Program.Token.SWITCH:
                return ParseSwitchStatement();
            case NanoScript.Program.Token.STRUCT:
                return ParseStructDeclarationStatement();
            case NanoScript.Program.Token.INTERFACE:
                return ParseInterfaceStatement();
            case NanoScript.Program.Token.DOUBLEPLUS:
            case NanoScript.Program.Token.DOUBLEMINUS:
            default:
                Console.WriteLine($"Parser ERROR[3/1]: {ctx.Peek_tk()}:{ctx.Peek_tk()}");
                return null;
        }
        return null;
    }
    private VariableDeclarationStatement? ParseVariableDeclarationStatement() {
        ctx.CreateFrame();
        var res = new VariableDeclarationStatement();
        if (ctx.Peek_tk(NanoScript.Program.Token.LET) || ctx.Peek_tk(NanoScript.Program.Token.VAR) ||
            ctx.Peek_tk(NanoScript.Program.Token.CONST)) {
            res.prefix = ctx.Consume();
            if (ctx.Consume_tk(NanoScript.Program.Token.DOT)) {
                res.isSelf = true;
                res.identifier = ParseIdentifierExpression();
            } else {
                res.identifier = ParseIdentifierExpression();
            }
            if (ctx.Peek_tk(NanoScript.Program.Token.COLON))
                res.typeDeclarationStatement = ParseTypeDeclarationStatement();
            if (ctx.Peek_tk(NanoScript.Program.Token.EQUAL)) {
                res.isAssign = ctx.Consume_tk(NanoScript.Program.Token.EQUAL);
                res.exp = ParseFullExpression();
            }
        } else {
            ctx.PopFrame();
            return null;
        }

        ctx.ClearFrame();
        return res;
    }
    private StructDeclarationStatement? ParseStructDeclarationStatement() {
        ctx.CreateFrame();
        StructDeclarationStatement res = new();
        TypeDeclarationStatement type;
        if (ctx.Consume_tk(PUB))
            res.isPublic = true;
        if (ctx.Consume_tk(STRUCT)) {
            var ident = ParseIdentifierExpression();
            if ((type = ParseTypeDeclarationStatement()) is not null)
                res.typeDeclarationStatement = type;
            else type = null;
            ctx.Consume_tk(LEFTBRACE);
            res.typeDeclarationStatement = type;
            res.identifier = ident;
            res.statements = ParseStatements();
            ctx.Consume_tk(RIGHTBRACE);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private ClassDeclarationStatement? ParseClassDeclarationStatement() {
        ctx.CreateFrame();
        var isPublic = false;
        ClassDeclarationStatement res;
        if (ctx.Consume_tk(PUB)) isPublic = true;
        if (ctx.Consume_tk(CLASS)) {
            var ident = ParseIdentifierExpression();
            ctx.Consume_tk(LEFTBRACE);
            res = new ClassDeclarationStatement() {
                identifier = ident,
                statements = ParseStatements(),
                isPublic = isPublic
            };
            ctx.Consume_tk(RIGHTBRACE);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //    | 'export'? 'pub'? 'fnc' '.'? identifier'(' (<ParseParameterList>)? ')' ':' type_decl '{' statement* '}'
    private FunctionDeclarationStatement? ParseFunctionDeclarationStatement() {
        ctx.CreateFrame();
        var res = new FunctionDeclarationStatement();
        TypeDeclarationStatement returnType;
        if (ctx.Consume_tk(EXPORT)) res.isExport = true;
        if (ctx.Consume_tk(PUB)) res.isPublic = true;
        if (ctx.Consume_tk(FNC)) {
            res.identifier = ParseIdentifierExpression();
            ctx.Consume_tk(LEFTPAREN);
            res.parameters = ParseParameterDeclarationList();
            ctx.Consume_tk(RIGHTPAREN);

            if ((returnType = ParseTypeDeclarationStatement()) is not null) {
                res.returnType = returnType;
            }
            ctx.Consume_tk(LEFTBRACE);
            res.statements = ParseStatements();
            ctx.Consume_tk(RIGHTBRACE);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //(identifier type_decl? (',' identifier type_decl? )* )?
    private List<ParameterDeclaration>? ParseParameterDeclarationList() {
        ctx.CreateFrame();
        var res = new List<ParameterDeclaration>();
        if (ctx.Peek_tk(IDENTIFIER)) {
            var tmp = new ParameterDeclaration();
            while ((tmp.identifier = ParseIdentifierExpression()) != null) {
                if (ctx.Peek_tk(COLON))
                    tmp.typeDeclarationStatement = ParseTypeDeclarationStatement();
                res.Add(tmp);
                tmp = new ParameterDeclaration();
                if (ctx.Peek_tk(COMMA))
                    ctx.Consume_tk(COMMA);
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private FunctionCallStatement? ParseFunctionCallStatement() {
        ctx.CreateFrame();
        var res = new FunctionCallStatement();
        if (ctx.Peek_tk(IDENTIFIER)) {
            res.identifier = ParseIdentifierExpression();
            ctx.Consume_tk(LEFTPAREN);
            Expression exp;
            if ((exp = ParseListExpression()) is not null) {
                res.parameters = ((ExpressionList)exp).expressions;
            }
            ctx.Consume_tk(RIGHTPAREN);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private ReturnStatement? ParseReturnStatement() {
        ctx.CreateFrame();
        var res = new ReturnStatement();
        if (ctx.Peek_tk(RETURN)) {
            ctx.Consume_tk(RETURN);
            res.exp = ParseExpression();
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private EnumDeclarationStatement? ParseEnumDeclarationStatement() {
        ctx.CreateFrame();
        var res = new EnumDeclarationStatement();
        if (ctx.Consume_tk(PUB))
            res.isPublic = true;
        if (ctx.Peek_tk(ENUM)) {
            ctx.Consume_tk(ENUM);
            if (ctx.Peek_tk(IDENTIFIER))
                res.identifier = ParseIdentifierExpression();
            if (ctx.Peek_tk(COLON))
                res.typeDeclarationStatement = ParseTypeDeclarationStatement();
            ctx.Consume_tk(LEFTBRACE);
            EnumValueDeclaration tmp;
            while ((tmp = ParseEnumValueDeclaration()) is not null) {
                res.enumValueDeclarations.Add(tmp);
                if (ctx.Peek_tk(COMMA)) {
                    ctx.Consume_tk(COMMA);
                }
            }
            ctx.Consume_tk(RIGHTBRACE);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private EnumValueDeclaration? ParseEnumValueDeclaration() {
        ctx.CreateFrame();
        var res = new EnumValueDeclaration();
        if (ctx.Peek_tk(IDENTIFIER)) {
            res.identifier = ParseIdentifierExpression();
            if (ctx.Consume_tk(COLON)) {
                res.exp = ParseExpression();
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //   | '.'? identifier type_decl? ('=' exp | '<<' exp | '>>' exp | '+=' exp | '-=' exp | '*=' exp | '/=' exp)?
    private AssignmentStatement? ParseAssignmentStatement() {
        ctx.CreateFrame();
        var res = new AssignmentStatement();
        if (ctx.Peek_tk(DOT)) {
            ctx.Consume_tk(DOT);
            res.isSelf = true;
        }
        if (ctx.Peek_tk(IDENTIFIER)) {
            res.identifier = ParseIdentifierExpression();
            if (ctx.Peek_tk(COLON)) {
                res.typeDeclarationStatement = ParseTypeDeclarationStatement();
            }
            res.assignmentType = ctx.Consume_tk() switch {
                EQUAL => AssignmentType.equal,
                PLUSEQUALS => AssignmentType.add,
                MINUSEQUALS => AssignmentType.sub,
                SLASHEQUALS => AssignmentType.div,
                DOUBLELEFT => AssignmentType.push,
                DOUBLERIGHT => AssignmentType.pop,
                TIMESEQUALS => AssignmentType.mul,
                _ => throw new NotImplementedException(),
            };
            res.exp = ParseExpression();
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| 'if' exp '{' statement* '}' ('else' 'if' '{' statement* '}')* ('else' '{' statement* '}')?
    private ConditionalStatement? ParseConditionalStatement() {
        ctx.CreateFrame();
        var res = new ConditionalStatement();
        if (ctx.Peek_tk(IF)) {
            ctx.Consume_tk(IF);
            res.ifConditionalStatement = new SubConditionalStatement() { isIf = true };
            res.ifConditionalStatement.exp = ParseExpression();
            ctx.Consume_tk(LEFTBRACE);
            res.ifConditionalStatement.statements = ParseStatements(RIGHTBRACE);
            ctx.Consume_tk(RIGHTBRACE);

            if (ctx.Peek_tk(ELSE) && ctx.PeekNext_tk(IF))
                res.elseIfConditionalStatements = new();
            while (ctx.Peek_tk(ELSE) && ctx.PeekNext_tk(IF)) {
                var tmp = new SubConditionalStatement();
                ctx.Consume_tk(ELSE);
                tmp.isElse = true;
                ctx.Consume_tk(IF);
                tmp.isIf = true;
                tmp.exp = ParseExpression();
                ctx.Consume_tk(LEFTBRACE);
                tmp.statements = ParseStatements(RIGHTBRACE);
                ctx.Consume_tk(RIGHTBRACE);
                res.elseIfConditionalStatements.Add(tmp);
            }
            if (ctx.Peek_tk(ELSE)) {
                ctx.Consume_tk(ELSE);
                ctx.Consume_tk(LEFTBRACE);
                res.elseConditionalStatement.statements = ParseStatements(RIGHTBRACE);
                ctx.Consume_tk(RIGHTBRACE);
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //    | 'switch' exp '{' (identifier ':' statement* 'break'?)* 'default' ':' statement* 'break' '}'
    private SwitchStatement? ParseSwitchStatement() {
        ctx.CreateFrame();
        var res = new SwitchStatement();
        if (ctx.Peek_tk(SWITCH)) {
            ctx.Consume_tk(SWITCH);
            res.exp = ParseExpression();
            ctx.Consume_tk(LEFTBRACE);
            res.subSwitchStatements = ParseSubSwitchStatements();
            if (ctx.Peek_tk(DEFAULT)) {
                res.defSubSwitchStatement = new();
                res.defSubSwitchStatement.isDefault = true;
                ctx.Consume_tk(DEFAULT);
                ctx.Consume_tk(COLON);
                res.defSubSwitchStatement.statements = ParseStatements(BREAK);
                if (ctx.Peek_tk(BREAK)) {
                    res.defSubSwitchStatement.isBreak = true;
                    ctx.Consume_tk(BREAK);
                }
            }
            ctx.Consume_tk(RIGHTBRACE);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //(identifier ':' statement* 'break'?)*
    private SubSwitchStatement? ParseSubSwitchStatement() {
        ctx.CreateFrame();
        var res = new SubSwitchStatement();
        if (ctx.Peek_tk(IDENTIFIER)) {
            res.identifier = ParseIdentifierExpression();
            ctx.Consume_tk(COLON);
            res.statements = ParseStatements(BREAK);
            if (ctx.Peek_tk(BREAK)) {
                res.isBreak = true;
                ctx.Consume_tk(BREAK);
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    private List<SubSwitchStatement> ParseSubSwitchStatements() {
        var res = new List<SubSwitchStatement>();
        SubSwitchStatement subSwitchStatement;
        while ((subSwitchStatement = ParseSubSwitchStatement()) != null) {
            res.Add(subSwitchStatement);
            if (ctx.Peek_tk(COMMA)) {
                ctx.Consume_tk(COMMA);
            }
        }
        return res;
    }
    //| 'for' (identifier 'in' identifier | identifier '=' exp ';' exp ';' exp | exp) '{' statement* '}'
    private ForStatement? ParseForStatement() {
        ctx.CreateFrame();
        var res = new ForStatement();
        Expression tmpexpr;
        if (ctx.Peek_tk(FOR)) {
            ctx.Consume_tk(FOR);
            if (ctx.Peek_tk(IDENTIFIER) && ctx.PeekNext_tk(IN)) {
                res.type = ForType.ForIn;
                res.elementIdentifier = ParseIdentifierExpression();
                ctx.Consume_tk(IN);
                res.listIdentifier = ParseIdentifierExpression();
                ctx.Consume_tk(LEFTBRACE);
                res.statements = ParseStatements();
                ctx.Consume_tk(RIGHTBRACE);
            } else if (ctx.Peek_tk(IDENTIFIER) && ctx.PeekNext_tk(EQUAL)) {
                res.type = ForType.For;
                res.elementIdentifier = ParseIdentifierExpression();
                ctx.Consume_tk(EQUAL);
                res.exp_def = ParseExpression();
                ctx.Consume_tk(SEMICOLON);
                res.exp_cond = ParseExpression();
                ctx.Consume_tk(SEMICOLON);
                res.exp_incr = ParseExpression();
                ctx.Consume_tk(LEFTBRACE);
                res.statements = ParseStatements();
                ctx.Consume_tk(RIGHTBRACE);
            } else if ((tmpexpr = ParseExpression()) is not null) {
                res.type = ForType.While;
                res.exp_cond = tmpexpr;
                ctx.Consume_tk(LEFTBRACE);
                res.statements = ParseStatements();
                ctx.Consume_tk(RIGHTBRACE);
            } else if(ctx.Peek_tk(LEFTBRACE)) {
                ctx.Consume_tk(LEFTBRACE);
                res.statements = ParseStatements(RIGHTBRACE);
                ctx.Consume_tk(RIGHTBRACE);
            } else {
                ctx.PopFrame();
                return null;
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| 'error' exp
    private ErrorStatement? ParseErrorStatement() {
        ctx.CreateFrame();
        var res = new ErrorStatement();
        if (ctx.Peek_tk(ERROR)) {
            ctx.Consume_tk(ERROR);
            res.exp = ParseExpression();
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| 'break' | 'continue'
    private BreakContinueStatement? ParseBreakContinueStatement() {
        ctx.CreateFrame();
        var res = new BreakContinueStatement();
        if (ctx.Peek_tk(BREAK) || ctx.Peek_tk(CONTINUE)) {
            res.ControlFlowModifierType = 
                ctx.Peek_tk(BREAK) ? ControlFlowModifierType.@break : ControlFlowModifierType.@continue;
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| 'interface' identifier type_decl? '{' statement '}'
    private InterfaceStatement? ParseInterfaceStatement() {
        ctx.CreateFrame();
        var res = new InterfaceStatement();
        if (ctx.Peek_tk(INTERFACE)) {
            ctx.Consume_tk(INTERFACE);
            res.identifier = ParseIdentifierExpression();
            res.typeDeclarationStatement = ParseTypeDeclarationStatement();
            ctx.Consume_tk(LEFTBRACE);
            res.statements = ParseStatements();
            res.statements = ParseStatements(RIGHTBRACE);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| 'union' type_decl? '{' statement* '}'
    private UnionStatement? ParseUnionStatement() {
        throw new NotSupportedException();
        ctx.CreateFrame();
        var res = new UnionStatement();
        if (ctx.Peek_tk(UNION)) {
            ctx.Consume_tk(UNION);
            res.typeDeclarationStatement = ParseTypeDeclarationStatement();
            ctx.Consume_tk(LEFTBRACE);
            res.statements = ParseStatements();
            ctx.Consume_tk(RIGHTBRACE);
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| ('def'|'type') identifier ('=' exp)?
    private DeclarationStatement? ParseDeclarationStatement() {
        ctx.CreateFrame();
        var res = new DeclarationStatement();
        if (ctx.Peek_tk(DEF) || ctx.Peek_tk(TYPE)) {
            res.declarationType = ctx.Peek_tk() switch {
                DEF => DeclarationType.def,
                TYPE => DeclarationType.type,
                _ => throw new Exception("Unknown Error type", new("Error A100")),
            };
            ctx.Consume_tk(IDENTIFIER);
            if (ctx.Peek_tk(EQUAL)) {
                ctx.Consume_tk(EQUAL);
                res.exp = ParseExpression();
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| 'assert' exp
    private AssertionStatement? ParseAssertionStatement() {
        ctx.CreateFrame();
        var res = new AssertionStatement();
        if (ctx.Peek_tk(ASSERT)) {
            ctx.Consume_tk();
            res.exp = ParseExpression();
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| '::' identifier
    private LabelStatement? ParseLabelStatement() {
        ctx.CreateFrame();
        var res = new LabelStatement();
        if (ctx.Peek_tk(DOUBLECOLON)) {
            ctx.Consume_tk();
            res.identifier = ParseIdentifierExpression();
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| 'goto' identifier
    private GotoStatement? ParseGotoStatement() {
        ctx.CreateFrame();
        var res = new GotoStatement();
        if (ctx.Peek_tk(GOTO)) {
            ctx.Consume_tk(GOTO);
            res.identifier = ParseIdentifierExpression();
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
    //| 'type' identifier '=' exp
    //| ':' identifier
    private TypeDeclarationStatement? ParseTypeDeclarationStatement() {
        ctx.CreateFrame();
        var res = new TypeDeclarationStatement();
        if (ctx.Consume_tk(NanoScript.Program.Token.COLON)) {
            if (ctx.Consume_tk(NanoScript.Program.Token.LEFTBRACKET)) {
                res.typeDeclarationType = TypeDeclarationType.array;
                if (!ctx.Peek_tk(NanoScript.Program.Token.RIGHTBRACKET))
                    res.exp = ParseFullExpression();
                ctx.Consume_tk(NanoScript.Program.Token.RIGHTBRACKET);
                res.identifier = ParseIdentifierExpression();
            } else if (ctx.Consume_tk(NanoScript.Program.Token.LEFTBRACE)) {
                res.typeDeclarationType = TypeDeclarationType.table;
                ctx.Consume_tk(NanoScript.Program.Token.RIGHTBRACE);
            } else if (ctx.Consume_tk(NanoScript.Program.Token.DOUBLEBRACES)) {
                res.typeDeclarationType = TypeDeclarationType.table;
            } else {
                res.typeDeclarationType = TypeDeclarationType.value;
                res.identifier = ParseIdentifierExpression();
            }
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
}