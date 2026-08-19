using Parseus.Parser.Implicit;

public partial class TinyScriptParser {
	// recursive descent parser for expressions, following operator precedence and associativity rules through recursion.
	public interface IExpression { }

	public class CExpression {
		public IExpression Expression;
	}
	
	public class BinaryExpression : IExpression {
		public IExpression Left;
		public string? Operator;
		public IExpression? Right;
	}

	public class UnaryExpression : IExpression {
		public string Operator;
		public IExpression Operand;
	}

	public class AtomExpression : TinyScriptParser.IExpression {
		public bool IsNull;
		public string? BoolValue;
		public string? NumberValue;
		public string? StringValue;
		public string? IdentifierValue;
		public TinyScriptParser.CExpression GroupingExpr;
	}

	private static readonly
	BaseParser.Parser<TinyScriptParser.CExpression> LogicalExpressionParser = new((c, self) => {
		Node(c, LogicalOrExpressionParser, l => { self.Expression = l; });
	});

	private static readonly
	BaseParser.Parser<TinyScriptParser.CExpression> ArithmeticExpressionParser = new((c, self) => {
		Node(c, AdditionExpressionParser, s => { self.Expression = s; });
	});
	
	private static readonly
	BaseParser.Parser<BinaryExpression> LogicalOrExpressionParser = new((c, self) => {
		//Node(c, LogicalAndExpressionParser, l => { self.Left = l; });
		Node(c, LogicalAndExpressionParser, l => { self.Left = l; });
		RepeatOpt(c, c => {
			Token(c, Tokens.OR, t => { self.Operator = t; });
			//Node(c, LogicalAndExpressionParser, r => { self.Right = r; });
			Node(c, LogicalOrExpressionParser, r => { self.Right = r; });
		});
	});
	private static readonly
	BaseParser.Parser<BinaryExpression> LogicalAndExpressionParser = new((c, self) => {
		//Node(c, LogicalEqualExpressionParser, out var m);
		Node(c, LogicalEqualExpressionParser, l => { self.Left = l; });
		RepeatOpt(c, c => {
			Token(c, Tokens.AND, t => { self.Operator = t; });
			Node(c, LogicalAndExpressionParser, r => { self.Right = r; });
			//Node(c, LogicalEqualExpressionParser, a => {  });
			Console.Write("");
		});
	});
	private static readonly
	BaseParser.Parser<BinaryExpression> LogicalEqualExpressionParser = new((c, self) => {
		Node(c, LogicalComparisionExpressionParser, l => { self.Left = l; });
		RepeatOpt(c, c => {
			Token(c, Tokens.EQL, t => { self.Operator = t; });
			Node(c, LogicalEqualExpressionParser, r => { self.Right = r; });
		});
	});
	private static readonly
	BaseParser.Parser<BinaryExpression> LogicalComparisionExpressionParser = new((c, self) => {
		Node(c, AdditionExpressionParser, l => { self.Left = l; });
		RepeatOpt(c, c => {
			Alt(c,
				c => { Token(c, Tokens.LEQ, t => { self.Operator = t; } ); },
				c => { Token(c, Tokens.LSS, t => { self.Operator = t; } ); },
				c => { Token(c, Tokens.GTR, t => { self.Operator = t; } ); },
				c => { Token(c, Tokens.GEQ, t => { self.Operator = t; } ); },
				c => { Token(c, Tokens.NEQ, t => { self.Operator = t; } ); },
				c => { Token(c, Tokens.EQL, t => { self.Operator = t; } ); }
			);
			//Node(c, LogicalComparisionExpressionParser, r => {});
			Node(c, LogicalComparisionExpressionParser, r => { self.Right = r; });
		});
	});

	private static readonly
	BaseParser.Parser<BinaryExpression> AdditionExpressionParser = new((c, self) => {
		Node(c, MultiplicationExpressionParser, l => { self.Left = l; });
		RepeatOpt(c, c => {
			Alt(c, c => {
				Token(c, Tokens.PLUS, t => { self.Operator = t; });
			}, c => {
				Token(c, Tokens.MINUS, t => { self.Operator = t; });
			});
			Node(c, AdditionExpressionParser, r => { self.Right = r; });
		});
	});
	private static readonly
	BaseParser.Parser<BinaryExpression> MultiplicationExpressionParser = new((c, self) => {
		Node(c, ExponentialExpressionParser, l => { self.Left = l; });
		RepeatOpt(c, c => {
			Alt(c, c => {
				Token(c, Tokens.STAR, t => { self.Operator = t; });
			}, c => {
				Token(c, Tokens.SLASH, t => { self.Operator = t; });
			});
			Node(c, MultiplicationExpressionParser, r => { self.Right = r; });
		});
	});
	
	private static readonly
	BaseParser.Parser<BinaryExpression> ExponentialExpressionParser = new((c, self) => {
		Node(c, UnaryExpressionParser, l => { self.Left = l; });
		RepeatOpt(c, c => {
			Token(c, Tokens.POW, t => { self.Operator = t; });
			Node(c, ExponentialExpressionParser, r => { self.Right = r; });
		});
	});

	private static readonly
	BaseParser.Parser<UnaryExpression> UnaryExpressionParser = new((c, self) => {
		Opt(c, c => {
			Token(c, Tokens.NOT, t => { self.Operator = t; });
		});
		Node(c, AtomParser, l => { self.Operand = l; });
	});

	private static readonly
	BaseParser.Parser<AtomExpression> AtomParser = new((c, self) => {
		Alt(c, c => {
			Literal(c, Tokens.NULL, t => { self.IsNull = t; });
		}, c => {
			Token(c, Tokens.TRUE, t => { self.BoolValue = t; });
		}, c => {
			Token(c, Tokens.FALSE, t => { self.BoolValue = t; });
		}, c => {
			Token(c, Tokens.NUMBER, t => { self.NumberValue = t; });
		}, c => {
			Token(c, Tokens.STRING, t => { self.StringValue = t; });
		}, c => {
			Token(c, Tokens.IDENTIFIER, t => { self.IdentifierValue = t; });
		}, c => {
			// ... '(' LogicalExpression ')' ...
			Token(c, Tokens.OPENPAREN);
			Node(c, LogicalExpressionParser, e => { self.GroupingExpr = e; });
			Token(c, Tokens.CLOSEPAREN);
		}/*, c => {
			// ... '{' 
			// SET
		}*/);
	});

	// math set
	public class MathSetExpression;
	private static readonly
	BaseParser.Parser<MathSetExpression> MathSetExpressionParser = new((c, self) => { });
}