namespace EasyAutoScript.Expressions
{
    public class ExpressionEvaluator(Dictionary<string, object> nameAndValue) : IExpressionVisitor
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
                throw new EvaluatorException($"Expected a Number but recieved a: {value.GetType()}");
            }
        }
        #endregion

        public object VisitBooleanLiteralExpression(BooleanLiteralExpression expression)
        {
            return expression.value;
        }

        public object VisitGetForegroundWindowExpression(GetForegroundWindowExpression expression)
        {
            return NativeMethods.GetForegroundWindow();
        }

        public object VisitIdentifierExpression(IdentifierExpression expression)
        {
            if (nameAndValue.TryGetValue(expression.name, out object? value))
            {
                return value;
            }
            throw new EvaluatorException($"Unable to retrieve a stored value for: {expression.name}");
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