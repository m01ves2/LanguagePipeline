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
        Minus, //-
        Divide, // \\
        OpenParen, //(
        CloseParen, //)
        KeywordLet, //let
        Semicolon, // ;
        EOF,
    }

    public record Token(string Text, TokenType Type, int Position);
}
