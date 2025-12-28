using System.Diagnostics.Metrics;

namespace Pipeline.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "foo => (bar + 42=)";
            var lexer = new Lexer();
            var tree = lexer.Tokenize(input);

            Console.WriteLine("input: " + input);

            foreach (var token in tree) {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Token: " + token.Text);
                Console.WriteLine("Token type: " + token.Type);
                Console.WriteLine("Token position " + token.Position);
            }
        }
    }
}