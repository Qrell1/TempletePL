using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempletePL.Expressions;

namespace TempletePL.Statements
{
    public class ForStatement : Statement
    {
        private VariableDeclarationStatement variableDeclarationStatement;
        private Expression condition;
        private Expression operation;
        private List<Statement> statements;

        public ForStatement(VariableDeclarationStatement variableDeclarationStatement, Expression condition, Expression operation, List<Statement> statements)
        {
            this.variableDeclarationStatement = variableDeclarationStatement;
            this.condition = condition;
            this.operation = operation;
            this.statements = statements;
        }

        public void run()
        {
            variableDeclarationStatement.run();


            for (int j = 0; condition.run().GetBool(); operation.run())
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
