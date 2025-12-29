namespace Pipeline.Lexer.TokenResolver
{
    public enum TokenType
    {
        NONE,
        Bad,
        Identifier, //x, foo, bar
        Number, //123
        Equal, //=
        Lambda, //=>
        Plus, //+
        Star, //*
        OpenParen, //(
        CloseParen, //)
        KeywordLet, //let
        EOF,
    }

    public record Token(string Text, TokenType Type, int Position);
}
