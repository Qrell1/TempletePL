using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class UnaryExpression : Expression
    {
        private Expression exp;
        private Token operation;

        public UnaryExpression(Expression exp, Token operation)
        {
            this.exp = exp;
            this.operation = operation;
        }

        public Value run()
        {
            switch (operation.value)
            {
                case "-": return new NumberValue(-exp.run().GetDouble());
                case "+": return new NumberValue(exp.run().GetDouble());
                default: return new NumberValue(0);
            }
        }

        public override string ToString()
        {
            return operation.value + exp.ToString();
        }
    }
}
