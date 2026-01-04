using Pipeline.Parser.ASTParser;

namespace Pipeline.Parser.AST.Statements
{
    public class BadStatementSyntax : StatementSyntax
    {
        public string Message { get; }

        public override string NodeName => $"BadStatement ({Message})";

        public BadStatementSyntax(string message)
        {
            Message = message;
        }
        public override object? Execute(Context context)
        {
            throw new InvalidOperationException($"Cannot execute invalid statement: {Message}");
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield break;
        }
    }
}
