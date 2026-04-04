using TempletePL.Expressions;
using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Statements
{
    public class ArrayDeclarationStatement : Statement
    {
        private string variable;
        private Expression expr;

        public ArrayDeclarationStatement (string variable, Expression expr)
        {
            this.variable = variable;
            this.expr = expr;
        }

        public void run()
        {
            int count = (int)expr.run().GetDouble();
            if (Variables.ArrayMap.Peek().ContainsKey(variable)) Variables.ArrayMap.Peek()[variable] = new Value[count];
            Variables.ArrayMap.Peek().Add(variable, new Value[count]);
        }

        public override string ToString()
        {
            return "[" + expr.ToString() + "]" + variable;
        }
    }
}
