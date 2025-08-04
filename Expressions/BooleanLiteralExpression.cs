namespace EasyAutoScript.Expressions
{
    public class BooleanLiteralExpression(bool value) : IExpression
    {
        public readonly bool value = value;

        public object Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitBooleanLiteralExpression(this);
        }
    }
}