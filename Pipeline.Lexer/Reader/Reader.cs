namespace Pipeline.Lexer.Reader
{
    public class Reader : IReader
    {
        private readonly string input;
        private int position = 0;

        public Reader(string input)
        {
            this.input = input;
        }

        public char? ReadChar()
        {
            if ( position < input.Length )
                return input[position++];
            else
                return null; // EOF
        }
    }
}
