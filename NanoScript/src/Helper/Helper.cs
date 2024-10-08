using System.Xml;

using Newtonsoft.Json;
namespace NanoScript.Helper;
public static class Helper {
	private static StreamWriter? logWriter;
    public static bool ContainsNumbers(this string? str) {
	    int check = 0;
	    if (str is not null)
        	foreach (char c in str) {
        	    if (c >= '0' && c <= '9') {
	    	        check++;
        	    }
        	}
        return check > 0;
    }
    public static void ToConsole(this object obj) {
        Console.WriteLine(obj);
    }
    public static void ToFile(this object obj, string filename) {
        using(StreamWriter writer = new StreamWriter(filename, Encoding.Default, new FileStreamOptions() {
	              Access = FileAccess.Write,
	              Mode = FileMode.Create
              })) {
            writer.WriteLine(obj);
            writer.Close();
        }
    }
    public static string ToXml(this string obj) {
	    var doc = JsonConvert.DeserializeXNode(obj, "root");
	    return doc?.ToString() ?? "<null>";
    }
    public static void ToLog(this object obj, string from = "[system]", bool ctime = false, string pre = "", string post = "") {
	    var time = DateTime.Now.ToString("O");
	    logWriter ??= new("compiler.log");
	    logWriter.WriteLine($"{(ctime ? $"[{time}]" : "")} {from} {pre} {obj} {post}".Trim());
    }
    public static string Indent(int indent) {
	    var res = "";
	    for (int i = 0; i < indent; i++) {
		    res += "\t";
	    }
	    return res;
    }
    public static void IndentAppend(this StringBuilder sb, int indent, object obj) {
	    sb.Append(Indent(indent) + obj);
    }
    public static void IndentAppendLine(this StringBuilder sb, int indent, object obj) {
	    sb.AppendLine(Indent(indent) + obj);
    }
}