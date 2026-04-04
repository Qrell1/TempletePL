using TempletePL.Expressions;
using TempletePL.Structs;
using TempletePL.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TempletePL.Statements
{
    public class ArrayAssignStatement : Statement
    {
        private string variable;
        private Expression expr;
        private Expression right;

        public ArrayAssignStatement (string variable, Expression expr, Expression right)
        {
            this.variable = variable;
            this.expr = expr;
            this.right = right;
        }

        public void run ()
        {
            if (!Variables.ArrayMap.Peek().ContainsKey(variable)) throw new Exception($"Массива {variable} не существует");
            int count = (int)expr.run().GetDouble();
            Value value = right.run();
            if (Variables.ArrayMap.Peek()[variable].Length <= count) throw new Exception($"Индекс за пределом массива {variable}");
            Variables.ArrayMap.Peek()[variable][count] = value;
        }

        public override string ToString()
        {
            return variable + "[" + expr.ToString() + "] = " + right.ToString(); 
        }
    }
}
