namespace TenDayBrowser
{
    using System;
    using System.ComponentModel;

    public class BrowserExtendedNavigatingEventArgs : CancelEventArgs
    {
        private string _Frame;
        private object _pDisp;
        private Uri _Url;
        private UrlContext navigationContext;

        public BrowserExtendedNavigatingEventArgs(object automation, Uri url, string frame, UrlContext navigationContext)
        {
            this._Url = url;
            this._Frame = frame;
            this.navigationContext = navigationContext;
            this._pDisp = automation;
        }

        public object AutomationObject
        {
            get
            {
                return this._pDisp;
            }
            set
            {
                this._pDisp = value;
            }
        }

        public string Frame
        {
            get
            {
                return this._Frame;
            }
        }

        public UrlContext NavigationContext
        {
            get
            {
                return this.navigationContext;
            }
        }

        public Uri Url
        {
            get
            {
                return this._Url;
            }
        }
    }
}
