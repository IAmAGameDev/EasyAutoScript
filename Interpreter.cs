using EasyAutoScript.Components;
using EasyAutoScript.Expressions;
using EasyAutoScript.Statements;

namespace EasyAutoScript
{
    public class Interpreter(List<IStatement> statements)
    {
        readonly ExpressionEvaluator expressionEvaluator = new();

        public async Task Interpret()
        {
            foreach (var statement in statements)
            {
                switch (statement)
                {
                    case ClearStatement:
                        ClearStatementHandler.Execute();
                        break;

                    case SleepStatement sleepStatement:
                        {
                            double value = expressionEvaluator.ConvertToDouble(sleepStatement.expression);
                            SleepStatementHandler sleepStatementHandler = new(value);
                            await sleepStatementHandler.Execute();
                            break;
                        }
                    case WriteStatement writeStatement:
                        WriteStatementHandler writeStatementHandler = new(expressionEvaluator.Evaluate(writeStatement.expression));
                        writeStatementHandler.Execute();
                        break;

                    default:
                        throw new InterpreterException($"Unhandled statement: {statement}");
                }
            }
        }
    }
}