using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenDayBrowser
{
    using System;
    using System.Runtime.InteropServices;

    public class LoadTempHtml
    {
        private const int ERROR_FILE_NOT_FOUND = 2;
        private static int INTERNET_OPTION_CONNECTED_STATE = 50;
        private static int INTERNET_STATE_CONNECTED = 1;
        private static int INTERNET_STATE_DISCONNECTED = 0x10;
        private static int ISO_FORCE_DISCONNECTED = 1;

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetUrlCacheEntryInfo(string lpszUrlName, IntPtr lpCacheEntryInfo, ref int lpdwCacheEntryInfoBufferSize);
        [DllImport("wininet.dll")]
        private static extern bool InternetSetOption(int hInternet, int dwOption, ref INTERNET_CONNECTED_INFO lpBuffer, int dwBufferLength);
        public static void SetIEConnectionMode(bool offline)
        {
            INTERNET_CONNECTED_INFO lpBuffer = new INTERNET_CONNECTED_INFO();
            if (offline)
            {
                lpBuffer.dwConnectedState = INTERNET_STATE_DISCONNECTED;
                lpBuffer.dwFlags = ISO_FORCE_DISCONNECTED;
            }
            else
            {
                lpBuffer.dwConnectedState = INTERNET_STATE_CONNECTED;
            }
            InternetSetOption(0, INTERNET_OPTION_CONNECTED_STATE, ref lpBuffer, Marshal.SizeOf(lpBuffer));
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INTERNET_CONNECTED_INFO
        {
            public int dwConnectedState;
            public int dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LPINTERNET_CACHE_ENTRY_INFO
        {
            public int dwStructSize;
            private IntPtr lpszSourceUrlName;
            public IntPtr lpszLocalFileName;
            private int CacheEntryType;
            private int dwUseCount;
            private int dwHitRate;
            private int dwSizeLow;
            private int dwSizeHigh;
            private FILETIME LastModifiedTime;
            private FILETIME Expiretime;
            private FILETIME LastAccessTime;
            private FILETIME LastSyncTime;
            private IntPtr lpHeaderInfo;
            private int dwheaderInfoSize;
            private IntPtr lpszFileExtension;
            private int dwEemptDelta;
        }
    }
}
