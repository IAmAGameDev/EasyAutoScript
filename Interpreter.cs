using EasyAutoScript.Components;
using EasyAutoScript.Exceptions;
using EasyAutoScript.Expressions;
using EasyAutoScript.Statements;

namespace EasyAutoScript
{
    public class Interpreter
    {
        private readonly Dictionary<string, object> _variableNamesAndValues = [];
        private readonly ExpressionEvaluator _expressionEvaluator;
        private readonly List<IStatement> _statements;

        public Interpreter(List<IStatement> statements)
        {
            _expressionEvaluator = new(_variableNamesAndValues);
            _statements = statements;
        }

        public async Task Interpret()
        {
            foreach (IStatement statement in _statements)
            {
                switch (statement)
                {
                    case ClearStatement:
                        {
                            ClearStatementHandler.Execute();
                            break;
                        }

                    case SetForegroundWindowStatement setForegroundWindowStatement:
                        {
                            object value = _expressionEvaluator.Evaluate(setForegroundWindowStatement.expression);
                            if (value is double)
                            {
                                SetForegroundWindowStatementHandler setForegroundWindowStatementHandler = new(Convert.ToInt32(value));
                                setForegroundWindowStatementHandler.Execute();
                                break;
                            }
                            throw new InterpreterException($"Expected a 'double' IntPtr hWnd but recieved a: {value.GetType()}");
                        }

                    case SleepStatement sleepStatement:
                        {
                            object value = _expressionEvaluator.Evaluate(sleepStatement.expression);
                            if (value is double)
                            {
                                SleepStatementHandler sleepStatementHandler = new(Convert.ToDouble(value));
                                await sleepStatementHandler.Execute();
                                break;
                            }
                            throw new InterpreterException($"Expected a 'double' sleep duration but recieved a: {value.GetType()}");
                        }

                    case WriteStatement writeStatement:
                        {
                            WriteStatementHandler writeStatementHandler = new(_expressionEvaluator.Evaluate(writeStatement.expression));
                            writeStatementHandler.Execute();
                            break;
                        }

                    // Vars
                    case VarAssignStatement varAssignStatement:
                        {
                            _variableNamesAndValues[varAssignStatement.name] = _expressionEvaluator.Evaluate(varAssignStatement.expression);
                            break;
                        }
                    case VarStatement varStatement:
                        {
                            _variableNamesAndValues.Add(varStatement.name, _expressionEvaluator.Evaluate(varStatement.expression));
                            break;
                        }

                    // Error
                    default:
                        throw new InterpreterException($"Unable to Interpret: {statement}");
                }
            }
        }
    }
}