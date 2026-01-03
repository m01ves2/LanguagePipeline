namespace Pipeline.Parser.AST.Statements
{
    public class BadStatementSyntax : StatementSyntax
    {
        public override void Execute(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
