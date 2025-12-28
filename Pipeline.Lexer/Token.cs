namespace Tokenizer
{
    enum TokenType
    {
        NONE,
        BAD,
        Identifier, //x, foo, bar
        Number, //123
        Lambda, //=>
        Plus, //+
        Star, //*
        OpenParen, //(
        CloseParen, //)
        EOF,
    }

    record Token(string Text, TokenType Type, int Position);
}
