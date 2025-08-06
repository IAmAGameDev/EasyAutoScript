namespace EasyAutoScript.Expressions
{
    public class MouseGetPositionExpression(IExpression? expression) : IExpression
    {
        public readonly IExpression? expression = expression;

        public object Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitMouseGetPositionExpression(this);
        }
    }
}