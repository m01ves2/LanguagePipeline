using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Statements
{
    public class ProgramSyntax : StatementSyntax
    {
        public IReadOnlyList<StatementSyntax> Statements { get; }

        public ProgramSyntax(IReadOnlyList<StatementSyntax> statements)
        {
            Statements = statements;
        }

        public override object? Execute(Context context)
        {
            object? last = null;

            foreach (var statement in Statements) {
                last = statement.Execute(context);
            }

            return last;
        }
    }
}
