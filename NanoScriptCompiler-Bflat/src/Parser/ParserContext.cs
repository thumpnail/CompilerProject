using static NanoScript.Program;

namespace NanoScript.Parser;

enum StateType {
    None,
}

public class ParserContext {
    private StateType state;
    private List<NanoScript.Lexer.TokenElement<Token>> tokens;
    private Stack<int> frameStack;

    public int idx { get; private set; }
    //TODO: Fancy Error Handling
    public ParserContext() {
        state = StateType.None;
        idx = 0;
        frameStack = new();
    }
    public ParserContext(List<NanoScript.Lexer.TokenElement<Token>> tokens) {
        this.tokens = tokens;
        frameStack = new();
        state = StateType.None;
        idx = 0;
    }

    //public Program Parse() {
    //    return new Program(this);
    //}
    // functions for treversing the TokenList

    public bool boundCheck(int i) {
        return i >= 0 && i < tokens.Count;
    }
    public void CreateFrame() {
        frameStack.Push(idx);
    }
    public void ClearFrame() {
        frameStack.Pop();
    }
    public void PopFrame() {
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
    public string Consume() {
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
        return false;
    }

    public Token Peek_tk() {
        if (boundCheck(idx)) {
            return tokens[idx].token;
        }
        return Token.NONE;
    }
    public bool Peek_tk(Token token) {
        if (boundCheck(idx)) {
            return tokens[idx].token == token;
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
            idx++; // Remove the consumed token from the list
            return true;
        }
        return false;
    }
}