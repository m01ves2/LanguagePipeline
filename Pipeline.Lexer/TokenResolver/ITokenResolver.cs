using Pipeline.Lexer.TokenBuilder;

namespace Pipeline.Lexer.TokenResolver
{
    internal interface ITokenResolver
    {
        Token Resolve(RawToken rawToken);
    }
}
