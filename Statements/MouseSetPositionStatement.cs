using EasyAutoScript.Expressions;

namespace EasyAutoScript.Statements
{
    public class MouseSetPositionStatement(IExpression expression, IExpression expression2, IExpression? expression3) : IStatement
    {
        public readonly IExpression expression = expression;
        public readonly IExpression expression2 = expression2;
        public readonly IExpression? expression3 = expression3;
    }
}