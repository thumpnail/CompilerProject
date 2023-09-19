grammar FictionalLanguage;

program: module_statement+;

module_statement: 'pub'? 'mod' identifier imoport_statements* ( '{' statement* '}' | statement* )?;
imoport_statements: 'import' string ('as' identifier)? | 'import' identifier 'from' string;

statement
   // Variable Declaration and Assignment: Statements to declare and assign values to variables.
   : 'pub'? ('let'|'var'|'const')? '.'? identifier type_decl? ('=' exp)?
   // assignment Statement
   | '.'? identifier type_decl? ('=' exp | '<<' exp | '>>' exp | '+=' exp | '-=' exp | '*=' exp | '/=' exp)?
   // Conditional Statements: Statements that perform different actions based on conditions (e.g., if, else, switch).
   | 'if' exp '{' statement* '}' ('else' 'if' '{' statement* '}')* ('else' '{' statement* '}')?
   | 'switch' exp '{' (identifier ':' statement* 'break'?)* 'default' ':' statement* 'break' '}'
   // Looping Statements: Statements that repeat a block of code multiple times (e.g., for, while, do-while).
   | 'for' (identifier 'in' identifier | identifier '=' exp ';' exp ';' exp | exp) '{' statement* '}'
   // Function and Method Declaration: Statements that invoke functions or methods.
   | 'export'? 'pub'? 'fnc' '.'? identifier'('(identifier type_decl? (',' identifier type_decl? )* )?')' '{' statement* '}'
   // Return Statements: Statements that return values from functions or methods.
   | 'return' exp
   // Enum
   | 'enum' type_decl? '{' (identifier ('=' exp|'(' (identifier type_decl? (',' identifier type_decl? )* )? ')'|'{'  '}') )* '}'
   // Error Handling: Statements for handling errors or exceptions (e.g., try-catch, throw).
   | 'error' exp
   // Control Flow Statements: Statements for altering the flow of execution (e.g., break, continue, goto - though this is less common).
   | 'break' | 'continue' 
   | '::' identifier
   | 'goto' identifier
   // Declaration Statements: Statements for defining types, structures, classes, and interfaces.
   | ('def'|'type') identifier ('=' exp)?
   //struct
   | 'pub'? 'struct' identifier type_decl? '{' statement* '}'
   //class
   | 'pub'? 'class' identifier type_decl? '{' statement* '}'
   //interface
   | 'interface' identifier type_decl? '{' statement* '}'
   // union
   | 'union' type_decl? '{' statement* '}'
   // type definition
   | 'type' identifier '=' exp
   // Expression Statements: Statements that evaluate and execute expressions.
   // Memory Management: Statements for memory allocation and deallocation (e.g., malloc, free, new, delete).
   | 'new' exp
   | 'assert' exp
   ;

type_decl: ':' ('[' (identifier|number)? ']'|'{}')? identifier;

exp
   // 'match' '(' exp ')' '{' exp '=>' exp ('|' exp '=>' exp)* '}'
   //Literal Expressions: Constants representing specific values (e.g., numbers, strings, boolean values).
   : identifier
   | number
   | string
   | ('true'|'false')
   //Variable References: Expressions that refer to variables or memory locations.
   | '&'identifier
   //Arithmetic Expressions: Expressions that perform mathematical operations (e.g., addition, subtraction).
   | exp '+' exp
   | exp '-' exp
   | exp '/' exp
   | exp '*' exp
   //Grouping Expression:
   | '(' exp ')'
   //Logical Expressions: Expressions that involve logical operations (e.g., AND, OR).
   | exp '||' exp
   | exp '&&' exp
   //Comparison Expressions: Expressions that compare values (e.g., greater than, equal to).
   | exp '==' exp
   | exp '!=' exp
   | exp '<=' exp
   | exp '<' exp
   | exp '>=' exp
   | exp '>' exp
   //Function or Method Calls: Expressions that invoke functions or methods.
   | identifier '(' (exp (',' exp)*)? ')'
   //Conditional Expressions: Expressions that result in different values based on conditions (e.g., ternary operator).
   //Array and Indexing Expressions: Expressions to access elements in arrays or collections.
   | identifier '[' (exp | exp '..' exp) ']'
   //Member Access Expressions: Expressions to access properties or methods of objects or structures.
   | '.' identifier? ('.' identifier)* (':' identifier)?
   //Property Access Expressions: Expressions used to access properties of objects or structures.
   //Assignment Expressions: Expressions that assign values to variables or locations.
   //Type Conversion Expressions: Expressions that convert values between different data types.
   | '(' identifier ')' exp
   //Bitwise Expressions: Expressions that manipulate individual bits within values.
   | exp '&' exp
   | exp '|' exp
   | exp '^' exp
   | exp '<<' exp
   | exp '>>' exp
   | exp '~' exp
   //Unary Expressions: Expressions involving a single operand (e.g., negation, logical NOT).
   | '!' exp
   | '-' exp
   | '++' exp
   | exp '++'
   | '--' exp
   | exp'--'
   //Pointer or Reference Expressions: Expressions that deal with memory addresses (in languages like C and C++).
   | '&' exp
   | '*' exp
   //Regex or String Matching Expressions: Expressions used for pattern matching in strings.
   //Type Check Expressions: Expressions used to check the type of an object.
   | exp 'is' identifier
   //Array Creation Expressions: Expressions for creating new arrays or collections.
   | '[' exp (',' exp) ']'
   //Function Creation Expression
   | 'fnc' '(' (identifier type_decl? (',' identifier type_decl? )* )? ')' type_decl? '{' statement* '}'
   // internal functions
   | 'type' '(' exp ')'
   | 'size' '(' exp ')'
   | 'str' '(' exp ')'
   ;

string: '"' ANY '"';
ANY: [.];
identifier: WORD ('.' WORD)* (':' WORD);
WORD: [a-zA-Z_]*;
number: DIGIT* decimal?;
decimal: '.' DIGIT*;
DIGIT: [0-9];