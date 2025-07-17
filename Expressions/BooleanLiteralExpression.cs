using EasyAutoScript.Statements;

namespace EasyAutoScript.Expressions
{
    public class BooleanLiteralExpression(bool value) : IExpression
    {
        public readonly bool value = value;
    }
}