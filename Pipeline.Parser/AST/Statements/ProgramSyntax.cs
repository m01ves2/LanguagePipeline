using Pipeline.Parser.AST.Exceptions;
using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Statements
{
    public class ProgramSyntax : StatementSyntax
    {
        public IReadOnlyList<StatementSyntax> Statements { get; }

        public override string NodeName => $"Program";

        public ProgramSyntax(IReadOnlyList<StatementSyntax> statements)
        {
            Statements = statements;
        }

        public override object? Execute(Context context)
        {
            object? last = null;

            try {
                foreach (var statement in Statements) {
                    last = statement.Execute(context);
                }
            }
            catch (RuntimeException ex) {
                last = $"{ex.Message}";
            }

            return last;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Statements;
        }
    }
}
