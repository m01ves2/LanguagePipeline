//Program::= Statement * EOF

//Statement::= LetStatement
//              | ExpressionStatement

//LetStatement::= 'let' Identifier '=' Expression ';'

//ExpressionStatement ::= Expression ';'

//Expression  ::= Additive

//Additive    ::= Multiplicative (('+' ) Multiplicative)*

//Multiplicative ::= Primary (('*') Primary)*

//Primary     ::= Number
//              | Identifier
//              | '(' Expression ')'

//Parse:
//ParseExpression()
//  ->ParseAdditive()
//     ->ParseMultiplicative()
//        ->ParsePrimary()

using Pipeline.Lexer.TokenResolver;
using Pipeline.Parser.AST;
using Pipeline.Parser.AST.Expressions;
using Pipeline.Parser.AST.Statements;
using System.Reflection.Metadata.Ecma335;

namespace Pipeline.Parser.ASTParser
{
    public partial class Parser
    {
        private readonly List<Token> _tokens;
        private int _position;
        private Token Current => GetToken(_position);

        public Parser(List<Token> tokens)
        {
            _position = 0;
            _tokens = tokens;
        }

        Token GetToken(int index)
        {
            if (index >= _tokens.Count)
                return _tokens[^1]; // EOF _tokens[_tokens.Count - 1]
            return _tokens[index];
        }

        Token Peek(int offset)
        {
            return GetToken(_position + offset);
        }

        void Consume()
        {
            _position++;
        }

        //Token Match(TokenType type)
        //{
        //    if (Current.Type == type) {
        //        Consume();
        //    }

        //    //ReportError($"Expected {type}, got {Current.Type}");
        //    return new Token("", type, Current.Position); // fake token
        //}

        public SyntaxNode Parse()
        {
            return ParseProgram();
        }
    }
}
