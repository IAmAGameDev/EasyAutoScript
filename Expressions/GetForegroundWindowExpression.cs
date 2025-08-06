namespace EasyAutoScript.Expressions
{
    public class GetForegroundWindowExpression() : IExpression
    {
        public object Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitGetForegroundWindowExpression(this);
        }
    }
}