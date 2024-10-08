using System.Xml.Linq;

using NanoScript.Parser.Attributes;
namespace NanoScript.Parser.AstNodes;

//    //Array Creation Expressions: Expressions for creating new arrays or collections.
//    | '[' exp (',' exp) ']'
[AstNode("ArrayCreationExpression","abc")]
public class ArrayCreationExpression : IExpression {
	public List<IExpression> expressions = new();
	public  string  GenCS() {
		var res = "";
		foreach(var item in expressions) {
			res += $"{item.GenCS()},";
		}
		res.Remove(res.Length-2);
		return $"[{res}]";
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		return $"<ArrayCreationExpression>{string.Join('\n', from x in expressions select x.ToXml())}</ArrayCreationExpression>";
	}
}