using TempletePL.Structs;
using TempletePL.Types;

namespace TempletePL.Expressions
{
    public class ConditionExpression : Expression
    {
        private Expression left;
        private Expression right;
        private Token operation;

        public ConditionExpression(Expression left, Expression right, Token operation)
        {
            this.left = left;
            this.right = right;
            this.operation = operation;
        }

        public Value run ()
        {
            Value leftValue = left.run();
            Value rightValue = right.run();

            if (operation.value == "&&")
                return new BoolValue((leftValue.GetBool() && rightValue.GetBool()) ? true : false);
            if (operation.value == "||")
                return new BoolValue((leftValue.GetBool() || rightValue.GetBool()) ? true : false);

            if (leftValue.GetType() == typeof(StringValue)
                && rightValue.GetType() == typeof(StringValue))
            {
                switch (operation.value)
                {
                    case ">": return new BoolValue((leftValue.GetString().Length > rightValue.GetString().Length) ? true : false);
                    case "<": return new BoolValue((leftValue.GetString().Length < rightValue.GetString().Length) ? true : false);
                    case ">=": return new BoolValue((leftValue.GetString().Length >= rightValue.GetString().Length) ? true : false);
                    case "<=": return new BoolValue((leftValue.GetString().Length <= rightValue.GetString().Length) ? true : false);
                    case "==": return new BoolValue((leftValue.GetString() == rightValue.GetString()) ? true : false);
                    case "!=": return new BoolValue((leftValue.GetString() != rightValue.GetString()) ? true : false);
                    default: return new BoolValue((leftValue.GetString() == rightValue.GetString()) ? true : false);
                }
            }

            if (leftValue.GetType() == typeof(StringValue))
            {
                double rightValueNumber = rightValue.GetDouble();
                switch (operation.value)
                {
                    case ">": return new BoolValue((leftValue.GetString().Length > rightValueNumber) ? true : false);
                    case "<": return new BoolValue((leftValue.GetString().Length < rightValueNumber) ? true : false);
                    case ">=": return new BoolValue((leftValue.GetString().Length >= rightValueNumber) ? true : false);
                    case "<=": return new BoolValue((leftValue.GetString().Length <= rightValueNumber) ? true : false);
                    case "==": return new BoolValue((leftValue.GetString().Length == rightValueNumber) ? true : false);
                    case "!=": return new BoolValue((leftValue.GetString().Length != rightValueNumber) ? true : false);
                    default: return new BoolValue((leftValue.GetString().Length == rightValueNumber) ? true : false);
                }
            }
            if (rightValue.GetType() == typeof(StringValue))
            {
                double leftValueNumber = leftValue.GetDouble();
                switch (operation.value)
                {
                    case ">": return new BoolValue((leftValueNumber > rightValue.GetString().Length) ? true : false);
                    case "<": return new BoolValue((leftValueNumber < rightValue.GetString().Length) ? true : false);
                    case ">=": return new BoolValue((leftValueNumber >= rightValue.GetString().Length) ? true : false);
                    case "<=": return new BoolValue((leftValueNumber <= rightValue.GetString().Length) ? true : false);
                    case "==": return new BoolValue((leftValueNumber == rightValue.GetString().Length) ? true : false);
                    case "!=": return new BoolValue((leftValueNumber != rightValue.GetString().Length) ? true : false);
                    default: return new BoolValue((leftValueNumber == rightValue.GetString().Length) ? true : false);
                }
            }

            double leftNumber =  leftValue.GetDouble();
            double rightNumber = rightValue.GetDouble();
            switch (operation.value)
            {
                case ">": return new BoolValue((leftNumber > rightNumber) ? true : false);
                case "<": return new BoolValue((leftNumber < rightNumber) ? true : false);
                case ">=": return new BoolValue((leftNumber >= rightNumber) ? true : false);
                case "<=": return new BoolValue((leftNumber <= rightNumber) ? true : false);
                case "==": return new BoolValue((leftNumber == rightNumber) ? true : false);
                case "!=": return new BoolValue((leftNumber != rightNumber) ? true : false);
                default: return new BoolValue((leftNumber == rightNumber) ? true : false);
            }
        }

        public override string ToString()
        {
            return left.ToString() + operation.value + right.ToString();
        }
    }
}
