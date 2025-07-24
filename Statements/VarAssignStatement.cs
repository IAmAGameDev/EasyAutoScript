namespace EasyAutoScript.Statements
{
    public class VarAssignStatement(string name, IExpression expression) : IStatement
    {
        public readonly string name = name;
        public readonly IExpression expression = expression;
    }
}