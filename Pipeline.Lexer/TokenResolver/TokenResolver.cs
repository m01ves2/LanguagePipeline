using Pipeline.Lexer.RawTokenBuilder;

namespace Pipeline.Lexer.TokenResolver
{
    public class TokenResolver : ITokenResolver
    {
        private readonly Dictionary<string, TokenType> Operators = new Dictionary<string, TokenType>(){
            { "+", TokenType.Plus },
            { "*", TokenType.Star },
            { "-", TokenType.Minus },
            { "/", TokenType.Divide },
            { "=>", TokenType.Lambda },
            { "=", TokenType.Equal },
            { "(", TokenType.OpenParen },
            { ")", TokenType.CloseParen },
            { ";", TokenType.Semicolon},
        };

        private readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>(){
            { "let", TokenType.KeywordLet}
        };

        public IEnumerable<Token> Resolve(RawToken rawToken)
        {
            TokenType type;
            switch (rawToken.Kind) {
                case RawTokenKind.Identifier:
                    if (Keywords.ContainsKey(rawToken.Text)) {
                        type = Keywords[rawToken.Text];
                    }
                    else
                        type = TokenType.Identifier;
                    break;
                case RawTokenKind.Number:
                    type = TokenType.Number;
                    break;
                case RawTokenKind.Bad:
                    type = TokenType.Bad;
                    break;
                case RawTokenKind.Operator:
                    if (Operators.ContainsKey(rawToken.Text)) {
                        type = Operators[rawToken.Text];
                    }
                    else
                        type = TokenType.Bad;
                    break;
                case RawTokenKind.Semicolon:
                        type = TokenType.Semicolon;
                    break;
                default:
                    type = TokenType.Bad;
                    break;
            }

            if(type == TokenType.Bad) {
                foreach (var t in SplitBadToken(rawToken))
                    yield return t;
            }
            else {
                yield return new Token(rawToken.Text, type, rawToken.Position);
            }
        }

        public HashSet<char> GetOperatorChars()
        {
            HashSet<char> chars = new HashSet<char>();

            foreach (var item in Operators.Keys) {
                for(int i = 0; i < item.Length; i++) {
                    chars.Add(item[i]);
                }
            }
            return chars;
        }


        private IEnumerable<Token> SplitBadToken(RawToken badToken)
        {
            string text = badToken.Text;
            int pos = badToken.Position;

            while (text.Length > 0) {
                // ищем максимальный оператор в начале строки
                string match = null;
                TokenType type = TokenType.Bad;

                foreach (var op in Operators.Keys.OrderByDescending(s => s.Length)) {
                    if (text.StartsWith(op)) {
                        match = op;
                        type = Operators[op];
                        break;
                    }
                }

                if (match != null) {
                    // валидный оператор
                    yield return new Token(match, type, pos);
                    text = text.Substring(match.Length);
                    pos += match.Length;
                }
                else {
                    // символ не оператор → определяем тип
                    char c = text[0];

                    TokenType t = char.IsLetter(c) ? TokenType.Identifier :
                                  char.IsDigit(c) ? TokenType.Number :
                                  TokenType.Bad;   // <--- вот здесь ^ превратится в Bad

                    yield return new Token(c.ToString(), t, pos);

                    text = text.Substring(1);
                    pos += 1;
                }
            }
        }

    }
}
