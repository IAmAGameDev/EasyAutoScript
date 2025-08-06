namespace EasyAutoScript.Expressions
{
    public class GetWindowTitleExpression(IExpression? expression) : IExpression
    {
        public readonly IExpression? expression = expression;

        public object Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitGetWindowTitleExpression(this);
        }
    }
}