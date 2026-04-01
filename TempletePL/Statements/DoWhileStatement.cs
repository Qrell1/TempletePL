using TempletePL.Expressions;

namespace TempletePL.Statements
{
    public class DoWhileStatement : Statement
    {
        private Expression condition;
        private List<Statement> statements;

        public DoWhileStatement(Expression condition, List<Statement> statements)
        {
            this.condition = condition;
            this.statements = statements;
        }

        public void run()
        {
            do
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
            } while (condition.run().GetBool());
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
