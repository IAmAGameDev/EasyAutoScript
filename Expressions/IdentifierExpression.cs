using EasyAutoScript.Statements;

namespace EasyAutoScript.Expressions
{
    public class IdentifierExpression(string name) : IExpression
    {
        public readonly string name = name;
    }
}