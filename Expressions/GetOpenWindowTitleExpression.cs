using EasyAutoScript.Native;
using EasyAutoScript.Statements;

namespace EasyAutoScript.Expressions
{
    public class GetOpenWindowTitleExpression() : IExpression
    {
        public static string Evaluate()
        {
            return NativeMethods.GetOpenWindowTitle();
        }
    }
}