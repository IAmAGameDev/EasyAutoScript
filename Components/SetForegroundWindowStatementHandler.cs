namespace EasyAutoScript.Components
{
    public class SetForegroundWindowStatementHandler(IntPtr value)
    {
        public void Execute()
        {
            NativeMethods.SetForegroundWindow(value);
        }
    }
}