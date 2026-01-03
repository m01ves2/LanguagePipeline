using Pipeline.Lexer.Classifier;
using Pipeline.Lexer.RawTokenBuilder;
using Pipeline.Lexer.Reader;
using Pipeline.Lexer.TokenResolver;
using Pipeline.Parser;
using Pipeline.Parser.AST;
using Pipeline.Parser.AST.Expressions;
using Pipeline.Parser.AST.Statements;
using Pipeline.Parser.ASTParser;

namespace Pipeline.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "((2 + 3) * 4 - 1) * 5 / 2 - 3; 4*2; 4-3";
            Context context = new Context();

            IReader reader = new Reader(input);
            ITokenResolver resolver = new TokenResolver();

            IRawTokenBuilder builder = new RawTokenBuilder();
            IClassifier classifier = new Classifier(resolver.GetOperatorChars());

            Lexer.Lexer lexer = new Lexer.Lexer(reader, builder, classifier, resolver);
            var tokens = lexer.Process();

            Console.WriteLine("input: " + input);

            foreach (var token in tokens) {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Token: " + token.Text);
                Console.WriteLine("Token type: " + token.Type);
                Console.WriteLine("Token position " + token.Position);
            }

            var parser = new Parser.ASTParser.Parser(tokens.ToList());
            SyntaxNode node = parser.Parse();
            Console.WriteLine("Parser has finished working");

            if (node is ProgramSyntax) {
                var program = (ProgramSyntax)node;
                Console.WriteLine("Result: " + program.Execute(context));
            }
        }
    }
}