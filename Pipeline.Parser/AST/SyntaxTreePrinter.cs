namespace Pipeline.Parser.AST
{
    public static class SyntaxTreePrinter
    {
        public static void Print(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└── " : "├── ";
            Console.WriteLine(indent + marker + node.NodeName);

            indent += isLast ? "    " : "│   ";

            var children = node.GetChildren().ToList();
            for (int i = 0; i < children.Count; i++) {
                Print(children[i], indent, i == children.Count - 1);
            }
        }
    }
}
