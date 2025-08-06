using System.Collections;

namespace EasyAutoScript.Components
{
    public class WriteStatementHandler(object value)
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
                case IEnumerable enumerable:
                    foreach (var item in enumerable)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                default:
                    throw new InterpreterException($"Unhandled Write Type: {value.GetType()}");
            }
        }
    }
}