using System.Text;
namespace TinyScript;

public static class ParenthesesUtils
{
	public static string StringParenthesesResolver(string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return input;

		Stack<List<string>> stack = new Stack<List<string>>();
		List<string> current = new List<string>();
		StringBuilder token = new StringBuilder();

		foreach (char c in input)
		{
			if (c == '(')
			{
				FlushToken();
				stack.Push(current);
				current = new List<string>();
			}
			else if (c == ')')
			{
				FlushToken();

				string group;
				if (current.Count == 1)
					group = current[0]; // remove unnecessary parentheses
				else
					group = "(" + string.Join(" ", current) + ")";

				current = stack.Pop();
				current.Add(group);
			}
			else if (char.IsWhiteSpace(c))
			{
				FlushToken();
			}
			else
			{
				token.Append(c);
			}
		}

		FlushToken();

		return string.Join(" ", current);

		void FlushToken()
		{
			if (token.Length > 0)
			{
				current.Add(token.ToString());
				token.Clear();
			}
		}
	}
}