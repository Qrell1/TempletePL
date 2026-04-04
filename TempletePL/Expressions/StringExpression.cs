using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class StringExpression : Expression
    {
        private StringValue value;

        public StringExpression (string value)
        {
            this.value = new StringValue(value);
        }

        public Value run ()
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
