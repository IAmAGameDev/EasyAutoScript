using EasyAutoScript.Expressions;

namespace EasyAutoScript.Statements
{
    public class MouseSetPositionRelativeStatement(IExpression expression, IExpression expression2) : IStatement
    {
        public readonly IExpression expression = expression;
        public readonly IExpression expression2 = expression2;
    }
}