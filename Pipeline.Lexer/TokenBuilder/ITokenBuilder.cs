namespace Pipeline.Lexer.TokenBuilder
{
    internal interface ITokenBuilder
    {
        void Append(char c);
        RawToken Build(RawTokenKind kind);
        void Start(int startPosition);
    }
}
