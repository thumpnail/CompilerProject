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
    private List<Statement> ParseStatements() {
        var res = new List<Statement>();
        Statement tmp;
        while ((tmp = ParseStatement()) != null) {
            res.Add(tmp);
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
                    default: break;
                }
                break;
            case NanoScript.Program.Token.CONST:
            case NanoScript.Program.Token.VAR:
            case NanoScript.Program.Token.LET:
                return ParseVariableDeclarationStatement();
            case NanoScript.Program.Token.NONE:
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
        throw new NotImplementedException();
    }
    private ClassDeclarationStatement? ParseClassDeclarationStatement() {
        throw new NotImplementedException();
    }
    private FunctionDeclarationStatement? ParseFunctionDeclarationStatement() {
        throw new NotImplementedException();
    }
    private FunctionCallStatement? ParseFunctionCallStatement() {
        ctx.CreateFrame();
        var res = new FunctionCallStatement();
        if (ctx.Peek_tk(NanoScript.Program.Token.IDENTIFIER)) {

        } else {
            return null;
        }
        ctx.PopFrame();
        return res;
    }
    private ReturnStatement? ParseReturnStatement() {
        throw new NotImplementedException();
    }
    private EnumDeclarationStatement? ParseEnumDeclarationStatement() {
        throw new NotImplementedException();
    }
    private AssignmentStatement? ParseAssignmentStatement() {
        throw new NotImplementedException();
    }
    private ConditionalStatement? ParseConditionalStatement() {
        throw new NotImplementedException();
    }
    private SwitchStatement? ParseSwitchStatement() {
        throw new NotImplementedException();
    }
    private ForStatement? ParseForStatement() {
        throw new NotImplementedException();
    }
    private ErrorStatement? ParseErrorStatement() {
        throw new NotImplementedException();
    }
    private BreakContinueStatement? ParseBreakContinueStatement() {
        throw new NotImplementedException();
    }
    private InterfaceStatement? ParseInterfaceStatement() {
        throw new NotImplementedException();
    }
    private UnionStatement? ParseUnionStatement() {
        throw new NotImplementedException();
    }
    private DeclarationStatement? ParseDeclarationStatement() {
        throw new NotImplementedException();
    }
    private AssertionStatement? ParseAssertionStatement() {
        throw new NotImplementedException();
    }
    private LabelStatement? ParseLabelStatement() {
        throw new NotImplementedException();
    }
    private GotoStatement? ParseGotoStatement() {
        throw new NotImplementedException();
    }
    private TypeDeclarationStatement? ParseTypeDeclarationStatement() {
        ctx.CreateFrame();
        var res = new TypeDeclarationStatement();
        if (ctx.Consume_tk(NanoScript.Program.Token.COLON)) {
            if (ctx.Consume_tk(NanoScript.Program.Token.LEFTBRACKET)) {
                res.typeDeclarationType = TypeDeclarationType.array;
                if (!ctx.Peek_tk(NanoScript.Program.Token.RIGHTBRACKET))
                    res.exp = ParseFullExpression();
                ctx.Consume_tk(NanoScript.Program.Token.RIGHTBRACKET);
                // } else if(ctx.Consume_tk(LEFTBRACE)) {
                //     res.typeDeclarationType = TypeDeclarationType.table;
                //     ctx.Consume_tk(RIGHTBRACE);
            } else {
                res.typeDeclarationType = TypeDeclarationType.value;
                res.identifier = ParseIdentifierExpression();
            }
            res.identifier = ParseIdentifierExpression();
        } else {
            ctx.PopFrame();
            return null;
        }
        ctx.ClearFrame();
        return res;
    }
}