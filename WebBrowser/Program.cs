using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TenDayBrowser
{
    static class Program
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            long hwnd = 0L;
            if ((args.Length >= 1) && !string.IsNullOrEmpty(args[0]))
            {
                hwnd = Convert.ToInt64(args[0]);
            }
            Application.Run(new TenDayBrowser(hwnd));
        }
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
