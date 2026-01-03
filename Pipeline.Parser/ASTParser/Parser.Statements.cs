using Pipeline.Lexer.TokenResolver;
using Pipeline.Parser.AST.Statements;

namespace Pipeline.Parser.ASTParser
{
    public partial class Parser
    {
        private ProgramSyntax ParseProgram()
        {
            var statements = new List<StatementSyntax>();

            while (Current.Type != TokenType.EOF) {
                statements.Add(ParseStatement());
            }

            return new ProgramSyntax(statements);
        }

        private StatementSyntax ParseStatement() 
        {
            if (Current.Type == TokenType.KeywordLet)
                return ParseLetStatement();
            else
                return ParseExpressionStatement();
        }

        private StatementSyntax ParseExpressionStatement()
        {
            var expression = ParseExpression();
            //Consume(TokenType.Semicolon);
            return new ExpressionStatementSyntax(expression);
        }
        private StatementSyntax ParseLetStatement()
        {
            return new BadStatementSyntax("ParseLetStatement");
        }

    }
}
