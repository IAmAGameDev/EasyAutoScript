using EasyAutoScript.Expressions;

namespace EasyAutoScript.Statements
{
    public class WriteStatement(IExpression expression) : IStatement
    {
        public readonly IExpression expression = expression;
    }
}