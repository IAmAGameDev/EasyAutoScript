namespace EasyAutoScript.Expressions
{
    public class GetOpenWindowTitleExpression() : IExpression
    {
        public object Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitGetOpenWindowTitleExpression(this);
        }
    }
}