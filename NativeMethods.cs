using System.Runtime.InteropServices;

namespace EasyAutoScript
{
    public partial class NativeMethods
    {
        [LibraryImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static partial IntPtr GetForegroundWindowImport();

        [LibraryImport("user32.dll", EntryPoint = "GetWindowTextLengthW")]
        private static partial int GetWindowTextLengthWImport(IntPtr hWnd);

        [LibraryImport("user32.dll", EntryPoint = "GetWindowTextW", StringMarshalling = StringMarshalling.Utf16)]
        private static partial int GetWindowTextWImport(IntPtr hWnd, [Out] char[] lpString, int nMaxCount);

        [LibraryImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetForegroundWindowImport(IntPtr hWnd);

        public static IntPtr GetForegroundWindow()
        {
            return GetForegroundWindowImport();
        }

        public static string GetWindowTitle(IntPtr hWnd)
        {
            if (hWnd == 0)
            {
                hWnd = GetForegroundWindow();
            }
            int windowLength = GetWindowTextLengthWImport(hWnd);
            if (windowLength == 0)
            {
                throw new EvaluatorException("Unable to find window");
            }
            char[] windowTitle = new char[windowLength];
            GetWindowTextWImport(hWnd, windowTitle, windowLength + 1);
            return new string(windowTitle);
        }

        public static void SetForegroundWindow(IntPtr hWnd)
        {
            SetForegroundWindowImport(hWnd);
        }
    }
}