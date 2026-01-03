using Pipeline.Parser.AST.Expressions;
using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Statements
{
    public class LetStatementSyntax : StatementSyntax
    {
        public string Identifier { get; }
        public ExpressionSyntax Expression { get; }
        
        public LetStatementSyntax(string identifier, ExpressionSyntax expression)
        {
            Identifier = identifier;
            Expression = expression;
        }

        public override object? Execute(Context context)
        {
            context.SetVariable(Identifier, Expression.Evaluate(context));
            return null;
        }
    }
}
