using System.Text.RegularExpressions;

namespace NanoScript.Lexer;
// Extreme Lexer Change Yes
public struct TokenElement<T> : IEquatable<TokenElement<T>> where T : Enum {
    public T token;
    public string Value;
    public int prio;
    public int index;
    public int length;
    public bool isSkipable { get { return prio < 0; } }
    public TokenElement(T token, string value, int index, int length, int prio = -1) {
        this.token = token;
        this.Value = value;
        this.index = index;
        this.length = length;
        this.prio = prio;
    }
    public bool Equals(TokenElement<T> other) {
	    return EqualityComparer<T>.Default.Equals(token, other.token) && Value == other.Value && prio == other.prio && index == other.index && length == other.length;
    }
    public override bool Equals(object? obj) {
	    return obj is TokenElement<T> other && Equals(other);
    }
    public override int GetHashCode() {
	    return HashCode.Combine(token, Value, prio, index, length);
    }
}

//Contains token and string/regex for that token
public struct Category<T> where T : Enum {
	public Guid guid = Guid.NewGuid();
    public T token;
    public bool isSkipable = false;
    public string regex;
    public Category(T token, string regex) {
        this.token = token;
        this.regex = regex;
    }
    public Category(T token, bool skipable, string regex) {
        this.token = token;
        this.regex = regex;
        this.isSkipable = skipable;
    }
}

public struct LexerResult<T> where T : Enum {
    public List<TokenElement<T>> result = new List<TokenElement<T>>();
    public string source = "";
    public LexerResult(string source, List<TokenElement<T>> result) {
        this.result = result;
        this.source = source;
    }
    public void AddResult(LexerResult<T> result) {
	    this.result.AddRange(result.result);
	    this.source += result.source;
    }
}
public class Lexer<T> where T : Enum {
    public List<Category<T>> _categories;
    public string _Source;
    public List<TokenElement<T>> _Result;

    public Lexer() {
        this._Source = String.Empty;
        this._categories = new();
        this._Result = new();
    }
    //creates a child category
    public Lexer<T> child(T tk, string lit) {
        if (lit is null)
            throw new Exception();
        if (lit != String.Empty)
			_categories.Add(new Category<T>(tk, lit));
        return this;
    }
    public Lexer<T> skipable(T tk, string lit) {
        if (lit is null)
            throw new Exception();
        if (lit != String.Empty)
	        _categories.Add(new Category<T>(tk, true, lit));
        return this;
    }
    public LexerResult<T> LexFile(string source) {
	    return this.Lex(File.ReadAllText(source));
    }
    public LexerResult<T> LexOld(string source) {
	    // Caching
	    var regexCache = new Dictionary<Guid, Regex>();
	    foreach (var cat in _categories)
		    regexCache.Add(cat.guid, new(cat.regex));
	    
        this._Source = source;
        int prioc = 0;
        
        foreach (var cat in _categories) {
	        //retrieve cache object
	        var rgx = regexCache[cat.guid];
	        // All Matches
	        var res = rgx.Matches(this._Source);
	        for (int i = 0; i < res.Count; i++) {
		        if(!cat.isSkipable)
			        _Result.Add(new(
				        cat.token, 
				        res[i].Value, 
				        res[i].Index, 
				        res[i].Length, 
				        prio: prioc));
	        }
	        
            prioc++;
        }
        _Result.Sort((element, tokenElement) => {
            if (element.index > tokenElement.index)
                return 1;
            else if (element.index < tokenElement.index)
                return -1;
            else
                return 0;
        });
        //Include priority
        _Result = _Result
            .GroupBy(o => o.index)
            .Select(g => g.OrderByDescending(o => o.length).ThenBy(o => o.prio).First()) // get the one whith highest length
            .ToList()
            ;
        var rmlist = new List<TokenElement<T>>();
        foreach (var item1 in _Result) {
            int eidx = item1.index + item1.length;
            foreach (var item2 in _Result.Where(x => (x.index < eidx && x.index > item1.index) || x.prio.Equals(-1))) {
	            rmlist.Add(item2);
	            if (!rmlist.Contains(item2)) {
		            rmlist.Add(item2);
	            }
            }
        }
        
        _Result.RemoveAll(rmlist.Contains);
        return new LexerResult<T>(source,_Result);
    }
    public LexerResult<T> Lex(string? source) {
	    if (source is null) {
		    return new("", new List<TokenElement<T>>());
	    }
	    var regexCache = new Dictionary<Guid, Regex>();
	    foreach (var cat in _categories)
		    regexCache.Add(cat.guid, new(cat.regex));
	    
	    this._Source = source;
	    int prioc = 0;
	    foreach (var cat in _categories) {
		    var rgx = regexCache[cat.guid];
		    var res = rgx.Matches(this._Source);
		    for (int i = 0; i < res.Count; i++) {
			    var match = res[i];
			    if(cat.isSkipable) 
				    _Result.Add(new(cat.token, match.Value, match.Index, match.Length));
			    else
				    _Result.Add(new(cat.token, match.Value, match.Index, match.Length, prio: prioc));
		    }
		    prioc++;
	    }
	    _Result.Sort((element, tokenElement) => {
		    if (element.index > tokenElement.index)
			    return 1;
		    else if (element.index < tokenElement.index)
			    return -1;
		    else
			    return 0;
	    });
	    //Include priority
	    _Result = _Result
			    .GroupBy(o => o.index)
			    .Select(g => g.OrderByDescending(o => o.length).ThenBy(o => o.prio).First()) // get the one whith highest length
			    .ToList();
	    
	    var rmlist = new List<TokenElement<T>>();
	    foreach (var item1 in _Result) {
		    int eidx = item1.index + item1.length;
		    foreach (var item2 in _Result.Where(x => (x.index < eidx && x.index > item1.index) || x.isSkipable)) {
			    rmlist.Add(item2);
		    }
	    }
	    //foreach (var item in rmlist) {
		//    _Result.Remove(item);
	    //}
	    _Result.RemoveAll(rmlist.Contains);
	    return new LexerResult<T>(source, _Result);
    }
}