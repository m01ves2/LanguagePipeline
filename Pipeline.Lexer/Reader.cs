//Reader — максимально тупой
//class Reader
//{
//    input
//    position

//    function nextChar() -> char?
//        if position >= input.length
//            return null   // EOF
//        return input[position++]
//}

namespace Pipeline.Lexer
{
    public class Reader
    {
        private readonly String input;
        private int position = 0;

        public Reader(String input)
        {
            this.input = input;
        }

        public char? ReadChar()
        {
            if ( position < input.Length )
                return input[position++];
            else
                return null;
        }
    }
}
