using EasyAutoScript.Exceptions;
using EasyAutoScript.Native;

namespace EasyAutoScript.Expressions
{
    public class ExpressionEvaluator(Dictionary<string, object> variableNamesAndValues) : IExpressionVisitor
    {
        private Dictionary<string, object> _variableNamesAndValues = variableNamesAndValues;

        public object Evaluate(IExpression expression)
        {
            return expression.Accept(this);
        }

        public object VisitBooleanLiteralExpression(BooleanLiteralExpression expression)
        {
            return expression.value;
        }

        public object VisitGetAllOpenWindowTitlesExpression(GetAllOpenWindowTitlesExpression expression)
        {
            return NativeMethods.GetAllOpenWindowTitles(expression.displayHidden);
        }

        public object VisitGetForegroundWindowExpression(GetForegroundWindowExpression expression)
        {
            return NativeMethods.GetForegroundWindowPtr();
        }

        public object VisitGetOpenWindowTitleExpression(GetOpenWindowTitleExpression expression)
        {
            return NativeMethods.GetOpenWindowTitle();
        }

        public object VisitIdentifierExpression(IdentifierExpression expression)
        {
            _variableNamesAndValues.TryGetValue(expression.name, out object? value);
            if (value == null)
            {
                throw new ParserException($"Unable to retrieve variable value for: {expression.name}");
            }
            return value;
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