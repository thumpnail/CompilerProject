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
    public override string GenBflat() {
        var res = new StringBuilder();
        foreach (var item in moduleStatements) {
            res.Append(item.GenBflat());
        }
        return $"{res}\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

// 
// ModuleStatement: 'mod' identifier ImportStatement* ( '{' statement* '}' | statement* )?;
public class ModuleStatement : Statement {
    public IdentifierExpression moduleName;
    public List<ImportStatement> importStatements;
    public bool isSubModule;
    public List<Statement> statements;
    public override string GenBflat() {
        var res = new StringBuilder();
        foreach (var item in importStatements) {
            res.Append(item.GenBflat());
        }
        res.Append($"namespace module_{moduleName.GenBflat()} {{\n");
        foreach (var item in statements) {
            res.Append(item.GenBflat());
        }
        res.Append("\n}");
        return $"{res}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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

    public override string GenBflat() {
        var res = new StringBuilder();
        if (isAs) {
            if (importString.EndsWith(".nano")) {
                //TODO: Namepace Magic
            } else {
                //Guessing that it is a c# namepace
                res.Append($"using {identifier.GenBflat()} = {importString.Substring(1, importString.Length - 2)};");
            }
        } else {
            res.Append($"using {importString};");
        }
        // TODO: parse nano files
        return $"{res}\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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
    public override string GenBflat() {
        var res = new StringBuilder();
        if (isPublic) res.Append("public ");
        if (prefix == "let") res.Append("readonly ");
        else if (prefix == "const") res.Append("const ");

        if (typeDeclarationStatement != null) {
            res.Append($"{typeDeclarationStatement.GenBflat()} ");
        } else {
            res.Append($"var ");
        }

        res.Append($"{identifier.GenBflat()}");
        if (isAssign) res.Append($" = {exp.GenBflat()}");
        res.Append(";");

        return $"{res}\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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
    public override string GenBflat() {
        var res = new StringBuilder();
        if (isSelf) res.Append("this.");
        res.Append($"{identifier.GenBflat()} ");
        if (typeDeclarationStatement != null) {
            res.Append($"{typeDeclarationStatement.GenBflat()} ");
        }
        switch (assignmentType) {
            case AssignmentType.none:
                break;
            case AssignmentType.equal:
                res.Append($" = {exp.GenBflat()}");
                break;
            case AssignmentType.push:
                res.Append($".Add({exp.GenBflat()})");
                break;
            case AssignmentType.pop:
                res.Append($".Last({exp.GenBflat()})");
                break;
            case AssignmentType.add:
                res.Append($" += {exp.GenBflat()}");
                break;
            case AssignmentType.sub:
                res.Append($" -= {exp.GenBflat()}");
                break;
            case AssignmentType.mul:
                res.Append($" *= {exp.GenBflat()}");
                break;
            case AssignmentType.div:
                res.Append($" /= {exp.GenBflat()}");
                break;
            default:
                break;
        }
        return $"{res};";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    // Conditional Statements: Statements that perform different actions based on conditions (e.g., if, else, switch).
//    | 'if' exp '{' statement* '}' ('else' 'if' '{' statement* '}')* ('else' '{' statement* '}')?
public class ConditionalStatement : Statement {
    public SubConditionalStatement ifConditionalStatement;
    public List<SubConditionalStatement> elseIfConditionalStatements = new();
    public SubConditionalStatement elseConditionalStatement;
    public override string GenBflat() {
        var res = $"{ifConditionalStatement.GenBflat()}";
        foreach (var statement in elseIfConditionalStatements) {
            res += $"{statement.GenBflat()}";
        }
        res += $"{elseConditionalStatement?.GenBflat()}";
        return res;
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

public class SubConditionalStatement : Statement {
    public bool isElse;
    public bool isIf;
    public Expression exp;
    public List<Statement> statements = new();
    public override string GenBflat() {
        var res = $"{(isElse ? "else" : "")} {(isIf ? $"if({exp.GenBflat()})" : "")} {{";
        foreach (var statement in statements) {
            res += $"{statement.GenBflat()}";
        }
        res += "\n}";
        return $"{res}\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//TODO: Switch identifier with expression
//    | 'switch' exp '{' (identifier ':' statement* 'break'?)* 'default' ':' statement* 'break' '}'
public class SwitchStatement : Statement {
    public Expression exp;
    public List<SubSwitchStatement> subSwitchStatements = new();
    //default
    public SubSwitchStatement defSubSwitchStatement;
    public override string GenBflat() {
        return "//SwitchStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//(identifier ':' statement* 'break'?)*
public class SubSwitchStatement : Statement {
    public IdentifierExpression identifier;
    public List<Statement> statements = new();
    public bool isBreak = false;
    public bool isDefault = false;
    public override string GenBflat() {
        return "//SubSwitchStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    // Control Flow Statements: Statements for altering the flow of execution (e.g., break, continue, goto - though this is less common).
//    | 'break' | 'continue'
public enum ControlFlowModifierType {
    @break,
    @continue
}

public class BreakContinueStatement : Statement {
    public ControlFlowModifierType ControlFlowModifierType;
    public override string GenBflat() {
        return "//BreakContinueStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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

    public override string GenBflat() {
        var res = "";
        if (type == ForType.For) {
            res = $"for(int {exp_def.GenBflat()}; {exp_cond.GenBflat()}; {exp_incr.GenBflat()}) {{\n";
        } else if (type == ForType.While) {
            res = $"while({(exp_cond is null? "true" : exp_cond.GenBflat())}) {{\n";
        } else if (type == ForType.ForIn) {
            res = $"foreach(var {elementIdentifier.GenBflat()} in {listIdentifier.GenBflat()}) {{\n";
        }
        foreach (var statement in statements) {
            res += $"{statement.GenBflat()};\n";
        }
        res += "}";
        return res;
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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
    public override string GenBflat() {
        var res = new StringBuilder();
        if (isExport) res.Append("export ");
        if (isPublic) res.Append("public ");
        if (!isSelf) res.Append("static ");
        if (returnType != null) res.Append($"{returnType.GenBflat()} ");
        else res.Append("void ");

        if (identifier.isSelf) {
            res.Append($"{identifier.lastIdentifier} ");
        } else {
            res.Append($"{identifier.GenBflat()}");
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
                res.Append(item.GenBflat());
                if (i < parameters.Count - 1) {
                    res.Append(", ");
                }
            }
        res.Append(") {\n");
        if (statements != null)
            for (int i = 0; i < statements.Count; i++) {
                var item = statements[i];
                res.AppendLine(item.GenBflat());
            }
        res.Append("\n}");
        return $"{res}\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

public class ParameterDeclaration : Statement {
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;

    public override string GenBflat() {
        var res = new StringBuilder();
        if (typeDeclarationStatement != null) res.Append($"{typeDeclarationStatement.GenBflat()} ");
        res.Append($"{identifier.GenBflat()}");
        return $"{res}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    // Return Statements: Statements that return values from functions or methods.
//    | 'return' exp
public class ReturnStatement : Statement {
    public Expression exp;

    public override string GenBflat() {
        return $"return {exp.GenBflat()};";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    // Enum
//    | 'enum' identifier? type_decl? '{' enum_declariation* '}'
public class EnumDeclarationStatement : Statement {
    public TypeDeclarationStatement typeDeclarationStatement;
    public IdentifierExpression identifier;
    public List<EnumValueDeclaration> enumValueDeclarations = new();
    // public Expression exp;
    public bool isPublic;

    public override string GenBflat() {
        var res = $"{(isPublic ? "public" : "")} enum {identifier.GenBflat()} {typeDeclarationStatement?.GenBflat()} {{\n";
        foreach (var item in enumValueDeclarations) {
            res += $"{item.GenBflat()}\n";
        }
        res += "}\n";
        return res;
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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

    public override string GenBflat() {
        return $"{identifier.GenBflat()} {(((exp is null? "":"= ")+exp?.GenBflat())??"")},";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    // Error Handling: Statements for handling errors or exceptions (e.g., try-catch, throw).
//    | 'error' exp
public class ErrorStatement : Statement {
    public Expression exp;

    public override string GenBflat() {
        return "//ErrorStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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
    public override string GenBflat() {
        return "//DeclarationStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //class
//    | 'pub'? 'class' identifier type_decl? '{' statement* '}'
public class StructDeclarationStatement : Statement {
    public bool isPublic;
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;

    public override string GenBflat() {
        return "//StructDeclarationStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //class
//    | 'pub'? 'class' identifier type_decl? '{' statement '}'
public class ClassDeclarationStatement : Statement {
    public bool isPublic;
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;

    public override string GenBflat() {
        var res = new StringBuilder();
        Console.WriteLine($"CLASSDEF: pub:{isPublic}, id:{identifier.GenBflat()}");
        if (isPublic) res.Append("public ");
        res.Append($"class {identifier.GenBflat()}");
        if (typeDeclarationStatement != null)
            res.Append(typeDeclarationStatement.GenBflat());
        res.Append(" {\n");
        foreach (var item in statements) {
            res.Append(item.GenBflat());
        }
        res.Append("}\n");
        return $"{res}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //interface
//    | 'interface' identifier type_decl? '{' statement '}'
public class InterfaceStatement : Statement {
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;

    public override string GenBflat() {
        return "//InterfaceStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    // union
//    | 'union' type_decl? '{' statement* '}'
public class UnionStatement : Statement {
    public IdentifierExpression identifier;
    public TypeDeclarationStatement typeDeclarationStatement; //???
    public List<Statement> statements;

    public override string GenBflat() {
        return "//UnionStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    // Expression Statements: Statements that evaluate and execute expressions.
//    // Input/Output Statements: Statements for interacting with the console or other input/output streams.
//    // Memory Management: Statements for memory allocation and deallocation (e.g., malloc, free, new, delete).
//    // Assertion Statements: Statements for specifying conditions that must be true at certain points in the program.
//    | 'assert' exp
public class AssertionStatement : Statement {
    public Expression exp;

    public override string GenBflat() {
        return "//AssertionStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    // Label Statements: Statements that mark a specific point in the code for jumping or referencing (e.g., break to label).
//    | '::' identifier
public class LabelStatement : Statement {
    public IdentifierExpression identifier;

    public override string GenBflat() {
        return "//LabelStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    | 'goto' identifier
public class GotoStatement : Statement {
    public IdentifierExpression identifier;

    public override string GenBflat() {
        return "//GotoStatement:\n";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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

    public override string GenBflat() {
        var res = new StringBuilder();
        if (typeDeclarationType == TypeDeclarationType.array) {
            res.Append($"{identifier.GenBflat()}");
            res.Append("[");
            if (exp != null) res.Append(exp.GenBflat());
            res.Append("]");
        } else if (typeDeclarationType == TypeDeclarationType.table) {
            res.Append($"dynamic ");
        } else if (identifier is null) {
            res.Append($"object ");
        } else {
            res.Append($"{identifier.GenBflat()} ");
        }
        return res.ToString();
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

// // Function Call Statement
// | identifier '(' (exp (',' exp)*)? ')'
public class FunctionCallStatement : Statement {
    public IdentifierExpression identifier;
    public List<Expression> parameters;

    public override string GenBflat() {
        var res = $"{identifier.GenBflat()}(";
        for (var i = 0; i < parameters.Count; i++) {
            res += parameters[i].GenBflat();
            if (i < parameters.Count - 1)
                res += ", ";
        }
        return $"{res});";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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
    public override string GenBflat() {
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
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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
    public override string ToString() {
        return number.ToString();
    }
    public override string GenBflat() {
        return $"{this}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    | string
public class StringExpression : Expression {
    public string str;
    public override string ToString() {
        return str;
    }
    public override string GenBflat() {
        return $"{this}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    | ('true'|'false')
public class BooleanExpression : Expression {
    public bool value;
    public override string ToString() {
        return value.ToString();
    }
    public override string GenBflat() {
        return $"{this}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //Grouping Expression:
//    | '(' exp ')'
public class GroupingExpression : Expression {
    public Expression exp;
    public override string GenBflat() {
        return $"({exp.GenBflat()})";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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
    public override string GenBflat() {
        return $"{left.GenBflat()} {GetBinOp(operatorType)} {right.GenBflat()}";
    }
    public string GetBinOp(BinaryOperatorType type) {
        return type switch {
            BinaryOperatorType.add => "+",
            BinaryOperatorType.sub => "-",
            BinaryOperatorType.div => "/",
            BinaryOperatorType.mul => "*",
            BinaryOperatorType.mod => "%",
            BinaryOperatorType.pow => "**",
            BinaryOperatorType.doubleOr => "||",
            BinaryOperatorType.doubleAnd => "&&",
            BinaryOperatorType.equals => "==",
            BinaryOperatorType.notEquals => "!=",
            BinaryOperatorType.lessEquals => "<=",
            BinaryOperatorType.less => "<",
            BinaryOperatorType.greaterEquals => ">=",
            BinaryOperatorType.greater => ">",
            BinaryOperatorType.and => "&",
            BinaryOperatorType.or => "|",
            BinaryOperatorType.xor => "^",
            BinaryOperatorType.shl => "<<",
            BinaryOperatorType.shr => ">>",
            BinaryOperatorType.not => "~",
            _ => type.ToString()
        };
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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
    public override string GenBflat() {
        if(isBefore)
            return $"{operatorType} {exp.GenBflat()}";
        return $"{exp.GenBflat()} {operatorType}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

public class ExpressionList : Expression {
    public List<Expression> expressions = new();
    public override string GenBflat() {
        var res = "";
        for (int i = 0; i < expressions.Count(); i++) {
            res += expressions[i].GenBflat();
            if(i < expressions.Count()) res += ",";
        }
        return res;
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

public class IndexExpression : Expression {
    public Expression index;
    public Expression identifier;
    public IndexExpression(Expression index, Expression identifier) {
        this.index = index;
        this.identifier = identifier;
    }
    public override string GenBflat() {
        return $"{identifier.GenBflat()}[{index.GenBflat()}]";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //Pointer or Reference Expressions: Expressions that deal with memory addresses (in languages like C and C++).
//    | '&' exp
//    | '*' exp
public class PointerOrReferenceExpression : Expression {
    public bool isPointer = false;
    public Expression exp;
    public bool isReference {
        get { return !isPointer; }
        set { isPointer = !value; }
    }
    public override string GenBflat() {
        return $"{(isPointer ? "*" : "&")}{exp.GenBflat()}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //Function or Method Calls: Expressions that invoke functions or methods.
//    | identifier '(' (exp (',' exp)*)? ')'
public class FunctionCallExpression : Expression {
    public Expression identifier;
    public List<Expression> parameters;
    public override string GenBflat() {
        var paras = "";
        foreach (var item in parameters) {
            paras += $"{item.GenBflat()},";
        }
        paras.Remove(paras.Length-2);
        return $"{identifier.GenBflat()}({paras})";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //Conditional Expressions: Expressions that result in different values based on conditions (e.g., ternary operator).
//    //Array and Indexing Expressions: Expressions to access elements in arrays or collections.
//    | identifier '[' (exp | exp '..' exp) ']'
public class ArrayIndexingExpression : Expression {
    public Expression identifier;
    public Expression index;
    public bool isRange;
    public (Expression, Expression) range;
    public override string GenBflat() {
        var res = "";
        if(!isRange) {
            res = index.GenBflat();
        } else {
            res = $"{range.Item1.GenBflat()} .. {range.Item2.GenBflat()}";
        }

        return $"{identifier.GenBflat()}[{res}]";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //Property Access Expressions: Expressions used to access properties of objects or structures.
//    //Assignment Expressions: Expressions that assign values to variables or locations.
//    //Type Conversion Expressions: Expressions that convert values between different data types.
//    | '(' identifier ')' exp
public class TypeConversionExpression : Expression {
    public IdentifierExpression identifier;
    public Expression exp;
    public override string GenBflat() {
        return $"({identifier.GenBflat()}){exp.GenBflat()}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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

    public bool isIs {
        get { return !isAs; }
        set { isAs = !value; }
    }
    public IdentifierExpression identifier;
    public override string GenBflat() {
        return $"{exp.GenBflat()} {(isAs ? "as" : "is")} {identifier.GenBflat()}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //Tuple Creation Expression:
//    | '(' exp (',' exp) ')'
[MissingFeature("Tuple")]
public class TupleCreationExpression : Expression {
    public Expression exp;
    public List<Expression> expressions;
    public override string GenBflat() {
        var res = "";
        foreach(var item in expressions) {
            res += $"{item.GenBflat()},";
        }
        res.Remove(res.Length-2);
        return $"({res})";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //Array Creation Expressions: Expressions for creating new arrays or collections.
//    | '[' exp (',' exp) ']'
public class ArrayCreationExpression : Expression {
    public List<Expression> expressions;
    public override string GenBflat() {
        var res = "";
        foreach(var item in expressions) {
            res += $"{item.GenBflat()},";
        }
        res.Remove(res.Length-2);
        return $"[{res}]";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //Array Creation Expression
//    | identifier '{' ( (exp (',' exp)*)? '}'
public class InstanceInitializationExpression : Expression {
    public IdentifierExpression identifier;
    public List<Expression> expressions;
    public override string GenBflat() {
        return "WHY JUST WHY";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}

//    //Function Creation Expression
//    | 'fnc' '(' (identifier type_decl? (',' identifier type_decl? )* )? ')' type_decl? '{' statement* '}'
public class FunctionCreationExpression : Expression {
    public List<ParameterDeclaration> parameters;
    public TypeDeclarationStatement typeDeclarationStatement;
    public List<Statement> statements;
    public override string GenBflat() {
        // ((Func<string>)item.GenBflat).Invoke(); <- this is valid c#
        var paras = "";
        var block = "";
        return $"({paras}) => {{\n{block}\n}}";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
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
    public override string GenBflat() {
        var paras = "";
        foreach(var item in parameters) {
            paras += item.GenBflat()+",";
        }
        paras.Remove(paras.Length-2);
        return $"{functionName}()";
    }
    public override List<int> TranspileToByteCode() {throw new NotImplementedException();}
    public override List<string> TranspileToAsm() {throw new NotImplementedException();}
}
//    ; ANOMALY
//      Ylamona.AiE