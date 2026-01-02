using Pipeline.Lexer.Classifier;
using Pipeline.Lexer.Reader;
using Pipeline.Lexer.RawTokenBuilder;
using Pipeline.Lexer.TokenResolver;

namespace Pipeline.Lexer
{
    public class Lexer
    {
        private readonly IReader _reader;
        private readonly IRawTokenBuilder _rawTokenBuilder;
        private readonly IClassifier _classifier;
        private readonly ITokenResolver _tokenResolver;

        private Dictionary<ClassifierState, RawTokenKind> MapStateToRawKind = new Dictionary<ClassifierState, RawTokenKind>()
        {
            { ClassifierState.Identifier, RawTokenKind.Identifier },
            { ClassifierState.Number, RawTokenKind.Number },
            { ClassifierState.Operator, RawTokenKind.Operator },
            { ClassifierState.Bad, RawTokenKind.Bad },
        };
        public Lexer(IReader reader, IRawTokenBuilder rawTokenBuilder, IClassifier classifier, ITokenResolver tokenResolver)
        {
            _reader = reader;
            _classifier = classifier;
            _tokenResolver = tokenResolver;
            _rawTokenBuilder = rawTokenBuilder;
        }

        public IEnumerable<Token> Process()
        {
            List<Token> tokens = new List<Token>();
            char? r = null;
            int index = -1;
            bool isReprocess = false;
            while (true) {

                if (!isReprocess) {
                    r = _reader.ReadChar();
                    index++;
                }
                isReprocess = false;

                if (r is null) {
                    if (_classifier.State != ClassifierState.Start) {
                        RawToken rawToken = _rawTokenBuilder.Build(MapStateToRawKind[_classifier.State]);
                        tokens.AddRange(_tokenResolver.Resolve(rawToken));
                    }
                    break;
                }

                char c = r.Value;
                var classifierAction = _classifier.Process(c);

                switch (classifierAction) {
                    case ClassifierAction.Continue: {
                            if(_classifier.State != ClassifierState.Start) {

                                if(_rawTokenBuilder.isEmpty()) 
                                    _rawTokenBuilder.Start(index);                   
                                
                                _rawTokenBuilder.Append(c);
                            }
                            break; 
                        }
                    case ClassifierAction.Emit: {
                            if (_rawTokenBuilder.isEmpty())
                                _rawTokenBuilder.Start(index);

                            RawToken rawToken = _rawTokenBuilder.Build(MapStateToRawKind[_classifier.State]);
                            tokens.AddRange(_tokenResolver.Resolve(rawToken));

                            _classifier.Reset();
                            break;
                        }
                    case ClassifierAction.EmitAndReprocess: {
                            if (_rawTokenBuilder.isEmpty())
                                _rawTokenBuilder.Start(index);

                            isReprocess = true;
                            RawToken rawToken = _rawTokenBuilder.Build(MapStateToRawKind[_classifier.State]);
                            tokens.AddRange(_tokenResolver.Resolve(rawToken));
                            _classifier.Reset();
                            break;
                        }
                    case ClassifierAction.Error: {
                            if (!_rawTokenBuilder.isEmpty()) {
                                // Выпустить уже накопленный токен
                                RawToken rawToken = _rawTokenBuilder.Build(MapStateToRawKind[_classifier.State]);
                                tokens.AddRange(_tokenResolver.Resolve(rawToken));

                            }

                            // Создать отдельный RawToken для текущего Bad-символа
                            _rawTokenBuilder.Start(index);
                            _rawTokenBuilder.Append(c);
                            RawToken badRawToken = _rawTokenBuilder.Build(RawTokenKind.Bad); // метод Build с явным символом
                            tokens.AddRange(_tokenResolver.Resolve(badRawToken));

                            _classifier.Reset(); // Classifier в Start
                            isReprocess = false; // символ уже обработан
                            break;
                        }
                }
            }

            tokens.Add(new Token("EOF", TokenType.EOF, index));
            return tokens;
        }
    }
}
