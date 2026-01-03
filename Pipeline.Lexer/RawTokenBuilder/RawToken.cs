namespace Pipeline.Lexer.RawTokenBuilder
{
    public enum RawTokenKind
    {
        Bad,
        Identifier, //x, foo, bar
        Number, //123
        Operator, // +, *, -, /, =, >, ), (,
        Semicolon, //;
    }

    public record RawToken(string Text, RawTokenKind Kind, int Position);
}
