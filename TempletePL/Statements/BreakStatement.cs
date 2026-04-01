using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempletePL.Statements
{
    public class BreakStatement : Statement
    {
        public BreakStatement() { }

        public void run()
        {
            throw new Exception("B");
        }

        public override string ToString()
        {
            return "break";
        }
    }
}
