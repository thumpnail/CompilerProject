using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Principal;
namespace ParserNew;

public record ParserDefinition();

public class CustomLanguageParser {
    public record Identifier(/*Need definition*/);
    public static Parser<Identifier> IdentifierParser = new(/*Need definition*/);
    public record Expression(/*Need definition*/);
    public static Parser<Expression> ExpressionParser = new(/*Need definition*/);
    public record AssignStatement(string type, Identifier name, Identifier typedcl, Expression exp);
    public static Parser<AssignStatement> AssignmentStatement = new(new Optional(new Literal("pub")), new Literal("let","var","const"), IdentifierParser, new Optional(new Literal(":"), IdentifierParser), new Literal("="), ExpressionParser);

    // Parser definition classes
    interface IParseNode {}
    class Optional(params object[] opts) : IParseNode;
    class Literal(params object[] lits) : IParseNode;
}

public class Parser<T> {
    public Parser(params object[] ps) {}
}