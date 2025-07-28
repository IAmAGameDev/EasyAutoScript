using System.Runtime.InteropServices;
using EasyAutoScript.Exceptions;

namespace EasyAutoScript.Native
{
    public static partial class NativeMethods
    {
        [LibraryImport("user32.dll")]
        private static partial IntPtr GetForegroundWindow();

        [LibraryImport("user32.dll")]
        private static partial int GetWindowTextLengthW(IntPtr hWnd);

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
        private static partial int GetWindowTextW(IntPtr hWnd, [Out] char[] lpString, int nMaxCount);

        public static IntPtr GetForegroundWindowPtr()
        {
            return GetForegroundWindow();
        }

        internal static string GetOpenWindowTitle()
        {
            IntPtr hWnd = GetForegroundWindow();
            int windowLength = GetWindowTextLengthW(hWnd);
            if (windowLength == 0)
            {
                throw new InterpreterException($"Unable to retrieve window text length with HWND: {hWnd}");
            }
            char[] buffer = new char[windowLength + 1];
            int chars = GetWindowTextW(hWnd, buffer, windowLength + 1);
            return new string(buffer, 0, chars);
        }
    }
}