using System.Runtime.InteropServices;

namespace EasyAutoScript.Native
{
    public static partial class NativeMethods
    {
        [LibraryImport("user32.dll")]
        private static partial IntPtr GetForegroundWindow();

        public static IntPtr GetForegroundWindowPtr()
        {
            return GetForegroundWindow();
        }
    }
}