using TempletePL.Expressions;
using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Statements
{
    public class VariableDeclarationStatement : Statement
    {
        private string variable;
        private Expression expr;

        public VariableDeclarationStatement(string variable, Expression expr)
        {
            this.variable = variable;
            this.expr = expr;
        }

        public string GetVariable()
        {
            return variable;
        }

        public void run ()
        {
            Value value = expr.run();
            Variables.AddVariable(variable, value);
        }

        public override string ToString()
        {
            return "\n var " + variable + " = " + expr.ToString();
        }
    }
}
