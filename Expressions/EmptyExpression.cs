namespace EasyAutoScript.Expressions
{
    public class EmptyExpression() : IExpression
    {
        public object? Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitEmptyExpression(this);
        }
    }
}