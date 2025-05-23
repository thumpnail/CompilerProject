using NanoScript.Parser;

using Parseus.Parser.Implicit;

using NanoScript.Parser.AstNodes;

using static NanoScript.Token;

using Parseus.Parser.ObjectBased;

public class NanoScriptParser : BaseParser {
	public record NanoProgram {}
	public record NanoStatement {}

	public override NanoProgram Parse(string src) {
		return new();
	}
	// program         = { statement } ;
	private static Parser<NanoProgram> nanoProgramParser = new((c, self) => {

	});
	// program         = { statement } ;

	// statement       = variable_declaration
	//                 | constant_declaration
	//                 | operation
	//                 | function_definition
	//                 | function_call
	//                 | jump_statement
	//                 | conditional_statement
	//                 | loop_statement
	//                 | struct_definition
	//                 | package_definition
	//                 | include_statement
	//                 | destruct_statement
	//                 | error_statement ;
	// variable_declaration = "let" identifier value { value } ;
	// constant_declaration = "cst" identifier value { value } ;
	// value           = identifier
	//                 | number
	//                 | string
	//                 | character
	//                 | array_definition ;
	// array_definition = "[" value { "," value } "]" ;
	// operation       = identifier identifier operator identifier ;
	// operator        = "+" | "-" | "*" | "/" ;
	// function_definition = "fnc" identifier [ parameter_list ] block "ret" [ return_value { return_value } ] ;
	// parameter_list  = identifier { identifier } ;
	// return_value    = identifier | value ;
	// function_call   = "cll" identifier { identifier | value } ;
	// jump_statement  = "jmp" identifier | ":" identifier ;
	// conditional_statement = "iff" condition block [ "elf" condition block ] [ "els" block ] "ext" ;
	// condition       = identifier logical_operator identifier ;
	// logical_operator = "EQL" | "LES" | "GRT" | "TRU" | "FLS" ;
	// loop_statement  = "whl" condition block "ext"
	//                 | "for" identifier value condition value value block "ext"
	//                 | "for" identifier identifier block "ext" ;
	// struct_definition = "tbl" identifier [ parameter_list ] block "ext" ;
	// package_definition = "pck" identifier block "ext" ;
	// include_statement = "inc" identifier [ identifier ] ;
	// destruct_statement = "~" identifier ;
	// error_statement = "err" number [ string ] ;
	// block           = { statement } ;
	// identifier      = ? any valid identifier ? ;
	// number          = ? any valid number ? ;
	// string          = '"' ? any valid string content ? '"' ;
	// character       = "'" ? any valid character ? "'" ;
}