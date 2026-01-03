namespace Pipeline.Parser.AST.Expressions
{
    public class LiteralExpressionSyntax : ExpressionSyntax
    {
        public double Value { get; }

        public LiteralExpressionSyntax(double value)
        {
            Value = value;
        }

        public override string Print() => Value.ToString();

        public override double Evaluate(Context context)
        {
            return Value;
        }
    }
}
