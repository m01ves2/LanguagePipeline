using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Expressions
{
    public class BadExpressionSyntax : ExpressionSyntax
    {
        public string Message { get; }

        public BadExpressionSyntax(string message)
        {
            Message = message;
        }

        public override double Evaluate(Context context)
        {
            throw new InvalidOperationException($"Cannot evaluate invalid expression: {Message}");
        }
    }
}
