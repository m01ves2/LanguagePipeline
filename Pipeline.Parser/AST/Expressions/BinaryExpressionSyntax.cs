namespace Pipeline.Parser.AST.Expressions
{
    public enum BinaryOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide,
    }
    public class BinaryExpressionSyntax : ExpressionSyntax
    {
        public ExpressionSyntax Left { get; }
        public BinaryOperation Operation { get; }
        public ExpressionSyntax Right { get; }

        public BinaryExpressionSyntax(
            ExpressionSyntax left,
            BinaryOperation operation,
            ExpressionSyntax right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
            Operation = operation;
        }

        public override double Evaluate(Context ctx)
        {
            var left = (double)Left.Evaluate(ctx);
            var right = (double)Right.Evaluate(ctx);

            switch (Operation) {
                case BinaryOperation.Add: return left + right;
                case BinaryOperation.Subtract: return left - right;
                case BinaryOperation.Multiply: return left * right;
                case BinaryOperation.Divide:
                    if (right == 0)
                        throw new DivideByZeroException("Run time error: division by zero.");
                    else
                        return ((double)left) / right;
                default: 
                    throw new InvalidOperationException("Unknown operation");
            }
        }

    }
}
