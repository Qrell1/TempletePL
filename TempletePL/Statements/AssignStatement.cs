using TempletePL.Expressions;
using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Statements
{
    internal class AssignStatement : Statement
    {
        private string variable;
        private Expression expr;

        public AssignStatement(string variable, Expression expr)
        {
            this.variable = variable;
            this.expr = expr;
        }

        public void run()
        {
            Value value = expr.run();
            if (Variables.VariablesMap.Peek().ContainsKey(variable))
                Variables.VariablesMap.Peek()[variable] = value;
            else Variables.VariablesMap.Peek().Add(variable, value);
        }

        public override string ToString()
        {
            return "\n" + variable + " = " + expr.ToString();
        }
    }
}
