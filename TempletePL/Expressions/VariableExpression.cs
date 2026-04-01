using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class VariableExpression : Expression
    {
        private string variable;

        public VariableExpression (string variable)
        {
            this.variable = variable;
        }

        public Value run ()
        {
            return Variables.GetValue (variable);
        }

        public override string ToString()
        {
            return variable;
        }
    }
}
