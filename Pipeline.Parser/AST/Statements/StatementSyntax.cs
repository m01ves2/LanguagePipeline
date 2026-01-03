namespace Pipeline.Parser.AST.Statements
{
    public abstract class StatementSyntax : SyntaxNode
    {
        public abstract void Execute(Context context);

        public override string Print()
        {
            throw new NotImplementedException();
        }
    }
}
