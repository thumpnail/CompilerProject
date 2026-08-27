using System.Text;

using Parseus.Parser.Implicit;

public partial class TinyScriptParser {
	// recursive descent parser for expressions, following operator precedence and associativity rules through recursion.
	public interface IExpression : IPrintable;

	public class CExpression {
		public IExpression Expression;
	}

	public class BinaryExpression : IExpression {
		public IExpression Left;
		public string? Operator;
		public IExpression? Right;

		public string Print() {
			var sb = new StringBuilder();
			if (Right is null) {
				sb.Append($"{Left.Print()}");
			} else {
				sb.Append($"{Left.Print()} {Operator} {Right.Print()}");
			}

			return sb.ToString();
		}
	}

	public class UnaryExpression : IExpression {
		public string Operator;
		public IExpression Operand;

		public string Print() {
			var sb = new StringBuilder();
			sb.Append($"{Operator}{Operand.Print()}");
			return sb.ToString();
		}
	}

	public class AtomExpression : IExpression {
		public bool IsNull;
		public string? BoolValue;
		public string? NumberValue;
		public string? StringValue;
		public string? IdentifierValue;
		public CExpression GroupingExpr;

		public string Print() {
			var sb = new StringBuilder();
			if (IsNull) {
				sb.Append("null");
			} else {
				sb.Append(BoolValue ??
				          NumberValue ?? StringValue ?? IdentifierValue ?? $"({GroupingExpr.Expression.Print()})");
			}

			return sb.ToString();
		}
	}

	private static readonly
		Parser<CExpression> LogicalExpressionParser = new((c, self) => {
			Node(c, LogicalOrExpressionParser, l => { self.Expression = l; });
		});

	private static readonly
		Parser<BinaryExpression> LogicalOrExpressionParser = new((c, self) => {
			//Node(c, LogicalAndExpressionParser, l => { self.Left = l; });
			Node(c, LogicalAndExpressionParser, l => { self.Left = l; });
			RepeatOpt(c, c => {
				Token(c, Tokens.OR, t => { self.Operator = t; });
				//Node(c, LogicalAndExpressionParser, r => { self.Right = r; });
				Node(c, LogicalAndExpressionParser, r => { self.Right = r; });
			});
		});

	private static readonly
		Parser<BinaryExpression> LogicalAndExpressionParser = new((c, self) => {
			//Node(c, LogicalEqualExpressionParser, out var m);
			Node(c, LogicalEqualExpressionParser, l => { self.Left = l; });
			RepeatOpt(c, c => {
				Token(c, Tokens.AND, t => { self.Operator = t; });
				Node(c, LogicalEqualExpressionParser, r => { self.Right = r; });
			});
		});

	private static readonly
		Parser<CExpression> ArithmeticExpressionParser = new((c, self) => {
			Node(c, AdditionExpressionParser, s => { self.Expression = s; });
		});

	private static readonly
		Parser<BinaryExpression> LogicalEqualExpressionParser = new((c, self) => {
			Node(c, LogicalComparisionExpressionParser, l => { self.Left = l; });
			RepeatOpt(c, c => {
				Token(c, Tokens.EQL, t => { self.Operator = t; });
				Node(c, LogicalComparisionExpressionParser, r => { self.Right = r; });
			});
		});

	private static readonly
		Parser<BinaryExpression> LogicalComparisionExpressionParser = new((c, self) => {
			Node(c, AdditionExpressionParser, l => { self.Left = l; });
			RepeatOpt(c, c => {
				Alt(c, [
					c => { Token(c, Tokens.LEQ, t => { self.Operator = t; }); },
					c => { Token(c, Tokens.LSS, t => { self.Operator = t; }); },
					c => { Token(c, Tokens.GTR, t => { self.Operator = t; }); },
					c => { Token(c, Tokens.GEQ, t => { self.Operator = t; }); },
					c => { Token(c, Tokens.NEQ, t => { self.Operator = t; }); },
					c => { Token(c, Tokens.EQL, t => { self.Operator = t; }); }
				]);
				//Node(c, LogicalComparisionExpressionParser, r => {});
				Node(c, AdditionExpressionParser, r => { self.Right = r; });
			});
		});

	private static readonly
		Parser<BinaryExpression> AdditionExpressionParser = new((c, self) => {
			Node(c, MultiplicationExpressionParser, l => { self.Left = l; });
			RepeatOpt(c, c => {
				Alt(c, [
					c => Token(c, Tokens.PLUS, t => { self.Operator = t; }),
					c => Token(c, Tokens.MINUS, t => { self.Operator = t; })
				]);
				Node(c, MultiplicationExpressionParser, r => { self.Right = r; });
			});
		});

	private static readonly
		Parser<BinaryExpression> MultiplicationExpressionParser = new((c, self) => {
			Node(c, ExponentialExpressionParser, l => { self.Left = l; });
			RepeatOpt(c, c => {
				Alt(c, [
					c => Token(c, Tokens.STAR, t => { self.Operator = t; }),
					c => Token(c, Tokens.SLASH, t => { self.Operator = t; })
				]);
				Node(c, ExponentialExpressionParser, r => { self.Right = r; });
			});
		});

	private static readonly
		Parser<BinaryExpression> ExponentialExpressionParser = new((c, self) => {
			Node(c, UnaryExpressionParser, l => { self.Left = l; });
			RepeatOpt(c, c => {
				Token(c, Tokens.POW, t => { self.Operator = t; });
				Node(c, UnaryExpressionParser, r => { self.Right = r; });
			});
		});

	private static readonly
		Parser<UnaryExpression> UnaryExpressionParser = new((c, self) => {
			Opt(c, c => {
				Token(c, Tokens.NOT, t => { self.Operator = t; });
			});
			Node(c, AtomParser, l => { self.Operand = l; });
		});

	private static readonly
		Parser<AtomExpression> AtomParser = new((c, self) => {
			Alt(c, [
				c => Literal(c, Tokens.NULL, t => { self.IsNull = t; }),
				c => Token(c, Tokens.TRUE, t => { self.BoolValue = t; }),
				c => Token(c, Tokens.FALSE, t => { self.BoolValue = t; }),
				c => Token(c, Tokens.NUMBER, t => { self.NumberValue = t; }),
				c => Token(c, Tokens.STRING, t => { self.StringValue = t; }),
				c => Token(c, Tokens.IDENTIFIER, t => { self.IdentifierValue = t; }),
				c => {
					// ... '(' LogicalExpression ')' ...
					Token(c, Tokens.OPENPAREN);
					Node(c, LogicalExpressionParser, e => { self.GroupingExpr = e; });
					Token(c, Tokens.CLOSEPAREN);
				}
			]);
		});
}