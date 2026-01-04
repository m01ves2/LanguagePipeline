using Pipeline.Parser.AST.Exceptions;

namespace Pipeline.Parser.ASTParser
{
    public class Context
    {
        Dictionary<string, double> variables;
        public Context()
        {
            variables = new Dictionary<string, double>();
        }
        
        public double GetVariable(string name)
        {
            double value;
            if (variables.TryGetValue(name, out value))
                return value;
            else
                throw new RuntimeException($"Runtime error: variable '{name}' not found");
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
