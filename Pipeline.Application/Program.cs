using Pipeline.Lexer.Reader;
using Pipeline.Lexer.RawTokenBuilder;
using Pipeline.Lexer.Classifier;
using Pipeline.Lexer.TokenResolver;
using System.Data;

namespace Pipeline.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "let a =>^(2+3);==>=;";

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
        }
    }
}