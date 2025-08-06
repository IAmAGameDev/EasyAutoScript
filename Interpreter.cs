using EasyAutoScript.Components;
using EasyAutoScript.Expressions;
using EasyAutoScript.Statements;

namespace EasyAutoScript
{
    public class Interpreter(List<IStatement> statements)
    {
        private readonly Dictionary<string, object> nameAndValue = [];

        public async Task Interpret()
        {
            ExpressionEvaluator expressionEvaluator = new(nameAndValue);
            foreach (var statement in statements)
            {
                switch (statement)
                {
                    case ClearStatement:
                        ClearStatementHandler.Execute();
                        break;
                    case SetForegroundWindowStatement setForegroundWindowStatement:
                        NativeMethods.SetForegroundWindow(expressionEvaluator.ConvertToIntPtr(setForegroundWindowStatement.expression));
                        break;
                    case SleepStatement sleepStatement:
                        {
                            double value = expressionEvaluator.ConvertToDouble(sleepStatement.expression);
                            SleepStatementHandler sleepStatementHandler = new(value);
                            await sleepStatementHandler.Execute();
                            break;
                        }
                    case VarStatement varStatement:
                        if (!nameAndValue.TryAdd(varStatement.name, expressionEvaluator.Evaluate(varStatement.expression)))
                        {
                            throw new InterpreterException($"You already have a variable defined as: {varStatement.name}");
                        }
                        break;
                    case VarAssignStatement varAssignStatement:
                        if (nameAndValue.ContainsKey(varAssignStatement.name))
                        {
                            nameAndValue[varAssignStatement.name] = expressionEvaluator.Evaluate(varAssignStatement.expression);
                        }
                        break;
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