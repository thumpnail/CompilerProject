using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using NanoScriptCompiler_Bflat.Helper;
using static NanoScript.Program.Token;

namespace NanoScript.Parser;

// program: module_statement*;
public class Program : Statement {
    public List<ModuleStatement> moduleStatements;
    public override string TranspileToBflat() {
        var res = new StringBuilder();
        foreach (var item in moduleStatements) {
            res.Append(item.TranspileToBflat());
        }
        return $"{res}\n";
    }
}

// 
// ModuleStatement: 'mod' identifier ImportStatement* ( '{' statement* '}' | statement* )?;
public class ModuleStatement : Statement {
    public IdentifierExpression moduleName;
    public List<ImportStatement> importStatements;
    public bool isSubModule;
    public List<Statement> statements;
    public override string TranspileToBflat() {
        var res = new StringBuilder();
        foreach (var item in importStatements) {
            res.Append(item.TranspileToBflat());
        }
        res.Append($"namespace module_{moduleName.TranspileToBflat()} {{\n");
        foreach (var item in statements) {
            res.Append(item.TranspileToBflat());
        }
        res.Append("\n}");
        return $"{res}";
    }
}

// ImportStatement: 'import' string ('as' identifier)? | 'import' identifier 'from' string;
public class ImportStatement : Statement {
    public IdentifierExpression identifier;
    public string importString;
    public bool isAs;

    public bool isFrom
    {
        get { return !isAs; }
        set { isAs = !value; }
    }

    public override string TranspileToBflat() {
        var res = new StringBuilder();
        if (isAs) {
            if (importString.EndsWith(".nano")) {
                //TODO: Namepace Magic
            } else {
                //Guessing that it is a c# namepace
                res.Append($"using {identifier.TranspileToBflat()} = {importString.Substring(1, importString.Length - 2)};");
            }
        } else {
            res.Append($"using {importString};");
        }
        // TODO: parse nano files
        return $"{res}\n";
    }
}

// 
// statement
//    // Variable Declaration and Assignment: Statements to declare and assign values to variables.
//    : 'pub'? ('let'|'var'|'const')? '.'? identifier type_decl? ('=' exp)?
//[Parser("VariableDeclarationStatement","pub?","let|var|const?")]
public class VariableDeclarationStatement : Statement {
    public bool isPublic;
    public string prefix; //'let'|'var'|'const'
    public bool isSelf;
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;
    public bool isAssign;
    public Expression exp;
    public override string TranspileToBflat() {
        var res = new StringBuilder();
        if (isPublic) res.Append("public ");
        if (prefix == "let") res.Append("readonly ");
        else if (prefix == "const") res.Append("const ");

        if (typeDeclarationStatement != null) {
            res.Append($"{typeDeclarationStatement.TranspileToBflat()} ");
        } else {
            res.Append($"var ");
        }

        res.Append($"{identifier.TranspileToBflat()}");
        if (isAssign) res.Append($" = {exp.TranspileToBflat()}");
        res.Append(";");

        return $"{res}\n";
    }
}

//    // assignment Statement
//    | '.'? identifier type_decl? ('=' exp | '<<' exp | '>>' exp | '+=' exp | '-=' exp | '*=' exp | '/=' exp)?
public enum AssignmentType {
    none,
    equal,
    push,
    pop,
    add,
    sub,
    mul,
    div
}

public class AssignmentStatement : Statement {
    public bool isSelf;
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;
    public AssignmentType assignmentType;
    public Expression exp;
    public override string TranspileToBflat() {
        var res = new StringBuilder();
        if (isSelf) res.Append("this.");
        res.Append($"{identifier.TranspileToBflat()} ");
        if (typeDeclarationStatement != null) {
            res.Append($"{typeDeclarationStatement.TranspileToBflat()} ");
        }
        switch (assignmentType) {
            case AssignmentType.none:
                break;
            case AssignmentType.equal:
                res.Append($" = {exp.TranspileToBflat()}");
                break;
            case AssignmentType.push:
                res.Append($".Add({exp.TranspileToBflat()})");
                break;
            case AssignmentType.pop:
                res.Append($".Last({exp.TranspileToBflat()})");
                break;
            case AssignmentType.add:
                res.Append($" += {exp.TranspileToBflat()}");
                break;
            case AssignmentType.sub:
                res.Append($" -= {exp.TranspileToBflat()}");
                break;
            case AssignmentType.mul:
                res.Append($" *= {exp.TranspileToBflat()}");
                break;
            case AssignmentType.div:
                res.Append($" /= {exp.TranspileToBflat()}");
                break;
            default:
                break;
        }
        return $"{res};";
    }
}

//    // Conditional Statements: Statements that perform different actions based on conditions (e.g., if, else, switch).
//    | 'if' exp '{' statement* '}' ('else' 'if' '{' statement* '}')* ('else' '{' statement* '}')?
public class ConditionalStatement : Statement {
    public SubConditionalStatement ifConditionalStatement;
    public List<SubConditionalStatement> elseIfConditionalStatements = new();
    public SubConditionalStatement elseConditionalStatement;
    public override string TranspileToBflat() {
        return "ConditionalStatement:";
    }
}

public class SubConditionalStatement : Statement {
    public bool isElse;
    public bool isIf;
    public Expression exp;
    public List<Statement> statements;
    public override string TranspileToBflat() {
        return "SubConditionalStatement:";
    }
}

//TODO: Switch identifier with expression
//    | 'switch' exp '{' (identifier ':' statement* 'break'?)* 'default' ':' statement* 'break' '}'
public class SwitchStatement : Statement {
    public Expression exp;
    public List<SubSwitchStatement> subSwitchStatements = new();
    //default
    public SubSwitchStatement defSubSwitchStatement;
    public override string TranspileToBflat() {
        return "SwitchStatement:";
    }
}

//(identifier ':' statement* 'break'?)*
public class SubSwitchStatement : Statement {
    public IdentifierExpression identifier;
    public List<Statement> statements = new();
    public bool isBreak = false;
    public bool isDefault = false;
    public override string TranspileToBflat() {
        return "SubSwitchStatement:";
    }
}

//    // Control Flow Statements: Statements for altering the flow of execution (e.g., break, continue, goto - though this is less common).
//    | 'break' | 'continue'
public enum ControlFlowModifierType {
    @break,
    @continue
}

public class BreakContinueStatement : Statement {
    public ControlFlowModifierType ControlFlowModifierType;
    public override string TranspileToBflat() {
        return "BreakContinueStatement:";
    }
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

    public override string TranspileToBflat() {
        return "ForStatement:";
    }
}

//    // Function and Method Declaration: Statements that invoke functions or methods.
//    | 'export'? 'pub'? 'fnc' '.'? identifier'('(identifier type_decl? (',' identifier type_decl? )* )?')' ':' type_decl '{' statement* '}'
public class FunctionDeclarationStatement : Statement {
    public bool isExport;
    public bool isPublic;
    public bool isSelf;
    public IdentifierExpression identifier;
    public List<ParameterDeclaration> parameters = new();
    public List<Statement> statements = new();
    public TypeDeclarationStatement returnType;
    public override string TranspileToBflat() {
        var res = new StringBuilder();
        if (isExport) res.Append("export ");
        if (isPublic) res.Append("public ");
        if (!isSelf) res.Append("static ");
        if (returnType != null) res.Append($"{returnType.TranspileToBflat()} ");
        else res.Append("void ");

        if (identifier.isSelf) {
            res.Append($"{identifier.lastIdentifier} ");
        } else {
            res.Append($"{identifier.TranspileToBflat()}");
        }

        res.Append("(");
        if (identifier.isSelf) {
            res.Append($"this {identifier.identifier}");
            foreach (var VARIABLE in identifier.identifier) {
                res.Append($".{VARIABLE}");
            }
        }
        if (parameters != null)
            for (int i = 0; i < parameters.Count; i++) {
                var item = parameters[i];
                res.Append(item.TranspileToBflat());
                if (i < parameters.Count - 1) {
                    res.Append(", ");
                }
            }
        res.Append(") {\n");
        if (statements != null)
            for (int i = 0; i < statements.Count; i++) {
                var item = statements[i];
                res.AppendLine(item.TranspileToBflat());
            }
        res.Append("\n}");
        return $"{res}\n";
    }
}

public class ParameterDeclaration : Statement {
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;

    public override string TranspileToBflat() {
        var res = new StringBuilder();
        if (typeDeclarationStatement != null) res.Append($"{typeDeclarationStatement.TranspileToBflat()} ");
        res.Append($"{identifier.TranspileToBflat()}");
        return $"{res}";
    }
}

//    // Return Statements: Statements that return values from functions or methods.
//    | 'return' exp
public class ReturnStatement : Statement {
    public Expression exp;

    public override string TranspileToBflat() {
        return $"return {exp.TranspileToBflat()};";
    }
}

//    // Enum
//    | 'enum' identifier? type_decl? '{' enum_declariation* '}'
public class EnumDeclarationStatement : Statement {
    public TypeDeclarationStatement typeDeclarationStatement;
    public IdentifierExpression identifier;
    public List<EnumValueDeclaration> enumValueDeclarations = new();
    public Expression exp;
    public bool isPublic;

    public override string TranspileToBflat() {
        return "EnumDeclarationStatement:";
    }
}

public enum EnumValueType {
    none,
    simple,
    tuple,
    block
}

//    |(identifier ('=' exp | '(' (identifier type_decl? (',' identifier type_decl? )* )? ')' | '{'  '}') )
public class EnumValueDeclaration : Statement {
    public IdentifierExpression identifier;
    public Expression exp;
    public EnumValueType type;
    //public List<ParameterDeclaration> parameters;

    public override string TranspileToBflat() {
        return "EnumValueDeclaration:";
    }
}

//    // Error Handling: Statements for handling errors or exceptions (e.g., try-catch, throw).
//    | 'error' exp
public class ErrorStatement : Statement {
    public Expression exp;

    public override string TranspileToBflat() {
        return "ErrorStatement:";
    }
}

//    // Declaration Statements: Statements for defining types, structures, classes, and interfaces.
//    | ('def'|'type') identifier ('=' exp)?
public enum DeclarationType {
    def,
    type
}

public class DeclarationStatement : Statement {
    public DeclarationType declarationType;
    public IdentifierExpression identifier;
    public Expression exp;
    public override string TranspileToBflat() {
        return "DeclarationStatement:";
    }
}

//    //class
//    | 'pub'? 'class' identifier type_decl? '{' statement* '}'
public class StructDeclarationStatement : Statement {
    public bool isPublic;
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;

    public override string TranspileToBflat() {
        return "StructDeclarationStatement:";
    }
}

//    //class
//    | 'pub'? 'class' identifier type_decl? '{' statement '}'
public class ClassDeclarationStatement : Statement {
    public bool isPublic;
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;

    public override string TranspileToBflat() {
        var res = new StringBuilder();
        if (isPublic) res.Append("public ");
        res.Append($"class {identifier.TranspileToBflat()}");
        if (typeDeclarationStatement != null)
            res.Append(typeDeclarationStatement.TranspileToBflat());
        res.Append(" {\n");
        foreach (var item in statements) {
            res.Append(item.TranspileToBflat());
        }
        res.Append("}\n");
        return $"{res}";
    }
}

//    //interface
//    | 'interface' identifier type_decl? '{' statement '}'
public class InterfaceStatement : Statement {
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;

    public override string TranspileToBflat() {
        return "InterfaceStatement:";
    }
}

//    // union
//    | 'union' type_decl? '{' statement* '}'
public class UnionStatement : Statement {
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement; //???
    public List<Statement> statements;

    public override string TranspileToBflat() {
        return "UnionStatement:";
    }
}

//    // Expression Statements: Statements that evaluate and execute expressions.
//    // Input/Output Statements: Statements for interacting with the console or other input/output streams.
//    // Memory Management: Statements for memory allocation and deallocation (e.g., malloc, free, new, delete).
//    // Assertion Statements: Statements for specifying conditions that must be true at certain points in the program.
//    | 'assert' exp
public class AssertionStatement : Statement {
    public Expression exp;

    public override string TranspileToBflat() {
        return "AssertionStatement:";
    }
}

//    // Label Statements: Statements that mark a specific point in the code for jumping or referencing (e.g., break to label).
//    | '::' identifier
public class LabelStatement : Statement {
    public IdentifierExpression identifier;

    public override string TranspileToBflat() {
        return "LabelStatement:";
    }
}

//    | 'goto' identifier
public class GotoStatement : Statement {
    public IdentifierExpression identifier;

    public override string TranspileToBflat() {
        return "GotoStatement:";
    }
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
    public IdentifierExpression identifier;
    public TypeDeclarationType typeDeclarationType;
    public Expression exp;

    public override string TranspileToBflat() {
        var res = new StringBuilder();
        if (typeDeclarationType == TypeDeclarationType.array) {
            res.Append($"{identifier.TranspileToBflat()}");
            res.Append("[");
            if (exp != null) res.Append(exp.TranspileToBflat());
            res.Append("]");
        } else if (typeDeclarationType == TypeDeclarationType.table) {
            res.Append($"dynamic ");
        } else if (identifier is null) {
            res.Append($"object ");
        } else {
            res.Append($"{identifier.TranspileToBflat()} ");
        }
        return res.ToString();
    }
}

// // Function Call Statement
// | identifier '(' (exp (',' exp)*)? ')'
public class FunctionCallStatement : Statement {
    public IdentifierExpression identifier;
    public List<Expression> parameters;

    public override string TranspileToBflat() {
        var res = $"{identifier.TranspileToBflat()}(";
        for (var i = 0; i < parameters.Count; i++) {
            res += parameters[i].TranspileToBflat();
            if (i < parameters.Count - 1)
                res += ", ";
        }
        return $"{res});";
    }
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
    public override string TranspileToBflat() {
        var res = new StringBuilder();
        if (isSelf)
            res.Append("this.");

        res.Append(identifier);

        foreach (var VARIABLE in identifiers) {
            res.Append($".{VARIABLE}");
        }

        if (isExtension) {
            res.Append($".{lastIdentifier}");
        }

        return $"{res}";
    }
}

public interface INumber {
}

//    | number
public struct IntegerExpression : INumber {
    public Int128 int_number;
    public string RAW;
    public IntegerExpression(string val) {
        this.int_number = Int128.Parse(val);
        this.RAW = val;
    }

    public override string ToString() {
        return RAW;
    }
}

public struct FloatExpression : INumber {
    public double double_number;
    public string RAW;
    public FloatExpression(string val) {
        this.double_number = double.Parse(val);
        this.RAW = val;
    }

    public override string ToString() {
        return RAW;
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
    mul, // *
    mod, // %
    pow, // **

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
    public BinaryExpression(Expression left, BinaryOperatorType operatorType, Expression right) {
        this.left = left;
        this.right = right;
        this.operatorType = operatorType;
    }
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

public class IndexExpression : Expression {
    public Expression index;
    public Expression identifier;
    public IndexExpression(Expression index, Expression identifier) {
        this.index = index;
        this.identifier = identifier;
    }
}

//    //Pointer or Reference Expressions: Expressions that deal with memory addresses (in languages like C and C++).
//    | '&' exp
//    | '*' exp
public class PointerOrReferenceExpression : Expression {
    public bool isPointer = false;

    public bool isReference
    {
        get { return !isPointer; }
        set { isPointer = !value; }
    }

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

    public bool isIs
    {
        get { return !isAs; }
        set { isAs = !value; }
    }

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
//    | identifier '{' ( (exp (',' exp)*)? '}'
public class InstanceInitializationExpression : Expression {
    public IdentifierExpression identifier;
    public List<Expression> expressions;
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