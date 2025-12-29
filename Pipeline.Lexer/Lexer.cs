using Pipeline.Lexer.Classifier;
using Pipeline.Lexer.Reader;
using Pipeline.Lexer.TokenBuilder;
using Pipeline.Lexer.TokenResolver;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Pipeline.Lexer
{
    internal class Lexer
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
                        tokens.Add(_tokenResolver.Resolve(rawToken));
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
                            tokens.Add(_tokenResolver.Resolve(rawToken));

                            _classifier.Reset();
                            break;
                        }
                    case ClassifierAction.EmitAndReprocess: {
                            if (_rawTokenBuilder.isEmpty())
                                _rawTokenBuilder.Start(index);

                            isReprocess = true;
                            RawToken rawToken = _rawTokenBuilder.Build(MapStateToRawKind[_classifier.State]);
                            tokens.Add(_tokenResolver.Resolve(rawToken));
                            _classifier.Reset();
                            break;
                        }
                    case ClassifierAction.Error: {
                            RawToken rawToken = _rawTokenBuilder.Build(MapStateToRawKind[_classifier.State]);
                            tokens.Add(_tokenResolver.Resolve(rawToken));
                            _classifier.Reset();
                            break;
                        }
                }
            }
            return tokens;
        }
    }
}
