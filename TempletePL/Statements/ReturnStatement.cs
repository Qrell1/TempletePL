using TempletePL.Expressions;
using TempletePL.Structs;

namespace TempletePL.Statements
{
    public class ReturnStatement : Statement
    {
        private Expression expr;

        public ReturnStatement(Expression expr)
        {
            this.expr = expr;
        }

        public Expression GetExpression()
        {
            return expr;
        }

        public void run()
        {
            Functions.ReturnExpression = expr;
            throw new Exception("R");
        }

        public override string ToString()
        {
            return "return";
        }
    }
}
