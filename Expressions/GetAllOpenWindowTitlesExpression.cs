namespace EasyAutoScript.Expressions
{
    public class GetAllOpenWindowTitlesExpression(IExpression? expression) : IExpression
    {
        public readonly IExpression? expression = expression;

        public object Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitGetAllOpenWindowTitlesExpression(this);
        }
    }
}