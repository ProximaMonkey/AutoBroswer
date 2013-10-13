namespace TenDayBrowser
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbDaat;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpData;
    }
}
