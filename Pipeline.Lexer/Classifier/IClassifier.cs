namespace Pipeline.Lexer.Classifier
{
    public interface IClassifier
    {
        ClassifierState State { get; }
        ClassifierAction Process(char c);
        void Reset();
    }
}
