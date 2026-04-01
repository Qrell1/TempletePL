using TempletePL.Expressions;

namespace TempletePL.Statements
{
    public class ElseStatement : Statement
    {
        private Expression condition;
        private List<Statement> statements;
        private ElseStatement elseStatement;

        public ElseStatement(Expression condition, List<Statement> statements, ElseStatement elseStatement) 
        {
            this.condition = condition;
            this.statements = statements;
            this.elseStatement = elseStatement;
        }

        public void run()
        {
            if (condition != null)
            {
                bool value = condition.run().GetBool();

                if (value)
                {
                    foreach (Statement s in statements)
                        s.run();
                    return;
                }
                else if (elseStatement != null)
                {
                    elseStatement.run();
                }
                return;
            }
            foreach (Statement s in statements)
                s.run();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
