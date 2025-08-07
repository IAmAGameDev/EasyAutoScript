namespace EasyAutoScript.Expressions
{
    public interface IExpression
    {
        public object? Accept(IExpressionVisitor visitor);
    }
}