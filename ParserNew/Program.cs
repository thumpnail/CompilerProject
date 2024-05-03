// See https://aka.ms/new-console-template for more information

public class Program {
    enum astnodetype {
        AssignmentStatement
    }
    record Expression();
    record AssignmentStatement(astnodetype type, string name, string valueType, Expression value);
    
    public static void Main(string[] args) {
        var testsrc = "let value : int = test + 2";
        var parsedtree = (astnodetype.AssignmentStatement, "value", "int", ("test","+"), "2");
        Console.WriteLine("Hello, World!");
    }
}