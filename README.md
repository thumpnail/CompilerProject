# CompilerProject (AI Gen Readme)

A compiler and language toolchain written in C# (.NET 9), consisting of the NanoScript programming language, its compiler pipeline, and the Parseus parser framework that powers it.

---

## Table of Contents

- [CompilerProject (AI Gen Readme)](#compilerproject-ai-gen-readme)
	- [Table of Contents](#table-of-contents)
	- [Overview](#overview)
	- [Repository Structure](#repository-structure)
	- [Projects](#projects)
		- [NanoScript](#nanoscript)
		- [NanoScript.Tiny](#nanoscripttiny)
		- [Parseus](#parseus)
		- [Parseus.Ini](#parseusini)
		- [Parseus.Sbnf](#parseussbnf)
	- [NanoScript Language](#nanoscript-language)
		- [Syntax Overview](#syntax-overview)
		- [Code Examples](#code-examples)
	- [Building](#building)
	- [Diagnostic System](#diagnostic-system)
	- [Tools](#tools)
		- [ParseTester (`tools/parse_tester/`)](#parsetester-toolsparse_tester)
	- [Project Status](#project-status)

---

## Overview

CompilerProject is a monorepo containing:

- **NanoScript** -- A statically-typed, module-based scripting language that compiles to C# source code. The pipeline covers lexing, parsing to an AST, and C# code generation.
- **Parseus** -- A general-purpose recursive-descent parser framework used as the foundation for all parsing work in this repository.
- **NanoScript.Tiny** -- A minimal, self-contained dialect of NanoScript built directly on top of Parseus.
- Supporting libraries for parsing INI files and SBNF grammars.

The entire solution targets **.NET 9** and is authored in **C# 13**.

---

## Repository Structure

```
CompilerProject/
├── CompilerProject.sln          # Solution file
├── NanoScript/                  # Main NanoScript compiler
│   ├── src/
│   │   └── NanoScript/
│   │       ├── Lexer/           # Tokenizer
│   │       ├── Parser/          # Recursive-descent parser + AST nodes
│   │       └── ByteCompiler/    # Bytecode generation (in progress)
│   ├── stdlib/                  # Standard library (System.nano)
│   ├── syntax/                  # Formal grammar definitions (EBNF / SBNF)
│   └── sample/                  # Example .nano source files
├── NanoScript.Tiny/             # Minimal NanoScript dialect
├── Parseus/                     # Parser framework
├── Parseus.Ini/                 # INI file parser built on Parseus
├── Parseus.Sbnf/                # SBNF grammar parser built on Parseus
└── tools/
    └── parse_tester/            # CLI tool for exercising the parser
```

---

## Projects

### NanoScript

The main compiler project (`NanoScript/NanoScript.csproj`).

**Pipeline:**

1. **Lexer** -- Regex-driven tokenizer (`Lexer.cs`) that produces a stream of typed tokens.
2. **Parser** -- A hand-written recursive-descent parser that builds a typed AST from the token stream. The AST covers the full language: modules, classes, structs, enums, interfaces, functions, expressions, and statements.
3. **Code Generation** -- Each AST node implements `GenCS()` to emit equivalent C# source code.
4. **Formatting** -- The generated C# is pretty-printed using the Roslyn `Formatter` API before being written to disk.

**Output files written per compilation run:**

| File | Contents |
|------|----------|
| `output_tokens.txt` | Token dump from the lexer |
| `output_ast.json` | AST serialized as JSON |
| `output_ast.yaml` | AST serialized as YAML |
| `output_bflat.csx` | Generated and formatted C# source |

**Key dependencies:**

| Package | Purpose |
|---------|---------|
| `Microsoft.CodeAnalysis.CSharp` | Roslyn -- C# parsing and formatting of the generated output |
| `YamlDotNet` | YAML serialization of the AST |
| `Newtonsoft.Json` | JSON serialization of the AST |
| `BetterConsoleTables` | Pretty-print token tables during development |

---

### NanoScript.Tiny

A stripped-down NanoScript dialect (`NanoScript.Tiny/`) built directly on top of Parseus. It uses a more minimal keyword set (`def`, `fnc`, `let`, `iff`, `whl`, etc.) and serves as a lightweight testbed for parser experiments. It depends on `NanoScript` and reuses its lexer infrastructure.

---

### Parseus

The parser framework (`Parseus/`) that underpins all parsing in this repository.

**Core abstractions:**

| Class | Role |
|-------|------|
| `Lexer` | Builds a token stream from a source string using ordered regex rules |
| `BasicAParserContext` | Holds the token stream, current position, and source code for span tracking |
| `CancellationState` | Collects diagnostics (errors, warnings, notes) during parsing |
| `BaseParser` | Base class for parser implementations; provides combinators and diagnostic helpers |
| `ParseException` | Carries a structured `Diagnostic` when a fatal parse error is raised |

**Parser combinators available in `BaseParser`:**

- `Token` -- Match and consume a specific token type
- `Opt` -- Optional match (zero or one)
- `Repeat` / `RepeatOpt` -- One-or-more / zero-or-more
- `Alt` -- Ordered alternation (first match wins)
- `Node` -- Parse into a typed result node

---

### Parseus.Ini

An INI file parser (`Parseus.Ini/IniParser.cs`) implemented on top of the Parseus `Lexer`. Handles sections, key-value pairs, and comments.

---

### Parseus.Sbnf

A parser for SBNF (Simple BNF) grammar files (`Parseus.Sbnf/`). This allows grammar definitions to be loaded and used at runtime, and is used to drive grammar-based parsing experiments referenced from NanoScript.

---

## NanoScript Language

### Syntax Overview

NanoScript is a statically-typed, module-based language. Every source file begins with a `mod` declaration.

**Type system:**

- Primitive types: `int`, `float`, `bool`, `string`
- Composite: `class`, `struct`, `enum`, `interface`, `union`
- Arrays: `[]type` (e.g., `[]string`)

**Visibility:**

- `pub` -- exported from the module
- (no modifier) -- module-private

**Variable declarations:**

| Keyword | Semantics |
|---------|-----------|
| `let` | Immutable binding |
| `var` | Mutable variable |
| `const` | Compile-time constant |

**Control flow:** `if` / `else if` / `else`, `for`, `switch` / `default` / `break`, `return`, `goto`/label, `assert`, `error`

**Functions** are declared with `fnc`. An optional `export` modifier makes them available across modules.

**Formal grammar** (EBNF) is located at `NanoScript/syntax/nanoscript.ebnf`.

---

### Code Examples

**Hello World**

```nanoscript
mod main
import System

pub class Program {
    pub fnc Main(args: []string): int {
        println("Hello World")
        return 0
    }
}
```

**Variables and arithmetic**

```nanoscript
mod main

fnc main() {
    var i = 2
    var r = 0
    while(++i <= 1000) {
        if(i % 3 == 0 || i % 5 == 0) {
            r += i
        }
    }
    print(r)
}
```

**Classes and enums**

```nanoscript
mod main

pub class Program {
    enum ctype { a, b, c }

    let somevalue: int

    pub fnc checks(flag: bool): int {
        if flag {
            somevalue = 4
        } else if somevalue == 0 {
            somevalue = 3
        }
        return somevalue
    }
}
```

**Imports**

```nanoscript
mod main
import System                   // search stdlib dir
import "System.nano"            // explicit path
import "System.Console" as con  // aliased import
import MyLib from "libs"        // named import from path
```

---

## Building

**Prerequisites:** .NET 9 SDK

```bash
# Restore dependencies and build the entire solution
dotnet build CompilerProject.sln

# Run the main NanoScript compiler on a source file
dotnet run --project NanoScript -- path/to/file.nano

# Run the NanoScript.Tiny parser
dotnet run --project NanoScript.Tiny
```

Build artifacts and generated files are written to the project root directory when running the compiler.

---

## Diagnostic System

Parseus includes a production-quality, Rust-inspired diagnostic reporting system. It provides structured error, warning, note, and help messages with source locations, colored output, and code snippets.

**Severity levels:**

| Level | Usage |
|-------|-------|
| `Error` | Fatal parse errors that prevent compilation |
| `Warning` | Potential issues that do not stop compilation |
| `Note` | Additional context attached to an error or warning |
| `Help` | Actionable suggestions for fixing an error |

**Example output:**

```
error: script.nano
  unexpected end of expression: expected operand after '+'

  2 | let y = x +
    |             ^

note: expected operand after '+'
help: try adding a number like '5'

error: aborting due to 1 error
```

**Quick usage:**

```csharp
// Set up context
var ctx = new BasicAParserContext(lexResult);
ctx.SetSourceCode(sourceCode);

var state = new CancellationState();
var baseCtx = new BaseParserContext(ctx, state);

// Report diagnostics (position is tracked automatically)
BaseParser.ReportError(baseCtx, "expected identifier");
BaseParser.ReportWarning(baseCtx, "unused variable");

// Output all collected diagnostics
BaseParser.OutputDiagnostics(baseCtx);

if (state.HasErrors) {
    Console.WriteLine(BaseParser.GetDiagnosticSummary(baseCtx));
}
```

Diagnostic rendering supports ANSI colors with automatic TTY detection and is configurable via `DiagnosticRenderer.RenderOptions`.

Detailed documentation is available in:

- `DIAGNOSTIC_SYSTEM_GUIDE.md` -- full API reference and design notes
- `INTEGRATION_GUIDE.md` -- step-by-step integration instructions
- `IMPLEMENTATION_SUMMARY.md` -- summary of the implementation

---

## Tools

### ParseTester (`tools/parse_tester/`)

A command-line utility for testing the parser against source files or inline snippets. Build and run it with:

```bash
dotnet run --project tools/parse_tester
```

---

## Project Status

NanoScript is under active development. Current state:

| Component | Status |
|-----------|--------|
| Lexer | Complete |
| Parser / AST | Complete for core language constructs |
| C# Code Generation | Functional for expressions and basic statements |
| Bytecode Compiler | In progress |
| Runtime | Planned |
| Standard Library | Minimal (`System.nano`) |
| NanoScript.Tiny | Functional for expressions and control flow |
| Parseus Framework | Stable |
| Diagnostic System | Complete |
