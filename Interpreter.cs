using EasyAutoScript.Components;
using EasyAutoScript.Expressions;
using EasyAutoScript.Statements;

namespace EasyAutoScript
{
    public class Interpreter(List<IStatement> statements)
    {
        public void Interpret()
        {
            foreach (IStatement statement in statements)
            {
                switch (statement)
                {
                    case WriteStatement writeStatement:
                        WriteStatementHandler writeStatementHandler = new(EvaluateExpression(writeStatement.expression));
                        writeStatementHandler.Run();
                        break;
                }
            }
        }

        private object EvaluateExpression(IExpression expression)
        {
            if (expression is BooleanLiteralExpression booleanLiteralExpression)
                return booleanLiteralExpression.value;
            else if (expression is NumberLiteralExpression numberLiteralExpression)
                return numberLiteralExpression.value;
            else if (expression is StringLiteralExpression stringLiteralExpression)
                return stringLiteralExpression.value;
            else
                throw new Exception($"Unable to Interpret: {expression}");
        }
    }
}