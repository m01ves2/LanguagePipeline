namespace Pipeline.Lexer.TokenResolver
{
    public enum TokenType
    {
        NONE,
        Bad,
        Identifier, //x, foo, bar
        Number, //123
        Lambda, //=>
        Plus, //+
        Star, //*
        OpenParen, //(
        CloseParen, //)
        EOF,
    }

    public record Token(string Text, TokenType Type, int Position);
}
