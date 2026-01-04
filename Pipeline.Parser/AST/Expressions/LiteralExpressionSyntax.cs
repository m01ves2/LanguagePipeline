using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Expressions
{
    public class LiteralExpressionSyntax : ExpressionSyntax
    {
        public double Value { get; }

        public override string NodeName => $"LiteralExpression ({Value})";

        public LiteralExpressionSyntax(double value)
        {
            Value = value;
        }
        public override double Evaluate(Context context)
        {
            return Value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield break;
        }
    }
}
