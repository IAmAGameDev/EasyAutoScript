using EasyAutoScript.Statements;

namespace EasyAutoScript.Expressions
{
    public class NumberLiteralExpression(double value) : IExpression
    {
        public readonly double value = value;
    }
}