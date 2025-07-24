namespace EasyAutoScript.Statements
{
    public class VarStatement(string name, IExpression expression) : IStatement
    {
        public readonly string name = name;
        public readonly IExpression expression = expression;
    }
}