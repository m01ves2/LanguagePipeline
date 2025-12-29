using Pipeline.Lexer.TokenBuilder;

namespace Pipeline.Lexer.TokenResolver
{
    internal class TokenResolver : ITokenResolver
    {
        private readonly Dictionary<string, TokenType> Operators = new Dictionary<string, TokenType>(){
            { "+", TokenType.Plus },
            { "*", TokenType.Star },
            { "=>", TokenType.Lambda },
            { "=", TokenType.Equal },
            { "(", TokenType.OpenParen },
            { ")", TokenType.CloseParen },
        };

        private readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>(){
            { "let", TokenType.KeywordLet}
        };

        public Token Resolve(RawToken rawToken)
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
                default:
                    type = TokenType.Bad;
                    break;
            }
            return new Token(rawToken.Text, type, rawToken.Position);
        }
    }
}
