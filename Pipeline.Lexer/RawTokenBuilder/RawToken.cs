namespace Pipeline.Lexer.TokenBuilder
{
    public enum RawTokenKind
    {
        Bad,
        Identifier, //x, foo, bar
        Number, //123
        Operator, // +, *, =, >, ), (,
    }

    public record RawToken(string Text, RawTokenKind Kind, int Position);
}
