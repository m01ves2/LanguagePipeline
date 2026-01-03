using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Statements
{
    public abstract class StatementSyntax : SyntaxNode
    {
        public abstract object? Execute(Context context);

        public override string Print()
        {
            throw new NotImplementedException();
        }
    }
}
