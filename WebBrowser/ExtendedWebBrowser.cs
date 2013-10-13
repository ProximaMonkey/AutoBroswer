namespace TenDayBrowser
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Threading;
    using System.Windows.Forms;

    public class ExtendedTenDayBrowser : WebBrowser
    {
        private string _referer = string.Empty;
        private UnsafeNativeMethods.ITenDayBrowser2 axITenDayBrowser2;
        private AxHost.ConnectionPointCookie cookie;
        private EventHandler _DownloadComplete;
        private EventHandler _Downloading;
        private TenDayBrowserExtendedEvents events;
        private EventHandler _Quit;
        private EventHandler<BrowserExtendedNavigatingEventArgs> _StartNavigate;
        private EventHandler<BrowserExtendedNavigatingEventArgs> _StartNewWindow;

        public event EventHandler DownloadComplete
        {
            add
            {
                EventHandler handler2;
                EventHandler downloadComplete = this._DownloadComplete;
                do
                {
                    handler2 = downloadComplete;
                    EventHandler handler3 = (EventHandler)Delegate.Combine(handler2, value);
                    downloadComplete = Interlocked.CompareExchange<EventHandler>(ref this._DownloadComplete, handler3, handler2);
                }
                while (downloadComplete != handler2);
            }
            remove
            {
                EventHandler handler2;
                EventHandler downloadComplete = this._DownloadComplete;
                do
                {
                    handler2 = downloadComplete;
                    EventHandler handler3 = (EventHandler)Delegate.Remove(handler2, value);
                    downloadComplete = Interlocked.CompareExchange<EventHandler>(ref this._DownloadComplete, handler3, handler2);
                }
                while (downloadComplete != handler2);
            }
        }

        public event EventHandler Downloading
        {
            add
            {
                EventHandler handler2;
                EventHandler downloading = this._Downloading;
                do
                {
                    handler2 = downloading;
                    EventHandler handler3 = (EventHandler)Delegate.Combine(handler2, value);
                    downloading = Interlocked.CompareExchange<EventHandler>(ref this._Downloading, handler3, handler2);
                }
                while (downloading != handler2);
            }
            remove
            {
                EventHandler handler2;
                EventHandler downloading = this._Downloading;
                do
                {
                    handler2 = downloading;
                    EventHandler handler3 = (EventHandler)Delegate.Remove(handler2, value);
                    downloading = Interlocked.CompareExchange<EventHandler>(ref this._Downloading, handler3, handler2);
                }
                while (downloading != handler2);
            }
        }

        public event EventHandler Quit
        {
            add
            {
                EventHandler handler2;
                EventHandler quit = this._Quit;
                do
                {
                    handler2 = quit;
                    EventHandler handler3 = (EventHandler)Delegate.Combine(handler2, value);
                    quit = Interlocked.CompareExchange<EventHandler>(ref this._Quit, handler3, handler2);
                }
                while (quit != handler2);
            }
            remove
            {
                EventHandler handler2;
                EventHandler quit = this._Quit;
                do
                {
                    handler2 = quit;
                    EventHandler handler3 = (EventHandler)Delegate.Remove(handler2, value);
                    quit = Interlocked.CompareExchange<EventHandler>(ref this._Quit, handler3, handler2);
                }
                while (quit != handler2);
            }
        }

        public event EventHandler<BrowserExtendedNavigatingEventArgs> StartNavigate
        {
            add
            {
                EventHandler<BrowserExtendedNavigatingEventArgs> handler2;
                EventHandler<BrowserExtendedNavigatingEventArgs> startNavigate = this._StartNavigate;
                do
                {
                    handler2 = startNavigate;
                    EventHandler<BrowserExtendedNavigatingEventArgs> handler3 = (EventHandler<BrowserExtendedNavigatingEventArgs>)Delegate.Combine(handler2, value);
                    startNavigate = Interlocked.CompareExchange<EventHandler<BrowserExtendedNavigatingEventArgs>>(ref this._StartNavigate, handler3, handler2);
                }
                while (startNavigate != handler2);
            }
            remove
            {
                EventHandler<BrowserExtendedNavigatingEventArgs> handler2;
                EventHandler<BrowserExtendedNavigatingEventArgs> startNavigate = this._StartNavigate;
                do
                {
                    handler2 = startNavigate;
                    EventHandler<BrowserExtendedNavigatingEventArgs> handler3 = (EventHandler<BrowserExtendedNavigatingEventArgs>)Delegate.Remove(handler2, value);
                    startNavigate = Interlocked.CompareExchange<EventHandler<BrowserExtendedNavigatingEventArgs>>(ref this._StartNavigate, handler3, handler2);
                }
                while (startNavigate != handler2);
            }
        }

        public event EventHandler<BrowserExtendedNavigatingEventArgs> StartNewWindow
        {
            add
            {
                EventHandler<BrowserExtendedNavigatingEventArgs> handler2;
                EventHandler<BrowserExtendedNavigatingEventArgs> startNewWindow = this._StartNewWindow;
                do
                {
                    handler2 = startNewWindow;
                    EventHandler<BrowserExtendedNavigatingEventArgs> handler3 = (EventHandler<BrowserExtendedNavigatingEventArgs>)Delegate.Combine(handler2, value);
                    startNewWindow = Interlocked.CompareExchange<EventHandler<BrowserExtendedNavigatingEventArgs>>(ref this._StartNewWindow, handler3, handler2);
                }
                while (startNewWindow != handler2);
            }
            remove
            {
                EventHandler<BrowserExtendedNavigatingEventArgs> handler2;
                EventHandler<BrowserExtendedNavigatingEventArgs> startNewWindow = this._StartNewWindow;
                do
                {
                    handler2 = startNewWindow;
                    EventHandler<BrowserExtendedNavigatingEventArgs> handler3 = (EventHandler<BrowserExtendedNavigatingEventArgs>)Delegate.Remove(handler2, value);
                    startNewWindow = Interlocked.CompareExchange<EventHandler<BrowserExtendedNavigatingEventArgs>>(ref this._StartNewWindow, handler3, handler2);
                }
                while (startNewWindow != handler2);
            }
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        protected override void AttachInterfaces(object nativeActiveXObject)
        {
            this.axITenDayBrowser2 = (UnsafeNativeMethods.ITenDayBrowser2)nativeActiveXObject;
            base.AttachInterfaces(nativeActiveXObject);
            this.axITenDayBrowser2.Silent = true;
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        protected override void CreateSink()
        {
            base.CreateSink();
            this.events = new TenDayBrowserExtendedEvents(this);
            this.cookie = new AxHost.ConnectionPointCookie(base.ActiveXInstance, this.events, typeof(UnsafeNativeMethods.DTenDayBrowserEvents2));
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        protected override void DetachInterfaces()
        {
            this.axITenDayBrowser2 = null;
            base.DetachInterfaces();
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        protected override void DetachSink()
        {
            if (this.cookie != null)
            {
                this.cookie.Disconnect();
            }
            this.cookie = null;
            this.events = null;
            base.DetachSink();
        }

        protected virtual void OnDownloadComplete(EventArgs e)
        {
            if (this._DownloadComplete != null)
            {
                this._DownloadComplete(this, e);
            }
        }

        protected void OnDownloading(EventArgs e)
        {
            if (this._Downloading != null)
            {
                this._Downloading(this, e);
            }
        }

        protected void OnQuit()
        {
            EventHandler quit = this._Quit;
            if (quit != null)
            {
                quit(this, EventArgs.Empty);
            }
        }

        protected void OnStartNavigate(BrowserExtendedNavigatingEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (this._StartNavigate != null)
            {
                this._StartNavigate(this, e);
            }
        }

        protected void OnStartNewWindow(BrowserExtendedNavigatingEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (this._StartNewWindow != null)
            {
                this._StartNewWindow(this, e);
            }
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x210)
            {
                int num2 = m.WParam.ToInt32() & 0xffff;
                if (num2 == 2)
                {
                    this.OnQuit();
                }
            }
            base.WndProc(ref m);
        }

        public object Application
        {
            get
            {
                return this.axITenDayBrowser2.Application;
            }
        }

        public UnsafeNativeMethods.ITenDayBrowser2 ITenDayBrowser2
        {
            get
            {
                return this.axITenDayBrowser2;
            }
        }

        public string Referer
        {
            get
            {
                return this._referer;
            }
            set
            {
                this._referer = value;
            }
        }

        private class TenDayBrowserExtendedEvents : UnsafeNativeMethods.DTenDayBrowserEvents2
        {
            private ExtendedTenDayBrowser _Browser;

            public TenDayBrowserExtendedEvents()
            {
            }

            public TenDayBrowserExtendedEvents(ExtendedTenDayBrowser browser)
            {
                this._Browser = browser;
            }

            public void BeforeNavigate2(object pDisp, ref object URL, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
            {
                try
                {
                    Uri url = new Uri(URL.ToString());
                    string frame = null;
                    if (targetFrameName != null)
                    {
                        frame = targetFrameName.ToString();
                    }
                    BrowserExtendedNavigatingEventArgs e = new BrowserExtendedNavigatingEventArgs(pDisp, url, frame, UrlContext.None);
                    this._Browser.OnStartNavigate(e);
                    cancel = e.Cancel;
                    pDisp = e.AutomationObject;
                }
                catch
                {
                }
            }

            public void ClientToHostWindow(ref int CX, ref int CY)
            {
            }

            public void CommandStateChange(int Command, bool Enable)
            {
            }

            public void DocumentComplete(object pDisp, ref object URL)
            {
            }

            public void DownloadBegin()
            {
                this._Browser.OnDownloading(EventArgs.Empty);
            }

            public void DownloadComplete()
            {
                this._Browser.OnDownloadComplete(EventArgs.Empty);
            }

            public void FileDownload(ref bool cancel)
            {
            }

            public void NavigateComplete2(object pDisp, ref object URL)
            {
            }

            public void NavigateError(object pDisp, ref object URL, ref object frame, ref object statusCode, ref bool cancel)
            {
            }

            public void NewWindow2(ref object pDisp, ref bool cancel)
            {
                BrowserExtendedNavigatingEventArgs e = new BrowserExtendedNavigatingEventArgs(pDisp, null, null, UrlContext.None);
                this._Browser.OnStartNewWindow(e);
                cancel = e.Cancel;
                pDisp = e.AutomationObject;
            }

            public void NewWindow3(ref object ppDisp, ref bool Cancel, uint dwFlags, string bstrUrlContext, string bstrUrl)
            {
                BrowserExtendedNavigatingEventArgs e = new BrowserExtendedNavigatingEventArgs(ppDisp, new Uri(bstrUrl), null, (UrlContext)dwFlags);
                this._Browser.OnStartNewWindow(e);
                Cancel = e.Cancel;
                ppDisp = e.AutomationObject;
            }

            public void OnFullScreen(bool fullScreen)
            {
            }

            public void OnMenuBar(bool menuBar)
            {
            }

            public void OnQuit()
            {
            }

            public void OnStatusBar(bool statusBar)
            {
            }

            public void OnTheaterMode(bool theaterMode)
            {
            }

            public void OnToolBar(bool toolBar)
            {
            }

            public void OnVisible(bool visible)
            {
            }

            public void PrintTemplateInstantiation(object pDisp)
            {
            }

            public void PrintTemplateTeardown(object pDisp)
            {
            }

            public void PrivacyImpactedStateChange(bool bImpacted)
            {
            }

            public void ProgressChange(int progress, int progressMax)
            {
            }

            public void PropertyChange(string szProperty)
            {
            }

            public void SetSecureLockIcon(int secureLockIcon)
            {
            }

            public void StatusTextChange(string text)
            {
            }

            public void TitleChange(string text)
            {
            }

            public void UpdatePageStatus(object pDisp, ref object nPage, ref object fDone)
            {
            }

            [DispId(0x107)]
            public void WindowClosing(bool isChildWindow, ref bool cancel)
            {
            }

            public void WindowSetHeight(int height)
            {
            }

            public void WindowSetLeft(int left)
            {
            }

            public void WindowSetResizable(bool resizable)
            {
            }

            public void WindowSetTop(int top)
            {
            }

            public void WindowSetWidth(int width)
            {
            }
        }
    }
}
