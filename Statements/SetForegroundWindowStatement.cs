namespace EasyAutoScript.Statements
{
    public class SetForegroundWindowStatement(IExpression expression) : IStatement
    {
        public readonly IExpression expression = expression;
    }
}