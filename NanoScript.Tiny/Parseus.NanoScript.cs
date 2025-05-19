using NanoScript.Parser;
using Parseus.Parser.Implicit;
using NanoScript.Parser.AstNodes;
using static NanoScript.Token;
using Parseus.Parser.ObjectBased;

public class NanoScriptParser : BaseParser {
	public override ModuleStatement Parse(string src) {
		return new();
	}
	private static Parser<ModuleStatement> ModuleStatementParser = new((c, self) => {
		Token(c, MOD.ToString(), out _);
		Node(c, IdentifierExpressionParser, out self.moduleName);
		Opt(c, ctx => {
			Repeat(c, ctx => {
				Node(c, ImportStatementParser, out var importStatement);
			});
		});
		Opt(c, ctx => {
			Alt(c, c => {
					Token(c, LEFTBRACE.ToString(), out _);
					Node(c, StatementsParser, out var statements);
					Token(c, RIGHTBRACE.ToString(), out _);
					self.hasBody = true;
				},
				c => {
					Node(c, StatementsParser, out var statements);
				});
		});
	});
	private static Parser<List<ModuleStatement>> ModuleStatementsParser = new((ctx, self) => {
		Opt(ctx, ctx => {
			Repeat(ctx, ctx => {
				Node(ctx, ModuleStatementParser, statement => {
					self.Add(statement);
				});
			});
		});
	});
	private static Parser<List<IStatement>> StatementsParser = new((ctx, self) => {});
	private static Parser<List<ImportStatement>> ImportStatementsParser = new((ctx, self) => {});
	private static Parser<ImportStatement> ImportStatementParser = new((ctx, self) => {});
	//private static Parser<IStatement> StatementParser = new((ctx, self) => {});
	private static Parser<VariableDeclarationStatement> VariableDeclarationStatementParser = new((ctx, self) => {});
	private static Parser<StructDeclarationStatement> StructDeclarationStatementParser = new((ctx, self) => {});
	private static Parser<ClassDeclarationStatement> ClassDeclarationStatementParser = new((ctx, self) => {});
	private static Parser<FunctionDeclarationStatement> FunctionDeclarationStatementParser = new((ctx, self) => {});
	private static Parser<List<ParameterDeclaration>> ParameterDeclarationListParser = new((ctx, self) => {});
	private static Parser<FunctionCallStatement> FunctionCallStatementParser = new((ctx, self) => {});
	private static Parser<ReturnStatement> ReturnStatementParser = new((ctx, self) => {});
	private static Parser<EnumDeclarationStatement> EnumDeclarationStatementParser = new((ctx, self) => {});
	private static Parser<EnumValueDeclaration> EnumValueDeclarationParser = new((ctx, self) => {});
	private static Parser<AssignmentStatement> AssignmentStatementParser = new((ctx, self) => {});
	private static Parser<ConditionalStatement> ConditionalStatementParser = new((ctx, self) => {});
	private static Parser<SwitchStatement> SwitchStatementParser = new((ctx, self) => {});
	private static Parser<SubSwitchStatement> SubSwitchStatementParser = new((ctx, self) => {});
	private static Parser<List<SubSwitchStatement>> SubSwitchStatementsParser = new((ctx, self) => {});
	private static Parser<ForStatement> ForStatementParser = new((ctx, self) => {});
	private static Parser<ErrorStatement> ErrorStatementParser = new((ctx, self) => {});
	private static Parser<BreakContinueStatement> BreakContinueStatementParser = new((ctx, self) => {});
	private static Parser<InterfaceStatement> InterfaceStatementParser = new((ctx, self) => {});
	private static Parser<UnionStatement> UnionStatementParser = new((ctx, self) => {});
	private static Parser<DeclarationStatement> DeclarationStatementParser = new((ctx, self) => {});
	private static Parser<AssertionStatement> AssertionStatementParser = new((ctx, self) => {});
	private static Parser<LabelStatement> LabelStatementParser = new((ctx, self) => {});
	private static Parser<GotoStatement> GotoStatementParser = new((ctx, self) => {});
	private static Parser<TypeDeclarationStatement> TypeDeclarationStatementParser = new((ctx, self) => {});
	private static Parser<ArrayCreationExpression> ArrayCreationExpressionParser = new((ctx, self) => {});
	private static Parser<ArrayIndexingExpression> ArraylndexingExpressionParser = new((ctx, self) => {});
	//private static Parser<IExpression> BaseExpressionParser = new((ctx, self) => {});
	private static Parser<BooleanExpression> BooleanExpressionParser = new((ctx, self) => {});
	//private static Parser<IExpression> ExpressionParser = new((ctx, self) => {});
	//private static Parser<IExpression> FactorExpressionParser = new((ctx, self) => {});
	//private static Parser<IExpression> FullExpressionParser = new((ctx, self) => {});
	private static Parser<FunctionCallExpression> FunctionCallExpressionParser = new((ctx, self) => {});
	private static Parser<IdentifierExpression> IdentifierExpressionParser = new((ctx, self) => {});
	private static Parser<ExpressionList> ListExpressionParser = new((ctx, self) => {});
	//private static Parser<IExpression> LiteralExpressionParser = new((ctx, self) => {});
	private static Parser<NumberExpression> NumberExpressionParser = new((ctx, self) => {});
	private static Parser<StringExpression> StringExpressionParser = new((ctx, self) => {});
	//private static Parser<IExpression> TermExpressionParser = new((ctx, self) => {});
	private static Parser<TypeConversionExpression> TypeConversionExpressionParser = new((ctx, self) => {});
	private static Parser<UnaryExpression> UnaryExpressionParser = new((ctx, self) => {});
}