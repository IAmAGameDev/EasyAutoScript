namespace EasyAutoScript.Expressions
{
    public class GetAllOpenWindowTitlesExpression(bool displayHidden) : IExpression
    {
        public readonly bool displayHidden = displayHidden;

        public object Accept(IExpressionVisitor visitor)
        {
            return visitor.VisitGetAllOpenWindowTitlesExpression(this);
        }
    }
}