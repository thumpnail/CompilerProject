# Fehlende Elemente für NanoScript MVP

Basierend auf der Analyse der `NanoScript.ebnf`-Grammatik und der aktuellen Implementierung in den Parser-Dateien, sind folgende Punkte für ein Minimum Viable Product (MVP) noch offen:

---

## Fehlende Elemente im Parser

### 1. `block`
- **Regel**: `{ statement }`
- **Status**: Nicht explizit als eigenständige Regel implementiert.
- **Lösung**: Implementiere eine eigenständige Methode für `block`, die eine Liste von Statements parst.

### 2. `type_decl`
- **Regel**: `':' identifier`
- **Status**: Teilweise implementiert, aber es fehlen Tests und Validierungen.
- **Lösung**: Ergänze Tests und Validierungen für `type_decl`.

### 3. `assignment_operator`
- **Regel**: `assignment_operator := '=' | '<<' | '>>' | '+=' | '-=' | '*=' | '/='`
- **Status**: Teilweise implementiert, aber es fehlen einige Operatoren.
- **Lösung**: Ergänze die fehlenden Operatoren in der `AssignmentStatementParser`.

### 4. `expression_list`
- **Regel**: `expression_list := expression { ',' expression }`
- **Status**: Teilweise implementiert, aber es fehlen Tests und Validierungen.
- **Lösung**: Ergänze Tests und Validierungen für `expression_list`.

### 5. `function_call_statement`
- **Regel**: `identifier '(' [ expression_list ] ')'`
- **Status**: Implementiert, aber es fehlen Tests.
- **Lösung**: Schreibe Tests für Funktionsaufrufe.

### 6. `parameter_list` und `parameter`
- **Regel**: `parameter_list := parameter { ',' parameter }`
- **Status**: Implementiert, aber es fehlen Tests.
- **Lösung**: Schreibe Tests für Parameterlisten.

### 7. `error_statement`
- **Regel**: `'error' expression`
- **Status**: Implementiert, aber es fehlen Tests.
- **Lösung**: Schreibe Tests für Fehleranweisungen.

### 8. `break_statement` und `continue_statement`
- **Regeln**: `'break'` und `'continue'`
- **Status**: Implementiert, aber es fehlen Tests.
- **Lösung**: Schreibe Tests für Break- und Continue-Anweisungen.

### 9. `switch_statement`
- **Regel**: `'switch' expression '{' { identifier ':' { statement } [ 'break' ] } [ 'default' ':' { statement } 'break' ] '}'`
- **Status**: Implementiert, aber es fehlen Tests.
- **Lösung**: Schreibe Tests für Switch-Anweisungen.

### 10. `conditional_statement`
- **Regel**: `'if' expression '{' { statement } '}' { 'else' 'if' expression '{' { statement } '}' } [ 'else' '{' { statement } '}' ]`
- **Status**: Implementiert, aber es fehlen Tests.
- **Lösung**: Schreibe Tests für Bedingungsanweisungen.

---

## Nächste Schritte

1. **Implementiere die fehlenden Parser-Elemente**:
   - Beginne mit `block`, da es eine grundlegende Regel ist.

2. **Schreibe Tests für die bestehenden Parser**:
   - Fokussiere dich auf die oben genannten Elemente, die bereits implementiert sind, aber noch keine Tests haben.

3. **Validiere die Implementierung**:
   - Überprüfe die bestehenden Parser auf Vollständigkeit und Korrektheit.