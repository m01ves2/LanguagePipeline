using Pipeline.Lexer.RawTokenBuilder;

namespace Pipeline.Lexer.TokenResolver
{
    public interface ITokenResolver
    {
        IEnumerable<Token> Resolve(RawToken rawToken);
        HashSet<char> GetOperatorChars();
    }
}
