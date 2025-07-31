namespace EasyAutoScript.Expressions
{
    public class NumberLiteralExpression(double value) : IExpression
    {
        public readonly double value = value;

        public object Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitNumberLiteralExpression(this);
        }
    }
}