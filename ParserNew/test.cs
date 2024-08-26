//using System.Linq;
//using System.Collections.Generic;
//public record ParserDefinition();
//
//public class CustomLanguageParser {
//    public record Identifier(string Name);
//    public static Parser<Identifier> IdentifierParser = new(ParseIdentifier);
//
//    public record Expression(string Operation, List<Expression> Elements);
//    public static Parser<Expression> ExpressionParser = new(ParseExpression);
//
//    public record AssignStatement(string Type, Identifier Name, Identifier TypeDecl, Expression Exp);
//    public static Parser<AssignStatement> AssignmentStatement = new(
//        new Optional<IParseNode>(new Literal("pub")),
//        new Literal("let", "var", "const"),
//        IdentifierParser,
//        new Optional<IParseNode>(new Literal(":")),
//        IdentifierParser,
//        new Literal("="),
//        ExpressionParser
//    );
//
//    // Parser definition classes
//    public interface IParseNode {
//        bool Parse(ref string input, out object result);
//    }
//
//    public class Literal : IParseNode {
//        private readonly IEnumerable<string> literals;
//
//        public Literal(params string[] lits) => literals = lits;
//
//        public bool Parse(ref string input, out object result) {
//            result = null;
//            if (literals.Contains(input.Substring(0, 1).ToLower())) {
//                result = input.Substring(0, 1);
//                input = input.Substring(1);
//                return true;
//            }
//            return false;
//        }
//    }
//
//    public class Optional<TNode> : IParseNode where TNode : IParseNode {
//        public Optional() : this(new TNode()) {}
//
//        public Optional(TNode node) {
//            Node = node;
//        }
//
//        public TNode Node { get; }
//
//        public bool Parse(ref string input, out object result) {
//            result = null;
//            if (Node.Parse(ref input, out object value)) {
//                result = value;
//                return true;
//            }
//            return true;
//        }
//    }
//
//    public static bool ParseIdentifier(ref string input, out object result) {
//        result = Identifier.Create(input.Split('_')
//            .Select(p => char.ToLower(p[0]) + p.Substring(1))
//            .Aggregate((a, b) => a + b));
//        input = input.Substring(result.ToString().Length);
//        return true;
//    }
//
//    public static bool ParseExpression(ref string input, out object result) {
//        // A simple implementation would look something like this, but for more complex expressions, build a proper expression tree.
//        throw new System.NotImplementedException();
//    }
//}
//
//public class Parser<T> {
//    public Parser(params object[] ps) {
//        // Set up parser components
//        Nodes = ps.Select(p => (CustomLanguageParser.IParseNode)p).ToList();
//    }
//
//    public List<CustomLanguageParser.IParseNode> Nodes { get; }
//
//    public bool Parse(ref string input, out T result) {
//        result = default;
//        var current = 0;
//        if (current >= Nodes.Count) {
//            return false;
//        }
//
//        while (Nodes[current].Parse(ref input, out object output) && current < Nodes.Count) {
//            current++;
//        }
//
//        if (current < Nodes.Count) {
//            return false;
//        }
//
//        if (output is T typedOutput) {
//            result = typedOutput;
//            return true;
//        }
//        return false;
//    }
//}