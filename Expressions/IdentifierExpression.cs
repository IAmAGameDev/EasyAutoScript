namespace EasyAutoScript.Expressions
{
    public class IdentifierExpression(string name) : IExpression
    {
        public readonly string name = name;

        public object? Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitIdentifierExpression(this);
        }
    }
}