using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class BoolExpression : Expression
    {
        private bool value;

        public BoolExpression (bool value)
        {
            this.value = value;
        }

        public Value run()
        {
            return new BoolValue (value);
        }

        public override string ToString()
        {
            return (value) ? "true" : "false";
        }
    }
}
