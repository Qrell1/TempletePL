using TempletePL.FunctionsStructs;
using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class CallExpression : Expression
    {
        private Expression[] arguments;
        private string name;

        public CallExpression(Expression[] arguments, string name)
        {
            this.arguments = arguments;
            this.name = name;
        }

        public Value run()
        {
            Value[] values = new Value[arguments.Length];
            for (int i = 0; i < values.Length; i++) values[i] = arguments[i].run();

            if (!Functions.FunctionsMap.ContainsKey(name) && !Functions.SystemFunction.Contains(name)) throw new Exception($"Функции {name} не существует!");

            Function function = Functions.GetNewFunction(name);
            return function.run(values);
        }

        public override string ToString()
        {
            return name + arguments.ToString();
        }
    }
}
