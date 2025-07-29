using EasyAutoScript.Native;
using EasyAutoScript.Statements;

namespace EasyAutoScript.Expressions
{
    public class GetAllOpenWindowTitlesExpression(bool displayHidden) : IExpression
    {
        public readonly bool displayHidden = displayHidden;

        public string[] Evaluate()
        {
            return NativeMethods.GetAllOpenWindowTitles(displayHidden);
        }
    }
}