namespace EasyAutoScript.Expressions
{
    public class ExpressionEvaluator : IExpressionVisitor
    {
        #region Public Helpers
        public object Evaluate(IExpression expression)
        {
            return expression.Accept(this);
        }

        public double ConvertToDouble(IExpression expression)
        {
            object value = expression.Accept(this);
            if (value is double doubleValue)
            {
                return doubleValue;
            }
            else
            {
                throw new Exception($"Expected a Number but recieved a: {value.GetType()}");
            }
        }
        #endregion

        public object VisitBooleanLiteralExpression(BooleanLiteralExpression expression)
        {
            return expression.value;
        }

        public object VisitNumberLiteralExpression(NumberLiteralExpression expression)
        {
            return expression.value;
        }

        public object VisitStringLiteralExpression(StringLiteralExpression expression)
        {
            return expression.value;
        }
    }
}