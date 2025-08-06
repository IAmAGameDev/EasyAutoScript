
namespace EasyAutoScript.Expressions
{
    public class ExpressionEvaluator(Dictionary<string, object> nameAndValue) : IExpressionVisitor
    {
        #region Public Helpers
        public object Evaluate(IExpression expression)
        {
            return expression.Accept(this);
        }

        public bool ConvertToBoolean(IExpression expression)
        {
            object value = expression.Accept(this);
            if (value is bool boolValue)
            {
                return boolValue;
            }
            else
            {
                throw new EvaluatorException($"Expected a Boolean but recieved a: {value.GetType()}");
            }
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

        public IntPtr ConvertToIntPtr(IExpression expression)
        {
            object value = expression.Accept(this);
            if (value is IntPtr intPtrValue)
            {
                return intPtrValue;
            }
            else if (value is double doubleValue)
            {
                return (IntPtr)doubleValue;
            }
            else
            {
                throw new EvaluatorException($"Expected a IntPtr but recieved a: {value.GetType()}");
            }
        }
        #endregion

        public object VisitBooleanLiteralExpression(BooleanLiteralExpression expression)
        {
            return expression.value;
        }

        public object VisitGetAllOpenWindowTitlesExpression(GetAllOpenWindowTitlesExpression expression)
        {
            bool includeHidden = ConvertToBoolean(expression.expression ?? new BooleanLiteralExpression(false));
            return NativeMethods.GetAllOpenWindowsTitles(includeHidden);
        }

        public object VisitGetForegroundWindowExpression(GetForegroundWindowExpression expression)
        {
            return NativeMethods.GetForegroundWindow();
        }

        public object VisitGetWindowTitleExpression(GetWindowTitleExpression expression)
        {
            IExpression trueExpression = expression.expression ?? new NumberLiteralExpression(0);
            return NativeMethods.GetWindowTitle(ConvertToIntPtr(trueExpression));
        }

        public object VisitIdentifierExpression(IdentifierExpression expression)
        {
            if (nameAndValue.TryGetValue(expression.name, out object? value))
            {
                return value;
            }
            throw new EvaluatorException($"Unable to retrieve a stored value for: {expression.name}");
        }

        public object VisitMouseGetPositionExpression(MouseGetPositionExpression expression)
        {
            IExpression trueExpression = expression.expression ?? new NumberLiteralExpression(0);
            return NativeMethods.MouseGetPosition(ConvertToIntPtr(trueExpression));
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