using Parseus.Parser.Implicit;
namespace NanoScript.ParserV2;

public partial class Parser : BaseParser {
	interface IStatement;
	interface IExpression;
	class Statement {
		public IStatement internalStatement;
	}
	class Expression {
		public IExpression internalExpression;
	}
	class IfStatement : IStatement {
		public List<(Expression? condition,Statement block)> conditions = new();
	}
	private static Parser<Statement> StatementParser = new((c, self) => {
		
	});
	public override object Parse(string src) {
		throw new NotImplementedException();
	}
}