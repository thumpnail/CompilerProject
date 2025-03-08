using System.Text;

using Parseus.ParserV3;
namespace TinyScript;

public class CodeGenerator : SBNFParserGenerator {
	public CodeGenerator(AstNode rootNode) {
		var rules = rootNode.Children.FindAll(x=>x.Type == "Rule");
		foreach (var child in rules) {
			BuildClass(child);
		}
	}
	void BuildClass(AstNode ruleNode) {
		//creat the parse method
		var method = BuidMethod(ruleNode.Children.ToArray());
		CreateClass(ruleNode.Value, method.props, [method.method]);
	}
	(string[] props, string method) BuidMethod(AstNode[] nodes) {
		string[] props = ["testfield1"];
		return (props,CreateMethod("Parse", []));
	}
	string[] BuidMethods(AstNode[] ruleNode) {
		var res = new List<string>();
		foreach (var rule in ruleNode) {
			var stats = BuildStatements(rule.Children.ToArray());
			res.Add(CreateMethod(rule.Value, stats));
		}
		return res.ToArray();
	}
	string[] BuildStatements(AstNode[] nodes) {
		var res = new List<string>();
		foreach (var node in nodes) {
			res.Add(BuildStatement(node, depth:1));
		}
		return res.ToArray();
	}
	string BuildStatement(AstNode node, int depth) {
		var sb = new StringBuilder();
		sb.AppendLine();
		sb.Append("(");
		sb.Append($"{node.Value}[{node.Type}] ");
		if (node.Children.Any()) {
			sb.Append("|> {");
		}
		foreach (var child in node.Children) {
			sb.Append(BuildStatement(child, depth++));
			if (node.Children.Last() == child) {
				sb.Append("}");
			}
		}
		sb.Append(")");
		return sb.ToString();
	}
	public new string Build() {
		return base.Build();
	}
}