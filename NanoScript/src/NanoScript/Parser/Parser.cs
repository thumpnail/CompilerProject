using NanoScript.Parser.AstNodes;

namespace NanoScript.Parser;

public partial class Parser {
    public ParserContext ctx;
    public Parser(ParserContext ctx) {
        this.ctx = ctx;
    }
    // program: module_statement+;
    public ProgramStatement Parse() {
        var res = new ProgramStatement();
        //TODO: Some initioalization
        res.moduleStatements = ParseModuleStatements();
        return res;
    }
}