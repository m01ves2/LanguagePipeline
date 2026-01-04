using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Expressions
{
    public abstract class ExpressionSyntax : SyntaxNode
    {
        public abstract double Evaluate(Context context);
    }
}
