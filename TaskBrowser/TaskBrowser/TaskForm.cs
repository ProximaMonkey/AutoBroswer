namespace TaskBrowser
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class TaskForm : Form
    {
        private IntPtr _activeWindow = IntPtr.Zero;
        private TaskBrowser.IEItem _completedIeItem;
        private TaskBrowser.IEItem _ieItem;
        private bool _isFirstShow = true;
        private IntPtr _parentProcessHwnd = IntPtr.Zero;
        private MyTask _task;
        private bool _taskThreadRun;
        private Timer _timer;
        private int _waitDocCompleteTime = WAITDOCCOMPLETE;
        private int _waitFindTime = WAITFINDTIME;
        private TextBox addressTextBox;
        public static bool ALLOWSCROLLWHILEWAITING = true;
        public const string BROWSERNAME = "点击精灵浏览器";
        public static int CLICKCOUNTPERIP = 0;
        public static int CLICKINTERVALTIME = 2;
        public static int CLICKNEXTPAGE = 0;
        public static int CLICKPERDAY = 200;
        public static int CLICKRATE = 100;
        public static int COMPLETEWAITTIME = 2;
        private IContainer components;
        public static int DEEPCLICK = 0;
        public static int DEEPWAITTIME = 30;
        public static int FINDMAXPAGE = 20;
        public static int FINDPAGE = 0;
        private Button goButton;
        public static int HOMEWAITTIME = 10;
        public static int HOURSCOUNT = 0x30;
        private Label label1;
        public const int MESSAGE_CLOSETASK = 2;
        public const int MESSAGE_COMPLETETASK = 1;
        public const int MESSAGE_RUNTASK = 0;
        private Panel panel2;
        private Panel panel3;
        public const uint SC_MINIMIZE = 0xf020;
        private StatusStrip statusStrip1;
        public static int SYSTEMWAITTIME = 1;
        private ExtendedTabControl tabControl;
        public static int THREADINTERVAL = 200;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        public static int WAITDOCCOMPLETE = 20;
        public static int WAITFINDTIME = 20;

        public TaskForm(long hwnd)
        {
            this._activeWindow = Program.GetForegroundWindow();
            this.Text = "点击精灵浏览器";
            this._parentProcessHwnd = (IntPtr) hwnd;
            this.InitializeComponent();
            this.InitTaskManager();
            this.ShowTip1("欢迎使用点击精灵");
        }

        private void _browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs ev)
        {
            try
            {
                ExtendedWebBrowser webBrowser = sender as ExtendedWebBrowser;
                if (webBrowser != null)
                {
                    if (ev.Url.Equals(webBrowser.Url))
                    {
                        TaskBrowser.IEItem iEItem = this.GetIEItem(webBrowser);
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
                ExtendedWebBrowser browser = sender as ExtendedWebBrowser;
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
            ExtendedWebBrowser webBrowser = sender as ExtendedWebBrowser;
            if (!string.IsNullOrEmpty(webBrowser.Referer))
            {
                LoadTempHtml.SetIEConnectionMode(true);
            }
            TaskBrowser.IEItem iEItem = this.GetIEItem(webBrowser);
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
                    TaskBrowser.IEItem iEItem = this.GetIEItem(sender as ExtendedWebBrowser);
                    ExtendedWebBrowser browser = this.New(false, null);
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
                ExtendedWebBrowser browser = sender as ExtendedWebBrowser;
                if (browser != null)
                {
                    this.ShowTip3(browser.StatusText);
                }
            }
            catch
            {
            }
        }

        private void addressTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                string text = this.addressTextBox.Text;
                this.New(true, null).Navigate(text);
            }
        }

        private void buttonGoto_Click(object sender, EventArgs e)
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

        public void CompleteIeItem(TaskBrowser.IEItem ieItem)
        {
            this._completedIeItem = ieItem;
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == 0x4a)
            {
                System.Type cls = new TaskBrowser.COPYDATASTRUCT().GetType();
                TaskBrowser.COPYDATASTRUCT lParam = (TaskBrowser.COPYDATASTRUCT) m.GetLParam(cls);
                this.ParseMessage((int) lParam.dwData, ref lParam.lpData);
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private TaskBrowser.IEItem GetIEItem(ExtendedWebBrowser webBrowser)
        {
            if ((webBrowser != null) && (this._ieItem != null))
            {
                foreach (ExtendedWebBrowser browser in this._ieItem.Browsers)
                {
                    if (browser.Equals(webBrowser))
                    {
                        return this._ieItem;
                    }
                }
            }
            return null;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TaskForm));
            this.goButton = new Button();
            this.label1 = new Label();
            this.addressTextBox = new TextBox();
            this.panel2 = new Panel();
            this.toolStripStatusLabel4 = new ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new ToolStripStatusLabel();
            this.statusStrip1 = new StatusStrip();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new ToolStripStatusLabel();
            this.panel3 = new Panel();
            this.tabControl = new ExtendedTabControl();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.goButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.goButton.Location = new Point(0x359, 5);
            this.goButton.Name = "goButton";
            this.goButton.Size = new Size(0x4b, 0x17);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "转到";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new EventHandler(this.buttonGoto_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "地址";
            this.addressTextBox.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.addressTextBox.Location = new Point(0x31, 6);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new Size(0x322, 0x15);
            this.addressTextBox.TabIndex = 0;
            this.addressTextBox.KeyUp += new KeyEventHandler(this.addressTextBox_KeyUp);
            this.panel2.Controls.Add(this.goButton);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.addressTextBox);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3b0, 0x20);
            this.panel2.TabIndex = 10;
            this.toolStripStatusLabel4.AutoSize = false;
            this.toolStripStatusLabel4.ImageAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new Size(120, 0x11);
            this.toolStripStatusLabel4.TextAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.ImageAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new Size(300, 0x11);
            this.toolStripStatusLabel3.TextAlign = ContentAlignment.MiddleLeft;
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripStatusLabel1, this.toolStripStatusLabel2, this.toolStripStatusLabel3, this.toolStripStatusLabel4 });
            this.statusStrip1.Location = new Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(0x3b0, 0x16);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.ImageAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(160, 0x11);
            this.toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.ImageAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new Size(0x15d, 0x11);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.TextAlign = ContentAlignment.MiddleLeft;
            this.panel3.Location = new Point(0, 0x20);
            this.panel3.Margin = new Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x2e1, 0x102);
            this.panel3.TabIndex = 12;
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Location = new Point(0, 0x20);
            this.tabControl.Margin = new Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(0x3b0, 0x224);
            this.tabControl.TabIndex = 0;
            this.tabControl.MouseDoubleClick += new MouseEventHandler(this.TabControlDoubleClick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            //base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3b0, 0x25a);
            base.Controls.Add(this.tabControl);
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.statusStrip1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "TaskForm";
            base.FormClosing += new FormClosingEventHandler(this.TaskForm_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
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

        public ExtendedWebBrowser New(bool navigateHome, TaskBrowser.IEItem ieItem = new TaskBrowser.IEItem())
        {
            TabPage page = new TabPage();
            ExtendedWebBrowser browser = new ExtendedWebBrowser();
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
            browser.DocumentTitleChanged += new EventHandler(this.WebBrowser_DocumentTitleChanged);
            browser.Quit += new EventHandler(this.WebBrowser_Quit);
            page.Controls.Add(browser);
            if (navigateHome)
            {
                browser.GoHome();
            }
            this.tabControl.TabPages.Add(page);
            this.tabControl.SelectedTab = page;
            return browser;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if ((this._activeWindow != IntPtr.Zero) && this._isFirstShow)
            {
                Program.SetForegroundWindow(this._activeWindow);
                this._isFirstShow = false;
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
                                                XmlElement element = (XmlElement) node6;
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

        public void Remove(ExtendedWebBrowser browser)
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

        public void SendCompleteTask(int id, bool completed, int taskStep, string errorStr)
        {
            if (WindowUtil.IsWindow(this._parentProcessHwnd))
            {
                try
                {
                    TaskBrowser.COPYDATASTRUCT copydatastruct;
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
                    copydatastruct.dwData = (IntPtr) 1;
                    copydatastruct.lpData = outerXml;
                    copydatastruct.cbDaat = length + 2;
                    WindowUtil.SendMessage(this._parentProcessHwnd, 0x4a, 0, ref copydatastruct);
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
                TaskBrowser.COPYDATASTRUCT copydatastruct;
                string s = "a";
                int length = Encoding.Unicode.GetBytes(s).Length;
                copydatastruct.dwData = (IntPtr) 2;
                copydatastruct.lpData = s;
                copydatastruct.cbDaat = length + 2;
                WindowUtil.SendMessage(this._parentProcessHwnd, 0x4a, 0, ref copydatastruct);
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
                this._ieItem = new TaskBrowser.IEItem(this, task, this._waitFindTime, this._waitDocCompleteTime);
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

        private void TabControlDoubleClick(object sender, MouseEventArgs e)
        {
            TabControl control = sender as TabControl;
            if (control != null)
            {
                TabPage selectedTab = control.SelectedTab;
                if (selectedTab != null)
                {
                    ExtendedWebBrowser tag = selectedTab.Tag as ExtendedWebBrowser;
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

        private void TaskForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SendStopTask();
        }

        private void TaskFormClosing(object sender, FormClosingEventArgs e)
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

        private void TestTask()
        {
            this._task = new MyTask();
            TaskInfo task = new TaskInfo("6", "255", "", "", "");
            this._task.Insert(task, -1);
            task = new TaskInfo("4", "www.taobao.com", "", "", "");
            this._task.Insert(task, -1);
            task = new TaskInfo("1", "q", "小米手机", "", "");
            this._task.Insert(task, -1);
            task = new TaskInfo("2", "btn-search", "3", "", "");
            this._task.Insert(task, -1);
            task = new TaskInfo("2", "搜 索", "2", "", "");
            this._task.Insert(task, -1);
            task = new TaskInfo("7", "小米手机M红米2 1.5G四核4.7HD屏800W像素双卡双待 红米手机现货 手机", "下一页", "3", "");
            this._task.Insert(task, -1);
            task = new TaskInfo("3", "小米手机M红米2 1.5G四核4.7HD屏800W像素双卡双待 红米手机现货 手机", "", "2", "");
            this._task.Insert(task, -1);
            task = new TaskInfo("5", "10", "10", "http://detail.tmall.com/item.htm", "");
            this._task.Insert(task, -1);
            task = new TaskInfo("0", "10", "", "", "");
            this._task.Insert(task, -1);
            this.StartTaskThread(this._task);
        }

        public void UpdateAddressBox()
        {
            try
            {
                ExtendedWebBrowser activeBrowser = this.ActiveBrowser;
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

        private void WebBrowser_DocumentTitleChanged(object sender, EventArgs e)
        {
            ExtendedWebBrowser browser = sender as ExtendedWebBrowser;
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

        public void WebBrowser_Quit(object sender, EventArgs e)
        {
            ExtendedWebBrowser browser = sender as ExtendedWebBrowser;
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
            if ((m.Msg == 0x112) && (m.WParam.ToInt32() == 0xf020L))
            {
                base.Visible = false;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public ExtendedWebBrowser ActiveBrowser
        {
            get
            {
                TabPage selectedTab = this.tabControl.SelectedTab;
                if (selectedTab != null)
                {
                    return (selectedTab.Tag as ExtendedWebBrowser);
                }
                return null;
            }
        }

        public TaskBrowser.IEItem IEItem
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

