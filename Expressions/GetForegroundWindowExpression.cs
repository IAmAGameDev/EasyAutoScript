using EasyAutoScript.Native;
using EasyAutoScript.Statements;

namespace EasyAutoScript.Expressions
{
    public class GetForegroundWindowExpression() : IExpression
    {
        public static IntPtr Evaluate()
        {
            return NativeMethods.GetForegroundWindowPtr();
        }
    }
}