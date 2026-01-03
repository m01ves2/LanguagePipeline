using System.Xml.Linq;

namespace Pipeline.Parser.AST.Expressions
{
    public class IdentifierExpressionSyntax : ExpressionSyntax
    {
        private readonly string _name;

        public IdentifierExpressionSyntax(string name)
        {
            _name = name;
        }

        public override double Evaluate(Context context)
        {
            return context.GetVariable(_name);
        }
    }
}
