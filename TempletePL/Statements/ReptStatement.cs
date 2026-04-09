using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Expressions;

namespace TempletePL.Statements
{
    public class ReptStatement : Statement
    {
        private Expression expr;
        private List<Statement> statements;

        public ReptStatement(Expression expr, List<Statement> statements)
        {
            this.expr = expr;
            this.statements = statements;
        }

        public void run()
        {
            int count = (int)expr.run().GetDouble();

            for (int i = 0; i < count; i++)
            {
                try
                {
                    foreach (Statement s in statements)
                        s.run();
                }
                catch (Exception e)
                {
                    if (e.Message[0] == 'B') break;
                    if (e.Message[0] == 'C') continue;
                }
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
