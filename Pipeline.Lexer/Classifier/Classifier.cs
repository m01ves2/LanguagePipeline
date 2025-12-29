namespace Pipeline.Lexer.Classifier
{
    public sealed class Classifier : IClassifier
    {
        public ClassifierState State { get; private set; } = ClassifierState.Start;
        private static HashSet<char> _operatorChars;
                                //[
                                //    '(', ')', '+', '*', '=', '>', '<'
                                //];

        public Classifier(HashSet<char> operatorChars)
        {
            _operatorChars = operatorChars;
        }

        public ClassifierAction Process(char c)
        {
            switch (State) {
                case ClassifierState.Start:
                    return ProcessStart(c);

                case ClassifierState.Identifier:
                    return ProcessIdentifier(c);

                case ClassifierState.Number:
                    return ProcessNumber(c);

                case ClassifierState.Operator:
                    return ProcessOperator(c);

                case ClassifierState.Bad:
                    return ProcessBad(c);

                default:
                    throw new InvalidOperationException();
            }
        }

        public void Reset()
        {
            State = ClassifierState.Start;
        }

        private ClassifierAction ProcessStart(char c)
        {
            if (char.IsWhiteSpace(c)) {
                return ClassifierAction.Continue;
            }
            else if (char.IsLetter(c)) {
                State = ClassifierState.Identifier; // переключаем состояние
                return ClassifierAction.Continue;
            }
            else if (char.IsDigit(c)) {
                State = ClassifierState.Number;
                return ClassifierAction.Continue;
            }
            else if (isOperator(c)) {
                //TODO CurrentTokenType = 
                State = ClassifierState.Operator;
                return ClassifierAction.Continue;
            }
            else {
                State = ClassifierState.Bad;
                return ClassifierAction.Error;
            }
        }

        private ClassifierAction ProcessIdentifier(char c)
        {
            if (char.IsWhiteSpace(c)) {
                State = ClassifierState.Start;
                return ClassifierAction.Emit;
            }
            else if (char.IsLetter(c)) {
                return ClassifierAction.Continue;
            }
            else if (char.IsDigit(c)) {
                return ClassifierAction.Continue;
            }
            else if (isOperator(c)) {
                State = ClassifierState.Start;
                return ClassifierAction.EmitAndReprocess;
            }
            else {
                State = ClassifierState.Bad;
                return ClassifierAction.Error;
            }
        }

        private ClassifierAction ProcessNumber(char c)
        {
            if (char.IsWhiteSpace(c)) {
                State = ClassifierState.Start;
                return ClassifierAction.Emit;
            }
            else if (char.IsLetter(c)) {
                State = ClassifierState.Bad;
                return ClassifierAction.Continue;
            }
            else if (char.IsDigit(c)) {
                return ClassifierAction.Continue;
            }
            else if (isOperator(c)) {
                State = ClassifierState.Start;
                return ClassifierAction.EmitAndReprocess;
            }
            else {
                State = ClassifierState.Bad;
                return ClassifierAction.Error;
            }
        }
        private ClassifierAction ProcessOperator(char c)
        {
            if (char.IsWhiteSpace(c)) {
                State = ClassifierState.Start;
                return ClassifierAction.Emit;
            }
            else if (char.IsLetter(c)) {
                State = ClassifierState.Start;
                return ClassifierAction.EmitAndReprocess;
            }
            else if (char.IsDigit(c)) {
                State = ClassifierState.Start;
                return ClassifierAction.EmitAndReprocess;
            }
            else if (isOperator(c)) {
                return ClassifierAction.Continue;
            }
            else {
                State = ClassifierState.Bad;
                return ClassifierAction.Error;
            }
        }
        private ClassifierAction ProcessBad(char c)
        {
            if (char.IsWhiteSpace(c)) {
                State = ClassifierState.Start;
                return ClassifierAction.Emit;
            }
            else if (isOperator(c)) {
                State = ClassifierState.Start;
                return ClassifierAction.EmitAndReprocess;
            }
            else {
                return ClassifierAction.Continue;
            }
        }

        private bool isOperator(char c)
        {
            return _operatorChars.Contains(c);
        }
    }
}
