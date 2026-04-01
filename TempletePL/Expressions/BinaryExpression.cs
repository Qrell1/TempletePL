using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class BinaryExpression : Expression
    {
        private Expression left;
        private Expression right;
        private Token operation;

        public BinaryExpression(Expression left, Expression right, Token operation)
        {
            this.left = left;
            this.right = right;
            this.operation = operation;
        }

        public Value run ()
        {
            Value leftValue = left.run();
            Value rightValue = right.run();
            if (leftValue.GetType() == typeof(StringValue))
            {
                string leftString = leftValue.GetString();
                string rightString = rightValue.GetString();
                switch (operation.value)
                {
                    case "*":
                        int count = (int)rightValue.GetDouble();
                        string resualt = string.Empty;
                        for (int i = 0; i < count; i++)
                            resualt += leftString;
                        return new StringValue(resualt);
                    case "-":
                        int end = (int)rightValue.GetDouble();
                        resualt = leftString.Substring(0, end);
                        return new StringValue(resualt);
                    case "+":
                    default: return new StringValue(leftString + rightString);
                }
            }
            if (rightValue.GetType() == typeof(StringValue))
            {
                string leftString = leftValue.GetString();
                string rightString = rightValue.GetString();
                switch (operation.value)
                {
                    case "*":
                        int count = (int)leftValue.GetDouble();
                        string resualt = string.Empty;
                        for (int i = 0; i < count; i++)
                            resualt += rightString;
                        return new StringValue(resualt);
                    case "-":
                        int number = (int)leftValue.GetDouble();
                        return new NumberValue(number - rightString.Length);
                    case "+":
                    default: return new StringValue(leftString + rightString);
                }
            }

            double leftNumber = leftValue.GetDouble();
            double rightNumber = rightValue.GetDouble();
            switch (operation.value)
            {
                case "+": return new NumberValue(leftNumber + rightNumber);
                case "-": return new NumberValue(leftNumber - rightNumber);
                case "/": return new NumberValue(leftNumber / rightNumber);
                case "*": return new NumberValue(leftNumber * rightNumber);
                case "%": return new NumberValue(leftNumber % rightNumber);
                default: return new NumberValue(leftNumber + rightNumber);
            } 
        }

        public override string ToString()
        {
            return "[" + left.ToString() + operation.value + right.ToString() + "]";
        }
    }
}
