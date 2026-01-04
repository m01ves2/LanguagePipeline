using Pipeline.Lexer.TokenResolver;
using Pipeline.Parser.AST.Expressions;
using Pipeline.Parser.ASTParser.Exceptions;

//Parse Expression:
//ParseExpression()
//  ->ParseAdditive()
//     ->ParseMultiplicative()
//        ->ParsePrimary()


namespace Pipeline.Parser.ASTParser
{
    public partial class Parser
    {
        private ExpressionSyntax ParseExpression()
        {
            var expr = ParseAdditive();
            return expr;
        }

        private ExpressionSyntax ParseAdditive()
        {
            ExpressionSyntax left = ParseMultiplicative();

            while (Current.Type != TokenType.EOF && Current.Type != TokenType.Semicolon) {

                BinaryOperation binaryOperation;
                Token operationToken = Current;

                switch (operationToken.Type) {
                    case TokenType.Plus:
                        binaryOperation = BinaryOperation.Add;
                        Consume(TokenType.Plus); // поглотили +
                        break;
                    case TokenType.Minus:
                        binaryOperation = BinaryOperation.Subtract;
                        Consume(TokenType.Minus);
                        break;

                    case TokenType.CloseParen:
                    case TokenType.Semicolon:
                    case TokenType.EOF:
                        return left;

                    case TokenType.Bad:
                        throw new ParserException($"Parser error: Invalid token in additive expression at position {operationToken.Position}: {operationToken.Text}");
                    default:
                        throw new ParserException($"Parser error: Unexpected token {operationToken.Type} in additive expression at position {operationToken.Position}");

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

            while (Current.Type != TokenType.EOF && Current.Type != TokenType.Semicolon) {
                BinaryOperation binaryOperation;
                Token operationToken = Current;

                switch (operationToken.Type) {
                    case TokenType.Star:
                        binaryOperation = BinaryOperation.Multiply;
                        Consume(TokenType.Star); //поглотили *
                        break;

                    case TokenType.Divide:
                        binaryOperation = BinaryOperation.Divide;
                        Consume(TokenType.Divide);
                        break;

                    case TokenType.CloseParen:
                    case TokenType.Semicolon:
                    case TokenType.EOF:
                        return left;

                    case TokenType.Bad:
                        throw new ParserException($"Parser error: Invalid token in multiplicative expression at position {operationToken.Position}: {operationToken.Text}");

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
                throw new ParserException($"Parser error: Invalid token in primary expression at position {token.Position}: {token.Text}");
            }

            return expression;
        }

        private ExpressionSyntax ParsePrimaryNumber()
        {
            double value;
            Token token = Current;
            Consume(TokenType.Number); //поглотили токен
            bool success = double.TryParse(token.Text, out value);
            if (success)
                return new LiteralExpressionSyntax(value);
            else
                throw new ParserException($"Parser error: Invalid token in primary expression at position {token.Position}: {token.Text}");
        }
        private ExpressionSyntax ParsePrimaryIdentifier()
        {
            Token token = Current;
            Consume(TokenType.Identifier); //поглотили токен
            return new IdentifierExpressionSyntax(token.Text);
        }
        private ExpressionSyntax ParseExpressionPrimary()
        {
            Consume(TokenType.OpenParen); //поглотили (
            ExpressionSyntax expression = ParseExpression();
            Token token = Current;
            if (token.Type != TokenType.CloseParen) {
                throw new ParserException($"Parser error: Invalid token in primary expression at position {token.Position}: {token.Text}");
            }
            Consume(TokenType.CloseParen); //поглотили )
            return expression;
        }
    }
}
