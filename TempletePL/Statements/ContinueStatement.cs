using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempletePL.Statements
{
    public class ContinueStatement : Statement
    {
        public ContinueStatement() { }

        public void run()
        {
            throw new Exception("C");
        }

        public override string ToString()
        {
            return "continue";
        }
    }
}
