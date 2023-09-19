using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using NanoScriptCompiler_Bflat.Helper;
using static NanoScript.Program.Token;

namespace NanoScript.Parser;

// program: module_statement*;
public class Program : Statement {
    public List<ModuleStatement> moduleStatements;
}
// 
// ModuleStatement: 'mod' identifier ImportStatement* ( '{' statement* '}' | statement* )?;
public class ModuleStatement : Statement {
    public IdentifierExpression moduleName;
    public List<ImportStatement> importStatements;
    public bool isSubModule;
    public List<Statement> statements;
}
// ImportStatement: 'import' string ('as' identifier)? | 'import' identifier 'from' string;
public class ImportStatement : Statement {
    public IdentifierExpression identifier;
    public string importString;
    public bool isAs;
    public bool isFrom {
        get { return !isAs; }
        set { isAs = !value; }
    }
}
// 
// statement
//    // Variable Declaration and Assignment: Statements to declare and assign values to variables.
//    : 'pub'? ('let'|'var'|'const')? '.'? identifier type_decl? ('=' exp)?
//[Parser("VariableDeclarationStatement","pub?","let|var|const?")]
public class VariableDeclarationStatement : Statement {
    public bool isPublic;
    public string prefix;//'let'|'var'|'const'
    public bool isSelf;
    public IdentifierExpression identifier ;
    public TypeDeclarationStatement typeDeclarationStatement;
    public bool isAssign;
    public Expression exp;
}
//    // assignment Statement
//    | '.'? identifier type_decl? ('=' exp | '<<' exp | '>>' exp | '+=' exp | '-=' exp | '*=' exp | '/=' exp)?
public enum AssignmentType { none,equal,push,pop,add,sub,mul,div }
public class AssignmentStatement : Statement {
    public bool isSelf;
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;
    public AssignmentType assignmentType;
    public Expression exp;
}
//    // Conditional Statements: Statements that perform different actions based on conditions (e.g., if, else, switch).
//    | 'if' exp '{' statement* '}' ('else' 'if' '{' statement* '}')* ('else' '{' statement* '}')?
public class ConditionalStatement : Statement {
    public SubConditionalStatement subConditionalStatement;
    public List<SubConditionalStatement> subConditionalStatements = new();
    public SubConditionalStatement elseSubConditionalStatement;
}
public class SubConditionalStatement : Statement {
    public bool isElse;
    public bool isIf;
    public Expression exp;
    public List<Statement> statements;
}
//TODO: Switch identifier with expression
//    | 'switch' exp '{' (identifier ':' statement* 'break'?)* 'default' ':' statement* 'break' '}'
public class SwitchStatement : Statement {
    public Expression exp;
    public List<SubSwitchStatement> subSwitchStatements = new();
    //default
    public SubSwitchStatement defSubSwitchStatement;
}
//(identifier ':' statement* 'break'?)*
public class SubSwitchStatement : Statement {
    public IdentifierExpression identifier;
    public List<Statement> statements = new();
    public bool isBreak = false;
    public bool isDefault = false;
}
//    // Control Flow Statements: Statements for altering the flow of execution (e.g., break, continue, goto - though this is less common).
//    | 'break' | 'continue'
public enum ControlFlowModifierType {
    @break,@continue
}
public class BreakContinueStatement : Statement {
    public ControlFlowModifierType ControlFlowModifierType;
}
//    // Looping Statements: Statements that repeat a block of code multiple times (e.g., for, while, do-while).
//    | 'for' (identifier 'in' identifier | identifier '=' exp ';' exp ';' exp | exp) '{' statement* '}'
public enum ForType {
    While,
    For,
    ForIn
}
public class ForStatement : Statement {
    public IdentifierExpression elementIdentifier;
    public IdentifierExpression listIdentifier;

    public ForType type;
    
    public Expression exp_def;
    public Expression exp_cond;
    public Expression exp_incr;

    public List<Statement> statements;
}
//    // Function and Method Declaration: Statements that invoke functions or methods.
//    | 'export'? 'pub'? 'fnc' '.'? identifier'('(identifier type_decl? (',' identifier type_decl? )* )?')' ':' type_decl '{' statement* '}'
public class FunctionDeclarationStatement : Statement {
    public bool isExport;
    public bool isPublic;
    public bool isSelf;
    public IdentifierExpression identifier ;
    public List<ParameterDeclaration> parameters;
    public List<Statement> statements;
    public TypeDeclarationStatement returnType;
}
public class ParameterDeclaration : Statement {
    public IdentifierExpression identifier ;
    public TypeDeclarationStatement typeDeclarationStatement;
}
//    // Return Statements: Statements that return values from functions or methods.
//    | 'return' exp
public class ReturnStatement : Statement {
    public Expression exp;
}
//    // Enum
//    | 'enum' identifier? type_decl? '{' enum_declariation* '}'
public class EnumDeclarationStatement : Statement {
    public TypeDeclarationStatement typeDeclarationStatement;
    public IdentifierExpression identifier;
    List<EnumValueDeclaration> enumValueDeclarations;
}
public enum EnumValueType {
    none,simple,tuple,block
}
//    |(identifier ('=' exp | '(' (identifier type_decl? (',' identifier type_decl? )* )? ')' | '{'  '}') )
public class EnumValueDeclaration : Statement {
    public IdentifierExpression identifier ;
    public Expression exp;
    public EnumValueType type;
    //public List<ParameterDeclaration> parameters;
}
//    // Error Handling: Statements for handling errors or exceptions (e.g., try-catch, throw).
//    | 'error' exp
public class ErrorStatement : Statement {
    public Expression exp;
}
//    // Declaration Statements: Statements for defining types, structures, classes, and interfaces.
//    | ('def'|'type') identifier ('=' exp)?
public enum DeclarationType {
    def,type
}
public class DeclarationStatement : Statement {
    public DeclarationType declarationType;
    public IdentifierExpression identifier ;
    public Expression exp;
}
//    //class
//    | 'pub'? 'class' identifier type_decl? '{' statement* '}'
public class StructDeclarationStatement : Statement {
    public bool isPublic;
    public IdentifierExpression identifier ;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;
}
//    //class
//    | 'pub'? 'class' identifier type_decl? '{' statement '}'
public class ClassDeclarationStatement : Statement {
    public bool isPublic;
    public IdentifierExpression identifier ;
    public TypeDeclarationStatement typeDeclarationStatement; 
    public List<Statement> statements;
}
//    //interface
//    | 'interface' identifier type_decl? '{' statement '}'
public class InterfaceStatement : Statement {
    public IdentifierExpression identifier ;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;
}
//    // union
//    | 'union' type_decl? '{' statement* '}'
public class UnionStatement : Statement {
    public IdentifierExpression identifier ;
    public TypeDeclarationStatement typeDeclarationStatement;//???
    public List<Statement> statements;
}
//    // Expression Statements: Statements that evaluate and execute expressions.
//    // Input/Output Statements: Statements for interacting with the console or other input/output streams.
//    // Memory Management: Statements for memory allocation and deallocation (e.g., malloc, free, new, delete).
//    // Assertion Statements: Statements for specifying conditions that must be true at certain points in the program.
//    | 'assert' exp
public class AssertionStatement : Statement {
    public Expression exp;
}
//    // Label Statements: Statements that mark a specific point in the code for jumping or referencing (e.g., break to label).
//    | '::' identifier
public class LabelStatement : Statement {
    public IdentifierExpression identifier;
}
//    | 'goto' identifier
public class GotoStatement : Statement {
    public IdentifierExpression identifier;
}
//    // Macro and Preprocessor Directives: Special statements used for compile-time code generation (in C and C++).
//    | '#include' '<' string '>'
//    // Concurrency and Synchronization Statements: Statements for managing multi-threading and synchronization.
//    ;
// 
// type_decl: ':' ('[' (exp)? ']'|'{}')? identifier;
public enum TypeDeclarationType {
    value,
    array,
    table
}
public class TypeDeclarationStatement : Statement {
    public IdentifierExpression identifier ;
    public TypeDeclarationType typeDeclarationType;
    public Expression exp;
}
// // Function Call Statement
// | identifier '(' (exp (',' exp)*)? ')'
public class FunctionCallStatement : Statement {
    public IdentifierExpression identifier ;
    public List<Expression> parameters;
}
// 
// exp
//    // 'match' '(' exp ')' '{' exp '=>' exp ('|' exp '=>' exp)* '}'
//    //Literal Expressions: Constants representing specific values (e.g., numbers, strings, boolean values).
//    : identifier
//    //Member Access Expressions: Expressions to access properties or methods of objects or structures.
//    | '.'? identifier ('.' identifier)* (':' identifier)?
public class IdentifierExpression : Expression {
    public string identifier;
    public List<string> identifiers = new List<string>();
    public bool isExtension;
    public string lastIdentifier;
    public bool isSelf;
}
public interface INumber {}
//    | number
public struct IntegerExpression : INumber {
    public Int128 int_number;
    public string RAW;
    public IntegerExpression(string val) {
        this.int_number = Int128.Parse(val);
        this.RAW = val;
    }
}
public struct FloatExpression : INumber {
    public double double_number;
    public string RAW;
    public FloatExpression(string val) {
        this.double_number = double.Parse(val);
        this.RAW = val;
    }
}
public class NumberExpression : Expression {
    public INumber number;
}
//    | string
public class StringExpression : Expression {
    public string str;
}
//    | ('true'|'false')
public class BooleanExpression : Expression {
    public bool value;
}
//    //Grouping Expression:
//    | '(' exp ')'
public class GroupingExpression : Expression {
    public Expression exp;
}
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
//    //Bitwise Expressions: Expressions that manipulate individual bits within values.
//    | exp '&' exp
//    | exp '|' exp
//    | exp '^' exp
//    | exp '<<' exp
//    | exp '>>' exp
//    | exp '~' exp
public enum BinaryOperatorType {
    add, // +
    sub, // -
    div, // /
    mul,    // *
    mod,    // %
    pow,    // **
    
    doubleOr, // ||
    doubleAnd, // &&
    
    equals, // ==
    notEquals, // !=
    lessEquals, // <=
    less, // <
    greaterEquals, // >=
    greater, // >
    
    and, // &
    or, // |
    xor, // ^
    shl, // <<
    shr, // >>
    not, // ~
    none
}
public class BinaryExpression : Expression {
    public Expression left;
    public Expression right;
    public BinaryOperatorType operatorType = BinaryOperatorType.none;
}
//    //Unary Expressions: Expressions involving a single operand (e.g., negation, logical NOT).
//    | '!' exp
//    | '-' exp
//    | '++' exp
//    | exp '++'
//    | '--' exp
//    | exp'--'
public enum UnaryOperatorType {
    none,
    not, // !
    //neg, // -
    inc, // ++
    dec, // --
}
public class UnaryExpression : Expression {
    public bool isBefore;
    public Expression exp;
    public UnaryOperatorType operatorType;
}
public class ExpressionList : Expression {
    public List<Expression> expressions = new();
}
//    //Pointer or Reference Expressions: Expressions that deal with memory addresses (in languages like C and C++).
//    | '&' exp
//    | '*' exp
public class PointerOrReferenceExpression : Expression {
    public bool isPointer = false;
    public bool isReference { get { return !isPointer; } set { isPointer = !value; } }
    public Expression exp;
}
//    //Function or Method Calls: Expressions that invoke functions or methods.
//    | identifier '(' (exp (',' exp)*)? ')'
public class FunctionCallExpression : Expression {
    public Expression identifier;
    public List<Expression> parameters;
}
//    //Conditional Expressions: Expressions that result in different values based on conditions (e.g., ternary operator).
//    //Array and Indexing Expressions: Expressions to access elements in arrays or collections.
//    | identifier '[' (exp | exp '..' exp) ']'
public class ArrayIndexingExpression : Expression {
    public Expression identifier;
    public Expression index;
    public bool isRange;
    public (Expression, Expression) range;
}
//    //Property Access Expressions: Expressions used to access properties of objects or structures.
//    //Assignment Expressions: Expressions that assign values to variables or locations.
//    //Type Conversion Expressions: Expressions that convert values between different data types.
//    | '(' identifier ')' exp
public class TypeConversionExpression : Expression {
    public IdentifierExpression identifier;
    public Expression exp;
}
//    //Lambda or Anonymous Function Expressions: Expressions that define inline functions.
//    | '(' exp ')' '=>' '{' statement* '}'
//    //Regex or String Matching Expressions: Expressions used for pattern matching in strings.
//    //Type Check Expressions: Expressions used to check the type of an object.
//    | exp 'is' identifier
//    | exp 'as' identifier
public class TypeCheckExpression : Expression {
    public Expression exp;
    public bool isAs;
    public bool isIs { get { return !isAs; } set { isAs = !value; } }
    public IdentifierExpression identifier;
}
//    //Tuple Creation Expression:
//    | '(' exp (',' exp) ')'
[MissingFeature("Tuple")]
public class TupleCreationExpression : Expression {
    public Expression exp;
    public List<Expression> expressions;
}
//    //Array Creation Expressions: Expressions for creating new arrays or collections.
//    | '[' exp (',' exp) ']'
public class ArrayCreationExpression : Expression {
    public List<Expression> expressions;
}
//    //Array Creation Expression
//    | '{' ( identifier type_decl? ('=' exp)? )* '}'
[MissingFeature("Array initialization")]
public class ArrayInitializationExpression : Expression {
    public List<Expression> expressions;
    public TypeDeclarationStatement typeDeclarationStatement;
    public bool isAssign;
    public Expression exp;
}
//    //Function Creation Expression
//    | 'fnc' '(' (identifier type_decl? (',' identifier type_decl? )* )? ')' type_decl? '{' statement* '}'
public class FunctionCreationExpression : Expression {
    public List<ParameterDeclaration> parameters;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;
}
//    // internal functions
//    | 'type' '(' exp ')'
//    | 'size' '(' exp ')'
//    | 'str' '(' exp ')'
//    | 'len' '(' exp ')'
//    | 'print' '(' exp ')'
//    | 'println' '(' exp ')'
public class InternalFunctionCallExpression : Expression {
    public string functionName;
    public List<Expression> parameters;
}
//    ;