using System.Xml.Linq;
namespace NanoScript.Parser.AstNodes;

//    //Conditional Expressions: Expressions that result in different values based on conditions (e.g., ternary operator).
//    //Array and Indexing Expressions: Expressions to access elements in arrays or collections.
//    | identifier '[' (exp | exp '..' exp) ']'
public class ArrayIndexingExpression : IExpression {
	public IExpression? identifier;
	public IExpression? index;
	public bool isRange;
	public (IExpression, IExpression) range;
	public  string GenCS() {
		var res = new StringBuilder();
		res.Append(identifier?.GenCS());
		res.Append("[");
		if(!isRange) {
			res.Append(index?.GenCS());
		} else {
			res.Append($"{range.Item1.GenCS()} .. {range.Item2.GenCS()}");
		}
		res.Append("]");
		return res.ToString();
	}
	public  List<int> TranspileToByteCode() {throw new NotImplementedException();}
	public  List<string> TranspileToAsm() {throw new NotImplementedException();}
	public string ToXml() {
		var xml = new XElement("ArrayIndexingExpression",
			new XAttribute("Identifier", identifier?.ToXml()),
			new XElement("Index",
				new XAttribute("IsRange", isRange.ToString().ToLower()),
				isRange
					? new XElement("Range",
						new XElement("Start", range.Item1.ToXml()),
						new XElement("End", range.Item2.ToXml())
					)
					: new XElement("Value", index?.ToXml())
			)
		);
		return xml.ToString();
	}
}