using System.Collections;

namespace EasyAutoScript.Components
{
    public class WriteStatementHandler(object? value)
    {
        public void Execute()
        {
            switch (value)
            {
                case bool:
                case double:
                case string:
                case IntPtr:
                    Console.WriteLine(value);
                    break;
                case POINT point:
                    Console.WriteLine($"X: {point.X}, Y: {point.Y}");
                    break;
                case IEnumerable enumerable:
                    foreach (var item in enumerable)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                default:
                    if (value is null)
                    {
                        throw new InterpreterException($"A null value was passed in");
                    }
                    throw new InterpreterException($"Unhandled Write Type: {value.GetType()}");
            }
        }
    }
}