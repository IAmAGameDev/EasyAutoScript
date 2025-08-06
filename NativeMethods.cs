using System.Runtime.InteropServices;

namespace EasyAutoScript
{
    public partial class NativeMethods
    {
        [LibraryImport("user32.dll", EntryPoint = "EnumWindows")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool EnumWindowsImport(EnumWindowsProc enumProc, IntPtr lParam);
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [LibraryImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static partial IntPtr GetForegroundWindowImport();

        [LibraryImport("user32.dll", EntryPoint = "GetWindowTextLengthW")]
        private static partial int GetWindowTextLengthWImport(IntPtr hWnd);

        [LibraryImport("user32.dll", EntryPoint = "GetWindowTextW", StringMarshalling = StringMarshalling.Utf16)]
        private static partial int GetWindowTextWImport(IntPtr hWnd, [Out] char[] lpString, int nMaxCount);

        [LibraryImport("user32.dll", EntryPoint = "IsWindowVisible")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool IsWindowVisibleImport(IntPtr hWnd);

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
                throw new Exception("Unable to find window with given IntPtr");
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

        public static void SetForegroundWindow(IntPtr hWnd)
        {
            SetForegroundWindowImport(hWnd);
        }
    }
}