using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Statements
{
    public class SufixStatement : Statement
    {
        private string variable;
        private Token operation;

        public SufixStatement(string variable, Token operation)
        {
            this.variable = variable;
            this.operation = operation;
        }

        public void run()
        {
            double variableValueDouble = 0;

            switch (operation.value)
            {
                case "--":
                    variableValueDouble = Variables.GetValue(variable).GetDouble();
                    Variables.VariablesMap.Peek()[variable] = new NumberValue(variableValueDouble - 1);
                    break;
                case "++":
                    variableValueDouble = Variables.GetValue(variable).GetDouble();
                    Variables.VariablesMap.Peek()[variable] = new NumberValue(variableValueDouble + 1);
                    break;
                default: break;
            }
        }
    }
}
