using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace TenDayBrowser
{
    public class FindSubWindow
    {
        private string m_classname;
        private IntPtr m_hWnd = IntPtr.Zero;

        public FindSubWindow(IntPtr hwndParent, string classname)
        {
            this.m_classname = classname;
            this.FindChildClassHwnd(hwndParent, IntPtr.Zero);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32")]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);
        private bool FindChildClassHwnd(IntPtr hwndParent, IntPtr lParam)
        {
            EnumWindowProc callback = new EnumWindowProc(this.FindChildClassHwnd);
            IntPtr ptr = FindWindowEx(hwndParent, IntPtr.Zero, this.m_classname, string.Empty);
            if (ptr != IntPtr.Zero)
            {
                this.m_hWnd = ptr;
                callback = null;
                return false;
            }
            EnumChildWindows(hwndParent, callback, IntPtr.Zero);
            callback = null;
            return true;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        public IntPtr FoundHandle
        {
            get
            {
                return this.m_hWnd;
            }
        }

        private delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);
    }
}
