using TempletePL.Expressions;

namespace TempletePL.Statements
{
    public class WhileStatement : Statement
    {
        private Expression condition;
        private List<Statement> statements;

        public WhileStatement(Expression condition, List<Statement> statements)
        {
            this.condition = condition;
            this.statements = statements;
        }

        public void run()
        {
            while (condition.run().GetBool())
            {
                try
                {
                    foreach (Statement s in statements)
                        s.run();
                } catch (Exception e)
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
