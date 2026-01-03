namespace Pipeline.Parser.ASTParser
{
    public class Context
    {
        Dictionary<string, double> variables;
        public Context()
        {
            variables = new Dictionary<string, double>();
        }
        // получаем значение переменной по ее имени
        public double GetVariable(string name)
        {
            return variables[name];
        }

        public void SetVariable(string name, double value)
        {
            if (variables.ContainsKey(name))
                variables[name] = value;
            else
                variables.Add(name, value);
        }
    }
}
