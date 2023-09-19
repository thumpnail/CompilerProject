using System.Runtime.CompilerServices;
namespace NanoScript.Parser;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class ParserAttribute : Attribute {
    public static List<ParserAttribute> ParserAttributeList = new List<ParserAttribute>();
    public string Name;
    public List<string> guessableTokens;
    public ParserAttribute(string name, params string[] args) {
        if (args is not null) {
            guessableTokens = args.ToList();
            ParserAttributeList.Add(this);
            Name = name;
        }
        Name = name;
        ParserAttributeList.Add(this);
    }
}