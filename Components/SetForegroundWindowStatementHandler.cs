using EasyAutoScript.Native;

namespace EasyAutoScript.Components
{
    public class SetForegroundWindowStatementHandler(IntPtr value)
    {
        public void Execute()
        {
            NativeMethods.SetForegroundWindowFromPtr(value);
        }
    }
}