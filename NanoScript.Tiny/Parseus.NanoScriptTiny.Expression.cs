public partial class TinyScriptParser {
	public interface IExpression {
		string print();
	}

	public class CExpression {
		public IExpression Expression;
	}
	
	public class BinaryExpression : IExpression {
		public IExpression Left;
		public string Operator;
		public IExpression Right;

		public string print() {
			return $"({Left?.print()} {Operator} {Right?.print()})".Trim();
		}
	}

	public class UnaryExpression : IExpression {
		public string Operator;
		public IExpression Operand;

		public string print() {
			return $"({Operator} {Operand?.print()})".Trim();
		}
	}

	public class LiteralExpression : IExpression {
		public bool IsNull;
		public string BoolValue;
		public string NumberValue;
		public string StringValue;
		public string IdentifierValue;
		public string print() {
			return IsNull ? "null" :
				BoolValue != null ? BoolValue :
				NumberValue != null ? NumberValue :
				StringValue != null ? $"\"{StringValue}\"" :
				IdentifierValue != null ? IdentifierValue :
				"";
		}
	}

	private static readonly Parser<CExpression> LogicalExpressionParser = new((c, self) => {
		Node(c, LogicalOrExpressionParser, l => { self.Expression = l; });
	});

	private static readonly Parser<CExpression> ArithmeticExpressionParser = new((c, self) => {
		Node(c, AdditionExpressionParser, s => { self.Expression = s; });
	});
	
	private static readonly Parser<BinaryExpression> LogicalOrExpressionParser = new((c, self) => {
		IExpression acc = null;
		Node(c, LogicalAndExpressionParser, l => { acc = l; });
		RepeatOpt(c, c => {
			string op = null!;
			Token(c, Tokens.OR, out op);
			Node(c, LogicalAndExpressionParser, r => {
				var node = new BinaryExpression();
				node.Left = acc;
				node.Operator = op;
				node.Right = r;
				acc = node;
			});
		});
		// copy acc into self
		if (acc is BinaryExpression be) {
			self.Left = be.Left;
			self.Operator = be.Operator;
			self.Right = be.Right;
		} else {
			self.Left = acc;
			self.Operator = null;
			self.Right = null;
		}
	});
	private static readonly Parser<BinaryExpression> LogicalAndExpressionParser = new((c, self) => {
		IExpression acc = null;
		Node(c, LogicalEqualExpressionParser, l => { acc = l; });
		RepeatOpt(c, c => {
			string op = null!;
			Token(c, Tokens.AND, out op);
			Node(c, LogicalEqualExpressionParser, r => {
				var node = new BinaryExpression();
				node.Left = acc;
				node.Operator = op;
				node.Right = r;
				acc = node;
			});
		});
		if (acc is BinaryExpression be) {
			self.Left = be.Left;
			self.Operator = be.Operator;
			self.Right = be.Right;
		} else {
			self.Left = acc;
			self.Operator = null;
			self.Right = null;
		}
	});
	private static readonly Parser<BinaryExpression> LogicalEqualExpressionParser = new((c, self) => {
		IExpression acc = null;
		Node(c, LogicalComparisionExpressionParser, l => { acc = l; });
		RepeatOpt(c, c => {
			string op = null!;
			Token(c, Tokens.EQL, out op);
			Node(c, LogicalComparisionExpressionParser, r => {
				var node = new BinaryExpression();
				node.Left = acc;
				node.Operator = op;
				node.Right = r;
				acc = node;
			});
		});
		if (acc is BinaryExpression be) {
			self.Left = be.Left;
			self.Operator = be.Operator;
			self.Right = be.Right;
		} else {
			self.Left = acc;
			self.Operator = null;
			self.Right = null;
		}
	});
	private static readonly Parser<BinaryExpression> LogicalComparisionExpressionParser = new((c, self) => {
		IExpression acc = null;
		Node(c, AdditionExpressionParser, l => { acc = l; });
		RepeatOpt(c, c => {
			string op = null!;
			Alt(c,
				c => { Token(c, Tokens.LEQ, out op); }, 
				c => { Token(c, Tokens.LSS, out op); }, 
				c => { Token(c, Tokens.GTR, out op); }, 
				c => { Token(c, Tokens.GEQ, out op); }, 
				c => { Token(c, Tokens.NEQ, out op); }, 
				c => { Token(c, Tokens.EQL, out op); }
			);
			Node(c, LogicalComparisionExpressionParser, r => {
				var node = new BinaryExpression();
				node.Left = acc;
				node.Operator = op;
				node.Right = r;
				acc = node;
			});
		});
		if (acc is BinaryExpression be) {
			self.Left = be.Left;
			self.Operator = be.Operator;
			self.Right = be.Right;
		} else {
			self.Left = acc;
			self.Operator = null;
			self.Right = null;
		}
	});

	private static readonly Parser<BinaryExpression> AdditionExpressionParser = new((c, self) => {
		IExpression acc = null;
		Node(c, MultiplicationExpressionParser, l => { acc = l; });
		RepeatOpt(c, c => {
			string op = null!;
			Alt(c, c => { Token(c, Tokens.PLUS, out op); }, c => { Token(c, Tokens.MINUS, out op); });
			Node(c, AdditionExpressionParser, r => {
				var node = new BinaryExpression();
				node.Left = acc;
				node.Operator = op;
				node.Right = r;
				acc = node;
			});
		});
		if (acc is BinaryExpression be) {
			self.Left = be.Left;
			self.Operator = be.Operator;
			self.Right = be.Right;
		} else {
			self.Left = acc;
			self.Operator = null;
			self.Right = null;
		}
	});
	private static readonly Parser<BinaryExpression> MultiplicationExpressionParser = new((c, self) => {
		IExpression acc = null;
		Node(c, UnaryExpressionParser, l => { acc = l; });
		RepeatOpt(c, c => {
			string op = null!;
			Alt(c, c => { Token(c, Tokens.STAR, out op); }, c => { Token(c, Tokens.SLASH, out op); });
			Node(c, MultiplicationExpressionParser, r => {
				var node = new BinaryExpression();
				node.Left = acc;
				node.Operator = op;
				node.Right = r;
				acc = node;
			});
		});
		if (acc is BinaryExpression be) {
			self.Left = be.Left;
			self.Operator = be.Operator;
			self.Right = be.Right;
		} else {
			self.Left = acc;
			self.Operator = null;
			self.Right = null;
		}
	});

	private static readonly Parser<UnaryExpression> UnaryExpressionParser = new((c, self) => {
		Opt(c, c => {
			Token(c, Tokens.NOT, out self.Operator);
		});
		Node(c, LiteralParser, l => { self.Operand = l; });
	});

	private static readonly Parser<LiteralExpression> LiteralParser = new((c, self) => {
		Alt(c, c => {
			Literal(c, Tokens.NULL, out self.IsNull);
		}, c => {
			Token(c, Tokens.TRUE, out self.BoolValue);
		}, c => {
			Token(c, Tokens.FALSE, out self.BoolValue);
		}, c => {
			Token(c, Tokens.NUMBER, out self.NumberValue);
		}, c => {
			Token(c, Tokens.STRING, out self.StringValue);
		}, c => {
			Token(c, Tokens.IDENTIFIER, out self.IdentifierValue);
		});
	});
}