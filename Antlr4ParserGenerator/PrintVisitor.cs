using Antlr4Ast;
using System.Text;

partial class GenVisitor : GrammarVisitor {
    private int tk_counter;
    public GenVisitor(Grammar grammar) {
        Builder = new StringBuilder();
        this.grammar = grammar;
        rules = grammar.GetAllRules();
    }
    private string grname {
        get { return grammar.Name; }
    }
    private Grammar grammar;
    private IEnumerable<Rule> rules;
    public StringBuilder Builder { get; }
    public CodeGen gen = new();

    private int current_indet = 0;
    private string get_c_indent {
        get {
            var tmp = "";
            for (int i = 0; i < current_indet; i++) {
                tmp += "\t";
            }
            return tmp;
        }
        set {
            var tmp = "";
            for (int i = 0; i < current_indet; i++) {
                tmp += "\t";
            }
            this.get_c_indent = tmp;
        }
    }

    private void VisitChilds(IEnumerable<SyntaxNode> childs) {
        foreach (var node in childs) {
            switch (node.GetType().Name) {
            case nameof(TokenSpecList):
                Visit((TokenSpecList)node);
                break;
            case nameof(TokenRef):
                Visit((TokenRef)node);
                break;
            case nameof(LexerBlock):
                Visit((LexerBlock)node);
                break;
            case nameof(RuleRef):
                Visit((RuleRef)node);
                break;
            case nameof(OptionSpec):
                Visit((OptionSpec)node);
                break;
            case nameof(OptionSpecList):
                Visit((OptionSpecList)node);
                break;
            case nameof(Literal):
                Visit((Literal)node);
                break;
            case nameof(LexerMode):
                Visit((LexerMode)node);
                break;
            case nameof(LexerCommand):
                Visit((LexerCommand)node);
                break;
            case nameof(LexerCommandList):
                Visit((LexerCommandList)node);
                break;
            case nameof(LexerCharSet):
                Visit((LexerCharSet)node);
                break;
            case nameof(ImportSpec):
                Visit((ImportSpec)node);
                break;
            case nameof(ImportNameSpec):
                Visit((ImportNameSpec)node);
                break;
            case nameof(Grammar):
                Visit((Grammar)node);
                break;
            case nameof(EmptyElement):
                Visit((EmptyElement)node);
                break;
            case nameof(ElementOption):
                Visit((ElementOption)node);
                break;
            case nameof(ElementOptionList):
                Visit((ElementOptionList)node);
                break;
            case nameof(DotElement):
                Visit((DotElement)node);
                break;
            case nameof(CharRange):
                Visit((CharRange)node);
                break;
            case nameof(ChannelList):
                Visit((ChannelList)node);
                break;
            case nameof(Block):
                Visit((Block)node);
                break;
            case nameof(Alternative):
                Visit((Alternative)node);
                break;
            default: 
                Builder.AppendLine("==[Type] "+node.GetType());
                Visit(node);
                break;
            }
        }
    }

    internal void Generate() {
        InitHeader();
        gen.BeginClass("public", grname);
        gen.Define("ArrayReader<TokenTuple>", "ar");
        foreach (var rule in rules)
            Visit(rule);
        gen.EndClass();
    }

    internal string Build() { return gen.Build(); }
}