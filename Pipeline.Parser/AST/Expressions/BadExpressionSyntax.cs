using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Expressions
{
    public class BadExpressionSyntax : ExpressionSyntax
    {
        public string Message { get; }

        public override string NodeName => $"BadExpression ({Message})";

        public BadExpressionSyntax(string message)
        {
            Message = message;
        }

        public override double Evaluate(Context context)
        {
            throw new InvalidOperationException($"Cannot evaluate invalid expression: {Message}");
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield break; //return Enumerable.Empty<SyntaxNode>();
        }
    }
}
