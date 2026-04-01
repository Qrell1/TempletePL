using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class ValueExpression : Expression
    {
        private Value value;

        public ValueExpression(Value value)
        {
            this.value = value;
        }

        public Value run()
        {
            return value;
        }

        public override string ToString()
        {
            return value.GetString();
        }
    }
}
