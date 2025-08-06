using System.Runtime.InteropServices;

namespace EasyAutoScript
{
    public partial class NativeMethods
    {
        [LibraryImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static partial IntPtr GetForegroundWindowImport();

        public static IntPtr GetForegroundWindow()
        {
            return GetForegroundWindowImport();
        }
    }
}