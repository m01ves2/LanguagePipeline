namespace Pipeline.Lexer.TokenBuilder
{
    internal interface IRawTokenBuilder
    {
        void Append(char c);
        RawToken Build(RawTokenKind kind);
        void Start(int startPosition);
        void Reset();
        bool isEmpty();
    }
}
