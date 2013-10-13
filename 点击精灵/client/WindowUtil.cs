namespace client
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    internal class WindowUtil
    {
        private static Hashtable _processWnd;
        public const uint BM_CLICK = 0xf5;
        public const uint SC_MINIMIZE = 0xf020;
        public const uint SC_RESTORE = 0xf120;
        public const uint SW_HIDE = 0;
        public const uint SW_MAXIMIZE = 3;
        public const uint SW_MINIMIZE = 6;
        public const uint SW_NORMAL = 1;
        public const uint SW_RESTORE = 9;
        public const uint SW_SHOW = 5;
        public const uint SW_SHOWDEFAULT = 10;
        public const uint SW_SHOWMAXIMIZED = 3;
        public const uint SW_SHOWMINIMIZED = 2;
        public const uint SW_SHOWMINNOACTIVE = 7;
        public const uint SW_SHOWNA = 8;
        public const uint SW_SHOWNOACTIVATE = 4;
        public const uint SW_SHOWNORMAL = 1;
        public const long WS_DISABLED = 0x8000000L;

        static WindowUtil()
        {
            if (_processWnd == null)
            {
                _processWnd = new Hashtable();
            }
        }

        public static bool CheckEmail(string account)
        {
            return (!string.IsNullOrEmpty(account) && Regex.IsMatch(account, @"^\w([\w-\.]*)@(\w+(\.\w{2,3}){1,3})$"));
        }

        public static bool DisplayWindow(IntPtr hwnd)
        {
            bool flag = false;
            if (!IsWindowValid(hwnd))
            {
                for (int i = 0; i < 10; i++)
                {
                    SendMessage(hwnd, 0x112, 0xf120, 0);
                    Thread.Sleep(100);
                    if (IsWindowValid(hwnd))
                    {
                        return true;
                    }
                }
                return flag;
            }
            return true;
        }

        [DllImport("user32.dll", SetLastError=true)]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);
        private static bool EnumWindowsProc(IntPtr hwnd, uint lParam)
        {
            int iD = 0;
            if (GetParent(hwnd) == IntPtr.Zero)
            {
                GetWindowThreadProcessId(hwnd, out iD);
                if (iD == lParam)
                {
                    StringBuilder lpString = new StringBuilder(0x400);
                    GetWindowText(hwnd, lpString, lpString.Capacity);
                    if ("点击精灵浏览器".Equals(lpString.ToString()))
                    {
                        _processWnd[iD] = hwnd;
                        SetLastError(0);
                        return false;
                    }
                }
            }
            return true;
        }

        [DllImport("user32.dll", SetLastError=true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError=true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("KERNEL32.DLL", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        public static extern IntPtr GetCurrentProcess();
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError=true)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        public static IntPtr GetWindowHandleFromProcessID(int processID)
        {
            IntPtr zero = IntPtr.Zero;
            object obj2 = null;
            _processWnd.Clear();
            if ((!EnumWindows(new WNDENUMPROC(WindowUtil.EnumWindowsProc), processID) && (Marshal.GetLastWin32Error() == 0)) && _processWnd.Contains(processID))
            {
                obj2 = _processWnd[processID];
                if (obj2 != null)
                {
                    zero = (IntPtr) obj2;
                }
            }
            return zero;
        }

        [DllImport("user32.dll")]
        public static extern long GetWindowLong(IntPtr hWnd, int nIndex);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);
        [DllImport("user32.dll", SetLastError=true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        [DllImport("winInet.dll")]
        public static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);
        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hWnd);
        public static bool IsWindowValid(IntPtr hwnd)
        {
            return IsWindowVisible(hwnd);
        }

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("OLEACC.DLL")]
        public static extern int ObjectFromLresult(int lResult, ref Guid riid, int wParam, [In, Out, MarshalAs(UnmanagedType.Interface)] ref object ppvObject);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern bool RedrawWindow(IntPtr hwnd, COMRECT rcUpdate, IntPtr hrgnUpdate, int flags);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string lpString);
        [DllImport("user32", EntryPoint="SendMessageW")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, ref COPYDATASTRUCT lParam);
        [DllImport("user32", EntryPoint="SendMessageW")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint="SendMessageW")]
        public static extern int SendMessage(IntPtr hwnd, int wmsg, int wparam, StringBuilder lparam);
        [DllImport("user32")]
        public static extern int SendMessageA(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32")]
        public static extern int SendMessageA(IntPtr hwnd, int wMsg, int wParam, StringBuilder lparam);
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("kernel32.dll")]
        public static extern void SetLastError(uint dwErrCode);
        [DllImport("KERNEL32.DLL", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        public static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);
        [DllImport("user32.dll", SetLastError=true)]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, uint cmdShow);
        public static int StringToInt(string s)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(s);
            }
            catch
            {
            }
            return num;
        }

        public static uint StringToUint(string s)
        {
            uint num = 0;
            try
            {
                num = Convert.ToUInt32(s);
            }
            catch
            {
            }
            return num;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point point);

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);
    }
}

