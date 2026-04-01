using TempletePL.Expressions;
using TempletePL.Types;

namespace TempletePL.Statements
{
    public class OutStatement : Statement
    {
        private Expression expr;

        public OutStatement(Expression expr)
        {
           this.expr = expr;
        }

        public void run()
        {
            Value value = expr.run();
            Console.Write(value.GetString());
        }

        public override string ToString()
        {
            return "out: " + expr.ToString() + "\n";
        }
    }
}
