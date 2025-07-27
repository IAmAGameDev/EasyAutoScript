using EasyAutoScript.Components;
using EasyAutoScript.Exceptions;
using EasyAutoScript.Expressions;
using EasyAutoScript.Statements;

namespace EasyAutoScript
{
    public class Interpreter(List<IStatement> statements)
    {
        private Dictionary<string, object> _variableNamesAndValues = [];

        public async Task Interpret()
        {
            foreach (IStatement statement in statements)
            {
                switch (statement)
                {
                    case ClearStatement:
                        ClearStatementHandler.Execute();
                        break;

                    case SleepStatement sleepStatement:
                        object value = EvaluateExpression(sleepStatement.expression);
                        if (value is double)
                        {
                            SleepStatementHandler sleepStatementHandler = new(Convert.ToDouble(value));
                            await sleepStatementHandler.Execute();
                            break;
                        }
                        throw new InterpreterException($"Expected a 'double' sleep duration but recieved at: {value.GetType()}");

                    case WriteStatement writeStatement:
                        WriteStatementHandler writeStatementHandler = new(EvaluateExpression(writeStatement.expression));
                        writeStatementHandler.Execute();
                        break;


                    case VarAssignStatement varAssignStatement:
                        _variableNamesAndValues[varAssignStatement.name] = EvaluateExpression(varAssignStatement.expression);
                        break;
                    case VarStatement varStatement:
                        _variableNamesAndValues.Add(varStatement.name, EvaluateExpression(varStatement.expression));
                        break;


                    default:
                        throw new InterpreterException($"Unable to Interpret: {statement}");
                }
            }
        }

        public object EvaluateExpression(IExpression expression)
        {
            switch (expression)
            {
                case BooleanLiteralExpression booleanLiteralExpression:
                    return booleanLiteralExpression.value;
                case NumberLiteralExpression numberLiteralExpression:
                    return numberLiteralExpression.value;
                case StringLiteralExpression stringLiteralExpression:
                    return stringLiteralExpression.value;
                case IdentifierExpression identifierExpression:
                    _variableNamesAndValues.TryGetValue(identifierExpression.name, out object? value);
                    if (value != null) { return value; }
                    else throw new InterpreterException($"Unable to Interpret Identifier: {expression}");
                default:
                    throw new InterpreterException($"Unable to Interpret: {expression}");
            }
        }
    }
}