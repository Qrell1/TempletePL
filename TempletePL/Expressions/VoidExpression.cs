using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class VoidExpression : Expression
    {
        public VoidExpression() { }

        public Value run()
        {
            return new VoidValue();
        }

        public override string ToString()
        {
            return "[void]";
        }
    }
}
