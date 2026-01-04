using Pipeline.Lexer.Classifier;
using Pipeline.Lexer.RawTokenBuilder;
using Pipeline.Lexer.Reader;
using Pipeline.Lexer.TokenResolver;
using Pipeline.Parser.AST;
using Pipeline.Parser.AST.Statements;
using Pipeline.Parser.ASTParser;

namespace Pipeline.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "let a=> (2+3)*2;\nlet b =>a/10+2;\na + b";
            Context context = new Context();

            //Console.WriteLine("Input expression:");
            //string input = Console.ReadLine();

            IReader reader = new Reader(input);
            ITokenResolver resolver = new TokenResolver();
            IRawTokenBuilder builder = new RawTokenBuilder();
            IClassifier classifier = new Classifier(resolver.GetOperatorChars());

            Lexer.Lexer lexer = new Lexer.Lexer(reader, builder, classifier, resolver);
            var tokens = lexer.Process();

            Console.WriteLine("input:\n" + input);

            try {
                var parser = new Parser.ASTParser.Parser(tokens.ToList());
                SyntaxNode node = parser.Parse();

                if (node is ProgramSyntax) {
                    var program = (ProgramSyntax)node;
                    Console.WriteLine("Result: " + program.Execute(context));
                }

                SyntaxTreePrinter.Print(node);
            }
            catch (Exception e) {
                Console.WriteLine($"Syntax error: {e.Message}");
            }
        }
    }
}