using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class ArrayExpression : Expression
    {
        private string variable;
        private Expression expr;

        public ArrayExpression (string variable, Expression expr)
        {
            this.variable = variable;
            this.expr = expr;
        }

        public Value run ()
        {
            int number = (int)expr.run().GetDouble();
            if (!Variables.ArrayMap.Peek().ContainsKey(variable)) throw new Exception($"Списка {variable} не существует!");
            if (Variables.ArrayMap.Peek()[variable].Length <= number) return Variables.ArrayMap.Peek()[variable].Last();
            return Variables.ArrayMap.Peek()[variable][number];
        }

        public override string ToString()
        {
            return $"{variable}[{(int)expr.run().GetDouble()}]";
        }
    }
}
