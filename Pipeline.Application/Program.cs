using Pipeline.Lexer.Classifier;
using Pipeline.Lexer.RawTokenBuilder;
using Pipeline.Lexer.Reader;
using Pipeline.Lexer.TokenResolver;
using Pipeline.Parser;
using Pipeline.Parser.AST;
using Pipeline.Parser.AST.Expressions;
using System.Data;

namespace Pipeline.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "((2 + 3) * 4 - 1) * 5 / 2 - 3";
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

            Parser.Parser parser = new Parser.Parser(tokens.ToList());
            SyntaxNode node = parser.Parse();
            Console.WriteLine("Parser has finished working");

            if(node is ExpressionSyntax) {
                ExpressionSyntax expression = (ExpressionSyntax)node;
                Console.WriteLine("Result: " + expression.Evaluate(context));
            }
        }
    }
}