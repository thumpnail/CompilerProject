using System.IO;
using System;
using Antlr4Ast;
using System.Reflection.Metadata.Ecma335;

var input = File.ReadAllText(@"C:\Users\fried\OneDrive\Dokumente\Code\Rider Projects\CompilerProject\NanoScriptCompiler-Bflat\syntax\NanoScript.g4");
//var input = File.ReadAllText("..\\Antlr4ParserGenerator\\FictionalLanguage.g4");
// Parse the grammar
var grammar = Grammar.Parse(input);
// Print the grammar

var vis = new GenVisitor(grammar);
vis.Generate();
string output = vis.Build();
Console.Clear();
//Console.WriteLine(vis.Builder.ToString());
//Console.WriteLine("=============================================================================================");
Console.WriteLine(output);