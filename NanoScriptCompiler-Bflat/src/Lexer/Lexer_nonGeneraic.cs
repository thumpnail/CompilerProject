using System.Text;
using System.Text.RegularExpressions;
using Token = System.Int32;

namespace NanoScript.Lexer.nongeneric;
// Represents a Value wich contains cruicial information about the Token, Value, Index and Length
public class TokenElement {
    // The token
    public Token ftoken;
    // a priority value
    public int prio;
    // the raw string value
    public string value;
    // index in the source text
    public int index;
    //length of the string value
    public int length;
    // if the token is skippable, or to just ignore it
    public bool isSkipable;
    //constructor to initilize the Token Element
    public TokenElement(Token token, string value, int index, int length, bool isSkipable = false, int prio = 0) {
        this.ftoken = token;
        this.value = value;
        this.index = index;
        this.length = length;
        this.isSkipable = isSkipable;
        this.prio = prio;
    }
    // override the to string method wit a custom one for debug purposes
    public override string ToString() {
        return $"('{value}', #{ftoken}, [{index},{length}], p:{prio}){(isSkipable ? "!" : "")}";
    }
}

//Contains token and string/regex for that token
// my lexer uses Categories to set Token based on given literals
struct Category {
    // the token the category assigns to the literals
    public Token token;
    // well... to skip, comments for example
    public bool isSkipable = false;
    // list of the literals that do apply for the token
    public string[] literals;
    public Category(Token token, params string[] literals) {
        this.token = token;
        this.literals = literals;
    }
    public Category(Token token, bool skipable, params string[] literals) {
        this.token = token;
        this.literals = literals;
        this.isSkipable = skipable;
    }
}

//The Result that the lexer returns, with the source text and a list of Token Elements
public struct LexerResult {
    public List<TokenElement> result;
    public string source;
    public LexerResult(string source, List<TokenElement> result) {
        this.result = result;
        this.source = source;
    }
}
// The main Lexer Class wich is used to build the lexer and to execute it on a source text
public class Lexer {
    // List of categories wich apply to the given source text
    private List<Category> cats;
    //the actual source text
    private string source;
    //the result
    private List<TokenElement> result;
    // initialize the lexer
    public Lexer() {
        this.source = String.Empty;
        this.cats = new();
        this.result = new();
    }
    //creates a child/category with literals and a assignable token
    public Lexer child(Token tk, params string[] lit) {
        // throw an exception if no literals are given
        if (lit is null)
            throw new Exception();
        this.cats.Add(new Category(tk, lit));
        return this;
    }
    //creates a child/category with literals and a assignable token for explicit skipable
    public Lexer skipable(Token tk, params string[] lit) {
        if (lit is null)
            throw new Exception();
        cats.Add(new Category(tk, true, lit));
        return this;
    }
    // execute the lexer on a source string
    public LexerResult Lex(string source) {
        this.source = source;
        // the prio is just a incremental value representing "how deep" the lexer is inside the categories
        int prioc = 0;
        // iterate through the Categories and do a regex search on the source text and assign a token to the found items
        foreach (var cat in cats) {
            foreach (var str in cat.literals) {
                var rgx = new Regex(str);
                var res = rgx.Matches(this.source);
                for (int i = 0; i < res.Count; i++) {
                    var match = res[i];
                    if(cat.isSkipable)
                        result.Add(new(cat.token, match.Value, match.Index, match.Length, true));
                    else
                        result.Add(new(cat.token, match.Value, match.Index, match.Length, prio: prioc));
                }
            }
            prioc++;
        }
        // Sort the elements since those are unsorted since they are build based on categorys
        // so an element with index 4 can be at the end of the list and this fixes that
        result.Sort((element, tokenElement) => {
            if (element.index > tokenElement.index)
                return 1;
            else if (element.index < tokenElement.index)
                return -1;
            else
                return 0;
        });
        // filter everything else out that has a higher priority then the other elements grouped by index
        result = result
            .GroupBy(o => o.index)
            .Select(g => g.OrderBy(o => o.prio).First()) // get the one whith highest prio
            .ToList()
            ;
        // Remove Item list to mark all items that should be removed
        var rmlist = new List<TokenElement>();
        // add all remaining items that should be removed to that list...
        foreach (var item1 in result) {
            //"end index"
            int eidx = item1.index + item1.length;
            foreach (var item2 in result.Where(x => (x.index < eidx && x.index > item1.index) || x.isSkipable)) {
                rmlist.Add(item2);
            }
        }
        // remove the items in rmlist from the result
        foreach (var item in rmlist) {
            result.Remove(item);
        }
        // return the lexer result
        return new LexerResult(source,result);
    }
}