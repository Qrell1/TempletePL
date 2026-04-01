using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class SufixExpression : Expression
    {
        private string variable;
        private Token operation;
        private bool sufix;

        public SufixExpression(string variable, Token operation, bool sufix)
        {
            this.variable = variable;
            this.operation = operation;
            this.sufix = sufix;
        }

        public Value run()
        {
            NumberValue variableNumber = null;
            double variableValueDouble = 0;
            Value variableValue = null;

            if (sufix)
            {
                switch (operation.value)
                {
                    case "++":
                        variableValue = Variables.GetValue(variable);
                        variableValueDouble = variableValue.GetDouble();
                        variableNumber = new NumberValue(variableValueDouble + 1);
                        Variables.VariablesMap.Peek()[variable] = variableNumber;
                        return variableValue;
                    case "--":
                        variableValue = Variables.GetValue(variable);
                        variableValueDouble = variableValue.GetDouble();
                        variableNumber = new NumberValue(variableValueDouble - 1);
                        Variables.VariablesMap.Peek()[variable] = variableNumber;
                        return variableValue;
                    default: return new NumberValue(0);
                }
            }
            switch (operation.value)
            {
                case "++":
                    variableValueDouble = Variables.GetValue(variable).GetDouble();
                    variableNumber = new NumberValue(variableValueDouble + 1);
                    Variables.VariablesMap.Peek()[variable] = variableNumber;
                    return variableNumber;
                case "--":
                    variableValueDouble = Variables.GetValue(variable).GetDouble();
                    variableNumber = new NumberValue(variableValueDouble - 1);
                    Variables.VariablesMap.Peek()[variable] = variableNumber;
                    return variableNumber;
                default: return new NumberValue(0);
            }
        }

        public override string ToString()
        {
            return (sufix) ? "[" + variable + operation.value + "]" : "[" + operation.value + variable + "]";
        }
    }
}
