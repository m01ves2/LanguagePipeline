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

            //REVIEW исправили разбор всех bad-токенов на bad-токенов с изначальным типом RawTokenKind.Operator
            if (rawToken.Kind == RawTokenKind.Operator && type == TokenType.Bad) {
                foreach (var token in SplitOperators(rawToken))
                    yield return token;
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

        private IEnumerable<Token> SplitOperators(RawToken operatorsToken)
        {
            string text = operatorsToken.Text;
            int position = operatorsToken.Position;

            while (text.Length > 0) {
                // ищем максимальный оператор в оставшейся части text
                string? match = Operators.Keys
                    .OrderByDescending(op => op.Length)
                    .FirstOrDefault(op => text.StartsWith(op));

                if (match is null) {
                    yield return new Token(text[0].ToString(), TokenType.Bad, position);

                    text = text[1..];
                    position++;
                    continue;
                }

                yield return new Token(match, Operators[match], position);

                text = text[match.Length..];
                position += match.Length;
            }
        }

    }
}
