using System.Drawing;
using System.Runtime.InteropServices;

namespace EasyAutoScript
{
    public partial class NativeMethods
    {
        [LibraryImport("user32.dll", EntryPoint = "EnumWindows")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool EnumWindowsImport(EnumWindowsProc enumProc, IntPtr lParam);
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [LibraryImport("user32.dll", EntryPoint = "GetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool GetCursorPosImport(out POINT lpPoint);

        [LibraryImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static partial IntPtr GetForegroundWindowImport();

        [LibraryImport("user32.dll", EntryPoint = "GetWindowTextLengthW")]
        private static partial int GetWindowTextLengthWImport(IntPtr hWnd);

        [LibraryImport("user32.dll", EntryPoint = "GetWindowTextW", StringMarshalling = StringMarshalling.Utf16)]
        private static partial int GetWindowTextWImport(IntPtr hWnd, [Out] char[] lpString, int nMaxCount);

        [LibraryImport("user32.dll", EntryPoint = "IsWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool IsWindowImport(IntPtr hWnd);

        [LibraryImport("user32.dll", EntryPoint = "IsWindowVisible")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool IsWindowVisibleImport(IntPtr hWnd);

        [LibraryImport("user32.dll", EntryPoint = "ScreenToClient")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        [LibraryImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetCursorPosImport(int x, int y);

        [LibraryImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetForegroundWindowImport(IntPtr hWnd);

        public static List<string> GetAllOpenWindowsTitles(bool includeHidden)
        {
            string[] excludedWindows = ["Settings", "NVIDIA GeForce Overlay", "Windows Input Experience", "Program Manager"];
            List<string> windowTitles = [];
            EnumWindowsImport(delegate (IntPtr hWnd, IntPtr lParam)
            {
                if (!IsWindowVisibleImport(hWnd))
                {
                    return true;
                }
                string windowTitle = GetWindowTitleHelper(hWnd);
                if (windowTitle == string.Empty || (!includeHidden && excludedWindows.Contains(windowTitle)))
                {
                    return true;
                }

                if (!windowTitles.Contains(windowTitle))
                {
                    windowTitles.Add(windowTitle);
                }

                return true;
            }, IntPtr.Zero);

            return windowTitles;
        }

        public static IntPtr GetForegroundWindow()
        {
            return GetForegroundWindowImport();
        }

        public static string GetWindowTitle(IntPtr hWnd)
        {
            string windowTitle = GetWindowTitleHelper(hWnd);
            if (windowTitle == string.Empty)
            {
                throw new InterpreterException($"Unable to find window with given IntPtr hWnd: {hWnd}");
            }
            return windowTitle;
        }

        public static string GetWindowTitleHelper(IntPtr hWnd)
        {
            if (hWnd == 0)
            {
                hWnd = GetForegroundWindow();
            }
            int windowLength = GetWindowTextLengthWImport(hWnd);
            if (windowLength == 0)
            {
                return string.Empty;
            }
            char[] windowTitle = new char[windowLength];
            GetWindowTextWImport(hWnd, windowTitle, windowLength + 1);
            return new string(windowTitle);
        }

        public static POINT MouseGetPosition(IntPtr hWnd)
        {
            GetCursorPosImport(out POINT lpPoint);
            if (hWnd != 0)
            {
                if (!IsWindowImport(hWnd))
                {
                    throw new InterpreterException($"Unable to find window with given IntPtr hWnd: {hWnd}");
                }
                ScreenToClient(hWnd, ref lpPoint);
            }
            return lpPoint;
        }

        public static void SetForegroundWindow(IntPtr hWnd)
        {
            SetForegroundWindowImport(hWnd);
        }

        internal static void MouseSetPositionRelative(double x, double y)
        {
            GetCursorPosImport(out POINT point);
            SetCursorPosImport(point.X + Convert.ToInt32(x), point.Y + Convert.ToInt32(y));
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int X;
    public int Y;

    public static implicit operator Point(POINT point)
    {
        return new Point(point.X, point.Y);
    }
}