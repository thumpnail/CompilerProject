using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using Newtonsoft.Json;
using static NanoScript.Program.Token;

namespace NanoScript.Parser;

public partial class Parser {
    public ParserContext ctx;
    public Parser(ParserContext ctx) {
        this.ctx = ctx;
    }
    // program: module_statement+;
    public Program Parse() {
        var res = new Program();
        //TODO: Some initioalization
        res.moduleStatements = ParseModuleStatements();
        return res;
    }
// module_statement: 'mod' identifier imoport_statements* ( '{' statement* '}' | statement* );
// imoport_statements: 'import' string ('as' identifier) | 'import' identifier 'from' string;
//
// statement


//    // Variable Declaration and Assignment: Statements to declare and assign values to variables.
//    : 'pub' ('let' | 'var' | 'const') '.' identifier type_decl? ('=' exp)
// type_decl: ':' ('[' (identifier|number) ']'|'{}') identifier;
// exp
    
//    //Literal Expressions: Constants representing specific values (e.g., numbers, strings, boolean values).
//    : identifier
//    | number
//    | string
//    | ('true'|'false')
//    //Variable References: Expressions that refer to variables or memory locations.
//    | '&'identifier
//    //Arithmetic Expressions: Expressions that perform mathematical operations (e.g., addition, subtraction).
//    | exp '+' exp
//    | exp '-' exp
//    | exp '/' exp
//    | exp '*' exp
//    //Grouping Expression:
//    | '(' exp ')'
//    //Logical Expressions: Expressions that involve logical operations (e.g., AND, OR).
//    | exp '||' exp
//    | exp '&&' exp
//    //Comparison Expressions: Expressions that compare values (e.g., greater than, equal to).
//    | exp '==' exp
//    | exp '!=' exp
//    | exp '<=' exp
//    | exp '<' exp
//    | exp '>=' exp
//    | exp '>' exp
//    //Function or Method Calls: Expressions that invoke functions or methods.
//    | identifier '(' (exp (',' exp)*) ')'
//    //Conditional Expressions: Expressions that result in different values based on conditions (e.g., ternary operator).
//    //Array and Indexing Expressions: Expressions to access elements in arrays or collections.
//    | identifier '[' (exp | exp '..' exp) ']'
//    //Member Access Expressions: Expressions to access properties or methods of objects or structures.
//    | '.' identifier ('.' identifier)* (':' identifier)
//    //Property Access Expressions: Expressions used to access properties of objects or structures.
//    //Assignment Expressions: Expressions that assign values to variables or locations.
//    //Type Conversion Expressions: Expressions that convert values between different data types.
//    | '(' identifier ')' exp
//    //Bitwise Expressions: Expressions that manipulate individual bits within values.
//    | exp '&' exp
//    | exp '|' exp
//    | exp '^' exp
//    | exp '<<' exp
//    | exp '>>' exp
//    | exp '~' exp
//    //Unary Expressions: Expressions involving a single operand (e.g., negation, logical NOT).
//    | '!' exp
//    | '-' exp
//    | '++' exp
//    | exp '++'
//    | '--' exp
//    | exp'--'
//    //Pointer or Reference Expressions: Expressions that deal with memory addresses (in languages like C and C++).
//    | '&' exp
//    | '*' exp
//    //Lambda or Anonymous Function Expressions: Expressions that define inline functions.
//    | '(' exp ')' '=>' '{' statement* '}'
//    //Regex or String Matching Expressions: Expressions used for pattern matching in strings.
//    //Type Check Expressions: Expressions used to check the type of an object.
//    | exp 'is' identifier
//    //Tuple Creation Expression:
//    | '(' exp (',' exp) ')'
//    //Array Creation Expressions: Expressions for creating new arrays or collections.
//    | '[' exp (',' exp) ']'
//    //Array Creation Expression
//    | '{' ( identifier type_decl ('=' exp) )* '}'
//    //Function Creation Expression
//    | 'fnc' '(' (identifier type_decl (',' identifier type_decl )* ) ')' type_decl '{' statement* '}'
//    // internal functions
//    | 'type' '(' exp ')'
//    | 'size' '(' exp ')'
//    | 'str' '(' exp ')'
//    ;
    //identifier: WORD ('.' WORD)* (':' WORD) ('[' (exp | exp '..' exp) ']')?;
}