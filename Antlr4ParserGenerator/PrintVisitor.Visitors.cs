using System.Text;
using Antlr4Ast;

public partial class GenVisitor {

    public override void Visit(Rule rule) {
        // Implement the visit behavior for Rule here

        Builder.AppendLine($"[Rule] -> {rule.Name}");
        if (!rule.IsLexer)
            gen.BeginFunction("public", "void", $"parse_{rule.Name}", new() {});
        else
            gen.Const("int", $"tk_{rule.Name}", tk_counter++.ToString());
    	Visit(rule.AlternativeList);
        if (!rule.IsLexer)
            gen.EndFunction();
    }

    public override void Visit(AlternativeList alternativeList) {
        // Implement the visit behavior for AlternativeList here
        Builder.AppendLine($"{get_c_indent}[AlternativeList] -> {alternativeList.Items.ToString()}");
        for (int i = 0; i < alternativeList.Items.Count; i++) {
            var node = alternativeList.Items[i];
            if(i == 0)
                gen.BeginIf($"{node.Children().First()}");
            else
                gen.BeginElseIf($"{node.Children().First()}");
            Visit(node);
            if (i == alternativeList.Items.Count - 1)
                gen.EndIf();
        }
    }

    public override void Visit(Alternative alternative) {
        // Implement the visit behavior for Alternative here
        Builder.AppendLine($"{get_c_indent}[Alternative] -> {alternative.ToString()}");
        current_indet++;
        //TODO: Alternative checks
        VisitChilds(alternative.Children());
        //TODO: Closing Alternative Checks
        current_indet--;
    }

    public override void Visit(Block block) {
        // Implement the visit behavior for Block here
        Builder.AppendLine($"{get_c_indent}[Block] -> {block.ToString()}");
        switch (block.Suffix) {
        case SuffixKind.None:     // just a group
            gen.BeginIf($"{SuffixKind.None}");
            VisitChilds(block.Children());
            gen.EndIf();
            break;
        case SuffixKind.Star:     // repeat 0-n
            gen.BeginWhile($"true");
            VisitChilds(block.Children());
            gen.EndWhile();
            break;
        case SuffixKind.Plus:     // repeat 1-n
            gen.BeginDoWhile();
            VisitChilds(block.Children());
            gen.EndDoWhile($"true");
            break;
        case SuffixKind.Optional: // 0-1
            gen.BeginIf($"{SuffixKind.None}");
            VisitChilds(block.Children());
            gen.EndIf();
            break;
        default:
            // throw new NotImplementedException();
            break;
        }
    }
	
	// Literals(No Child Execution)
    public override void Visit(Literal literal) {
        // Implement the visit behavior for Literal here
        Builder.AppendLine($"{get_c_indent}[Literal] -> {literal.ToString()}");
		gen.Call("ctx.Consume", null, $"\"{literal.Text}\"");
    }

    public override void Visit(RuleRef ruleRef) {
        // Implement the visit behavior for RuleRef here
        Builder.AppendLine($"{get_c_indent}[RuleRef] -> {ruleRef.ToString()}");
        switch (ruleRef.Suffix) {
        case SuffixKind.None:     // just a group
            gen.BeginIf($"{SuffixKind.None}");
            gen.Call($"parse_{ruleRef.Name}");
            gen.EndIf();
            break;
        case SuffixKind.Star:     // repeat 0-n
            gen.BeginWhile($"true");
            gen.Call($"parse_{ruleRef.Name}");
            gen.EndWhile();
            break;
        case SuffixKind.Plus:     // repeat 1-n
            gen.BeginDoWhile();
            gen.Call($"parse_{ruleRef.Name}");
            gen.EndDoWhile($"true");
            break;
        case SuffixKind.Optional: // 0-1
            gen.BeginIf($"{SuffixKind.None}");
            gen.Call($"parse_{ruleRef.Name}");
            gen.EndIf();
            break;
        default:
            // throw new NotImplementedException();
            break;
        }
    }

    public override void Visit(TokenRef tokenRef) {
        // Implement the visit behavior for TokenRef here
        Builder.AppendLine($"{get_c_indent}[TokenRef] -> {tokenRef.ToString()}");
		
        switch (tokenRef.Suffix) {
        case SuffixKind.None:     // just a group
            gen.BeginIf($"{SuffixKind.None}");
            gen.Call("ctx.Consume", null, $"t_{tokenRef.Name}");
            gen.EndIf();
            break;
        case SuffixKind.Star:     // repeat 0-n
            gen.BeginWhile($"true");
            gen.Call("ctx.Consume", null, $"t_{tokenRef.Name}");
            gen.EndWhile();
            break;
        case SuffixKind.Plus:     // repeat 1-n
            gen.BeginDoWhile();
            gen.Call("ctx.Consume", null, $"t_{tokenRef.Name}");
            gen.EndDoWhile($"true");
            break;
        case SuffixKind.Optional: // 0-1
            gen.BeginIf($"{SuffixKind.None}");
            gen.Call("ctx.Consume", null, $"t_{tokenRef.Name}");
            gen.EndIf();
            break;
        default:
            // throw new NotImplementedException();
            break;
        }
    }

    public override void Visit(LexerCharSet lexerCharSet) {
        // Implement the visit behavior for LexerCharSet here
        Builder.AppendLine($"{get_c_indent}[LexerCharSet] -> {lexerCharSet.ToString()}");
		gen.Define("// generate .child(...) for lexer", $"{lexerCharSet}");
    }
}