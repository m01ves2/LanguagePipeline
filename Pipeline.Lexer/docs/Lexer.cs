using System.Text;

namespace Tokenizer
{
    enum LexerState
    {
        None,
        Identifier,
        Number,
        Operator,
        Bad
    }

    internal class Lexer
    {
        private List<Token> _tokens = new List<Token>();
        private LexerState _state = LexerState.None;
        private StringBuilder _buffer;
        private int _tokenStartPosition;

        public IReadOnlyList<Token> Tokenize(string input)
        {
            string currentTokenText = "";
            TokenType currentTokenType = TokenType.NONE;
            int currentTokenPosition = -1;
            
            char previousChar = ' ';
            for (int i = 0; i < input.Length; i++) {
                char c = input[i];

                if (c == ' ') {
                    if (_state != LexerState.None) { //End of current token
                        _tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                        ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                        _state = LexerState.None;
                    }

                    previousChar = c;
                }
                else if (c == '=') {
                    if (_state != LexerState.None) { //End of current token
                        _tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                        ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                        _state = LexerState.None;
                    }
                    _state = LexerState.Operator;
                    currentTokenText = "" + c;
                    currentTokenType = TokenType.BAD; //?
                    currentTokenPosition = i;

                    previousChar = c;
                }
                else if (c == '>') {
                    if (_state == LexerState.Operator && previousChar == '=') {
                        currentTokenText += c;
                        currentTokenType = TokenType.BAD;
                        _tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                        ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                        _state = LexerState.None;
                    }
                    else {//bad token
                        currentTokenText += c;
                        currentTokenType = TokenType.BAD;
                        _state = LexerState.Bad;
                    }

                    previousChar = c;
                }
                else if (c == '+') {
                    if (_state != LexerState.None) {
                        _tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                        ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                        _state = LexerState.None;
                    }
                    currentTokenText = "" + c;
                    currentTokenType = TokenType.Plus;
                    currentTokenPosition = i;
                    _tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                    ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                    _state = LexerState.None;

                    previousChar = c;
                }
                else if (c == '*') {
                    if (_state != LexerState.None) {
                        _tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                        ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                        _state = LexerState.None;
                    }
                    currentTokenText = "" + c;
                    currentTokenType = TokenType.Star;
                    currentTokenPosition = i;
                    _tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                    ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                    _state = LexerState.None;

                    previousChar = c;
                }
                else if (c == '(') {
                    if (_state != LexerState.None) {
                        _tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                        ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                        _state = LexerState.None;
                    }
                    currentTokenText = "" + c;
                    currentTokenType = TokenType.OpenParen;
                    currentTokenPosition = i;
                    tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                    ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                    state = LexerState.None;
                    
                    previousChar = c;
                }
                else if (c == ')') {
                    if (state != LexerState.None) {
                        tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                        ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                        state = LexerState.None;
                    }
                    currentTokenText = "" + c;
                    currentTokenType = TokenType.CloseParen;
                    currentTokenPosition = i;
                    tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                    ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                    state = LexerState.None;
                    previousChar = c;

                }
                else if (char.IsLetter(c)) {
                    if (state == LexerState.Identifier && currentTokenType != TokenType.Identifier) {//close current token
                        tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                        ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                        state = LexerState.None;
                    }

                    if (state == LexerState.None) {
                        currentTokenPosition = i;
                        currentTokenText = "";
                        state = LexerState.Identifier;
                    }
                    currentTokenText += c;
                    currentTokenType = TokenType.Identifier;
                    state = LexerState.Identifier;
                }
                else if (char.IsDigit(c)) {
                    if (state == LexerState.Number && currentTokenType != TokenType.Number) {//close current token
                        tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
                        ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
                        state = LexerState.None;
                    }

                    if (state == LexerState.None) {
                        currentTokenPosition = i;
                        currentTokenText = "";
                        state = LexerState.Number;
                    }

                    currentTokenText += c;
                    currentTokenType = TokenType.Number;
                    state = LexerState.Identifier;
                }
                else { //Bad token
                    currentTokenText += c;
                    currentTokenType = TokenType.BAD;
                    state = LexerState.Bad;
                }
            }

            if (!string.IsNullOrEmpty(currentTokenText)) {
                tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
            }
            tokens.Add(new Token("EOF", TokenType.EOF, position));

            return tokens;
        }

        private void ResetToken(out string currentTokenText, out TokenType currentTokenType, out int currentTokenPosition)
        {
            currentTokenText = "";
            currentTokenType = TokenType.NONE;
            currentTokenPosition = -1;
        }

        private void FinishToken()
        {
            tokens.Add(new Token(currentTokenText, currentTokenType, currentTokenPosition));
            ResetToken(out currentTokenText, out currentTokenType, out currentTokenPosition);
            state = LexerState.None;
        }
    }
}
