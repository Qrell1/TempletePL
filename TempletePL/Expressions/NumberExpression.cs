using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class NumberExpression : Expression
    {
        private Value value;

        public NumberExpression(double value)
        {
            this.value = new NumberValue( value );
        }

        public Value run()
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
