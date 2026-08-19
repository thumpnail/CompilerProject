# Rust-ähnliches Diagnostic System - Vollständige Implementierung

## 🎯 Überblick

Ein professionelles, hochwertiges Error- und Warning-Report-System für den Parseus-Parser, inspiriert von Rusts berühmter Diagnostic-Ausgabe. Das System bietet:

- ✅ **Farbige Konsolen-Ausgabe** mit automatischer TTY-Erkennung
- ✅ **Code-Snippets** mit Kontext um die Fehlerposition
- ✅ **Visuelle Marker** unter fehlerhaften Stellen (wie Rust)
- ✅ **Mehrere Severity-Level** (Error, Warning, Note, Help)
- ✅ **Zeile/Spalte Information** für präzise Fehlerberichte
- ✅ **Performance-optimiert** mit LineColumnCache
- ✅ **Nahtlose Integration** mit bestehender Parser-Infrastruktur

## 📁 Dateistruktur

```
Parseus/src/Parser/Diagnostics/
├── DiagnosticLevel.cs           # Severity Level Enum
├── TextLocation.cs              # Zeile/Spalte Informationen
├── Diagnostic.cs                # Haupt-Diagnostic-Klasse + LineColumnCache
├── DiagnosticRenderer.cs        # Rendering-Engine mit ANSI-Farben
└── README.md                    # Detaillierte API-Dokumentation

Parseus/src/Parser/Common/
├── CancellationState.cs         # ✓ Erweitert mit Diagnostic-Collection
├── BasicAParserContext.cs       # ✓ Erweitert mit Source-Code-Tracking
└── ParseException.cs            # ✓ Erweitert mit Diagnostic-Support

Parseus/src/Parser/Implicit/
└── Parser.cs                    # ✓ Erweitert mit Helper-Methoden

Parseus/src/Example/
└── DiagnosticExample.cs         # Drei umfangreiche Demo-Methoden

Parseus/src/Tests/
└── DiagnosticTests.cs           # 10 manuelle Test-Methoden
```

## 🚀 Quick Start

### Einfache Fehlerbehandlung

```csharp
// 1. Parser-Kontext mit Quellcode erstellen
var lexResult = lexer.Lex(sourceCode);
var parserCtx = new BasicAParserContext(lexResult);
parserCtx.SetSourceCode(sourceCode);  // ← wichtig!

var state = new CancellationState();
var ctx = new BaseParserContext(parserCtx, state);

// 2. Fehler melden (Position wird automatisch erfasst)
BaseParser.ReportError(ctx, "expected identifier");
BaseParser.ReportWarning(ctx, "unused variable");

// 3. Alle Diagnostics ausgeben
BaseParser.OutputDiagnostics(ctx);
Console.WriteLine($"error: {BaseParser.GetDiagnosticSummary(ctx)}");
```

### Ausgabe-Beispiel

```
error: input
  unexpected end of expression: expected operand after '+'

  2 | let y = x +
    |             ^

note: expected operand after '+'
help: try adding a number like '5'
```

## 🔑 Haupt-APIs

### CancellationState (Erweitert)
```csharp
// Diagnostics melden
state.ReportError(message, span?, sourceLabel?);
state.ReportWarning(message, span?, sourceLabel?);
state.ReportNote(message, span?, sourceLabel?);

// Status prüfen
bool hasErrors = state.HasErrors;
bool hasWarnings = state.HasWarnings;
List<Diagnostic> diags = state.Diagnostics;
```

### BasicAParserContext (Erweitert)
```csharp
// Quellcode setzen (für Code-Snippets)
ctx.SetSourceCode(sourceCode);

// Span-Informationen ermitteln
TextSpan span = ctx.GetCurrentSpan();
TextSpan span = ctx.GetSpanAt(tokenIndex);
TextSpan span = ctx.GetSpanBetween(startIdx, endIdx);
```

### BaseParser (Helper-Methoden)
```csharp
// Fehler mit Auto-Position-Tracking melden
BaseParser.ReportError(ctx, "message");
BaseParser.ReportWarning(ctx, "message");
BaseParser.ReportNote(ctx, "message");

// Diagnostics ausgeben
BaseParser.OutputDiagnostics(ctx);
BaseParser.SetSourceCode(ctx, source);
string summary = BaseParser.GetDiagnosticSummary(ctx);
```

### DiagnosticRenderer (Static)
```csharp
// Einzelne Diagnostic rendern
string output = DiagnosticRenderer.Render(diagnostic);
DiagnosticRenderer.Output(diagnostic);

// Mehrere Diagnostics
DiagnosticRenderer.OutputAll(diagnostics);
string summary = DiagnosticRenderer.GetSummary(diagnostics);

// Mit Optionen
var opts = new DiagnosticRenderer.RenderOptions {
    UseColors = false,
    ContextLines = 3
};
DiagnosticRenderer.Output(diagnostic, opts);
```

## 💡 Beispiele

### Beispiel 1: Syntax-Fehler

```csharp
var source = @"let x = 42
let y = x + 
let z = 10";

var diag = new Diagnostic(
    new DiagnosticMessage(
        DiagnosticLevel.Error,
        "unexpected end of expression"
    ),
    "script.nano"
)
.WithSourceCode(source)
.WithMessage(DiagnosticLevel.Help, "add an expression after '+'");

DiagnosticRenderer.Output(diag);
```

### Beispiel 2: Mehrere Fehler sammeln

```csharp
var state = new CancellationState();

// Mehrere Fehler melden
state.ReportError("error 1", sourceLabel: "file1.nano");
state.ReportError("error 2", sourceLabel: "file1.nano");
state.ReportWarning("warning 1", sourceLabel: "file1.nano");

// Alle auf einmal ausgeben
DiagnosticRenderer.OutputAll(state.Diagnostics);
Console.WriteLine($"error: {DiagnosticRenderer.GetSummary(state.Diagnostics)}");
```

### Beispiel 3: Mit Quellcode-Kontext

```csharp
var source = "let x = 10\nlet y = x + ";
var tokens = LexArithmetic(source);
var ctx = new BasicAParserContext(tokens);
ctx.SetSourceCode(source);

var state = new CancellationState();
var baseCtx = new BaseParserContext(ctx, state);

// Position setzen
ctx.Pos = 5;

// Fehler melden (Position wird automatisch erfasst)
BaseParser.ReportError(baseCtx, "expected expression after '+'");

// Mit Code-Snippet ausgeben
BaseParser.OutputDiagnostics(baseCtx);
```

## 🧪 Tests Ausführen

```csharp
// Manuelle Tests starten
DiagnosticTests.RunAllTests();

// Output:
// ════════════════════════════════════════════════════════════════
//   Diagnostic System - Manual Tests
// ════════════════════════════════════════════════════════════════
// 
// Test: TextSpan Creation
// ✓ PASSED
// 
// Test: LineColumnCache
// ✓ PASSED
// 
// ...
// ════════════════════════════════════════════════════════════════
//   ✓ ALL TESTS PASSED
// ════════════════════════════════════════════════════════════════
```

## 🎨 Diagnostic-Level

| Level | Farbe | Symbol | Verwendung |
|-------|-------|--------|-----------|
| **Error** | 🔴 Red | ^ | Fatale Fehler |
| **Warning** | 🟡 Yellow | - | Potenzielle Probleme |
| **Note** | 🔵 Cyan | ~ | Zusätzliche Info |
| **Help** | 🟢 Green | * | Lösungsvorschläge |

## ⚙️ Konfiguration

```csharp
var options = new DiagnosticRenderer.RenderOptions {
    UseColors = false,        // null = Auto-detect TTY
    ContextLines = 3,         // Mehr Context-Zeilen
    MaxWidth = 80            // Schmalere Ausgabe für Terminals
};

DiagnosticRenderer.Output(diagnostic, options);
```

## 🔧 Integration in bestehende Parser

### Schritt 1: Quellcode setzen
```csharp
var context = new BasicAParserContext(tokens);
context.SetSourceCode(sourceCode);  // ← Wichtig!
```

### Schritt 2: Parser erweitern
```csharp
if (!context.MatchToken("IDENTIFIER")) {
    BaseParser.ReportError(parserCtx, "expected identifier");
    return false;
}
```

### Schritt 3: Diagnostics ausgeben
```csharp
BaseParser.OutputDiagnostics(parserCtx);
if (parserCtx.State.HasErrors) {
    Environment.Exit(1);
}
```

## 📊 Performance

### LineColumnCache
- **Speicher**: ~2-3 Bytes pro Zeile
- **Erstellung**: O(n) beim SetSourceCode()
- **Lookup**: O(log n) binary search

Für eine 10.000-Zeilen-Datei: ~25-30 KB Cache-Speicher

### Diagnostic-Sammlung
- **Non-Blocking**: Fehler werden gesammelt, nicht sofort ausgegeben
- **Batch-Output**: Alle Fehler werden am Ende zusammen ausgegeben
- **Ideal für**: IDE-Integration und Multi-Error-Reports

## 🎯 Design-Prinzipien

1. **Rust-Inspiriert**: Ähnliche Ausgabe-Format wie Rust
2. **Benutzerfreundlich**: Klare, hilfreiche Fehler mit Kontext
3. **Performance-Optimiert**: Efficient line/column lookups
4. **Nahtlos integrierbar**: Funktioniert mit bestehender Infrastruktur
5. **Konfigurierbar**: TTY-Auto-Detect, Farben-Kontrolle
6. **Typ-Sicher**: Records und Strong Typing

## 🐛 Bekannte Limitierungen

- Nur Single-Line und Basic Multi-Line Highlighting (kein komplexes Underline-Pattern)
- Keine Diagnostic-Levels in ParseException selbst (nur optional)
- ANSI-Farben nur auf Linux/Mac/Windows 10+

## 📚 Weitere Dokumentation

Siehe `Parseus/src/Parser/Diagnostics/README.md` für:
- Vollständige API-Referenz
- Erweiterte Beispiele
- Best Practices
- Integration Guide

## 🤝 Beitrag

Das System kann erweitert werden um:
- [ ] JSON-Export für IDE-Integration
- [ ] Multi-File Diagnostic-Reports
- [ ] Quick-Fix Suggestions
- [ ] Locale-Unterstützung

## ✨ Features der Implementation

| Feature | Status |
|---------|--------|
| Farbige Ausgabe | ✅ |
| TTY-Erkennung | ✅ |
| Code-Snippets | ✅ |
| Line/Column Info | ✅ |
| Multiple Levels | ✅ |
| Related Messages | ✅ |
| Summary Output | ✅ |
| Performance Cache | ✅ |
| Parser Integration | ✅ |
| Exception Support | ✅ |

---

**Version**: 1.0  
**Komponenten**: 5 neue Dateien + 3 erweiterte Dateien  
**Zeilen Code**: ~1500 LoC  
**Test Coverage**: 10 manuelle Tests  

