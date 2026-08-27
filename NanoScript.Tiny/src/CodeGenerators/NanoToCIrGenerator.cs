using System.Text;

using static TinyScriptParser;

namespace TinyScript.CodeGenerators;

public class NanoToCIrGenerator {
	public string Visit() { return ""; }

	public string Visit(Script script) {
		var sb = new StringBuilder();
		foreach (var module in script.modules) {
			Console.WriteLine($"Entering Module: ${module.name}");
			Visit(module);
		}

		return sb.ToString();
	}

	public string Visit(ModuleDeclaration module) {
		var sb = new StringBuilder();
		foreach (var statement in module.Statements) {
			Visit(statement.Statement);
		}

		return sb.ToString();
	}

	private string Visit(IStatement statement) {
		switch (statement) {
			case CallStatement call: return Visit(call);
			case DefinitionStatement def: return Visit(def);
			case FunctionDefinitionStatement func: return Visit(func);
			case IfStatement @if: return Visit(@if);
			case ImportStatement import: return Visit(import);
			case ReturnStatement @return: return Visit(@return);
			case SetStatement set: return Visit(set);
			case VariableDefinitionStatement var: return Visit(var);
			case WhileStatement @while: return Visit(@while);
			default: return "";
		}
	}

	private string Visit(CallStatement CallStatement) {
		var sb = new StringBuilder();
		sb.Append('(');
		sb.Append($"call ${CallStatement.Identifier} ");

		foreach (var parameter in CallStatement.Parameters) {
			sb.Append($"(${string.Join(" ", CallStatement.Parameters.Select(x => Visit(x.Expression)))})");
		}

		sb.Append(')');
		return sb.ToString();
	}

	private string Visit(DefinitionStatement DefinitionStatement) {
		var sb = new StringBuilder();
		sb.Append($"(def ${DefinitionStatement.Identifier} ${Visit(DefinitionStatement.Value.Expression)})");
		return sb.ToString();
	}

	private string Visit(FunctionDefinitionStatement FunctionDefinitionStatement) {
		var sb = new StringBuilder();
		return sb.ToString();
	}

	private string Visit(IfStatement IfStatement) {
		var sb = new StringBuilder();
		return sb.ToString();
	}

	private string Visit(ImportStatement ImportStatement) {
		var sb = new StringBuilder();
		return sb.ToString();
	}

	private string Visit(ReturnStatement ReturnStatement) {
		var sb = new StringBuilder();
		return sb.ToString();
	}

	private string Visit(SetStatement SetStatement) {
		var sb = new StringBuilder();
		return sb.ToString();
	}

	private string Visit(VariableDefinitionStatement VariableDefinitionStatement) {
		var sb = new StringBuilder();
		return sb.ToString();
	}

	private string Visit(WhileStatement WhileStatement) {
		var sb = new StringBuilder();
		return sb.ToString();
	}

	public string Visit(IExpression expression) {
		switch (expression) {
			default: return "";
		}
	}
}