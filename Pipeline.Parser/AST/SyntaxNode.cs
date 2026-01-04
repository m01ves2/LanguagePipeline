namespace Pipeline.Parser.AST
{
    public abstract class SyntaxNode
    {
        public abstract IEnumerable<SyntaxNode> GetChildren();
        public abstract string NodeName { get; }
    }
}
