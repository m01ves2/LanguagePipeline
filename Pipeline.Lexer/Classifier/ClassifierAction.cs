namespace Pipeline.Lexer.Classifier
{
    public enum ClassifierAction
    {
        Continue,           // символ принят, продолжаем токен
        Emit,               // токен закончен
        EmitAndReprocess,   // токен закончен, символ обработать заново
        Error               // недопустимая последовательность
    }
}
