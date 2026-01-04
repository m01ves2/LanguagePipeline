# LanguagePipeline

LanguagePipeline is a minimal educational implementation of a programming language pipeline:
from raw text input to tokenization, parsing into an AST, and execution.

The project focuses on architectural clarity and separation of responsibilities,
not on language completeness or performance.

## Overview
The language processing pipeline consists of the following stages:

1. **Reader**
   Reads raw characters from the input source.

2. **Lexer**
   Converts a stream of characters into tokens.
   The lexer is state-based and may produce `Bad` tokens for invalid character sequences.

3. **Parser**
   Builds an Abstract Syntax Tree (AST) from the token stream.
   The parser is deterministic: it either produces a valid AST or throws a `ParserException`.

4. **AST**
   Represents a valid program structure.
   The AST never contains invalid or placeholder nodes.

5. **Execution**
   Executes the AST using a runtime context that stores variables and values.


## Pipeline Stages
- Reader
- Lexer
- Parser
- AST
- Execution

## Grammar

The language supports a minimal grammar:

Program       ::= Statement* EOF  
Statement     ::= LetStatement | ExpressionStatement  
LetStatement  ::= 'let' Identifier '=>' Expression  
Expression    ::= Additive  
Additive      ::= Multiplicative (('+' | '-') Multiplicative)*  
Multiplicative::= Primary (('*' | '/') Primary)*  
Primary       ::= Number | Identifier | '(' Expression ')'

## Error Handling Model

Error handling is intentionally separated by pipeline stages:

- **Lexer**
  Produces `Bad` tokens for invalid character sequences.
  The lexer does not throw exceptions.

- **Parser**
  Does not attempt error recovery.
  If an unexpected or invalid token is encountered, a `ParserException` is thrown.
  No AST is produced for syntactically invalid input.

- **AST / Execution**
  The AST assumes syntactic correctness.
  Runtime errors (e.g. division by zero, undefined variables) are reported via runtime exceptions.

## Limitations

- No type system.
- No functions or control flow constructs.
- No REPL or interactive execution.
- No asynchronous execution.
- Minimal error diagnostics (single-error reporting).

## Design Decisions

- The parser uses exceptions instead of "bad" AST nodes to keep the AST strictly valid.
- Expression parsing is implemented using recursive descent with explicit operator precedence.
- The project avoids asynchronous code to keep control flow explicit and easy to reason about.
- The implementation prioritizes clarity over extensibility or performance.

## Notes

This project is intentionally minimal and considered complete.
It serves as a foundation for further exploration of language tooling and compiler architecture.