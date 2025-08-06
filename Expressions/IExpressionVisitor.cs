namespace EasyAutoScript.Expressions
{
    public interface IExpressionVisitor
    {
        public object VisitBooleanLiteralExpression(BooleanLiteralExpression expression);
        public object VisitGetForegroundWindowExpression(GetForegroundWindowExpression expression);
        public object VisitGetWindowTitleExpression(GetWindowTitleExpression expression);
        public object VisitIdentifierExpression(IdentifierExpression expression);
        public object VisitNumberLiteralExpression(NumberLiteralExpression expression);
        public object VisitStringLiteralExpression(StringLiteralExpression expression);
    }
}