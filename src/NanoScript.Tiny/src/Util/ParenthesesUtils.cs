using System.Text;

namespace TinyScript;

public static class ParenthesesUtils {
	// copilot generated
	public static string StringParenthesesResolver(string input) {
		if (string.IsNullOrWhiteSpace(input))
			return input;

		Stack<List<string>> stack = new Stack<List<string>>();
		List<string> current = new List<string>();
		StringBuilder token = new StringBuilder();
		StringBuilder whitespace = new StringBuilder();

		foreach (char c in input) {
			if (c == '(') {
				FlushToken();
				FlushWhitespace();
				stack.Push(current);
				current = new List<string>();
			} else if (c == ')') {
				FlushToken();
				FlushWhitespace();

				string group;
				if (CountNonWhitespaceSegments(current) == 1)
					group = string.Concat(current); // remove unnecessary parentheses
				else
					group = "(" + string.Concat(current) + ")";

				current = stack.Pop();
				current.Add(group);
			} else if (char.IsWhiteSpace(c)) {
				FlushToken();
				whitespace.Append(c);
			} else {
				FlushWhitespace();
				token.Append(c);
			}
		}

		FlushToken();
		FlushWhitespace();

		return string.Concat(current);

		void FlushToken() {
			if (token.Length > 0) {
				current.Add(token.ToString());
				token.Clear();
			}
		}

		void FlushWhitespace() {
			if (whitespace.Length > 0) {
				current.Add(whitespace.ToString());
				whitespace.Clear();
			}
		}

		static int CountNonWhitespaceSegments(List<string> segments) {
			int count = 0;

			foreach (string segment in segments) {
				if (!string.IsNullOrWhiteSpace(segment))
					count++;
			}

			return count;
		}
	}
}