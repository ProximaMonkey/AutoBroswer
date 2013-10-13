using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml;

namespace TenDayBrowser
{
    public partial class TenDayBrowser : Form
    {
        public static int WAITDOCCOMPLETE = 20;
        public static int WAITFINDTIME = 20;

        public static int SYSTEMWAITTIME = 1;
        private ExtendedTabControl tabControl;
        public static int THREADINTERVAL = 200;

        public const uint SC_MINIMIZE = 0xf020;

        public const int MESSAGE_CLOSETASK = 2;
        public const int MESSAGE_COMPLETETASK = 1;
        public const int MESSAGE_RUNTASK = 0;
        public static int HOMEWAITTIME = 10;
        public static int HOURSCOUNT = 0x30;
        public static int DEEPCLICK = 0;
        public static int DEEPWAITTIME = 30;
        public static int FINDMAXPAGE = 20;
        public static int FINDPAGE = 0;
        public static int CLICKCOUNTPERIP = 0;
        public static int CLICKINTERVALTIME = 2;
        public static int CLICKNEXTPAGE = 0;
        public static int CLICKPERDAY = 200;
        public static int CLICKRATE = 100;
        public static int COMPLETEWAITTIME = 2;
        private bool _taskThreadRun;
        private Timer _timer;
        private int _waitDocCompleteTime = WAITDOCCOMPLETE;
        private int _waitFindTime = WAITFINDTIME;
        private TextBox addressTextBox;
        public static bool ALLOWSCROLLWHILEWAITING = true;

        private IntPtr _activeWindow = IntPtr.Zero;
        private IEItem _completedIeItem;
        private IEItem _ieItem;
        private bool _isFirstShow = true;
        private IntPtr _parentProcessHwnd = IntPtr.Zero;
        private MyTask _task;
        public const string BROWSERNAME = "十日浏览器";
        public TenDayBrowser(long hwnd)
        {
            this._activeWindow = Program.GetForegroundWindow();
            this.Text = BROWSERNAME;
            this._parentProcessHwnd = (IntPtr)hwnd;
            InitializeComponent();
            InitTaskManager();
        }
        public void InitTaskManager()
        {
            if (this._timer == null)
            {
                this._timer = new Timer();
                this._timer.Tick += new EventHandler(this.TaskThreadFunc);
                this._timer.Interval = THREADINTERVAL;
                this._timer.Enabled = false;
                this._timer.Start();
            }
        }
        private IEItem GetIEItem(ExtendedTenDayBrowser TenDayBrowser)
        {
            if ((TenDayBrowser != null) && (this._ieItem != null))
            {
                foreach (ExtendedTenDayBrowser browser in this._ieItem.Browsers)
                {
                    if (browser.Equals(TenDayBrowser))
                    {
                        return this._ieItem;
                    }
                }
            }
            return null;
        }
        public void SendCompleteTask(int id, bool completed, int taskStep, string errorStr)
        {
            if (WindowUtil.IsWindow(this._parentProcessHwnd))
            {
                try
                {
                    COPYDATASTRUCT copydatastruct;
                    XmlDocument document = new XmlDocument();
                    XmlElement newChild = document.CreateElement("root");
                    XmlElement element2 = document.CreateElement("taskid");
                    XmlElement element3 = document.CreateElement("isCompleted");
                    XmlElement element4 = document.CreateElement("errorString");
                    XmlElement element5 = document.CreateElement("taskStep");
                    element2.InnerText = id.ToString();
                    element3.InnerText = completed.ToString();
                    element4.InnerText = errorStr;
                    element5.InnerText = taskStep.ToString();
                    newChild.AppendChild(element2);
                    newChild.AppendChild(element3);
                    newChild.AppendChild(element4);
                    newChild.AppendChild(element5);
                    document.AppendChild(newChild);
                    string outerXml = document.OuterXml;
                    int length = Encoding.Unicode.GetBytes(outerXml).Length;
                    copydatastruct.dwData = (IntPtr)1;
                    copydatastruct.lpData = outerXml;
                    copydatastruct.cbDaat = length + 2;
                    WindowUtil.SendMessage(this._parentProcessHwnd, (int)WindowsMessages.WM_COPYDATA, 0, ref copydatastruct);
                    newChild = null;
                    document = null;
                }
                catch (Exception)
                {
                }
            }
        }

        public void SendStopTask()
        {
            if (WindowUtil.IsWindow(this._parentProcessHwnd) && (this._ieItem != null))
            {
                COPYDATASTRUCT copydatastruct;
                string s = "a";
                int length = Encoding.Unicode.GetBytes(s).Length;
                copydatastruct.dwData = (IntPtr)2;
                copydatastruct.lpData = s;
                copydatastruct.cbDaat = length + 2;
                WindowUtil.SendMessage(this._parentProcessHwnd,(int) WindowsMessages.WM_COPYDATA, 0, ref copydatastruct);
            }
        }
#region TenDayBrowser处理函数
        private void _browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs ev)
        {
            try
            {
                ExtendedTenDayBrowser TenDayBrowser = sender as ExtendedTenDayBrowser;
                if (TenDayBrowser != null)
                {
                    if (ev.Url.Equals(TenDayBrowser.Url))
                    {
                        IEItem iEItem = this.GetIEItem(TenDayBrowser);
                        if (iEItem != null)
                        {
                            iEItem.SetDocCompleted(true);
                        }
                    }
                    this.UpdateAddressBox();
                }
            }
            catch
            {
            }
        }
        private void _browser_DownloadComplete(object sender, EventArgs ex)
        {
            try
            {
                ExtendedTenDayBrowser browser = sender as ExtendedTenDayBrowser;
                if (browser != null)
                {
                    if (!string.IsNullOrEmpty(browser.Referer))
                    {
                        LoadTempHtml.SetIEConnectionMode(false);
                        browser.Referer = string.Empty;
                    }
                    if (browser.Document != null)
                    {
                        browser.Document.Window.Error += new HtmlElementErrorEventHandler(this.Window_Error);
                        this.UpdateAddressBox();
                    }
                }
            }
            catch
            {
            }
        }
        private void _browser_StartNavigate(object sender, BrowserExtendedNavigatingEventArgs ev)
        {
            ExtendedTenDayBrowser TenDayBrowser = sender as ExtendedTenDayBrowser;
            if (!string.IsNullOrEmpty(TenDayBrowser.Referer))
            {
                LoadTempHtml.SetIEConnectionMode(true);
            }
            IEItem iEItem = this.GetIEItem(TenDayBrowser);
            if (iEItem != null)
            {
                iEItem.SetDocCompleted(false);
            }
        }

        private void _browser_StartNewWindow(object sender, BrowserExtendedNavigatingEventArgs ev)
        {
            try
            {
                if ((ev.NavigationContext & UrlContext.HtmlDialog) != UrlContext.HtmlDialog)
                {
                    IEItem iEItem = this.GetIEItem(sender as ExtendedTenDayBrowser);
                    ExtendedTenDayBrowser browser = this.New(false, null);
                    if (iEItem != null)
                    {
                        iEItem.AddBrowser(browser);
                    }
                    iEItem = null;
                    ev.AutomationObject = browser.Application;
                }
            }
            catch (Exception)
            {
            }
        }

        private void _browser_StatusTextChanged(object sender, EventArgs ex)
        {
            try
            {
                ExtendedTenDayBrowser browser = sender as ExtendedTenDayBrowser;
                if (browser != null)
                {
                    this.ShowTip3(browser.StatusText);
                }
            }
            catch
            {
            }
        }
#endregion
        public ExtendedTenDayBrowser New(bool navigateHome, IEItem ieItem)
        {
            TabPage page = new TabPage();
            ExtendedTenDayBrowser browser = new ExtendedTenDayBrowser();
            if (ieItem != null)
            {
                ieItem.AddBrowser(browser);
            }
            page.Text = "新建窗口";
            page.Tag = browser;
            browser.Tag = page;
            browser.Dock = DockStyle.Fill;
            browser.DownloadComplete += new EventHandler(this._browser_DownloadComplete);
            browser.StartNavigate += new EventHandler<BrowserExtendedNavigatingEventArgs>(this._browser_StartNavigate);
            browser.StartNewWindow += new EventHandler<BrowserExtendedNavigatingEventArgs>(this._browser_StartNewWindow);
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this._browser_DocumentCompleted);
            browser.StatusTextChanged += new EventHandler(this._browser_StatusTextChanged);
            browser.DocumentTitleChanged += new EventHandler(this.TenDayBrowser_DocumentTitleChanged);
            browser.Quit += new EventHandler(this.TenDayBrowser_Quit);
            page.Controls.Add(browser);
            if (navigateHome)
            {
                browser.GoHome();
            }
            this.tabControl.TabPages.Add(page);
            this.tabControl.SelectedTab = page;
            return browser;
        }
        private void goButton_Click(object sender, EventArgs e)
        {
            this.New(true, null).Navigate(this.addressTextBox.Text);
        }
        private void ChangeWindowSize()
        {
            base.WindowState = FormWindowState.Maximized;
            int width = base.Width;
            int height = base.Height;
            base.WindowState = FormWindowState.Normal;
            Random random = new Random();
            int num3 = random.Next((base.Width * 4) / 10);
            base.Width = width - num3;
            num3 = random.Next((base.Height * 4) / 10);
            base.Height = height - num3;
            base.Location = new Point(0, 0);
        }
        public void CompleteIeItem(IEItem ieItem)
        {
            this._completedIeItem = ieItem;
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == (int)WindowsMessages.WM_COPYDATA)
            {
                System.Type cls = new COPYDATASTRUCT().GetType();
                COPYDATASTRUCT lParam = (COPYDATASTRUCT)m.GetLParam(cls);
                this.ParseMessage((int)lParam.dwData, ref lParam.lpData);
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }


        public void ShowTip1(string str)
        {
            this.toolStripStatusLabel1.Text = str;
        }

        public void ShowTip2(string str)
        {
            this.toolStripStatusLabel2.Text = str;
        }

        public void ShowTip3(string str)
        {
            this.toolStripStatusLabel3.Text = str;
        }

        public void ShowTip4(string str)
        {
            this.toolStripStatusLabel4.Text = str;
        }

        public void ShowUrl(string str)
        {
            this.addressTextBox.Text = str;
        }

        public void StartTaskThread(MyTask task)
        {
            this.StopTaskThread();
            this._taskThreadRun = true;
            if (this._ieItem == null)
            {
                this._ieItem = new IEItem(this, task, this._waitFindTime, this._waitDocCompleteTime);
                this.New(true, this._ieItem);
            }
        }

        public void StopTaskThread()
        {
            if (this._ieItem != null)
            {
                this._ieItem.StopThread();
            }
            this._taskThreadRun = false;
        }
        private void ProcessRunTask(string xmlDoc)
        {
            if (this._task == null)
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    XmlNode node = null;
                    document.LoadXml(xmlDoc);
                    foreach (XmlNode node2 in document.SelectSingleNode("root").ChildNodes)
                    {
                        if (node2.Name.Equals("tasks"))
                        {
                            foreach (XmlNode node3 in node2.ChildNodes)
                            {
                                if (node3.Name.Equals("task"))
                                {
                                    MyTask task = new MyTask();
                                    foreach (XmlNode node4 in node3.ChildNodes)
                                    {
                                        if (node4.Name.Equals("taskinfo"))
                                        {
                                            string s = string.Empty;
                                            string innerText = string.Empty;
                                            string str3 = string.Empty;
                                            string str4 = string.Empty;
                                            string str5 = string.Empty;
                                            foreach (XmlNode node5 in node4)
                                            {
                                                if (node5.Name.Equals("param1"))
                                                {
                                                    s = node5.InnerText;
                                                }
                                                else
                                                {
                                                    if (node5.Name.Equals("param2"))
                                                    {
                                                        innerText = node5.InnerText;
                                                        continue;
                                                    }
                                                    if (node5.Name.Equals("param3"))
                                                    {
                                                        str3 = node5.InnerText;
                                                        continue;
                                                    }
                                                    if (node5.Name.Equals("param4"))
                                                    {
                                                        str4 = node5.InnerText;
                                                        continue;
                                                    }
                                                    if (node5.Name.Equals("param5"))
                                                    {
                                                        str5 = node5.InnerText;
                                                    }
                                                }
                                            }
                                            if ((WindowUtil.StringToInt(s) > -1) && (WindowUtil.StringToInt(s) < 0x1d))
                                            {
                                                TaskInfo info = new TaskInfo(s, innerText, str3, str4, str5);
                                                task.Insert(info, -1);
                                            }
                                            continue;
                                        }
                                        if (node4.Name.Equals("params"))
                                        {
                                            foreach (XmlNode node6 in node4)
                                            {
                                                XmlElement element = (XmlElement)node6;
                                                if (element.Name.Equals("taskid"))
                                                {
                                                    task._id = WindowUtil.StringToInt(element.InnerText);
                                                }
                                                if (element.Name.Equals("ClickIntervalTime"))
                                                {
                                                    task._clickIntervalTime = WindowUtil.StringToInt(element.InnerText);
                                                }
                                                else
                                                {
                                                    if (element.Name.Equals("ClickCountPerIP"))
                                                    {
                                                        task._clickCountPerIP = WindowUtil.StringToInt(element.InnerText);
                                                        continue;
                                                    }
                                                    if (element.Name.Equals("ClickRate"))
                                                    {
                                                        task._clickRate = WindowUtil.StringToInt(element.InnerText);
                                                        continue;
                                                    }
                                                    if (element.Name.Equals("AllowScrollWhileWaiting"))
                                                    {
                                                        task._allowScrollWhileWaiting = Convert.ToBoolean(element.InnerText);
                                                        continue;
                                                    }
                                                    if (element.Name.Equals("vipmode"))
                                                    {
                                                        task.VipMode = Convert.ToBoolean(element.InnerText);
                                                    }
                                                }
                                            }
                                            continue;
                                        }
                                    }
                                    if (task._taskItems.Count > 0)
                                    {
                                        this._task = task;
                                    }
                                    task = null;
                                }
                            }
                            continue;
                        }
                        if (node2.Name.Equals("setting"))
                        {
                            foreach (XmlNode node7 in node2.ChildNodes)
                            {
                                if (node7.Name.Equals("waitFindTime"))
                                {
                                    this._waitFindTime = WindowUtil.StringToInt(node7.InnerText);
                                }
                                else if (node7.Name.Equals("waitDocCompleteTime"))
                                {
                                    this._waitDocCompleteTime = WindowUtil.StringToInt(node7.InnerText);
                                }
                                if (node7.Name.Equals("curAddress"))
                                {
                                    this.ShowTip1(node7.InnerText);
                                }
                            }
                            continue;
                        }
                    }
                    node = null;
                    document = null;
                    if (this._task != null)
                    {
                        this.StartTaskThread(this._task);
                    }
                }
                catch (Exception exception)
                {
                    Logger.Error(exception);
                }
            }
        }
        public void Remove(ExtendedTenDayBrowser browser)
        {
            if (browser != null)
            {
                TabPage tag = browser.Tag as TabPage;
                if (tag != null)
                {
                    this.tabControl.TabPages.Remove(tag);
                    tag.Dispose();
                }
            }
        }
        private void TabControlDoubleClick(object sender, MouseEventArgs e)
        {
            TabControl control = sender as TabControl;
            if (control != null)
            {
                TabPage selectedTab = control.SelectedTab;
                if (selectedTab != null)
                {
                    ExtendedTenDayBrowser tag = selectedTab.Tag as ExtendedTenDayBrowser;
                    if (tag != null)
                    {
                        this.Remove(tag);
                    }
                }
            }
        }

        private void TabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl.SelectedTab != null)
            {
                this.UpdateAddressBox();
            }
            else
            {
                this.ShowUrl("");
            }
        }

        private void TenDayBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SendStopTask();
        }

        private void TenDayBrowserClosing(object sender, FormClosingEventArgs e)
        {
            this.StopTaskThread();
            base.Dispose();
        }

        public void TaskThreadFunc(object sender, EventArgs ea)
        {
            if (this._completedIeItem != null)
            {
                this.SendCompleteTask(this._completedIeItem.Task._id, this._completedIeItem.IsCompleted, this._completedIeItem.TaskInfoIndex, this._completedIeItem.ErrorString);
                this._completedIeItem = null;
                this._ieItem = null;
                Application.Exit();
            }
            if ((this._ieItem != null) && this._ieItem.Update(this))
            {
                this.CompleteIeItem(this._ieItem);
            }
        }
        private void ParseMessage(int opcode, ref string data)
        {
            if (opcode == 0)
            {
                if (!this._taskThreadRun)
                {
                    this.ProcessRunTask(data);
                }
                else
                {
                    Logger.Trace("任务还在进行！");
                }
            }
        }

        public void UpdateAddressBox()
        {
            try
            {
                ExtendedTenDayBrowser activeBrowser = this.ActiveBrowser;
                if (((activeBrowser != null) && (activeBrowser.Document != null)) && (activeBrowser.Document.Url != null))
                {
                    string str = activeBrowser.Document.Url.ToString();
                    this.ShowUrl(str);
                }
            }
            catch
            {
            }
        }

        private void TenDayBrowser_DocumentTitleChanged(object sender, EventArgs e)
        {
            ExtendedTenDayBrowser browser = sender as ExtendedTenDayBrowser;
            if (browser != null)
            {
                TabPage tag = browser.Tag as TabPage;
                if (tag != null)
                {
                    try
                    {
                        string documentTitle = browser.DocumentTitle;
                        if (string.IsNullOrEmpty(documentTitle))
                        {
                            documentTitle = "新建窗口";
                        }
                        else if (documentTitle.Length > 30)
                        {
                            documentTitle = documentTitle.Substring(0, 30) + "...";
                        }
                        tag.Text = documentTitle;
                        tag.ToolTipText = browser.DocumentTitle;
                    }
                    catch
                    {
                    }
                }
            }
        }

        public void TenDayBrowser_Quit(object sender, EventArgs e)
        {
            ExtendedTenDayBrowser browser = sender as ExtendedTenDayBrowser;
            if (browser != null)
            {
                this.Remove(browser);
            }
        }

        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            e.Handled = true;
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == (int)WindowsMessages.WM_SYSCOMMAND) && (m.WParam.ToInt32() == 0xf020L))
            {
                base.Visible = false;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public ExtendedTenDayBrowser ActiveBrowser
        {
            get
            {
                TabPage selectedTab = this.tabControl.SelectedTab;
                if (selectedTab != null)
                {
                    return (selectedTab.Tag as ExtendedTenDayBrowser);
                }
                return null;
            }
        }

        public IEItem IEItem
        {
            get
            {
                return this._ieItem;
            }
        }

        public bool TaskThreadRun
        {
            get
            {
                return this._taskThreadRun;
            }
        }

        public int WaitDocCompleteTime
        {
            get
            {
                return this._waitDocCompleteTime;
            }
        }

        public int WaitFindTime
        {
            get
            {
                return this._waitFindTime;
            }
        }
    }
}
