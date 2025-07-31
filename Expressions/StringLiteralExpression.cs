namespace EasyAutoScript.Expressions
{
    public class StringLiteralExpression(string value) : IExpression
    {
        public readonly string value = value;

        public object Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitStringLiteralExpression(this);
        }
    }
}