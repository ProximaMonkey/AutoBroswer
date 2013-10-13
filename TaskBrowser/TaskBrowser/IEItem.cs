namespace TaskBrowser
{
    using mshtml;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public class IEItem
    {
        private DateTime _beforeWaitTime = new DateTime();
        private ArrayList _browsers = new ArrayList();
        private bool _clearCookieTip;
        private ClickEvent _clickEvent = new ClickEvent();
        private ExtendedWebBrowser _curWebBrowser;
        private string _errorString = string.Empty;
        private Point _fakeMousePoint = new Point(1, 1);
        private Point _fakeMouseTo = new Point(-1, -1);
        private int _fakeScrollHeight = -1;
        private bool _findTimeOut;
        private bool _inputClickTaobaoCloseButton;
        private int _inputIndex = -1;
        private int _inputKeyTime;
        private int _inputTotalKeyTime;
        private int _inputTotalWaitTime;
        private int _inputWaitTime;
        private bool _isCompleted;
        private bool _isDocCompleted;
        private bool _isMoveMouse;
        private bool _isScroll;
        private bool _loop;
        private int _loopTime;
        private int _navigateStatus;
        private DateTime _navigateTime = new DateTime();
        private DateTime _now = new DateTime();
        private string _randomClickLink = string.Empty;
        private int _randomClickLinkCount;
        private int _randomClickLinkIndex;
        private ElementTag _randomClickLinkTag = ElementTag.outerText;
        private DateTime _scrollTime = new DateTime();
        private bool _startLoop;
        private DateTime _startTaskTime = new DateTime();
        private IEStatus _status;
        private MyTask _task;
        private TaskInfo _taskInfo;
        private int _taskInfoIndex;
        private bool _threadRun;
        private int _totalWaitDocCompleteTime;
        private int _totalWaitFindTime;
        private DateTime _waitDocTime = new DateTime();
        private DateTime _waitFindTime = new DateTime();
        private int _waitTime;

        public IEItem(TaskForm parent, MyTask task, int totalWaitFindTime, int totalWaitDocCompleteTime)
        {
            this._startTaskTime = this._now = DateTime.Now;
            this._totalWaitFindTime = totalWaitFindTime;
            this._totalWaitDocCompleteTime = totalWaitDocCompleteTime;
            this._task = task;
            this._threadRun = true;
            this._taskInfoIndex = 0;
            this._startLoop = false;
            this._loop = false;
            this.ResetBrowserComplete();
            this.SetDocCompleted(false);
            parent.ShowTip1("任务ID:" + task._id);
        }

        public void AddBrowser(ExtendedWebBrowser browser)
        {
            this._curWebBrowser = browser;
            this._curWebBrowser.HandleDestroyed += new EventHandler(this.OnCloseBrowser);
            this._browsers.Add(this._curWebBrowser);
            this.ResetBrowserComplete();
        }

        [DllImport("Interop.CleanHistory2Lib.dll")]
        public static extern void AddUrlCache(string url, string str);
        private void CheckDocCompletTimeOut()
        {
            if (!this._isDocCompleted)
            {
                TimeSpan span = (TimeSpan) (this._now - this._waitDocTime);
                if (span.TotalSeconds >= this._totalWaitDocCompleteTime)
                {
                    this.SetDocCompleted(true);
                }
            }
        }

        [DllImport("Interop.CleanHistory2Lib.dll")]
        public static extern void ClearAutoFormHistory();
        [DllImport("Interop.CleanHistory2Lib.dll")]
        public static extern void ClearAutoPasswordHistory();
        [DllImport("Interop.CleanHistory2Lib.dll")]
        public static extern void ClearCookie();
        public bool ClearCookie(TaskForm parent, string clearStr)
        {
            bool flag = false;
            if (!this._clearCookieTip)
            {
                parent.ShowTip2("清除缓存，窗口将不能响应操作几秒钟，请您耐心等待");
                this._clearCookieTip = true;
                return flag;
            }
            this._clearCookieTip = false;
            if (clearStr == "8")
            {
                ClearInternetTempFile();
            }
            else if (clearStr == "2")
            {
                ClearCookie();
            }
            else if (clearStr == "48")
            {
                ClearAutoFormHistory();
                ClearAutoPasswordHistory();
            }
            else if (clearStr == "1")
            {
                ClearIEUrlHistory();
                ClearIEHistory();
            }
            else
            {
                ClearInternetTempFile();
                ClearCookie();
                ClearAutoFormHistory();
                ClearAutoPasswordHistory();
                ClearIEUrlHistory();
                ClearIEHistory();
            }
            return true;
        }

        [DllImport("Interop.CleanHistory2Lib.dll")]
        public static extern void ClearIEHistory();
        [DllImport("Interop.CleanHistory2Lib.dll")]
        public static extern void ClearIEUrlHistory();
        [DllImport("Interop.CleanHistory2Lib.dll")]
        public static extern void ClearInternetTempFile();
        public bool ClickButton(TaskForm parent, string itemName, string tagStr, string indexStr, ref bool isClick)
        {
            bool flag = false;
            if ((this._curWebBrowser != null) && (this._curWebBrowser.Document != null))
            {
                string str;
                if (string.IsNullOrEmpty(indexStr))
                {
                    str = "点击第1个按钮 \"" + itemName + "\"";
                }
                else
                {
                    str = string.Concat(new object[] { "点击第", WindowUtil.StringToInt(indexStr) + 1, "个按钮 \"", itemName, "\"" });
                }
                parent.ShowTip2(str);
                Rectangle rect = new Rectangle();
                IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                mshtml.IHTMLDocument2 domDocument = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument;
                isClick = false;
                if (HtmlUtil.GetButtonRect(domDocument, ref rect, itemName, tagStr, indexStr))
                {
                    flag = false;
                    this.MoveMouseTo(windowHwnd, domDocument, rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), false);
                    if (this.MoveTimeOut())
                    {
                        this.SetFakeMousePoint();
                        if (HtmlUtil.ClickButtonRect(windowHwnd, domDocument, itemName, tagStr, indexStr, ref isClick, ref this._fakeMousePoint, this._clickEvent) && isClick)
                        {
                            this.ResetBrowserComplete();
                        }
                        if (!isClick && this.FindTimeOut())
                        {
                            parent.ShowTip2("点击不到按钮 \"" + itemName + "\"");
                            flag = true;
                        }
                    }
                }
                else
                {
                    this.MoveMouseToBottom(windowHwnd, domDocument);
                    if (this.MoveTimeOut() && this.FindTimeOut())
                    {
                        parent.ShowTip2("没有找到按钮 \"" + itemName + "\"");
                        flag = true;
                    }
                }
                if (domDocument != null)
                {
                    Marshal.ReleaseComObject(domDocument);
                }
                return flag;
            }
            if (this.FindTimeOut())
            {
                flag = true;
            }
            return flag;
        }

        private bool ClickChecked(TaskForm parent, string itemName, string tagStr, string indexStr, ref bool isClick)
        {
            bool flag = false;
            if ((this._curWebBrowser != null) && (this._curWebBrowser.Document != null))
            {
                string str;
                if (string.IsNullOrEmpty(indexStr))
                {
                    str = "点击第1个复选框 \"" + itemName + "\"";
                }
                else
                {
                    str = string.Concat(new object[] { "点击第", WindowUtil.StringToInt(indexStr) + 1, "个复选框 \"", itemName, "\"" });
                }
                parent.ShowTip2(str);
                Rectangle rect = new Rectangle();
                IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                mshtml.IHTMLDocument2 domDocument = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument;
                isClick = false;
                if (HtmlUtil.GetCheckedRect(domDocument, ref rect, itemName, tagStr, indexStr))
                {
                    flag = false;
                    this.MoveMouseTo(windowHwnd, domDocument, rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), false);
                    if (this.MoveTimeOut())
                    {
                        this.SetFakeMousePoint();
                        if (HtmlUtil.ClickCheckedRect(windowHwnd, domDocument, itemName, tagStr, indexStr, ref isClick, ref this._fakeMousePoint, this._clickEvent) && isClick)
                        {
                            this.ResetBrowserComplete();
                        }
                        if (!isClick && this.FindTimeOut())
                        {
                            parent.ShowTip2("点击不到复选框 \"" + itemName + "\"");
                            this._errorString = this._errorString + "点击不到复选框 \"" + itemName;
                            flag = true;
                        }
                    }
                }
                else
                {
                    this.MoveMouseToBottom(windowHwnd, domDocument);
                    if (this.MoveTimeOut() && this.FindTimeOut())
                    {
                        parent.ShowTip2("没有找到复选框 \"" + itemName + "\"");
                        this._errorString = this._errorString + "没有找到复选框 \"" + itemName + "\"";
                        flag = true;
                    }
                }
                if (domDocument != null)
                {
                    Marshal.ReleaseComObject(domDocument);
                }
                return flag;
            }
            if (this.FindTimeOut())
            {
                flag = true;
            }
            return flag;
        }

        public bool ClickLink(TaskForm parent, string itemName, string keyword, string tagStr, string indexStr, ref bool isFind, ref bool isClick, bool setErrorCode, bool checkBusy, ref int clickLinkCount)
        {
            bool flag = false;
            if ((this._curWebBrowser != null) && (this._curWebBrowser.Document != null))
            {
                Rectangle rect = new Rectangle();
                IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                mshtml.IHTMLDocument2 domDocument = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument;
                if (setErrorCode)
                {
                    string str;
                    if (string.IsNullOrEmpty(indexStr))
                    {
                        str = "点击第1个超链接 \"" + itemName + "\"";
                    }
                    else
                    {
                        str = string.Concat(new object[] { "点击第", WindowUtil.StringToInt(indexStr) + 1, "个超链接 \"", itemName, "\"" });
                    }
                    parent.ShowTip2(str);
                }
                isClick = false;
                if (HtmlUtil.GetLinkRect(domDocument, ref rect, itemName, keyword, tagStr, indexStr))
                {
                    isFind = true;
                    flag = false;
                    this.MoveMouseTo(windowHwnd, domDocument, rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), checkBusy);
                    if (this.MoveTimeOut())
                    {
                        this.SetFakeMousePoint();
                        isFind = HtmlUtil.ClickLinkRect(windowHwnd, domDocument, itemName, keyword, tagStr, indexStr, ref isClick, ref this._fakeMousePoint, this._clickEvent, ref clickLinkCount);
                        if (isFind && isClick)
                        {
                            this.ResetBrowserComplete();
                        }
                        if (!isClick && this.FindTimeOut())
                        {
                            if (setErrorCode)
                            {
                                parent.ShowTip2("点击不到超链接 \"" + itemName + "\"");
                                this._errorString = this._errorString + "点击不到超链接 \"" + itemName;
                            }
                            isFind = false;
                            flag = true;
                        }
                    }
                }
                else
                {
                    isFind = true;
                    this.MoveMouseToBottom(windowHwnd, domDocument);
                    if (this.MoveTimeOut() && this.FindTimeOut())
                    {
                        if (setErrorCode)
                        {
                            parent.ShowTip2("没有找到超链接 \"" + itemName + "\"");
                            this._errorString = this._errorString + "没有找到超链接 \"" + itemName + "\"";
                        }
                        isFind = false;
                        flag = true;
                    }
                }
                if (domDocument != null)
                {
                    Marshal.ReleaseComObject(domDocument);
                }
                return flag;
            }
            if (this.FindTimeOut())
            {
                flag = true;
            }
            return flag;
        }

        private bool ClickRadio(TaskForm parent, string itemName, string tagStr, string indexStr, ref bool isClick)
        {
            bool flag = false;
            if ((this._curWebBrowser != null) && (this._curWebBrowser.Document != null))
            {
                string str;
                if (string.IsNullOrEmpty(indexStr))
                {
                    str = "点击第1个单选框 \"" + itemName + "\"";
                }
                else
                {
                    str = string.Concat(new object[] { "点击第", WindowUtil.StringToInt(indexStr) + 1, "个单选框 \"", itemName, "\"" });
                }
                parent.ShowTip2(str);
                Rectangle rect = new Rectangle();
                IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                mshtml.IHTMLDocument2 domDocument = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument;
                isClick = false;
                if (HtmlUtil.GetRadioRect(domDocument, ref rect, itemName, tagStr, indexStr))
                {
                    flag = false;
                    this.MoveMouseTo(windowHwnd, domDocument, rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), false);
                    if (this.MoveTimeOut())
                    {
                        this.SetFakeMousePoint();
                        if (HtmlUtil.ClickRadioRect(windowHwnd, domDocument, itemName, tagStr, indexStr, ref isClick, ref this._fakeMousePoint, this._clickEvent) && isClick)
                        {
                            this.ResetBrowserComplete();
                        }
                        if (!isClick && this.FindTimeOut())
                        {
                            parent.ShowTip2("点击不到单选框 \"" + itemName + "\"");
                            this._errorString = this._errorString + "点击不到单选框 \"" + itemName;
                            flag = true;
                        }
                    }
                }
                else
                {
                    this.MoveMouseToBottom(windowHwnd, domDocument);
                    if (this.MoveTimeOut() && this.FindTimeOut())
                    {
                        parent.ShowTip2("没有找到单选框 \"" + itemName + "\"");
                        this._errorString = this._errorString + "没有找到单选框 \"" + itemName + "\"";
                        flag = true;
                    }
                }
                if (domDocument != null)
                {
                    Marshal.ReleaseComObject(domDocument);
                }
                return flag;
            }
            if (this.FindTimeOut())
            {
                flag = true;
            }
            return flag;
        }

        private bool ClickTaobaoCloseButton(IntPtr hwnd, mshtml.IHTMLDocument2 doc)
        {
            Rectangle rect = new Rectangle();
            bool isClick = false;
            int clickLinkCount = 0;
            if (!this._inputClickTaobaoCloseButton && HtmlUtil.GetLinkRect(doc, ref rect, "close", "", "3", "0"))
            {
                this.MoveMouseTo(hwnd, doc, rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), false);
                if (this.MoveTimeOut())
                {
                    this.SetFakeMousePoint();
                    if (HtmlUtil.ClickLinkRect(hwnd, doc, "close", "", "3", "0", ref isClick, ref this._fakeMousePoint, this._clickEvent, ref clickLinkCount) && isClick)
                    {
                        this._inputClickTaobaoCloseButton = true;
                    }
                    if (!isClick && this.FindTimeOut())
                    {
                        this._inputClickTaobaoCloseButton = true;
                    }
                    if (this._inputClickTaobaoCloseButton)
                    {
                        this.ResetTaskTime();
                    }
                }
            }
            return this._inputClickTaobaoCloseButton;
        }

        public bool DeepClick(TaskForm parent, string clickCount, string waitTime, string keyword)
        {
            bool flag = false;
            mshtml.IHTMLDocument2 domDocument = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument;
            if (this._status == IEStatus.IEStatus_Wait)
            {
                TimeSpan span = (TimeSpan) (this._now - this._beforeWaitTime);
                if (span.TotalSeconds >= this._waitTime)
                {
                    flag = this.GetDeepClickLinkInfo(parent, domDocument, keyword);
                }
                else if (domDocument != null)
                {
                    IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                    this.MoveMouseToBottom(windowHwnd, domDocument);
                }
            }
            else if (this._status == IEStatus.IEStatus_None)
            {
                if (this._loopTime >= WindowUtil.StringToInt(clickCount))
                {
                    this._loop = false;
                    flag = true;
                }
                else
                {
                    this._status = IEStatus.IEStatus_Wait;
                    this._waitTime = WindowUtil.StringToInt(waitTime);
                    this._beforeWaitTime = this._now;
                    parent.ShowTip2(string.Concat(new object[] { "深入点击第", this._loopTime + 1, "次，等待", waitTime, "秒" }));
                }
            }
            else if (this._status == IEStatus.IEStatus_MoveToDest)
            {
                bool isClick = false;
                bool isFind = false;
                parent.ShowTip2(string.Concat(new object[] { "深入点击第", this._loopTime + 1, "次:", this._randomClickLink }));
                this.ClickLink(parent, this._randomClickLink, string.Empty, ((int) this._randomClickLinkTag).ToString(), this._randomClickLinkIndex.ToString(), ref isFind, ref isClick, false, false, ref this._randomClickLinkCount);
                if (isClick || (this._randomClickLinkCount >= 100))
                {
                    this._status = IEStatus.IEStatus_None;
                    flag = true;
                }
                else if ((this._randomClickLinkCount % 10) == 9)
                {
                    this._randomClickLinkCount++;
                    this.ResetTaskTime();
                    flag = this.GetDeepClickLinkInfo(parent, domDocument, keyword);
                }
            }
            else
            {
                flag = true;
            }
            if (domDocument != null)
            {
                Marshal.ReleaseComObject(domDocument);
            }
            return flag;
        }

        public bool FindPage(TaskForm parent, TaskCommand command, string param2, string param3, string param4, string param5)
        {
            mshtml.IHTMLDocument2 document;
            string str3;
            bool flag = false;
            bool isClick = false;
            bool isFind = false;
            Rectangle rect = new Rectangle();
            string[] strArray = param4.Split(new char[] { ',' });
            string str = string.Empty;
            string indexStr = string.Empty;
            int clickLinkCount = 0;
            if (strArray.Length >= 2)
            {
                str = strArray[1];
            }
            if (strArray.Length >= 3)
            {
                indexStr = strArray[2];
            }
            if (((this._curWebBrowser == null) || (this._curWebBrowser.Document == null)) || ((document = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument) == null))
            {
                if (this.FindTimeOut())
                {
                    flag = true;
                }
                return flag;
            }
            if (string.IsNullOrEmpty(str))
            {
                str3 = "查找第1个链接 \"" + param2 + "\"";
            }
            else
            {
                str3 = string.Concat(new object[] { "查找第", WindowUtil.StringToInt(str) + 1, "个链接 \"", param2, "\"" });
            }
            parent.ShowTip2(str3);
            if (((command == TaskCommand.Task_FindLinkLinkPage1) || (command == TaskCommand.Task_FindLinkHrefPage1)) || (command == TaskCommand.Task_FindLinkSrcPage1))
            {
                int num2 = 2;
                if (HtmlUtil.GetLinkRect(document, ref rect, param2, param5, num2.ToString(), str))
                {
                    IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                    this.MoveMouseTo(windowHwnd, document, rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), false);
                    if (this.MoveTimeOut())
                    {
                        this._loop = false;
                        flag = true;
                    }
                    goto Label_0410;
                }
            }
            if (((command == TaskCommand.Task_FindHrefLinkPage1) || (command == TaskCommand.Task_FindHrefHrefPage1)) || (command == TaskCommand.Task_FindHrefSrcPage1))
            {
                int num3 = 6;
                if (HtmlUtil.GetLinkRect(document, ref rect, param2, param5, num3.ToString(), str))
                {
                    IntPtr hwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                    this.MoveMouseTo(hwnd, document, rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), false);
                    if (this.MoveTimeOut())
                    {
                        this._loop = false;
                        flag = true;
                    }
                    goto Label_0410;
                }
            }
            if (((command == TaskCommand.Task_FindSrcLinkPage1) || (command == TaskCommand.Task_FindSrcHrefPage1)) || (command == TaskCommand.Task_FindSrcSrcPage1))
            {
                int num4 = 7;
                if (HtmlUtil.GetLinkRect(document, ref rect, param2, param5, num4.ToString(), str))
                {
                    IntPtr ptr3 = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                    this.MoveMouseTo(ptr3, document, rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), false);
                    if (this.MoveTimeOut())
                    {
                        this._loop = false;
                        flag = true;
                    }
                    goto Label_0410;
                }
            }
            if (this._loopTime < WindowUtil.StringToInt(param4.Split(new char[] { ',' })[0]))
            {
                if (!this._findTimeOut)
                {
                    if (!this.FindTimeOut())
                    {
                        IntPtr ptr4 = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                        this.MoveMouseToBottom(ptr4, document);
                    }
                    else
                    {
                        this._findTimeOut = true;
                        this._scrollTime = this._beforeWaitTime = this._waitFindTime = this._now;
                    }
                }
                else
                {
                    if (((command == TaskCommand.Task_FindLinkLinkPage1) || (command == TaskCommand.Task_FindHrefLinkPage1)) || (command == TaskCommand.Task_FindSrcLinkPage1))
                    {
                        this.ClickLink(parent, param3, string.Empty, 2.ToString(), indexStr, ref isFind, ref isClick, false, false, ref clickLinkCount);
                    }
                    else if (((command == TaskCommand.Task_FindLinkHrefPage1) || (command == TaskCommand.Task_FindHrefHrefPage1)) || (command == TaskCommand.Task_FindSrcHrefPage1))
                    {
                        this.ClickLink(parent, param3, string.Empty, 6.ToString(), indexStr, ref isFind, ref isClick, false, false, ref clickLinkCount);
                    }
                    else if (((command == TaskCommand.Task_FindLinkSrcPage1) || (command == TaskCommand.Task_FindHrefSrcPage1)) || (command == TaskCommand.Task_FindSrcSrcPage1))
                    {
                        this.ClickLink(parent, param3, string.Empty, 7.ToString(), indexStr, ref isFind, ref isClick, false, false, ref clickLinkCount);
                    }
                    if (!isFind)
                    {
                        this._errorString = this._errorString + "没有查找到下一页";
                        flag = true;
                    }
                }
            }
            else
            {
                parent.ShowTip2("超过查找页数，没有找到超地址 \"" + param2 + "\"");
                this._errorString = this._errorString + "超过查找页数，没有找到超地址 \"" + param2 + "\"";
                flag = true;
            }
        Label_0410:
            if (document != null)
            {
                Marshal.ReleaseComObject(document);
            }
            return flag;
        }

        private bool FindTimeOut()
        {
            if (this._isDocCompleted)
            {
                TimeSpan span = (TimeSpan) (this._now - this._waitFindTime);
                return (span.TotalSeconds >= this._totalWaitFindTime);
            }
            return false;
        }

        private bool Fresh(TaskForm parent)
        {
            if (this._curWebBrowser != null)
            {
                parent.ShowTip2("刷新当前页面");
                this._curWebBrowser.Refresh();
                this.ResetBrowserComplete();
            }
            return false;
        }

        private bool GetDeepClickLinkInfo(TaskForm parent, mshtml.IHTMLDocument2 doc2, string keyword)
        {
            bool flag = false;
            if (((this._curWebBrowser != null) && (this._curWebBrowser.Document != null)) && (doc2 != null))
            {
                IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                int clientWidth = 0;
                int clientHeight = 0;
                int scrollWidth = 0;
                int scrollHeight = 0;
                mshtml.IHTMLElement2 element = HtmlUtil.GetWindowWidthAndHeight(windowHwnd, doc2, ref clientWidth, ref clientHeight, ref scrollWidth, ref scrollHeight);
                mshtml.IHTMLElementCollection links = doc2.links;
                ArrayList list = new ArrayList();
                if (string.IsNullOrEmpty(keyword))
                {
                    keyword = "";
                }
                Regex regex = new Regex(keyword + @"(\w)?");
                foreach (mshtml.IHTMLElement element2 in links)
                {
                    if ((((element2.getAttribute("href", 0) != null) && (element2.getAttribute("target", 0) != null)) && element2.getAttribute("target", 0).ToString().ToLower().Equals("_blank")) && (string.IsNullOrEmpty(keyword) || regex.IsMatch(element2.getAttribute("href", 0).ToString())))
                    {
                        Rectangle elementRect = HtmlUtil.GetElementRect(doc2.body, element2);
                        if ((((elementRect.Height > 0) && (elementRect.Width > 0)) && (((elementRect.X + element.scrollLeft) > 0) && ((elementRect.X + element.scrollLeft) < scrollWidth))) && (((elementRect.Y + element.scrollTop) > 0) && ((elementRect.Y + element.scrollTop) < scrollHeight)))
                        {
                            list.Add(element2);
                        }
                    }
                }
                if (list.Count > 0)
                {
                    Random random = new Random();
                    int num5 = random.Next(list.Count);
                    random = null;
                    mshtml.IHTMLElement ele = list[num5] as mshtml.IHTMLElement;
                    if (!string.IsNullOrEmpty(ele.outerText) && !string.IsNullOrEmpty(ele.outerText.Trim()))
                    {
                        this._randomClickLink = ele.outerText;
                        this._randomClickLinkTag = ElementTag.outerText;
                    }
                    else
                    {
                        this._randomClickLinkTag = ElementTag.href;
                        this._randomClickLink = ele.getAttribute("href", 0).ToString();
                    }
                    this._randomClickLinkIndex = HtmlUtil.GetLinkElementIndex(doc2, ele, this._randomClickLink, ((int) this._randomClickLinkTag).ToString());
                    this._status = IEStatus.IEStatus_MoveToDest;
                    this._beforeWaitTime = this._now;
                    list = null;
                    random = null;
                    return flag;
                }
                if (doc2 != null)
                {
                    this.MoveMouseToBottom(windowHwnd, doc2);
                    if (this.MoveTimeOut() && this.FindTimeOut())
                    {
                        parent.ShowTip2("不存在 " + keyword);
                        this._loop = false;
                        flag = true;
                    }
                }
                return flag;
            }
            if (this.FindTimeOut())
            {
                flag = true;
            }
            return flag;
        }

        private int GetLoadPercent()
        {
            if (this._totalWaitFindTime > 0)
            {
                TimeSpan span = (TimeSpan) (this._now - this._beforeWaitTime);
                return ((((int) span.TotalSeconds) * 100) / this._totalWaitFindTime);
            }
            return 100;
        }

        public static IntPtr GetWindowHwnd(int hwnd)
        {
            FindSubWindow window = new FindSubWindow(new IntPtr(hwnd), "Internet Explorer_Server");
            return window.FoundHandle;
        }

        public bool InputText(TaskForm parent, string itemName, string text, string tagStr, string indexStr)
        {
            bool flag = false;
            if ((this._curWebBrowser != null) && (this._curWebBrowser.Document != null))
            {
                string str;
                Rectangle rect = new Rectangle();
                mshtml.IHTMLDocument2 domDocument = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument;
                IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                if (string.IsNullOrEmpty(indexStr))
                {
                    str = "在第1个文本框 \"" + itemName + "\" 输入 \"" + text + "\"";
                }
                else
                {
                    str = string.Concat(new object[] { "在第", WindowUtil.StringToInt(indexStr) + 1, "个文本框 \"", itemName, "\" 输入 \"", text, "\"" });
                }
                parent.ShowTip2(str);
                if (!HtmlUtil.GetInputElementRect(domDocument, ref rect, itemName, tagStr, indexStr))
                {
                    this.MoveMouseToBottom(windowHwnd, domDocument);
                }
                else
                {
                    this.MoveMouseTo(windowHwnd, domDocument, rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), false);
                    if (this.MoveTimeOut())
                    {
                        this.SetFakeMousePoint();
                        Random random = new Random();
                        mshtml.IHTMLElement elem = HtmlUtil.GetInputElement(domDocument, itemName, tagStr, indexStr);
                        if (elem != null)
                        {
                            if (this._inputIndex < 0)
                            {
                                rect = HtmlUtil.GetElementRect(domDocument.body, elem);
                                WindowUtil.ClickMouse(windowHwnd, this._fakeMousePoint.X, this._fakeMousePoint.Y);
                                this._inputWaitTime = this._inputKeyTime = this._inputIndex = 0;
                                this._inputTotalWaitTime = random.Next(0, 3);
                                this._inputTotalKeyTime = random.Next(0, 3);
                            }
                            else if (this._inputIndex >= text.Length)
                            {
                                this._inputIndex = -1;
                                flag = true;
                                int clientWidth = 0;
                                int clientHeight = 0;
                                int scrollWidth = 0;
                                int scrollHeight = 0;
                                if (HtmlUtil.GetWindowWidthAndHeight(windowHwnd, domDocument, ref clientWidth, ref clientHeight, ref scrollWidth, ref scrollHeight) != null)
                                {
                                    HtmlUtil.SetMousePoint(ref this._fakeMousePoint, clientWidth - 12, this._fakeMousePoint.Y, clientWidth, clientHeight);
                                    int lParam = (this._fakeMousePoint.Y << 0x10) + this._fakeMousePoint.X;
                                    WindowUtil.PostMessage(windowHwnd, 0x200, 0, lParam);
                                }
                            }
                            else if (this._inputWaitTime < this._inputTotalWaitTime)
                            {
                                this._inputWaitTime++;
                            }
                            else
                            {
                                int num = (this._fakeMousePoint.Y << 0x10) + this._fakeMousePoint.X;
                                if (this._inputKeyTime < this._inputTotalKeyTime)
                                {
                                    int wParam = 0xe5;
                                    WindowUtil.PostMessage(windowHwnd, 0x100, wParam, num);
                                    WindowUtil.PostMessage(windowHwnd, 0x101, wParam, num);
                                    this._inputKeyTime++;
                                    this._inputWaitTime = 0;
                                    this._inputTotalWaitTime = random.Next(0, 3);
                                }
                                else
                                {
                                    int num3 = 0;
                                    bool flag2 = false;
                                    if (elem.getAttribute("value", 0) != null)
                                    {
                                        string str2 = elem.getAttribute("value", 0).ToString();
                                        while ((num3 < text.Length) && (num3 < str2.Length))
                                        {
                                            if (!str2.Substring(0, num3 + 1).Equals(text.Substring(0, num3 + 1)))
                                            {
                                                break;
                                            }
                                            num3++;
                                        }
                                        if (num3 < str2.Length)
                                        {
                                            WindowUtil.ClickMouse(windowHwnd, this._fakeMousePoint.X, this._fakeMousePoint.Y);
                                        }
                                        while (num3 < str2.Length)
                                        {
                                            Thread.Sleep(random.Next(100));
                                            WindowUtil.SendMessage(windowHwnd, 0x100, 8, num);
                                            Thread.Sleep(random.Next(100));
                                            WindowUtil.SendMessage(windowHwnd, 0x101, 8, num);
                                            flag2 = true;
                                            num3++;
                                        }
                                    }
                                    if (!flag2)
                                    {
                                        this._inputIndex = num3;
                                        int num4 = random.Next(1, 3);
                                        int num5 = this._inputIndex;
                                        for (int i = 0; (i < num4) && (num5 < text.Length); i++)
                                        {
                                            WindowUtil.SendMessage(windowHwnd, 0x286, text[num5], 0);
                                            Thread.Sleep(random.Next(100));
                                            num5++;
                                        }
                                        if ((elem.getAttribute("value", 0) == null) || (num5 > elem.getAttribute("value", 0).ToString().Length))
                                        {
                                            WindowUtil.ClickMouse(windowHwnd, this._fakeMousePoint.X, this._fakeMousePoint.Y);
                                            WindowUtil.SendMessage(windowHwnd, 0x100, 0x23, num);
                                            Thread.Sleep(random.Next(100));
                                            WindowUtil.SendMessage(windowHwnd, 0x101, 0x23, num);
                                        }
                                    }
                                    this._inputWaitTime = this._inputKeyTime = 0;
                                    this._inputTotalWaitTime = random.Next(0, 3);
                                    this._inputTotalKeyTime = random.Next(0, 3);
                                }
                            }
                            Marshal.ReleaseComObject(elem);
                        }
                    }
                }
                if (domDocument != null)
                {
                    Marshal.ReleaseComObject(domDocument);
                }
            }
            if (this.FindTimeOut())
            {
                this._inputIndex = -1;
                parent.ShowTip2("没有找到文本框 \"" + itemName + "\"");
                flag = true;
            }
            return flag;
        }

        private bool isBrowserComplete()
        {
            return (((this._curWebBrowser != null) && !this._curWebBrowser.IsBusy) && (this._curWebBrowser.ReadyState == WebBrowserReadyState.Complete));
        }

        private void MoveMouseTo(IntPtr hwnd, mshtml.IHTMLDocument2 doc, int x, int y, bool shouldWait)
        {
            HtmlElementCollection elementsByTagName = this._curWebBrowser.Document.GetElementsByTagName("HTML");
            int count = elementsByTagName.Count;
            bool isTimeout = this.FindTimeOut();
            if ((count > 0) && (!HtmlUtil.MoveToDest(hwnd, elementsByTagName[0], doc, x, y, ref this._fakeMousePoint, shouldWait, isTimeout) || (shouldWait && !isTimeout)))
            {
                this._scrollTime = this._now;
            }
            this.SetFakeScroll();
        }

        private void MoveMouseToBottom(IntPtr hwnd, mshtml.IHTMLDocument2 doc)
        {
            if ((this._curWebBrowser.Document.GetElementsByTagName("HTML").Count > 0) && (!HtmlUtil.ScrollToBottom(hwnd, doc, this._curWebBrowser.Document.GetElementsByTagName("HTML")[0], this._fakeMousePoint, this.GetLoadPercent(), this.FindTimeOut()) || !this._isDocCompleted))
            {
                this._scrollTime = this._now;
            }
            this.SetFakeScroll();
        }

        private bool MoveTimeOut()
        {
            TimeSpan span = (TimeSpan) (this._now - this._scrollTime);
            if (span.TotalSeconds < TaskForm.COMPLETEWAITTIME)
            {
                return this.FindTimeOut();
            }
            return true;
        }

        public bool Navigate(TaskForm parent, string link, string referer)
        {
            bool flag = false;
            if (!link.StartsWith("http://") && !link.StartsWith("https://"))
            {
                link = "http://" + link;
            }
            if ((!string.IsNullOrEmpty(referer) && !referer.StartsWith("http://")) && !referer.StartsWith("https://"))
            {
                referer = "http://" + referer;
            }
            if (this._curWebBrowser != null)
            {
                parent.ShowTip2("输入网址 " + link);
                if (!string.IsNullOrEmpty(referer))
                {
                    if (this._navigateStatus == 0)
                    {
                        string str = "<body><meta http-equiv=\"expires\" content=\"Sunday 2 October 2099 01:00 GMT\" /></body>";
                        AddUrlCache(referer, str);
                        this._curWebBrowser.Navigate(referer);
                        this._navigateTime = DateTime.Now;
                        this._curWebBrowser.Referer = referer;
                        this._navigateStatus++;
                        return flag;
                    }
                    if (string.IsNullOrEmpty(this._curWebBrowser.Referer))
                    {
                        if (this._curWebBrowser.Url.Equals(referer))
                        {
                            this._navigateStatus = 0;
                            string urlString = "javascript: function Redirect(url) { var referLink = document.createElement('a'); referLink.href = url; document.body.appendChild(referLink); referLink.click();} Redirect('" + link + "')";
                            this._curWebBrowser.Navigate(urlString);
                            this.ResetBrowserComplete();
                        }
                        else
                        {
                            TimeSpan span = (TimeSpan) (DateTime.Now - this._navigateTime);
                            if (span.TotalSeconds >= 1.0)
                            {
                                this._navigateStatus = 0;
                            }
                        }
                    }
                    if (this.FindTimeOut())
                    {
                        this._navigateStatus = 0;
                        this._errorString = this._errorString + "输入网址" + link + "失败！请检查来路是否有语法错误。";
                        flag = true;
                    }
                    return flag;
                }
                this._curWebBrowser.Navigate(link);
                this.ResetBrowserComplete();
            }
            return flag;
        }

        public void OnCloseBrowser(object sender, EventArgs e)
        {
            this.StopThread();
        }

        private bool ParseItems(TaskForm parent)
        {
            bool isClick = false;
            bool isFind = false;
            int clickLinkCount = 0;
            this.ResetMouseControl();
            if (this._isDocCompleted)
            {
                TimeSpan span = (TimeSpan) (this._now - this._waitFindTime);
                parent.ShowTip4("完成:" + span.TotalSeconds.ToString());
            }
            else
            {
                TimeSpan span2 = (TimeSpan) (this._now - this._waitDocTime);
                parent.ShowTip4("等待:" + span2.TotalSeconds.ToString());
            }
            TaskCommand command = TaskCommand.Task_None;
            if ((this._taskInfo._param1 != null) && (this._taskInfo._param1 != ""))
            {
                command = (TaskCommand) WindowUtil.StringToInt(this._taskInfo._param1);
            }
            switch (command)
            {
                case TaskCommand.Task_Wait:
                    return this.Wait(parent, int.Parse(this._taskInfo._param2));

                case TaskCommand.Task_InputText:
                    return this.InputText(parent, this._taskInfo._param2, this._taskInfo._param3, this._taskInfo._param4, this._taskInfo._param5);

                case TaskCommand.Task_ClickButton:
                    return this.ClickButton(parent, this._taskInfo._param2, this._taskInfo._param3, this._taskInfo._param4, ref isClick);

                case TaskCommand.Task_ClickLink:
                    return this.ClickLink(parent, this._taskInfo._param2, this._taskInfo._param3, this._taskInfo._param4, this._taskInfo._param5, ref isFind, ref isClick, true, false, ref clickLinkCount);

                case TaskCommand.Task_Navigate:
                    return this.Navigate(parent, this._taskInfo._param2, this._taskInfo._param3);

                case TaskCommand.Task_DeepClick:
                    return this.DeepClick(parent, this._taskInfo._param2, this._taskInfo._param3, this._taskInfo._param4);

                case TaskCommand.Task_ClearCookie:
                    return this.ClearCookie(parent, this._taskInfo._param2);

                case TaskCommand.Task_FindLinkLinkPage1:
                case TaskCommand.Task_FindLinkHrefPage1:
                case TaskCommand.Task_FindHrefLinkPage1:
                case TaskCommand.Task_FindHrefHrefPage1:
                case TaskCommand.Task_FindLinkSrcPage1:
                case TaskCommand.Task_FindHrefSrcPage1:
                case TaskCommand.Task_FindSrcLinkPage1:
                case TaskCommand.Task_FindSrcHrefPage1:
                case TaskCommand.Task_FindSrcSrcPage1:
                    return this.FindPage(parent, command, this._taskInfo._param2, this._taskInfo._param3, this._taskInfo._param4, this._taskInfo._param5);

                case TaskCommand.Task_Fresh:
                    return this.Fresh(parent);

                case TaskCommand.Task_PressKey:
                    return this.PressKey(parent);

                case TaskCommand.Task_ClickRadio:
                    return this.ClickRadio(parent, this._taskInfo._param2, this._taskInfo._param3, this._taskInfo._param4, ref isClick);

                case TaskCommand.Task_ClickChecked:
                    return this.ClickChecked(parent, this._taskInfo._param2, this._taskInfo._param3, this._taskInfo._param4, ref isClick);
            }
            parent.ShowTip2("线程未知参数 " + this._taskInfo._param1);
            this._errorString = this._errorString + "线程未知参数 " + this._taskInfo._param1;
            return true;
        }

        private bool PressKey(TaskForm parent)
        {
            if ((this._curWebBrowser != null) && (this._curWebBrowser.Document != null))
            {
                parent.ShowTip2("输入回车键");
                IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                mshtml.IHTMLDocument2 domDocument = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument;
                mshtml.IHTMLElement activeElement = domDocument.activeElement;
                Rectangle elementRect = HtmlUtil.GetElementRect(domDocument.body, activeElement);
                this.MoveMouseTo(windowHwnd, domDocument, elementRect.X + (elementRect.Width / 2), elementRect.Y + (elementRect.Height / 2), false);
                if (this.MoveTimeOut())
                {
                    this.SetFakeMousePoint();
                    int lParam = (this._fakeMousePoint.Y << 0x10) + this._fakeMousePoint.X;
                    WindowUtil.SendMessage(windowHwnd, 0x100, 13, lParam);
                    WindowUtil.SendMessage(windowHwnd, 0x102, 13, lParam);
                    WindowUtil.SendMessage(windowHwnd, 0x101, 13, lParam);
                    this.ResetBrowserComplete();
                }
                if (domDocument != null)
                {
                    Marshal.ReleaseComObject(domDocument);
                }
            }
            else if (this.FindTimeOut())
            {
                return true;
            }
            return false;
        }

        public void ResetBrowserComplete()
        {
            this._status = IEStatus.IEStatus_SysWait;
            this._scrollTime = this._beforeWaitTime = this._waitFindTime = this._now;
        }

        private void ResetMouseControl()
        {
            this._isScroll = false;
            this._isMoveMouse = false;
        }

        private void ResetTaskTime()
        {
            this._findTimeOut = false;
            this._scrollTime = this._waitFindTime = this._beforeWaitTime = this._now;
        }

        public void SetDocCompleted(bool isDocCompleted)
        {
            this._isDocCompleted = isDocCompleted;
            this._waitDocTime = this._waitFindTime = this._now;
        }

        private void SetFakeMousePoint()
        {
            this._isMoveMouse = true;
        }

        private void SetFakeScroll()
        {
            this._isScroll = true;
        }

        public void StartThread()
        {
            this._threadRun = true;
        }

        public void StopThread()
        {
            if (this._threadRun)
            {
                this._threadRun = false;
                if (!this._isCompleted && string.IsNullOrEmpty(this._errorString))
                {
                    this._errorString = "用户取消任务";
                }
            }
        }

        public bool SysWait(TaskForm parent)
        {
            TimeSpan span = (TimeSpan) (this._now - this._beforeWaitTime);
            double totalSeconds = span.TotalSeconds;
            if (totalSeconds >= TaskForm.SYSTEMWAITTIME)
            {
                this._beforeWaitTime = this._now;
                this._status = IEStatus.IEStatus_None;
                return true;
            }
            parent.ShowTip4("等待网页加载" + totalSeconds.ToString());
            return false;
        }

        public bool SysWaitForComplete()
        {
            return this._isDocCompleted;
        }

        public bool Update(TaskForm parent)
        {
            bool flag = false;
            this._now = DateTime.Now;
            TimeSpan span = (TimeSpan) (this._now - this._startTaskTime);
            if (span.TotalMinutes > 20.0)
            {
                this._errorString = "任务超时";
                this._threadRun = false;
            }
            if (this._threadRun)
            {
                try
                {
                    if (this._status == IEStatus.IEStatus_SysWait)
                    {
                        flag = this.SysWait(parent);
                    }
                    else if (this._status == IEStatus.IEStatus_SysComplete)
                    {
                        flag = this.SysWaitForComplete();
                    }
                    else if (this._curWebBrowser != null)
                    {
                        flag = this.ParseItems(parent);
                    }
                }
                catch (Exception exception)
                {
                    flag = true;
                    Logger.Error(exception);
                }
                if (this._errorString != string.Empty)
                {
                    this._threadRun = false;
                }
                if (flag)
                {
                    this.ResetTaskTime();
                    this._inputClickTaobaoCloseButton = false;
                    this._randomClickLinkCount = 0;
                    this._status = IEStatus.IEStatus_None;
                    if (this._threadRun && (this._task.GetTaskInfo(ref this._taskInfo, ref this._taskInfoIndex, ref this._startLoop, ref this._loop, ref this._loopTime) != 0))
                    {
                        if (!this._isCompleted)
                        {
                            this._isCompleted = true;
                            this._status = IEStatus.IEStatus_SysComplete;
                        }
                        else
                        {
                            this._threadRun = false;
                        }
                    }
                }
                this.CheckDocCompletTimeOut();
            }
            if (!this._threadRun)
            {
                parent.ShowTip2("任务结束");
                parent.ShowTip3("");
                parent.ShowTip4("");
                return true;
            }
            return false;
        }

        private void UpdateMouseControl()
        {
            if ((!this._isScroll || !this._isMoveMouse) && (this._curWebBrowser.Document != null))
            {
                mshtml.IHTMLDocument2 domDocument = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument;
                if (domDocument != null)
                {
                    IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                    if (!this._isScroll)
                    {
                        HtmlElementCollection elementsByTagName = this._curWebBrowser.Document.GetElementsByTagName("HTML");
                        int count = elementsByTagName.Count;
                        if (this._fakeScrollHeight < -1)
                        {
                            this._fakeScrollHeight++;
                        }
                        else if (this._fakeScrollHeight == -1)
                        {
                            Random random = new Random();
                            int clientWidth = 0;
                            int clientHeight = 0;
                            int scrollWidth = 0;
                            int scrollHeight = 0;
                            if (((HtmlUtil.GetWindowWidthAndHeight(windowHwnd, domDocument, ref clientWidth, ref clientHeight, ref scrollWidth, ref scrollHeight) != null) && (clientHeight > 0)) && ((clientWidth > 0) && (scrollHeight >= clientHeight)))
                            {
                                this._fakeScrollHeight = random.Next(scrollHeight - clientHeight);
                            }
                            else
                            {
                                this._fakeScrollHeight = 0;
                            }
                        }
                        else if ((count > 0) && HtmlUtil.ScrollToDest(windowHwnd, domDocument, elementsByTagName[0], this._fakeScrollHeight, this._fakeMousePoint, this.FindTimeOut()))
                        {
                            Random random2 = new Random();
                            this._fakeScrollHeight = -random2.Next(0xbb8 / TaskForm.THREADINTERVAL);
                        }
                    }
                    if (!this._isMoveMouse)
                    {
                        if (this._fakeMouseTo.X < -1)
                        {
                            this._fakeMouseTo.X++;
                        }
                        else if (this._fakeMouseTo.X == -1)
                        {
                            Random random3 = new Random();
                            int num6 = 0;
                            int num7 = 0;
                            int num8 = 0;
                            int num9 = 0;
                            if (HtmlUtil.GetWindowWidthAndHeight(windowHwnd, domDocument, ref num6, ref num7, ref num8, ref num9) != null)
                            {
                                this._fakeMouseTo.X = random3.Next(num6);
                                this._fakeMouseTo.Y = random3.Next(num7);
                            }
                        }
                        else if (HtmlUtil.MoveToDest(windowHwnd, domDocument, this._fakeMouseTo, ref this._fakeMousePoint))
                        {
                            this._fakeMouseTo.X = -1;
                        }
                    }
                    Marshal.ReleaseComObject(domDocument);
                }
            }
        }

        public bool Wait(TaskForm parent, int waitTime)
        {
            if (this._status == IEStatus.IEStatus_Wait)
            {
                TimeSpan span = (TimeSpan) (this._now - this._beforeWaitTime);
                if (span.TotalSeconds >= this._waitTime)
                {
                    this._status = IEStatus.IEStatus_None;
                    return true;
                }
                if ((this._curWebBrowser != null) && (this._curWebBrowser.Document != null))
                {
                    mshtml.IHTMLDocument2 domDocument = (mshtml.IHTMLDocument2) this._curWebBrowser.Document.DomDocument;
                    if (domDocument != null)
                    {
                        IntPtr windowHwnd = GetWindowHwnd(this._curWebBrowser.Handle.ToInt32());
                        this.MoveMouseToBottom(windowHwnd, domDocument);
                    }
                }
            }
            else if (this._status == IEStatus.IEStatus_None)
            {
                parent.ShowTip2("等待 " + waitTime + " 秒");
                this._status = IEStatus.IEStatus_Wait;
                this._waitTime = waitTime;
                this._beforeWaitTime = this._now;
            }
            else
            {
                return true;
            }
            return false;
        }

        public ArrayList Browsers
        {
            get
            {
                return this._browsers;
            }
        }

        public string ErrorString
        {
            get
            {
                return this._errorString;
            }
        }

        public Point FackMousePoint
        {
            get
            {
                return this._fakeMousePoint;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return this._isCompleted;
            }
        }

        public MyTask Task
        {
            get
            {
                return this._task;
            }
        }

        public int TaskInfoIndex
        {
            get
            {
                return this._taskInfoIndex;
            }
        }
    }
}

