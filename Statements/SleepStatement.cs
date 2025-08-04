using EasyAutoScript.Expressions;

namespace EasyAutoScript.Statements
{
    public class SleepStatement(IExpression expression) : IStatement
    {
        public readonly IExpression expression = expression;
    }
}