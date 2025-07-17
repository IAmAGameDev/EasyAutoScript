using EasyAutoScript.Statements;

namespace EasyAutoScript.Expressions
{
    public class StringLiteralExpression(string value) : IExpression
    {
        public readonly string value = value;
    }
}