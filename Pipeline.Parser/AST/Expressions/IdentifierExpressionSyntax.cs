using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Expressions
{
    public class IdentifierExpressionSyntax : ExpressionSyntax
    {
        private readonly string _name;

        public IdentifierExpressionSyntax(string name)
        {
            _name = name;
        }

        public override string NodeName => $"IdentifierExpression ({_name})";

        public override double Evaluate(Context context)
        {
            return context.GetVariable(_name);
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield break;
        }
    }
}
