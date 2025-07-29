using System.Runtime.InteropServices;
using EasyAutoScript.Exceptions;

namespace EasyAutoScript.Native
{
    public static partial class NativeMethods
    {
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [LibraryImport("user32.dll")]
        private static partial IntPtr GetForegroundWindow();

        [LibraryImport("user32.dll")]
        private static partial int GetWindowTextLengthW(IntPtr hWnd);

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
        private static partial int GetWindowTextW(IntPtr hWnd, [Out] char[] lpString, int nMaxCount);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool IsWindowVisible(IntPtr hWnd);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetForegroundWindow(IntPtr hWnd);

        public static IntPtr GetForegroundWindowPtr()
        {
            return GetForegroundWindow();
        }

        public static string GetOpenWindowTitle()
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

        public static string[] GetAllOpenWindowTitles(bool displayHidden)
        {
            List<string> windows = [];
            List<string> hiddenWindows =
            [
                "Settings",
                "Windows Input Experience",
                "Windows Shell Experience Host",
                "NVIDIA GeForce Overlay",
                "Program Manager",
            ];
            EnumWindows(delegate (IntPtr hWnd, IntPtr param)
            {
                if (!IsWindowVisible(hWnd))
                {
                    return true;
                }
                int windowLength = GetWindowTextLengthW(hWnd);
                if (windowLength == 0)
                {
                    return true;
                }
                char[] buffer = new char[windowLength + 1];
                int chars = GetWindowTextW(hWnd, buffer, windowLength + 1);
                string windowTitle = new(buffer, 0, chars);
                if (!displayHidden && hiddenWindows.Contains(windowTitle))
                {
                    return true;
                }
                if (!windows.Contains(windowTitle))
                {
                    windows.Add(windowTitle);
                }
                return true;
            }, IntPtr.Zero);
            return [.. windows];
        }

        internal static void SetForegroundWindowFromPtr(IntPtr value)
        {
            SetForegroundWindow(value);
        }
    }
}