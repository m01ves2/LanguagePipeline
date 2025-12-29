namespace Pipeline.Lexer.Classifier
{
    public enum ClassifierState
    {
        Start,
        Identifier,
        Number,
        Operator,
        Bad
    }
}
