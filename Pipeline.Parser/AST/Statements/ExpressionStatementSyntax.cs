using Pipeline.Parser.AST.Expressions;
using Pipeline.Parser.AST.Statements;
using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser
{
    public class ExpressionStatementSyntax : StatementSyntax
    {
        public ExpressionSyntax Expression { get; }

        public ExpressionStatementSyntax(ExpressionSyntax expression)
        {
            Expression = expression;
        }

        public override object? Execute(Context context)
        {
            return Expression.Evaluate(context);
        }
    }
}
