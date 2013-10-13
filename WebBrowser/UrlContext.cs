namespace TenDayBrowser
{
    using System;

    [Flags]
    public enum UrlContext
    {
        FromProxy = 0x40,
        HtmlDialog = 0x20,
        None = 0,
        OverrideKey = 8,
        ShowHelp = 0x10,
        Unloading = 1,
        UserFirstInited = 4,
        UserInited = 2
    }
}
