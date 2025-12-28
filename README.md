## Internals

To deeply understand Roslyn and compilation pipeline, 
I implemented a minimal lexer with a finite state classifier.

This helped me clearly separate:
- character reading
- token classification (FSM)
- token emission
- parsing responsibilities

See: /Pipeline/Lexer

##Overall
Implemented a custom lexical analysis pipeline (FSM-based classifier) to better understand parsing and compilation stages before integrating Roslyn scripting.