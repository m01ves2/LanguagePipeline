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

//ParseExpression()
//  ->ParseAdditive()
//     ->ParseMultiplicative()
//        ->ParsePrimary()

using Pipeline.Lexer.TokenResolver;
using Pipeline.Parser.AST;
using Pipeline.Parser.AST.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Pipeline.Parser
{
    public class Parser
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

        public SyntaxNode Parse()
        {
            ExpressionSyntax expressionSyntax = ParseExpression();
            return expressionSyntax;
        }


        Token Match(TokenType type)
        {
            throw new NotImplementedException(); //если тип совпал → consume, если нет → создать диагностическую ошибку, но продолжить
        }

        private ExpressionSyntax ParseExpression()
        {
            var expr = ParseAdditive();
            //if (Current.Type == TokenType.Bad)
            //    return new BadExpressionSyntax("");
            return expr;
        }

        private ExpressionSyntax ParseAdditive()
        {
            ExpressionSyntax left = ParseMultiplicative();

            while (Current.Type != TokenType.EOF) {

                BinaryOperation binaryOperation;
                Token operationToken = Current;

                switch (operationToken.Type) {
                    case TokenType.Plus:
                        binaryOperation = BinaryOperation.Add;
                        Consume(); // поглотили +
                        break;
                    case TokenType.Minus:
                        binaryOperation = BinaryOperation.Subtract;
                        Consume();
                        break;

                    case TokenType.CloseParen:
                        return left;

                    default: return new BadExpressionSyntax("Parser error: wrong expression");
                }
                ExpressionSyntax right = ParseMultiplicative();
                ExpressionSyntax binaryExpression = new BinaryExpressionSyntax(left, binaryOperation, right);
                left = binaryExpression;
            }
            return left;
        }

        private ExpressionSyntax ParseMultiplicative()
        {
            ExpressionSyntax left = ParsePrimary();

            while (Current.Type != TokenType.EOF) {
                BinaryOperation binaryOperation;
                Token operationToken = Current;

                switch (operationToken.Type) {
                    case TokenType.Plus:
                        return left;
                    case TokenType.Minus:
                        return left;

                    case TokenType.Star:
                        binaryOperation = BinaryOperation.Multiply;
                        Consume(); //поглотили *
                        break;

                    case TokenType.Divide:
                        binaryOperation = BinaryOperation.Divide;
                        Consume();
                        break;

                    //case TokenType.CloseParen:
                    //    return left;

                    default:
                        return left;
                }
                ExpressionSyntax right = ParsePrimary();
                ExpressionSyntax binaryExpression = new BinaryExpressionSyntax(left, binaryOperation, right);
                left = binaryExpression;
            }
            return left;
        }

        private ExpressionSyntax ParsePrimary()
        {
            ExpressionSyntax expression;
            Token token = Current;

            if (token.Type == TokenType.Number) {
                expression = ParsePrimaryNumber();
            }
            else if (token.Type == TokenType.Identifier) {
                expression = ParsePrimaryIdentifier();
            }
            else if (token.Type == TokenType.OpenParen) {
                expression = ParseExpressionPrimary();
            }
            else {
                expression = new BadExpressionSyntax($"Wrong primary expression");
            }

            return expression;
        }

        private ExpressionSyntax ParsePrimaryNumber()
        {
            double value;
            Token token = Current; 
            Consume(); //поглотили токен
            bool success = double.TryParse(token.Text, out value);
            if (success)
                return new LiteralExpressionSyntax(value);
            else
                return new BadExpressionSyntax($"Can't parse literal expression: {token.Text}");
        }
        private ExpressionSyntax? ParsePrimaryIdentifier()
        {
            Token token = Current;
            Consume(); //поглотили токен
            return new IdentifierExpressionSyntax(token.Text);
        }
        private ExpressionSyntax ParseExpressionPrimary()
        {
            Consume(); //поглотили (
            ExpressionSyntax expression = ParseExpression();
            Token token = Current;
            if(token.Type != TokenType.CloseParen) {
                return new BadExpressionSyntax("Unexpected end of expression");
            }
            Consume(); //поглотили )
            return expression;
        }
    }
}
