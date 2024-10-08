using NanoScript;
using NanoScript.Lexer;

namespace NanoScript.Parser;

public enum StateType {
    None,
}

public class ParserContext {
	public StateType state;
    public LexerResult<Token> lexerResult;
    public List<TokenElement<Token>> tokens;
    public List<string> importedFiles = new List<string>();
    public Stack<int> frameStack;

    public int idx { get; set; }
    //TODO: Fancy Error Handling
    public ParserContext() {
        state = StateType.None;
        idx = 0;
        frameStack = new();
        tokens = new();
    }
    public ParserContext(LexerResult<Token> result) {
	    this.lexerResult = result;
        this.tokens = result.result;
        frameStack = new();
        state = StateType.None;
        idx = 0;
    }

    //public Program Parse() {
    //    return new Program(this);
    //}
    // functions for treversing the TokenList
    //TODO: Convert all to throwing dunctions and add try function that dont throw
    public bool boundCheck(int i) {
        return i >= 0 && i < tokens.Count;
    }
    public void CreateFrame() {
        frameStack.Push(idx);
    }
    /// <summary>
    /// If the execution was successfull. Return without reassigning the old value.
    /// </summary>
    public void ClearFrame() {
        frameStack.Pop();
    }
    /// <summary>
    /// If the execution failed. Return with reassigning the old value to the current index.
    /// </summary>
    public void PopFrame(string reason = "") {
	    Console.WriteLine($"Frame popped: {reason}");
        idx = frameStack.Pop();
    }
    public string Peek() {
        if (boundCheck(idx)) {
            return tokens[idx].Value;
        }
        return null;
    }
    public bool Peek(string Value) {
        if (boundCheck(idx)) {
            return tokens[idx].Value == Value;
        }
        Console.Error.WriteLine($"Expected {Value}");
        return false;
    }
    public string PeekAtIndex(int i) {
        if (boundCheck(i)) {
            return tokens[i].Value;
        }
        return null;
    }
    public string PeekNext() {
        if (boundCheck(idx + 1)) {
            return tokens[idx + 1].Value;
        }
        return null;
    }
    public string PeekNext(int i) {
        if (boundCheck(idx + i)) {
            return tokens[idx + i].Value;
        }
        return null;
    }
    public bool PeekNext(string Value) {
        if (boundCheck(idx)) {
            return tokens[idx + 1].Value == Value;
        }
        Console.Error.WriteLine($"Expected {Value}");
        return false;
    }
    public bool PeekRange(params string[] values) {
        if (boundCheck(idx + values.Length - 1)) {
            for (int i = 0; i < values.Length; i++) {
                if (tokens[idx + i].Value != values[i]) {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
    public string[] PeekRangeArray(int range) {
        var res = new List<string>();
        if (boundCheck(idx + range - 1))
            for (int i = 0; i < range; i++) {
                res.Add(tokens[idx + i].Value);
            }
        else
            throw new IndexOutOfRangeException();
        return res.ToArray();
    }
    public string[] PeekRangeArray(params string[] values) {
        var res = new List<string>();
        if (boundCheck(idx + values.Length - 1)) {
            for (int i = 0; i < values.Length; i++) {
                if (tokens[idx + i].Value != values[i]) {
                    return null;
                }
            }
        }
        return res.ToArray();
    }
    public string? Consume() {
        if (boundCheck(idx)) {
            return tokens[idx++].Value;
        }
        return null;
    }
    public bool Consume(string value) {
        if (boundCheck(idx) && tokens[idx].Value == value) {
            idx++; // Remove the consumed token from the list
            return true;
        }
        Console.Error.WriteLine($"Expected {value}");
        return false;
    }
    public bool Consume(Token value, out string? value2) {
	    if (boundCheck(idx) && tokens[idx].token == value) {
		    idx++; // Remove the consumed token from the list
		    value2 = tokens[idx].Value;
		    return true;
	    }
	    Console.Error.WriteLine($"Expected {value}");
	    value2 = null;
	    return false;
    }

    public Token Peek_tk() {
        if (boundCheck(idx)) {
            return tokens[idx].token;
        }
        return Token.NONE;
    }
    public bool Peek_tk(params Token[] token) {
        if (boundCheck(idx)) {
            foreach (var item in token) {
                if (item == tokens[idx].token) return true;
            }
        }
        return false;
    }
    public Token PeekAtIndex_tk(int i) {
        if (boundCheck(i)) {
            return tokens[i].token;
        }
        return Token.NONE;
    }
    public Token PeekNext_tk() {
        if (boundCheck(idx + 1)) {
            return tokens[idx + 1].token;
        }
        return Token.NONE;
    }
    public bool PeekNext_tk(Token token) {
        if (boundCheck(idx)) {
            return tokens[idx + 1].token == token;
        }
        Console.Error.WriteLine($"Expected {token}");
        return false;
    }
    public Token PeekNext_tk(int i) {
        if (boundCheck(idx + i)) {
            return tokens[idx + i].token;
        }
        return Token.NONE;
    }
    public Token Consume_tk() {
        if (boundCheck(idx)) {
            return tokens[idx++].token;
        }
        return Token.NONE;        
    }
    public bool Consume_tk(Token token) {
        if (boundCheck(idx) && tokens[idx].token == token) {
            idx++;
            return true;
        }
        Console.Error.WriteLine($"Expected {token}");
        return false;
    }
    public bool ErrRet(bool suc, string msg, ref IExpression? expr) {
	    PopFrame(msg);
	    expr = null;
	    return suc;
    }
}