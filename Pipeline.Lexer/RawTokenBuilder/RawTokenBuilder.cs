using System.Text;

namespace Pipeline.Lexer.RawTokenBuilder
{
    public class RawTokenBuilder : IRawTokenBuilder
    {
        private StringBuilder _sb = new StringBuilder();
        private int _startPosition = -1;

        public void Start(int startPosition)
        {
            _startPosition = startPosition;
        }
        public void Append(char c)
        {
            _sb.Append(c);
        }

        public RawToken Build(RawTokenKind kind)
        {
            RawToken rawToken =  new RawToken(_sb.ToString(), kind, _startPosition );
            Reset();
            return rawToken;
        }

        public void Reset()
        {
            _startPosition = -1;
            _sb.Clear();
        }

        public bool IsEmpty()
        {
            return _sb.Length == 0;
        }
    }
}
