using TempletePL.Expressions;
using TempletePL.Types;

namespace TempletePL.Statements
{
    public class IfStatement : Statement
    {
        private Expression condition;
        private List<Statement> statements;
        private ElseStatement elseStatement;

        public IfStatement(Expression condition, List<Statement> statements, ElseStatement elseStatement)
        {
            this.condition = condition;
            this.statements = statements;
            this.elseStatement = elseStatement;
        }

        public void run()
        {
            bool value = condition.run().GetBool();

            if (value)
            {
                foreach (Statement s in statements)
                {
                    s.run();
                }
                return;
            }
            else if (elseStatement != null) elseStatement.run();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
