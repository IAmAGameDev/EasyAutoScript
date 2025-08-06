using System.Runtime.InteropServices;

namespace EasyAutoScript
{
    public partial class NativeMethods
    {
        [LibraryImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static partial IntPtr GetForegroundWindowImport();

        [LibraryImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetForegroundWindowImport(IntPtr hWnd);

        public static IntPtr GetForegroundWindow()
        {
            return GetForegroundWindowImport();
        }

        public static void SetForegroundWindow(IntPtr hWnd)
        {
            SetForegroundWindowImport(hWnd);
        }
    }
}