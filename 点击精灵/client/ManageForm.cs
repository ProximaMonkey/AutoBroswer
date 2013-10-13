namespace client
{
    using mshtml;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class ManageForm : Form
    {
        private client.BrowserManager _browserManager = new client.BrowserManager();
        private int _buttonIndex;
        private Rectangle _CaptureRect = new Rectangle();
        private client.ChangeUserForm _changeUserForm;
        private int _checkedIndex;
        private client.CreateAccountForm _createAccountForm;
        private string _curAddress = string.Empty;
        private int _curTaskIndex;
        private EngineManager _engineManager = new EngineManager();
        private int _findLinkIndex;
        private int _findNextIndex;
        private Timer _findTimer = new Timer();
        private client.GateSession _gateSession = new client.GateSession();
        private DateTime _getNetTaskTime = new DateTime();
        private ArrayList _getTasks = new ArrayList();
        private int _inputIndex;
        private string _ip = string.Empty;
        private bool _isInitialisedMyTask;
        private bool _isStopConnectToServer;
        private string _K = string.Empty;
        private int _linkIndex;
        private client.LoginUtil _loginUtil = new client.LoginUtil();
        private IntPtr _mainWnd = IntPtr.Zero;
        private int _maxTaskThreadsCount = 1;
        private int _modifyTaskIndex = -1;
        private int _modifyTaskItemIndex = -1;
        private ArrayList _packets = new ArrayList();
        private ArrayList _panels = new ArrayList();
        private int _port;
        private int _radioIndex;
        private int _runningTask;
        private DateTime _sendTime = new DateTime();
        private client.ServerSession _serverSession = new client.ServerSession();
        private string _setIp = string.Empty;
        private bool _startNetTask;
        private NetManagerStatus _status;
        private MyTask _task = new MyTask();
        private ArrayList _tasks = new ArrayList();
        private bool _taskThreadRun;
        private Timer _taskTimer = new Timer();
        private MyTask _testTask;
        private MessageForm _tipForm = new MessageForm();
        private int _waitDocCompleteTime = 20;
        private int _waitFindTime = 20;
        public const string BROWSERNAME = "点击精灵浏览器";
        private Button button1;
        private Button button2;
        private Button button3;
        private Button buttonBuyVIP;
        private Button buttonChangeUser;
        private Button buttonClearCookie;
        private Button buttonClearTaskItems;
        private Button buttonClickButton;
        private Button buttonClickChecked;
        private Button buttonClickLink;
        private Button buttonClickRadio;
        private Button buttonCustomAreaSelectAll;
        private Button buttonCustomAreaSelectNone;
        private Button buttonCustomSystemDistribute;
        private Button buttonCustomTemplateSaveTask;
        private Button buttonCustonTemplateAdd;
        private Button buttonCustonTemplateTest;
        private Button buttonEditAccount;
        private Button buttonEnterInputText;
        private Button buttonEnterWebSite;
        private Button buttonFindButton;
        private Button buttonFindChecked;
        private Button buttonFindInputText;
        private Button buttonFindLink;
        private Button buttonFindPage;
        private Button buttonFindRadio;
        private Button buttonFresh;
        private Button buttonInsertClearCookie;
        private Button buttonInsertClickButton;
        private Button buttonInsertClickChecked;
        private Button buttonInsertClickLink;
        private Button buttonInsertClickRadio;
        private Button buttonInsertDeepClick;
        private Button buttonInsertFindPage;
        private Button buttonInsertFresh;
        private Button buttonInsertInputText;
        private Button buttonInsertPressKey;
        private Button buttonInsertWait;
        private Button buttonInsertWebSite;
        private Button buttonPay;
        private Button buttonPressKey;
        private Button buttonReLogin;
        private Button buttonWait;
        private CheckBox checkBoxAllowScrollWhileLoad;
        private CheckBox checkBoxAllowScrollWhileWait;
        private CheckBox checkBoxCustomAnHui;
        private CheckBox checkBoxCustomAoMen;
        private CheckBox checkBoxCustomBeiJing;
        private CheckBox checkBoxCustomChongQing;
        private CheckBox checkBoxCustomFuJian;
        private CheckBox checkBoxCustomGanSu;
        private CheckBox checkBoxCustomGuangDong;
        private CheckBox checkBoxCustomGuangXi;
        private CheckBox checkBoxCustomGuiZhou;
        private CheckBox checkBoxCustomHaiNan;
        private CheckBox checkBoxCustomHeBei;
        private CheckBox checkBoxCustomHeiLongjiang;
        private CheckBox checkBoxCustomHeNan;
        private CheckBox checkBoxCustomHuBei;
        private CheckBox checkBoxCustomHuNan;
        private CheckBox checkBoxCustomJiangSu;
        private CheckBox checkBoxCustomJiangXi;
        private CheckBox checkBoxCustomJiLin;
        private CheckBox checkBoxCustomLiaoNing;
        private CheckBox checkBoxCustomNeiMengGu;
        private CheckBox checkBoxCustomNingXia;
        private CheckBox checkBoxCustomOtherProvince;
        private CheckBox checkBoxCustomQingHai;
        private CheckBox checkBoxCustomSanDong;
        private CheckBox checkBoxCustomSanXi;
        private CheckBox checkBoxCustomShangHai;
        private CheckBox checkBoxCustomShanXi;
        private CheckBox checkBoxCustomSiChuang;
        private CheckBox checkBoxCustomTaiWan;
        private CheckBox checkBoxCustomTianJin;
        private CheckBox checkBoxCustomXiangGan;
        private CheckBox checkBoxCustomXinJiang;
        private CheckBox checkBoxCustomXiZang;
        private CheckBox checkBoxCustomYunNan;
        private CheckBox checkBoxCustomZheJiang;
        private CheckBox checkBoxMoveMouse;
        public static int CLICKNEXTPAGE = 0;
        public static int CLICKPERDAY = 200;
        public const string CLOSEBROWSER = "隐藏浏览器";
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private ComboBox comboBoxTaskBrowserCount;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private DataGridView dataGridViewTasks;
        private DataGridView dataGridViewTemplate;
        public static int DEEPCLICK = 0;
        public static int DEEPWAITTIME = 30;
        public static int FINDMAXPAGE = 20;
        public static int FINDPAGE = 0;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private GroupBox groupBoxClearCookie;
        private GroupBox groupBoxClickButton;
        private GroupBox groupBoxClickChecked;
        private GroupBox groupBoxClickLink;
        private GroupBox groupBoxClickRadio;
        private GroupBox groupBoxDeepClick;
        private GroupBox groupBoxFindPage;
        private GroupBox groupBoxFresh;
        private GroupBox groupBoxInputText;
        private GroupBox groupBoxNavigate;
        private GroupBox groupBoxWait;
        private GroupBox groupPressKey;
        public static int HOMEWAITTIME = 10;
        public static int HOURSCOUNT = 0x30;
        private Label label1;
        private Label label10;
        private Label label100;
        private Label label101;
        private Label label102;
        private Label label103;
        private Label label104;
        private Label label105;
        private Label label106;
        private Label label107;
        private Label label108;
        private Label label109;
        private Label label11;
        private Label label110;
        private Label label111;
        private Label label125;
        private Label label126;
        private Label label127;
        private Label label128;
        private Label label129;
        private Label label130;
        private Label label131;
        private Label label132;
        private Label label133;
        private Label label134;
        private Label label135;
        private Label label136;
        private Label label137;
        private Label label138;
        private Label label139;
        private Label label140;
        private Label label141;
        private Label label142;
        private Label label143;
        private Label label144;
        private Label label145;
        private Label label146;
        private Label label147;
        private Label label148;
        private Label label15;
        private Label label16;
        private Label label2;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label28;
        private Label label29;
        private Label label3;
        private Label label30;
        private Label label31;
        private Label label33;
        private Label label60;
        private Label label61;
        private Label label62;
        private Label label64;
        private Label label65;
        private Label label66;
        private Label label68;
        private Label label69;
        private Label label70;
        private Label label71;
        private Label label72;
        private Label label88;
        private Label label89;
        private Label label9;
        private Label label90;
        private Label label91;
        private Label label92;
        private Label label93;
        private Label label94;
        private Label label95;
        private Label label96;
        private Label label97;
        private Label label98;
        private Label label99;
        private Label labelAccountTip;
        private Label labelCustomTotalScore;
        private Label labelDayCompleteNetTasksCount;
        private Label labelDayCompleteTask;
        private Label labelDayCosumeScore;
        private Label labelDayNetTaskScore;
        private Label labelGetTotalScore;
        private Label labelGetTotalTask;
        private Label labelPublishTotalScore;
        private Label labelPublishTotalTask;
        private Label labelRunTasksCount;
        private Label labelScore;
        private Label labelServerTasksCount;
        private Label labelTasksCount;
        private Label labelUserName;
        private LinkLabel linkLabel1;
        private ListBox listBoxTaskItemLine;
        private ListBox listBoxTaskItems;
        private ListBox listBoxTemplateCategory;
        public const string MAINFORMNAME = "点击精灵1.0.0.37";
        public const int MESSAGE_CLOSETASK = 2;
        public const int MESSAGE_COMPLETETASK = 1;
        public const int MESSAGE_RUNTASK = 0;
        private NotifyIcon notifyIcon1;
        public const string OPENBROWSER = "打开浏览器";
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        public static string PAUSETASK = "暂停挂机";
        private RadioButton radioButtonAll;
        private RadioButton radioButtonCookie;
        private RadioButton radioButtonHistory;
        private RadioButton radioButtonPassword;
        private RadioButton radioButtonTempFile;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        public static string STARTTASK = "开始挂机";
        private const string SYSTEMSETTINGFILE = "setting.xml";
        private TabControl tabControl1;
        private TabControl tabControl3;
        private TabControl tabControl4;
        private TabPage tabPage3;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private TabPage tabPage9;
        private TabPage tabPageCommonTemplate;
        private TabPage tabPageCustonTemplate;
        private TabPage tabPageTaskManage;
        private TabPage tabPageUser;
        public const int TASKBROWSER_COUNT = 5;
        private const string TEMPLATESETTINGFILE = "template.xml";
        private TextBox textBoxButtonID;
        private TextBox textBoxCheckedID;
        private TextBox textBoxClickIntervalTime;
        private TextBox textBoxClickPerIp;
        private TextBox textBoxClickPerPC;
        private TextBox textBoxCustomClickPerDay;
        private TextBox textBoxCustomClock0;
        private TextBox textBoxCustomClock1;
        private TextBox textBoxCustomClock10;
        private TextBox textBoxCustomClock11;
        private TextBox textBoxCustomClock12;
        private TextBox textBoxCustomClock13;
        private TextBox textBoxCustomClock14;
        private TextBox textBoxCustomClock15;
        private TextBox textBoxCustomClock16;
        private TextBox textBoxCustomClock17;
        private TextBox textBoxCustomClock18;
        private TextBox textBoxCustomClock19;
        private TextBox textBoxCustomClock2;
        private TextBox textBoxCustomClock20;
        private TextBox textBoxCustomClock21;
        private TextBox textBoxCustomClock22;
        private TextBox textBoxCustomClock23;
        private TextBox textBoxCustomClock24;
        private TextBox textBoxCustomClock25;
        private TextBox textBoxCustomClock26;
        private TextBox textBoxCustomClock27;
        private TextBox textBoxCustomClock28;
        private TextBox textBoxCustomClock29;
        private TextBox textBoxCustomClock3;
        private TextBox textBoxCustomClock30;
        private TextBox textBoxCustomClock31;
        private TextBox textBoxCustomClock32;
        private TextBox textBoxCustomClock33;
        private TextBox textBoxCustomClock34;
        private TextBox textBoxCustomClock35;
        private TextBox textBoxCustomClock36;
        private TextBox textBoxCustomClock37;
        private TextBox textBoxCustomClock38;
        private TextBox textBoxCustomClock39;
        private TextBox textBoxCustomClock4;
        private TextBox textBoxCustomClock40;
        private TextBox textBoxCustomClock41;
        private TextBox textBoxCustomClock42;
        private TextBox textBoxCustomClock43;
        private TextBox textBoxCustomClock44;
        private TextBox textBoxCustomClock45;
        private TextBox textBoxCustomClock46;
        private TextBox textBoxCustomClock47;
        private TextBox textBoxCustomClock5;
        private TextBox textBoxCustomClock6;
        private TextBox textBoxCustomClock7;
        private TextBox textBoxCustomClock8;
        private TextBox textBoxCustomClock9;
        private TextBox textBoxDeepClickText;
        private TextBox textBoxDeepClickTimes;
        private TextBox textBoxDeepClickWaitTime;
        private TextBox textBoxDocWaitTime2;
        private TextBox textBoxFindKeyword;
        private TextBox textBoxFindMaxPage;
        private TextBox textBoxFindPage;
        private TextBox textBoxFindPageNext;
        private TextBox textBoxFindWaitTime2;
        private TextBox textBoxInputBoxName;
        private TextBox textBoxInputText;
        private TextBox textBoxLink;
        private TextBox textBoxLinkKeyword;
        private TextBox textBoxRadioID;
        private TextBox textBoxTaskIntervalTimeMax;
        private TextBox textBoxTaskIntervalTimeMin;
        private TextBox textBoxTaskNote;
        private TextBox textBoxWaitTime;
        private TextBox textBoxWebSite;
        private TextBox textBoxWebSiteRefer;
        public static int THREADINTERVAL = 200;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonOpenBrowser;
        private ToolStripButton toolStripButtonStartTask;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem ToolStripMenuItem5;
        private ToolStripMenuItem ToolStripMenuItem6;
        private ToolStripMenuItem ToolStripMenuItem7;
        private ToolStripMenuItem ToolStripMenuItem8;
        public const string VERSION = "1.0.0.37";
        public const int WAITDOCCOMPLETE = 20;
        public const int WAITFINDTIME = 20;
        private WebBrowser webBrowser1;

        public ManageForm()
        {
            this.InitializeComponent();
            this.toolStripButtonStartTask.Text = STARTTASK;
            this.toolStripButtonOpenBrowser.Text = "打开浏览器";
            this.LoadSetting();
            this.InitTaskManager();
            this.InitNetManager();
            this.InitHtmlElementManager();
            if (this._runningTask != 0)
            {
                this.StartTask(null);
                this.toolStripButtonStartTask.Text = PAUSETASK;
            }
        }

        private void _AddIeItems()
        {
            if (this.GetTestTask() == null)
            {
                this.GetMyTask();
            }
        }

        public void addGetTask(MyTask task)
        {
            MyTask task2 = (MyTask) task.Clone();
            foreach (MyTask task3 in this._getTasks)
            {
                if (task3._id == task2._id)
                {
                    this._getTasks.Remove(task3);
                    break;
                }
            }
            this._getTasks.Add(task2);
        }

        private void AddPacket(WorldPacket packet)
        {
            PacketProcessInfo info = new PacketProcessInfo(packet);
            this._packets.Add(info);
        }

        public void AddTask(MyTask task)
        {
            MyTask task2 = (MyTask) task.Clone();
            foreach (MyTask task3 in this._tasks)
            {
                if (task3._id == task2._id)
                {
                    this._tasks.Remove(task3);
                    break;
                }
            }
            this._tasks.Add(task2);
            this.UpdateAccount();
            this.ReloadTasks();
        }

        public void AllowNumKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonAreaSelectAll_Click(object sender, EventArgs e)
        {
            this.SetAllAreaClicks(true);
        }

        private void buttonAreaSelectNone_Click(object sender, EventArgs e)
        {
            this.SetAllAreaClicks(false);
        }

        private void buttonBuyVIP_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.djjlseo.com/buyvip" + this.GetUrlSessionStr());
        }

        private void buttonChangeUser_Click(object sender, EventArgs e)
        {
            if (this._changeUserForm == null)
            {
                this._changeUserForm = new client.ChangeUserForm(this);
            }
            this._changeUserForm.Show();
        }

        private void buttonClearCookieClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[6];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonClearTask(object sender, EventArgs e)
        {
            this._task._taskItems.Clear();
            this.ReloadTask(true);
        }

        private void buttonClickButtonClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[2];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonClickCheckedClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[11];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonClickLinkClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[3];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonClickRadioClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[10];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonCustonTemplateAddClick(object sender, EventArgs e)
        {
            if ((this._task != null) && (this._task._taskItems.Count > 0))
            {
                string str = this.textBoxTaskNote.Text.Trim();
                if (str.Length >= 0x20)
                {
                    str = str.Remove(0x1c) + "...";
                }
                this._task.Note = str;
                this._task.SetAreaClick(this.GetAreasClick());
                this._task.SetHourClick(this.GetHoursClick());
                this.SendAddTask(this._task);
                this._task = new MyTask();
                this.ResetTemplateComponent();
                this.tabControl1.SelectedTab = this.tabPageTaskManage;
            }
        }

        private void buttonCustonTemplateSaveClick(object sender, EventArgs e)
        {
            if ((this._task != null) && (this._task._taskItems.Count > 0))
            {
                string str = this.textBoxTaskNote.Text.Trim();
                if (str.Length >= 0x20)
                {
                    str = str.Remove(0x1c) + "...";
                }
                this._task.Note = str;
                this._task.SetAreaClick(this.GetAreasClick());
                this._task.SetHourClick(this.GetHoursClick());
                if (this._modifyTaskIndex != -1)
                {
                    this.SendModifyTask(this._task);
                    this._modifyTaskIndex = -1;
                }
                else
                {
                    this.SendAddTask(this._task);
                }
                this._task = new MyTask();
                this.ResetTemplateComponent();
                this.tabControl1.SelectedTab = this.tabPageTaskManage;
            }
        }

        private void buttonCustonTemplateTestClick(object sender, EventArgs e)
        {
            if ((this._task != null) && (this._task._taskItems.Count > 0))
            {
                this._task.SetAreaClick(this.GetAreasClick());
                this._task.SetHourClick(this.GetHoursClick());
                this.StartTask(this._task);
            }
        }

        private void buttonDeepClickClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[5];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonEditAccount_Click(object sender, EventArgs e)
        {
            if (this._createAccountForm == null)
            {
                this._createAccountForm = new client.CreateAccountForm(this);
            }
            this._createAccountForm.Show();
        }

        private void buttonEnterWebSiteClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[0];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonExchange_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.djjlseo.com/member/credit" + this.GetUrlSessionStr());
        }

        private void buttonFindPageClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[7];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonFreshClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[8];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonInputTextClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[1];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonInsertClearCookieClick(object sender, EventArgs e)
        {
            string str;
            if (this.radioButtonTempFile.Checked)
            {
                str = "8";
            }
            else if (this.radioButtonCookie.Checked)
            {
                str = "2";
            }
            else if (this.radioButtonPassword.Checked)
            {
                str = "48";
            }
            else if (this.radioButtonHistory.Checked)
            {
                str = "1";
            }
            else
            {
                str = "255";
            }
            if (this._modifyTaskItemIndex == -1)
            {
                int num = 6;
                TaskInfo task = new TaskInfo(num.ToString(), str, "", "", "");
                this._task.Insert(task, this._curTaskIndex++);
            }
            else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
            {
                TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                int num3 = 6;
                if (info2._param1.Equals(num3.ToString()))
                {
                    info2._param2 = str;
                    this._task._taskItems[this._modifyTaskItemIndex] = info2;
                }
                else
                {
                    int num4 = 6;
                    TaskInfo info3 = new TaskInfo(num4.ToString(), str, "", "", "");
                    this._task.Insert(info3, this._curTaskIndex++);
                }
            }
            this._modifyTaskItemIndex = -1;
            this.ReloadTask(false);
            this.SetTaskItem(TaskCommand.Task_None);
        }

        private void buttonInsertClickButtonClick(object sender, EventArgs e)
        {
            string str = this.textBoxButtonID.Text.Trim();
            ElementTag iD = ElementTag.ID;
            if (!string.IsNullOrEmpty(str))
            {
                TaskInfo info;
                if (this.textBoxButtonID.Tag != null)
                {
                    iD = (ElementTag) this.textBoxButtonID.Tag;
                    this.textBoxButtonID.Tag = null;
                }
                if (this._modifyTaskItemIndex == -1)
                {
                    int num = 2;
                    int num2 = (int) iD;
                    info = new TaskInfo(num.ToString(), str, num2.ToString(), this._buttonIndex.ToString(), "");
                    this._task.Insert(info, this._curTaskIndex++);
                }
                else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
                {
                    TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                    int num4 = 2;
                    if (info2._param1.Equals(num4.ToString()))
                    {
                        info2._param2 = str;
                        info2._param3 = ((int) iD).ToString();
                        info2._param4 = this._buttonIndex.ToString();
                        this._task._taskItems[this._modifyTaskItemIndex] = info2;
                    }
                    else
                    {
                        int num6 = 2;
                        int num7 = (int) iD;
                        info = new TaskInfo(num6.ToString(), str, num7.ToString(), this._buttonIndex.ToString(), "");
                        this._task.Insert(info, this._curTaskIndex++);
                    }
                }
                this._buttonIndex = 0;
                this._modifyTaskItemIndex = -1;
                this.ReloadTask(false);
                this.SetTaskItem(TaskCommand.Task_None);
            }
        }

        private void buttonInsertClickCheckedClick(object sender, EventArgs e)
        {
            string str = this.textBoxCheckedID.Text.Trim();
            ElementTag iD = ElementTag.ID;
            if (!string.IsNullOrEmpty(str))
            {
                TaskInfo info;
                if (this.textBoxCheckedID.Tag != null)
                {
                    iD = (ElementTag) this.textBoxCheckedID.Tag;
                    this.textBoxCheckedID.Tag = null;
                }
                if (this._modifyTaskItemIndex == -1)
                {
                    int num = 12;
                    int num2 = (int) iD;
                    info = new TaskInfo(num.ToString(), str, num2.ToString(), this._checkedIndex.ToString(), "");
                    this._task.Insert(info, this._curTaskIndex++);
                }
                else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
                {
                    TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                    int num4 = 12;
                    if (info2._param1.Equals(num4.ToString()))
                    {
                        info2._param2 = str;
                        info2._param3 = ((int) iD).ToString();
                        info2._param4 = this._checkedIndex.ToString();
                        this._task._taskItems[this._modifyTaskItemIndex] = info2;
                    }
                    else
                    {
                        int num6 = 12;
                        int num7 = (int) iD;
                        info = new TaskInfo(num6.ToString(), str, num7.ToString(), this._checkedIndex.ToString(), "");
                        this._task.Insert(info, this._curTaskIndex++);
                    }
                }
                this._checkedIndex = 0;
                this._modifyTaskItemIndex = -1;
                this.ReloadTask(false);
                this.SetTaskItem(TaskCommand.Task_None);
            }
        }

        private void buttonInsertClickLinkClick(object sender, EventArgs e)
        {
            string str = this.textBoxLink.Text.Trim();
            string str2 = this.textBoxLinkKeyword.Text.Trim();
            ElementTag outerText = ElementTag.outerText;
            if (str2 == "")
            {
                str2 = string.Empty;
            }
            if (!string.IsNullOrEmpty(str))
            {
                if (this.textBoxLink.Tag != null)
                {
                    outerText = (ElementTag) this.textBoxLink.Tag;
                    this.textBoxLink.Tag = null;
                }
                if (this._modifyTaskItemIndex == -1)
                {
                    int num = 3;
                    int num2 = (int) outerText;
                    TaskInfo task = new TaskInfo(num.ToString(), str, str2, num2.ToString(), this._linkIndex.ToString());
                    this._task.Insert(task, this._curTaskIndex++);
                }
                else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
                {
                    TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                    int num4 = 3;
                    if (info2._param1.Equals(num4.ToString()))
                    {
                        info2._param2 = str;
                        info2._param3 = str2;
                        info2._param4 = ((int) outerText).ToString();
                        info2._param5 = this._linkIndex.ToString();
                        this._task._taskItems[this._modifyTaskItemIndex] = info2;
                    }
                    else
                    {
                        int num6 = 3;
                        int num7 = (int) outerText;
                        TaskInfo info3 = new TaskInfo(num6.ToString(), str, str2, num7.ToString(), this._linkIndex.ToString());
                        this._task.Insert(info3, this._curTaskIndex++);
                    }
                }
                this._linkIndex = 0;
                this._modifyTaskItemIndex = -1;
                this.ReloadTask(false);
                this.SetTaskItem(TaskCommand.Task_None);
            }
        }

        private void buttonInsertClickRadioClick(object sender, EventArgs e)
        {
            string str = this.textBoxRadioID.Text.Trim();
            ElementTag iD = ElementTag.ID;
            if (!string.IsNullOrEmpty(str))
            {
                TaskInfo info;
                if (this.textBoxRadioID.Tag != null)
                {
                    iD = (ElementTag) this.textBoxRadioID.Tag;
                    this.textBoxRadioID.Tag = null;
                }
                if (this._modifyTaskItemIndex == -1)
                {
                    int num = 11;
                    int num2 = (int) iD;
                    info = new TaskInfo(num.ToString(), str, num2.ToString(), this._radioIndex.ToString(), "");
                    this._task.Insert(info, this._curTaskIndex++);
                }
                else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
                {
                    TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                    int num4 = 11;
                    if (info2._param1.Equals(num4.ToString()))
                    {
                        info2._param2 = str;
                        info2._param3 = ((int) iD).ToString();
                        info2._param4 = this._radioIndex.ToString();
                        this._task._taskItems[this._modifyTaskItemIndex] = info2;
                    }
                    else
                    {
                        int num6 = 11;
                        int num7 = (int) iD;
                        info = new TaskInfo(num6.ToString(), str, num7.ToString(), this._radioIndex.ToString(), "");
                        this._task.Insert(info, this._curTaskIndex++);
                    }
                }
                this._radioIndex = 0;
                this._modifyTaskItemIndex = -1;
                this.ReloadTask(false);
                this.SetTaskItem(TaskCommand.Task_None);
            }
        }

        private void buttonInsertDeepClickClick(object sender, EventArgs e)
        {
            string str = this.textBoxDeepClickTimes.Text.Trim();
            string str2 = this.textBoxDeepClickWaitTime.Text.Trim();
            string str3 = this.textBoxDeepClickText.Text.Trim();
            if ((!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2)) && (WindowUtil.StringToInt(str) != 0))
            {
                if (this._modifyTaskItemIndex == -1)
                {
                    int num = 5;
                    TaskInfo task = new TaskInfo(num.ToString(), str, str2, str3, "");
                    this._task.Insert(task, this._curTaskIndex++);
                }
                else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
                {
                    TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                    int num3 = 5;
                    if (info2._param1.Equals(num3.ToString()))
                    {
                        info2._param2 = str;
                        info2._param3 = str2;
                        info2._param4 = str3;
                        this._task._taskItems[this._modifyTaskItemIndex] = info2;
                    }
                    else
                    {
                        int num4 = 5;
                        TaskInfo info3 = new TaskInfo(num4.ToString(), str, str2, str3, "");
                        this._task.Insert(info3, this._curTaskIndex++);
                    }
                }
                this._modifyTaskItemIndex = -1;
                this.ReloadTask(false);
                this.SetTaskItem(TaskCommand.Task_None);
            }
        }

        private void buttonInsertFindPageClick(object sender, EventArgs e)
        {
            string str;
            string str2 = this.textBoxFindPage.Text.Trim();
            string str3 = this.textBoxFindPageNext.Text.Trim();
            string str4 = this.textBoxFindMaxPage.Text.Trim();
            string str5 = this.textBoxFindKeyword.Text.Trim();
            ElementTag outerText = ElementTag.outerText;
            ElementTag tag = ElementTag.outerText;
            if (str5 == "")
            {
                str5 = string.Empty;
            }
            if ((string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3)) || string.IsNullOrEmpty(str4))
            {
                return;
            }
            if (this.textBoxFindPage.Tag != null)
            {
                outerText = (ElementTag) this.textBoxFindPage.Tag;
            }
            if (this.textBoxFindPageNext.Tag != null)
            {
                tag = (ElementTag) this.textBoxFindPageNext.Tag;
            }
            this.textBoxFindPage.Tag = this.textBoxFindPageNext.Tag = null;
            TaskInfo task = null;
            if (this._modifyTaskItemIndex != -1)
            {
                if ((this._modifyTaskItemIndex < 0) || (this._modifyTaskItemIndex >= this._task._taskItems.Count))
                {
                    goto Label_0534;
                }
                task = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                int num11 = 7;
                if (!task._param1.Equals(num11.ToString()))
                {
                    int num12 = 13;
                    if (!task._param1.Equals(num12.ToString()))
                    {
                        int num13 = 15;
                        if (!task._param1.Equals(num13.ToString()))
                        {
                            int num14 = 0x11;
                            if (!task._param1.Equals(num14.ToString()))
                            {
                                switch (outerText)
                                {
                                    case ElementTag.outerText:
                                        switch (tag)
                                        {
                                            case ElementTag.outerText:
                                                str = 7.ToString();
                                                goto Label_04C0;

                                            case ElementTag.href:
                                                str = 13.ToString();
                                                goto Label_04C0;
                                        }
                                        str = 0x13.ToString();
                                        goto Label_04C0;

                                    case ElementTag.href:
                                        switch (tag)
                                        {
                                            case ElementTag.outerText:
                                                str = 15.ToString();
                                                goto Label_04C0;

                                            case ElementTag.href:
                                                str = 0x11.ToString();
                                                goto Label_04C0;
                                        }
                                        str = 0x15.ToString();
                                        goto Label_04C0;
                                }
                                switch (tag)
                                {
                                    case ElementTag.outerText:
                                        str = 0x17.ToString();
                                        goto Label_04C0;

                                    case ElementTag.href:
                                        str = 0x19.ToString();
                                        goto Label_04C0;
                                }
                                str = 0x1b.ToString();
                                goto Label_04C0;
                            }
                        }
                    }
                }
                switch (outerText)
                {
                    case ElementTag.outerText:
                        switch (tag)
                        {
                            case ElementTag.outerText:
                                task._param1 = 7.ToString();
                                goto Label_0395;

                            case ElementTag.href:
                                task._param1 = 13.ToString();
                                goto Label_0395;
                        }
                        task._param1 = 0x13.ToString();
                        goto Label_0395;

                    case ElementTag.href:
                        switch (tag)
                        {
                            case ElementTag.outerText:
                                task._param1 = 15.ToString();
                                goto Label_0395;

                            case ElementTag.href:
                                task._param1 = 0x11.ToString();
                                goto Label_0395;
                        }
                        task._param1 = 0x15.ToString();
                        goto Label_0395;
                }
                switch (tag)
                {
                    case ElementTag.outerText:
                        task._param1 = 0x17.ToString();
                        goto Label_0395;

                    case ElementTag.href:
                        task._param1 = 0x19.ToString();
                        goto Label_0395;
                }
                task._param1 = 0x1b.ToString();
            }
            else
            {
                switch (outerText)
                {
                    case ElementTag.outerText:
                        switch (tag)
                        {
                            case ElementTag.outerText:
                                str = 7.ToString();
                                break;

                            case ElementTag.href:
                                str = 13.ToString();
                                break;
                        }
                        str = 0x13.ToString();
                        break;

                    case ElementTag.href:
                        switch (tag)
                        {
                            case ElementTag.outerText:
                                str = 15.ToString();
                                break;

                            case ElementTag.href:
                                str = 0x11.ToString();
                                break;
                        }
                        str = 0x15.ToString();
                        break;

                    default:
                        switch (tag)
                        {
                            case ElementTag.outerText:
                                str = 0x17.ToString();
                                break;

                            case ElementTag.href:
                                str = 0x19.ToString();
                                break;
                        }
                        str = 0x1b.ToString();
                        break;
                }
                task = new TaskInfo(str, str2, str3, string.Concat(new object[] { str4, ',', this._findLinkIndex.ToString(), ',', this._findNextIndex.ToString() }), str5);
                this._task.Insert(task, this._curTaskIndex++);
                goto Label_0534;
            }
        Label_0395:
            task._param2 = str2;
            task._param3 = str3;
            task._param4 = string.Concat(new object[] { str4, ',', this._findLinkIndex.ToString(), ',', this._findNextIndex.ToString() });
            task._param5 = str5;
            this._task._taskItems[this._modifyTaskItemIndex] = task;
            goto Label_0534;
        Label_04C0:;
            task = new TaskInfo(str, str2, str3, string.Concat(new object[] { str4, ',', this._findLinkIndex.ToString(), ',', this._findNextIndex.ToString() }), str5);
            this._task.Insert(task, this._curTaskIndex++);
        Label_0534:
            this._findLinkIndex = this._findNextIndex = 0;
            this._modifyTaskItemIndex = -1;
            this.ReloadTask(false);
            this.SetTaskItem(TaskCommand.Task_None);
        }

        private void buttonInsertFreshClick(object sender, EventArgs e)
        {
            if (this._modifyTaskItemIndex == -1)
            {
                int num = 9;
                TaskInfo task = new TaskInfo(num.ToString(), "", "", "", "");
                this._task.Insert(task, this._curTaskIndex++);
            }
            else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
            {
                TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                int num3 = 9;
                if (!info2._param1.Equals(num3.ToString()))
                {
                    int num4 = 9;
                    TaskInfo info3 = new TaskInfo(num4.ToString(), "", "", "", "");
                    this._task.Insert(info3, this._curTaskIndex++);
                }
            }
            this._modifyTaskItemIndex = -1;
            this.ReloadTask(false);
            this.SetTaskItem(TaskCommand.Task_None);
        }

        private void buttonInsertInputTextClick(object sender, EventArgs e)
        {
            string str = this.textBoxInputBoxName.Text.Trim();
            string str2 = this.textBoxInputText.Text.Trim();
            ElementTag iD = ElementTag.ID;
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
            {
                if (this.textBoxInputBoxName.Tag != null)
                {
                    iD = (ElementTag) this.textBoxInputBoxName.Tag;
                    this.textBoxInputBoxName.Tag = null;
                }
                if (this._modifyTaskItemIndex == -1)
                {
                    TaskInfo info;
                    if (this._inputIndex == 0)
                    {
                        int num = 1;
                        int num2 = (int) iD;
                        info = new TaskInfo(num.ToString(), str, str2, num2.ToString(), "");
                    }
                    else
                    {
                        int num3 = 1;
                        int num4 = (int) iD;
                        info = new TaskInfo(num3.ToString(), str, str2, num4.ToString(), this._inputIndex.ToString());
                    }
                    this._task.Insert(info, this._curTaskIndex++);
                }
                else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
                {
                    TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                    int num6 = 1;
                    if (info2._param1.Equals(num6.ToString()))
                    {
                        info2._param2 = str;
                        info2._param3 = str2;
                        info2._param4 = ((int) iD).ToString();
                        if (this._inputIndex == 0)
                        {
                            info2._param5 = "";
                        }
                        else
                        {
                            info2._param5 = this._inputIndex.ToString();
                        }
                        this._task._taskItems[this._modifyTaskItemIndex] = info2;
                    }
                    else
                    {
                        TaskInfo info3;
                        if (this._inputIndex == 0)
                        {
                            int num8 = 1;
                            int num9 = (int) iD;
                            info3 = new TaskInfo(num8.ToString(), str, str2, num9.ToString(), "");
                        }
                        else
                        {
                            int num10 = 1;
                            int num11 = (int) iD;
                            info3 = new TaskInfo(num10.ToString(), str, str2, num11.ToString(), this._inputIndex.ToString());
                        }
                        this._task.Insert(info3, this._curTaskIndex++);
                    }
                }
                this._inputIndex = 0;
                this._modifyTaskItemIndex = -1;
                this.ReloadTask(false);
                this.SetTaskItem(TaskCommand.Task_None);
            }
        }

        private void buttonInsertPressKeyClick(object sender, EventArgs e)
        {
            if (this._modifyTaskItemIndex == -1)
            {
                int num = 10;
                TaskInfo task = new TaskInfo(num.ToString(), "", "", "", "");
                this._task.Insert(task, this._curTaskIndex++);
            }
            else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
            {
                TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                int num3 = 10;
                if (!info2._param1.Equals(num3.ToString()))
                {
                    int num4 = 10;
                    TaskInfo info3 = new TaskInfo(num4.ToString(), "", "", "", "");
                    this._task.Insert(info3, this._curTaskIndex++);
                }
            }
            this._modifyTaskItemIndex = -1;
            this.ReloadTask(false);
            this.SetTaskItem(TaskCommand.Task_None);
        }

        private void buttonInsertWaitClick(object sender, EventArgs e)
        {
            string text = this.textBoxWaitTime.Text;
            if (!string.IsNullOrEmpty(text))
            {
                if (this._modifyTaskItemIndex == -1)
                {
                    int num = 0;
                    TaskInfo task = new TaskInfo(num.ToString(), text, "", "", "");
                    this._task.Insert(task, this._curTaskIndex++);
                }
                else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
                {
                    TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                    int num3 = 0;
                    if (info2._param1.Equals(num3.ToString()))
                    {
                        info2._param2 = text;
                        this._task._taskItems[this._modifyTaskItemIndex] = info2;
                    }
                    else
                    {
                        int num4 = 0;
                        TaskInfo info3 = new TaskInfo(num4.ToString(), text, "", "", "");
                        this._task.Insert(info3, this._curTaskIndex++);
                    }
                }
                this._modifyTaskItemIndex = -1;
                this.ReloadTask(false);
                this.SetTaskItem(TaskCommand.Task_None);
            }
        }

        private void buttonInsertWebSiteClick(object sender, EventArgs e)
        {
            string str = this.textBoxWebSite.Text.Trim();
            string str2 = this.textBoxWebSiteRefer.Text.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                if (!str.StartsWith("http://") && !str.StartsWith("https://"))
                {
                    str = "http://" + str;
                }
                if ((!string.IsNullOrEmpty(str2) && !str2.StartsWith("http://")) && !str2.StartsWith("https://"))
                {
                    str2 = "http://" + str2;
                }
                if (this._modifyTaskItemIndex == -1)
                {
                    int num = 4;
                    TaskInfo task = new TaskInfo(num.ToString(), str, str2, "", "");
                    this._task.Insert(task, this._curTaskIndex++);
                }
                else if ((this._modifyTaskItemIndex >= 0) && (this._modifyTaskItemIndex < this._task._taskItems.Count))
                {
                    TaskInfo info2 = (TaskInfo) this._task._taskItems[this._modifyTaskItemIndex];
                    int num3 = 4;
                    if (info2._param1.Equals(num3.ToString()))
                    {
                        info2._param2 = str;
                        info2._param3 = str2;
                        this._task._taskItems[this._modifyTaskItemIndex] = info2;
                    }
                    else
                    {
                        int num4 = 4;
                        TaskInfo info3 = new TaskInfo(num4.ToString(), str, str2, "", "");
                        this._task.Insert(info3, this._curTaskIndex++);
                    }
                }
                this._modifyTaskItemIndex = -1;
                this.ReloadTask(false);
                this.SetTaskItem(TaskCommand.Task_None);
            }
        }

        private void buttonOpenTaskForm_Click(object sender, EventArgs e)
        {
            if (this.toolStripButtonOpenBrowser.Text == "打开浏览器")
            {
                if (this.BrowserManager.GetBrowsersCount() == 0)
                {
                    this.StartBrowserProcess(true);
                    this.toolStripButtonOpenBrowser.Text = "隐藏浏览器";
                }
                else
                {
                    this.BrowserManager.ShowWindow();
                    this.toolStripButtonOpenBrowser.Text = "隐藏浏览器";
                }
            }
            else
            {
                this.BrowserManager.HideWindow();
                this.toolStripButtonOpenBrowser.Text = "打开浏览器";
            }
        }

        private void buttonPay_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.djjlseo.com/pay" + this.GetUrlSessionStr());
        }

        private void buttonPressKeyClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[9];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void buttonReLogin_Click(object sender, EventArgs e)
        {
            this._loginUtil.Reset();
            this._loginUtil.LoadSetting();
            this.ConnectToServer();
        }

        private void buttonStartMyTasks_Click(object sender, EventArgs e)
        {
            if (this.toolStripButtonStartTask.Text == STARTTASK)
            {
                this.StartTask(null);
                this._runningTask = 1;
                this.toolStripButtonStartTask.Text = PAUSETASK;
            }
            else
            {
                this.StopTasks();
                this._runningTask = 0;
            }
        }

        private void buttonSystemDistribute_Click(object sender, EventArgs e)
        {
            int total = WindowUtil.StringToInt(this.textBoxCustomClickPerDay.Text);
            this.SystemDistribute(total);
        }

        private void buttonWaitClick(object sender, EventArgs e)
        {
            PanelInfo info = (PanelInfo) this._panels[4];
            info._show = !info._show;
            this.ShowPanels();
        }

        private void CalculateScore(int tabIndex)
        {
            this.labelCustomTotalScore.Text = "任务共消耗金币: " + this._task.CalculateScore().ToString();
        }

        public void ChangeUserLoginInfo(string account, string password)
        {
            this.ResetLoginUser();
            this._loginUtil.Account = account;
            this._loginUtil.Password = password;
            this._loginUtil.AccountTime = 0;
            this._loginUtil.K = string.Empty;
            this.Init(true);
        }

        public void CheckNetTime()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this._getNetTaskTime);
            if ((span.TotalSeconds >= 60.0) && !this.IsConnecting())
            {
                if (this.IsConnected())
                {
                    if (!this._isStopConnectToServer)
                    {
                        this.SendGetLoginInfo(false);
                    }
                }
                else
                {
                    this.buttonReLogin_Click(null, null);
                }
            }
        }

        public void ClearPackets()
        {
            this._packets.Clear();
        }

        public void ClickCancleUpdate()
        {
            base.Close();
        }

        public void ClickUpdate()
        {
            base.Close();
            Process.Start("update.exe");
        }

        public void CloseBrowserProcess(bool stopAll = true, uint taskID = 0)
        {
            this._browserManager.StopBrowserTask(stopAll, taskID);
        }

        private static void ClosePopupWindow()
        {
            IntPtr hwnd = WindowUtil.FindWindowEx(WindowUtil.FindWindow(null, "来自网页的消息"), IntPtr.Zero, "Button", "确定");
            if (hwnd != IntPtr.Zero)
            {
                WindowUtil.SendMessage(hwnd, 0xf5, 0, 0);
            }
        }

        private void comboBoxTaskBrowserCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._maxTaskThreadsCount = this.comboBoxTaskBrowserCount.SelectedIndex + 1;
            if (this._maxTaskThreadsCount > 5)
            {
                this._maxTaskThreadsCount = 5;
            }
            this.SaveSetting(false);
        }

        public void CompleteTask(uint id, bool completed, int taskStep, string errorString)
        {
            if (id == 0)
            {
                if (!this.StartNetTask)
                {
                    this.StopTasks();
                }
                if (completed)
                {
                    MessageBox.Show("测试任务成功");
                }
                else
                {
                    MessageBox.Show("测试任务失败");
                }
            }
            else
            {
                this.SendCompletTask(id, taskStep);
            }
            this._browserManager.StopBrowserTask(false, id);
        }

        public void ConnectToServer()
        {
            this.ResetLoginUser();
            this.Init(true);
            this.buttonReLogin.Visible = false;
            this.ShowAccountTip("正在连接服务器");
        }

        private bool CouldGetTask()
        {
            return (this._browserManager.GetBrowsersCount() < this._maxTaskThreadsCount);
        }

        private void dataGridViewTasks_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (((e.ColumnIndex >= 0) && (e.ColumnIndex < this.dataGridViewTasks.ColumnCount)) && ((e.RowIndex >= 0) && (e.RowIndex < this.dataGridViewTasks.Rows.Count)))
            {
                int rowIndex = e.RowIndex;
                if ((rowIndex >= 0) && (rowIndex < this._tasks.Count))
                {
                    this._task = (MyTask) ((MyTask) this.Tasks[rowIndex]).Clone();
                    this._modifyTaskIndex = rowIndex;
                    this.ReloadTask(true);
                    this.tabControl1.SelectedTab = this.tabPageCustonTemplate;
                }
            }
        }

        private void dataGridViewTasks_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.listBoxTaskItems.ContextMenu = null;
                if (((e.RowIndex >= 0) && (e.RowIndex < this.dataGridViewTasks.Rows.Count)) && ((e.ColumnIndex >= 0) && (e.ColumnIndex < this.dataGridViewTasks.ColumnCount)))
                {
                    this.dataGridViewTasks.ClearSelection();
                    this.dataGridViewTasks.Rows[e.RowIndex].Selected = true;
                    this.dataGridViewTasks.CurrentCell = this.dataGridViewTasks.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.contextMenuStrip2.Show(Control.MousePosition.X, Control.MousePosition.Y);
                }
            }
            this.dataGridViewTasks.Refresh();
        }

        private void dataGridViewTasks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (((e.ColumnIndex == 3) && (e.RowIndex >= 0)) && ((e.RowIndex < this.dataGridViewTasks.Rows.Count) && (e.Value != null)))
            {
                string s = e.Value.ToString();
                Rectangle cellBounds = e.CellBounds;
                float single1 = 96f / (72f / e.CellStyle.Font.Size);
                float num = 96f / (72f / e.CellStyle.Font.Size);
                cellBounds.Y += (e.CellBounds.Height - ((int) num)) / 2;
                e.PaintBackground(e.ClipBounds, false);
                if (e.State == (DataGridViewElementStates.Visible | DataGridViewElementStates.Selected | DataGridViewElementStates.Displayed))
                {
                    e.PaintBackground(e.ClipBounds, true);
                }
                if (s.Equals("运行中"))
                {
                    Brush brush = new SolidBrush(Color.Green);
                    e.Graphics.DrawString(s, this.Font, brush, cellBounds, StringFormat.GenericDefault);
                }
                else
                {
                    Brush brush2 = new SolidBrush(Color.Red);
                    e.Graphics.DrawString(s, this.Font, brush2, cellBounds, StringFormat.GenericDefault);
                }
                e.Handled = true;
            }
        }

        private void dataGridViewTemplate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (((e.ColumnIndex >= 0) && (e.ColumnIndex < this.dataGridViewTemplate.ColumnCount)) && ((e.RowIndex >= 0) && (e.RowIndex < this.dataGridViewTemplate.Rows.Count)))
            {
                EngineCaterogy caterogy = this._engineManager.GetCaterogy(this._engineManager.SelectedIndex);
                if (caterogy != null)
                {
                    MyTask template = caterogy.GetTemplate(e.RowIndex);
                    if (template != null)
                    {
                        this._task = (MyTask) template.Clone();
                        this.ReloadTask(true);
                        this.tabControl1.SelectedTab = this.tabPageCustonTemplate;
                    }
                }
            }
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == 0x4a)
            {
                System.Type cls = new client.COPYDATASTRUCT().GetType();
                client.COPYDATASTRUCT lParam = (client.COPYDATASTRUCT) m.GetLParam(cls);
                this.ParseMessage((int) lParam.dwData, ref lParam.lpData);
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        public void DisConnect()
        {
            this._gateSession.DisConnect();
            this._serverSession.DisConnect();
            this._status = (this._status >= NetManagerStatus.NETMANAGER_STATUS_CONNECTED_GATE) ? NetManagerStatus.NETMANAGER_STATUS_CONNECTED_GATE : NetManagerStatus.NETMANAGER_STATUS_NULL;
        }

        protected override void Dispose(bool disposing)
        {
            if (this._findTimer != null)
            {
                this._findTimer.Dispose();
                this._findTimer = null;
            }
            this._task = null;
            if (this._panels != null)
            {
                this._panels.Clear();
            }
            this._panels = null;
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawCaptureBox(IntPtr fromHandle, mshtml.IHTMLElement ele)
        {
            if (ele == null)
            {
                WindowUtil.RedrawWindow(fromHandle, null, IntPtr.Zero, 0x85);
            }
            else
            {
                mshtml.IHTMLDocument2 iEWindowDocument = HtmlUtil.GetIEWindowDocument(fromHandle);
                Rectangle elementRect = HtmlUtil.GetElementRect(iEWindowDocument.body, ele);
                elementRect.Inflate(1, 1);
                if (this._CaptureRect != elementRect)
                {
                    WindowUtil.RedrawWindow(fromHandle, null, IntPtr.Zero, 0x85);
                    this._CaptureRect = elementRect;
                }
                else
                {
                    Graphics.FromHwnd(fromHandle).DrawRectangle(new Pen(Color.Blue, 3f), elementRect);
                }
                Marshal.ReleaseComObject(iEWindowDocument);
            }
        }

        private void FindButton(IntPtr fromHandle, int x, int y)
        {
            string itemName = "";
            mshtml.IHTMLDocument2 iEWindowDocument = HtmlUtil.GetIEWindowDocument(fromHandle);
            mshtml.IHTMLElement ele = HtmlUtil.FindElement(fromHandle, iEWindowDocument, x, y);
            bool flag = false;
            if ((ele != null) && (ele.tagName != null))
            {
                if (ele.tagName.ToLower().Equals("input") && (ele.getAttribute("type", 0) != null))
                {
                    string str2 = ele.getAttribute("type", 0) as string;
                    if (str2.ToLower().Equals("submit") || str2.ToLower().Equals("button"))
                    {
                        flag = true;
                    }
                }
                else if (ele.tagName.ToLower().Equals("button"))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                if (ele.id != null)
                {
                    this.textBoxButtonID.Tag = ElementTag.ID;
                    itemName = ele.id;
                }
                else if ((ele.getAttribute("name", 0) != null) && (ele.getAttribute("name", 0).ToString() != ""))
                {
                    this.textBoxButtonID.Tag = ElementTag.name;
                    itemName = ele.getAttribute("name", 0) as string;
                }
                else if (!string.IsNullOrEmpty(ele.outerText))
                {
                    this.textBoxButtonID.Tag = ElementTag.outerText;
                    itemName = ele.outerText;
                }
                else if (!string.IsNullOrEmpty(ele.className))
                {
                    this.textBoxButtonID.Tag = ElementTag.className;
                    itemName = ele.className;
                }
                else if ((ele.getAttribute("value", 0) != null) && (ele.getAttribute("value", 0).ToString() != ""))
                {
                    this.textBoxButtonID.Tag = ElementTag.value;
                    itemName = ele.getAttribute("value", 0).ToString();
                }
                else
                {
                    this.textBoxButtonID.Tag = ElementTag.outerHTML;
                    itemName = ele.outerHTML;
                }
                this._buttonIndex = HtmlUtil.GetButtonElementIndex(iEWindowDocument, ele, itemName, ((int) this.textBoxButtonID.Tag).ToString());
            }
            else
            {
                ele = null;
            }
            this.DrawCaptureBox(fromHandle, ele);
            this.textBoxButtonID.Text = itemName;
            if (iEWindowDocument != null)
            {
                Marshal.FinalReleaseComObject(iEWindowDocument);
            }
        }

        private void FindButtonMouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            this._findTimer.Tag = TaskCommand.Task_ClickButton;
            this._findTimer.Enabled = true;
        }

        private void FindChecked(IntPtr fromHandle, int x, int y)
        {
            string itemName = "";
            mshtml.IHTMLDocument2 iEWindowDocument = HtmlUtil.GetIEWindowDocument(fromHandle);
            mshtml.IHTMLElement ele = HtmlUtil.FindElement(fromHandle, iEWindowDocument, x, y);
            bool flag = false;
            if ((ele != null) && (ele.tagName != null))
            {
                if (ele.tagName.ToLower().Equals("label"))
                {
                    if ((ele.getAttribute("htmlFor", 0) != null) && !string.IsNullOrEmpty(ele.getAttribute("htmlFor", 0).ToString()))
                    {
                        flag = true;
                    }
                }
                else if (ele.tagName.ToLower().Equals("input") && (ele.getAttribute("type", 0) != null))
                {
                    string str2 = ele.getAttribute("type", 0) as string;
                    if (str2.ToLower().Equals("checkbox"))
                    {
                        flag = true;
                    }
                }
            }
            else
            {
                ele = null;
            }
            if (flag)
            {
                if (ele.id != null)
                {
                    this.textBoxCheckedID.Tag = ElementTag.ID;
                    itemName = ele.id;
                }
                else if ((ele.getAttribute("value", 0) != null) && (ele.getAttribute("value", 0).ToString() != ""))
                {
                    this.textBoxCheckedID.Tag = ElementTag.name;
                    itemName = ele.getAttribute("value", 0) as string;
                }
                else if (!string.IsNullOrEmpty(ele.outerText))
                {
                    this.textBoxCheckedID.Tag = ElementTag.outerText;
                    itemName = ele.outerText;
                }
                else
                {
                    this.textBoxCheckedID.Tag = ElementTag.outerHTML;
                    itemName = ele.outerHTML;
                }
                this._checkedIndex = HtmlUtil.GetCheckedElementIndex(iEWindowDocument, ele, itemName, ((int) this.textBoxCheckedID.Tag).ToString());
            }
            else
            {
                ele = null;
            }
            this.DrawCaptureBox(fromHandle, ele);
            this.textBoxCheckedID.Text = itemName;
            if (iEWindowDocument != null)
            {
                Marshal.FinalReleaseComObject(iEWindowDocument);
            }
        }

        private void FindCheckedMouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            this._findTimer.Tag = TaskCommand.Task_ClickChecked;
            this._findTimer.Enabled = true;
        }

        private void FindDestPageMouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            this._findTimer.Tag = TaskCommand.Task_FindLinkLinkPage1;
            this._findTimer.Enabled = true;
        }

        private void FindInput(IntPtr fromHandle, int x, int y)
        {
            string itemName = "";
            mshtml.IHTMLDocument2 iEWindowDocument = HtmlUtil.GetIEWindowDocument(fromHandle);
            mshtml.IHTMLElement ele = HtmlUtil.FindElement(fromHandle, iEWindowDocument, x, y);
            if ((ele != null) && (ele.tagName != null))
            {
                if (ele.tagName.ToLower().Equals("label"))
                {
                    IHTMLLabelElement element2 = ele as IHTMLLabelElement;
                    ele = HtmlUtil.GetInputElement(iEWindowDocument, element2.htmlFor, 0.ToString(), string.Empty);
                }
                if (((ele != null) && (ele.tagName != null)) && ele.tagName.ToLower().Equals("input"))
                {
                    if ((ele.id != null) && (ele.id != ""))
                    {
                        this.textBoxInputBoxName.Tag = ElementTag.ID;
                        itemName = ele.id;
                    }
                    else if ((ele.getAttribute("name", 0) != null) && (ele.getAttribute("name", 0).ToString() != ""))
                    {
                        this.textBoxInputBoxName.Tag = ElementTag.name;
                        itemName = ele.getAttribute("name", 0) as string;
                    }
                    else if (!string.IsNullOrEmpty(ele.className))
                    {
                        this.textBoxInputBoxName.Tag = ElementTag.className;
                        itemName = ele.className;
                    }
                    else
                    {
                        this.textBoxInputBoxName.Tag = ElementTag.outerHTML;
                        itemName = ele.outerHTML;
                    }
                    this._inputIndex = HtmlUtil.GetInputElementIndex(iEWindowDocument, ele, itemName, ((int) this.textBoxInputBoxName.Tag).ToString());
                }
                else
                {
                    ele = null;
                }
            }
            this.DrawCaptureBox(fromHandle, ele);
            if (iEWindowDocument != null)
            {
                Marshal.ReleaseComObject(iEWindowDocument);
            }
            this.textBoxInputBoxName.Text = itemName;
        }

        private bool FindLink(IntPtr fromHandle, int x, int y, TextBox textBox, ref int linkIndex)
        {
            string itemName = "";
            bool flag = false;
            mshtml.IHTMLDocument2 iEWindowDocument = HtmlUtil.GetIEWindowDocument(fromHandle);
            mshtml.IHTMLElement ele = HtmlUtil.FindElement(fromHandle, iEWindowDocument, x, y);
            mshtml.IHTMLElement element2 = null;
            while (ele != null)
            {
                if (ele.tagName != null)
                {
                    string str2 = ele.tagName.ToLower();
                    if (str2.Equals("img"))
                    {
                        element2 = ele;
                    }
                    else if (str2.Equals("a"))
                    {
                        break;
                    }
                }
                ele = ele.parentElement;
            }
            if (((ele != null) && (ele.tagName != null)) && ele.tagName.ToLower().Equals("a"))
            {
                if (element2 != null)
                {
                    textBox.Tag = ElementTag.src;
                    itemName = element2.getAttribute("src", 0).ToString();
                    flag = true;
                }
                else if (ele.outerText != null)
                {
                    textBox.Tag = ElementTag.outerText;
                    itemName = ele.outerText;
                    flag = true;
                }
                else if (ele.getAttribute("href", 0) != null)
                {
                    textBox.Tag = ElementTag.href;
                    itemName = ele.getAttribute("href", 0).ToString();
                    flag = true;
                }
                if (flag)
                {
                    linkIndex = HtmlUtil.GetLinkElementIndex(iEWindowDocument, ele, itemName, ((int) textBox.Tag).ToString());
                }
            }
            if (!flag)
            {
                ele = null;
            }
            this.DrawCaptureBox(fromHandle, ele);
            if (textBox != null)
            {
                textBox.Text = itemName;
            }
            if (iEWindowDocument != null)
            {
                Marshal.FinalReleaseComObject(iEWindowDocument);
            }
            return flag;
        }

        private void FindLinkMouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            this._findTimer.Tag = TaskCommand.Task_ClickLink;
            this._findTimer.Enabled = true;
        }

        private void FindMouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            this._findTimer.Enabled = false;
        }

        private void FindPageNextMouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            this._findTimer.Tag = TaskCommand.Task_FindLinkLinkPage2;
            this._findTimer.Enabled = true;
        }

        private void FindRadio(IntPtr fromHandle, int x, int y)
        {
            string itemName = "";
            mshtml.IHTMLDocument2 iEWindowDocument = HtmlUtil.GetIEWindowDocument(fromHandle);
            mshtml.IHTMLElement ele = HtmlUtil.FindElement(fromHandle, iEWindowDocument, x, y);
            bool flag = false;
            if ((ele != null) && (ele.tagName != null))
            {
                if (ele.tagName.ToLower().Equals("label"))
                {
                    if ((ele.getAttribute("htmlFor", 0) != null) && !string.IsNullOrEmpty(ele.getAttribute("htmlFor", 0).ToString()))
                    {
                        flag = true;
                    }
                }
                else if (ele.tagName.ToLower().Equals("input") && (ele.getAttribute("type", 0) != null))
                {
                    string str2 = ele.getAttribute("type", 0) as string;
                    if (str2.ToLower().Equals("radio"))
                    {
                        flag = true;
                    }
                }
            }
            else
            {
                ele = null;
            }
            if (flag)
            {
                if (ele.id != null)
                {
                    this.textBoxRadioID.Tag = ElementTag.ID;
                    itemName = ele.id;
                }
                else if ((ele.getAttribute("value", 0) != null) && (ele.getAttribute("value", 0).ToString() != ""))
                {
                    this.textBoxRadioID.Tag = ElementTag.value;
                    itemName = ele.getAttribute("value", 0) as string;
                }
                else if (!string.IsNullOrEmpty(ele.outerText))
                {
                    this.textBoxRadioID.Tag = ElementTag.outerText;
                    itemName = ele.outerText;
                }
                else
                {
                    this.textBoxRadioID.Tag = ElementTag.outerHTML;
                    itemName = ele.outerHTML;
                }
                this._radioIndex = HtmlUtil.GetRadioElementIndex(iEWindowDocument, ele, itemName, ((int) this.textBoxRadioID.Tag).ToString());
            }
            else
            {
                ele = null;
            }
            this.DrawCaptureBox(fromHandle, ele);
            this.textBoxRadioID.Text = itemName;
            if (iEWindowDocument != null)
            {
                Marshal.FinalReleaseComObject(iEWindowDocument);
            }
        }

        private void FindRadioMouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            this._findTimer.Tag = TaskCommand.Task_ClickRadio;
            this._findTimer.Enabled = true;
        }

        private void FindTextMouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            this._findTimer.Tag = TaskCommand.Task_InputText;
            this._findTimer.Enabled = true;
        }

        private void FindThreadFunc(object sender, EventArgs ea)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            Point point = new Point(x, y);
            bool flag = false;
            try
            {
                IntPtr hWnd = WindowUtil.WindowFromPoint(point);
                StringBuilder lpString = new StringBuilder(0x100);
                WindowUtil.GetClassName(hWnd, lpString, lpString.Capacity);
                if (lpString.ToString() == "Internet Explorer_Server")
                {
                    Timer timer = (Timer) sender;
                    switch (((TaskCommand) timer.Tag))
                    {
                        case TaskCommand.Task_InputText:
                            this.FindInput(hWnd, x, y);
                            goto Label_01A5;

                        case TaskCommand.Task_ClickButton:
                            this.FindButton(hWnd, x, y);
                            goto Label_01A5;

                        case TaskCommand.Task_ClickLink:
                            if (this.FindLink(hWnd, x, y, this.textBoxLink, ref this._linkIndex))
                            {
                                this.textBoxLinkKeyword.Text = "";
                            }
                            goto Label_01A5;

                        case TaskCommand.Task_FindLinkLinkPage1:
                        case TaskCommand.Task_FindLinkHrefPage1:
                        case TaskCommand.Task_FindHrefLinkPage1:
                        case TaskCommand.Task_FindHrefHrefPage1:
                        case TaskCommand.Task_FindLinkSrcPage1:
                        case TaskCommand.Task_FindHrefSrcPage1:
                        case TaskCommand.Task_FindSrcLinkPage1:
                        case TaskCommand.Task_FindSrcHrefPage1:
                        case TaskCommand.Task_FindSrcSrcPage1:
                            if (this.FindLink(hWnd, x, y, this.textBoxFindPage, ref this._findLinkIndex))
                            {
                                this.textBoxFindKeyword.Text = "";
                            }
                            goto Label_01A5;

                        case TaskCommand.Task_FindLinkLinkPage2:
                        case TaskCommand.Task_FindLinkHrefPage2:
                        case TaskCommand.Task_FindHrefLinkPage2:
                        case TaskCommand.Task_FindHrefHrefPage2:
                        case TaskCommand.Task_FindLinkSrcPage2:
                        case TaskCommand.Task_FindHrefSrcPage2:
                        case TaskCommand.Task_FindSrcLinkPage2:
                        case TaskCommand.Task_FindSrcHrefPage2:
                        case TaskCommand.Task_FindSrcSrcPage2:
                            this.FindLink(hWnd, x, y, this.textBoxFindPageNext, ref this._findNextIndex);
                            goto Label_01A5;

                        case TaskCommand.Task_ClickRadio:
                            this.FindRadio(hWnd, x, y);
                            goto Label_01A5;

                        case TaskCommand.Task_ClickChecked:
                            this.FindChecked(hWnd, x, y);
                            goto Label_01A5;
                    }
                    flag = true;
                }
            }
            catch (Exception)
            {
                flag = true;
            }
        Label_01A5:
            if (flag)
            {
                Timer timer2 = (Timer) sender;
                timer2.Enabled = false;
            }
        }

        private void FormEnter(object sender, EventArgs e)
        {
            this.InitSetting();
            this.ResetTemplateComponent();
            this._mainWnd = base.Handle;
            this.ConnectToServer();
        }

        private void FormMainClosing(object sender, FormClosingEventArgs e)
        {
            this.CloseBrowserProcess(true, 0);
            this.StopTaskThread();
            this.DisConnect();
            this.notifyIcon1.Visible = false;
        }

        public ArrayList GetAreasClick()
        {
            ArrayList list = new ArrayList();
            list.Add(this.checkBoxCustomHeBei.Checked);
            list.Add(this.checkBoxCustomSanXi.Checked);
            list.Add(this.checkBoxCustomLiaoNing.Checked);
            list.Add(this.checkBoxCustomJiLin.Checked);
            list.Add(this.checkBoxCustomHeiLongjiang.Checked);
            list.Add(this.checkBoxCustomJiangSu.Checked);
            list.Add(this.checkBoxCustomZheJiang.Checked);
            list.Add(this.checkBoxCustomAnHui.Checked);
            list.Add(this.checkBoxCustomFuJian.Checked);
            list.Add(this.checkBoxCustomJiangXi.Checked);
            list.Add(this.checkBoxCustomSanDong.Checked);
            list.Add(this.checkBoxCustomHeNan.Checked);
            list.Add(this.checkBoxCustomHuBei.Checked);
            list.Add(this.checkBoxCustomHuNan.Checked);
            list.Add(this.checkBoxCustomGuangDong.Checked);
            list.Add(this.checkBoxCustomHaiNan.Checked);
            list.Add(this.checkBoxCustomSiChuang.Checked);
            list.Add(this.checkBoxCustomGuiZhou.Checked);
            list.Add(this.checkBoxCustomYunNan.Checked);
            list.Add(this.checkBoxCustomShanXi.Checked);
            list.Add(this.checkBoxCustomGanSu.Checked);
            list.Add(this.checkBoxCustomQingHai.Checked);
            list.Add(this.checkBoxCustomTaiWan.Checked);
            list.Add(this.checkBoxCustomBeiJing.Checked);
            list.Add(this.checkBoxCustomTianJin.Checked);
            list.Add(this.checkBoxCustomShangHai.Checked);
            list.Add(this.checkBoxCustomChongQing.Checked);
            list.Add(this.checkBoxCustomNeiMengGu.Checked);
            list.Add(this.checkBoxCustomXiZang.Checked);
            list.Add(this.checkBoxCustomXinJiang.Checked);
            list.Add(this.checkBoxCustomNingXia.Checked);
            list.Add(this.checkBoxCustomGuangXi.Checked);
            list.Add(this.checkBoxCustomXiangGan.Checked);
            list.Add(this.checkBoxCustomAoMen.Checked);
            list.Add(this.checkBoxCustomOtherProvince.Checked);
            return list;
        }

        public ArrayList GetHoursClick()
        {
            ArrayList list = new ArrayList();
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock0.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock1.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock2.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock3.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock4.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock5.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock6.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock7.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock8.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock9.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock10.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock11.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock12.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock13.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock14.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock15.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock16.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock17.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock18.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock19.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock20.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock21.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock22.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock23.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock24.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock25.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock26.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock27.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock28.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock29.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock30.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock31.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock32.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock33.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock34.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock35.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock36.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock37.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock38.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock39.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock40.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock41.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock42.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock43.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock44.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock45.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock46.Text));
            list.Add(WindowUtil.StringToUint(this.textBoxCustomClock47.Text));
            return list;
        }

        public MyTask GetMyTask()
        {
            MyTask netTask = null;
            if (this._startNetTask && (this.NetTasks.Count > 0))
            {
                if (this._browserManager.GetEmptyBrowserWindow())
                {
                    netTask = this.GetNetTask();
                    if (netTask != null)
                    {
                        this.RunTask(netTask);
                    }
                    return netTask;
                }
                if (!this._browserManager.GetEmptyBrowserProcess() && this.CouldGetTask())
                {
                    this.StartBrowserProcess(false);
                }
                return netTask;
            }
            if (this._browserManager.GetEmptyBrowserWindow() || this.CouldGetTask())
            {
                this.SendNetTask();
            }
            return netTask;
        }

        public MyTask GetNetTask()
        {
            MyTask task = null;
            if (this._getTasks.Count > 0)
            {
                task = this._getTasks[0] as MyTask;
                this._getTasks.RemoveAt(0);
            }
            return task;
        }

        private uint GetRunTasksCount()
        {
            if (this._tasks == null)
            {
                return 0;
            }
            uint num = 0;
            foreach (MyTask task in this._tasks)
            {
                if (task._isRun)
                {
                    num++;
                }
            }
            return num;
        }

        public void GetTaskPacket(MyTask task, WorldPacket ms)
        {
            ms.InputInt(task._taskItems.Count);
            foreach (TaskInfo info in task._taskItems)
            {
                ms.InputString(info._param1, 0x20);
                ms.InputString(info._param2, 0x100);
                ms.InputString(info._param3, 0x100);
                ms.InputString(info._param4, 0x100);
                ms.InputString(info._param5, 0x100);
            }
            if ((task._HourClicks != null) && (task._HourClicks.Count == 0x30))
            {
                for (int i = 0; i < 0x30; i++)
                {
                    ms.InputUInt32((uint) task._HourClicks[i]);
                }
            }
            else
            {
                for (int j = 0; j < 0x30; j++)
                {
                    ms.InputUInt32(0);
                }
            }
            int[] numArray = new int[2];
            if ((task._AreaClicks != null) && (task._AreaClicks.Count == 0x23))
            {
                for (int k = 0; k < 0x1f; k++)
                {
                    numArray[0] |= ((bool) task._AreaClicks[k]) ? (((int) 1) << k) : 0;
                }
                for (int m = 0x1f; m < 0x23; m++)
                {
                    numArray[1] |= ((bool) task._AreaClicks[m]) ? (((int) 1) << (m - 0x1f)) : 0;
                }
            }
            ms.InputInt(numArray[0]);
            ms.InputInt(numArray[1]);
            ms.InputString(task.Note, 0x40);
        }

        public MyTask GetTestTask()
        {
            MyTask task = null;
            if (this._testTask != null)
            {
                if (this._browserManager.GetEmptyBrowserWindow())
                {
                    if (this._testTask != null)
                    {
                        task = this._testTask;
                        this._testTask = null;
                        this.RunTask(task);
                    }
                    return task;
                }
                if (!this._browserManager.GetEmptyBrowserProcess())
                {
                    this.StartBrowserProcess(true);
                }
            }
            return task;
        }

        private string GetUrlSessionStr()
        {
            string str = "";
            if (this.IsConnected() && (this.LoginUtil.AccountTime == 0))
            {
                string salt = client.LoginUtil.GenerateSalt();
                string str3 = client.LoginUtil.GenerateKey(this.LoginUtil.K, salt);
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "?sid=", this.LoginUtil.AccountID, "&k=", str3, "&s=", salt });
            }
            return str;
        }

        private void HandleAccountInfo(WorldPacket recvPacket)
        {
            if (recvPacket != null)
            {
                uint num = 0;
                uint validClick = 0;
                uint invalidClick = 0;
                uint taskID = 0;
                uint todayValidClick = 0;
                uint todayInvalidClick = 0;
                uint yesterdayValidClick = 0;
                uint yesterdayInvalidClick = 0;
                this._loginUtil.Score = recvPacket.ReadUInt32();
                this._loginUtil.DayConsumeScore = recvPacket.ReadUInt32();
                this._loginUtil.DayCompleteTask = recvPacket.ReadUInt32();
                this._loginUtil.DayNetTaskScore = recvPacket.ReadUInt32();
                this._loginUtil.DayNetTaskCount = recvPacket.ReadUInt32();
                this._loginUtil.ServerTasksCount = recvPacket.ReadUInt32();
                num = recvPacket.ReadUInt32();
                for (uint i = 0; i < num; i++)
                {
                    taskID = recvPacket.ReadUInt32();
                    validClick = recvPacket.ReadUInt32();
                    invalidClick = recvPacket.ReadUInt32();
                    todayValidClick = recvPacket.ReadUInt32();
                    todayInvalidClick = recvPacket.ReadUInt32();
                    yesterdayValidClick = recvPacket.ReadUInt32();
                    yesterdayInvalidClick = recvPacket.ReadUInt32();
                    this.SetValidClick(taskID, validClick, invalidClick, todayValidClick, todayInvalidClick, yesterdayValidClick, yesterdayInvalidClick);
                }
                this._loginUtil.PublishTotalScore = recvPacket.ReadUInt32();
                this._loginUtil.PublishTotalTask = recvPacket.ReadUInt32();
                this._loginUtil.GetTotalScore = recvPacket.ReadUInt32();
                this._loginUtil.GetTotalTask = recvPacket.ReadUInt32();
                this._loginUtil.VipRemainDays = recvPacket.ReadUInt32();
                this.UpdateAccount();
            }
        }

        private void HandleCheckAccount(WorldPacket recvPacket)
        {
            if (recvPacket != null)
            {
                int ret = recvPacket.ReadInt32();
                if (this.CreateAccountForm != null)
                {
                    this.CreateAccountForm.CheckAccount(ret);
                }
            }
            this.ClearPackets();
        }

        public void HandleCompleteTask(WorldPacket recvPacket)
        {
            if (recvPacket != null)
            {
                this.LoginUtil.Score = recvPacket.ReadUInt32();
                this.LoginUtil.DayNetTaskScore = recvPacket.ReadUInt32();
                this.LoginUtil.DayNetTaskCount = recvPacket.ReadUInt32();
                this.UpdateAccount();
            }
        }

        public void HandleEditAccount(WorldPacket recvPacket)
        {
            if (recvPacket == null)
            {
                return;
            }
            int num = recvPacket.ReadInt32();
            if (num == 0)
            {
                this.LoginUtil.Account = recvPacket.ReadString();
                this.LoginUtil.AccountID = recvPacket.ReadUInt32();
                int num2 = recvPacket.ReadInt32();
                this.LoginUtil.K = this.K;
                this.LoginUtil.AccountTime = 0;
                this.LoginUtil.SaveSetting();
                this.UpdateAccount();
                if (num2 != 0)
                {
                    this.ConnectToServer();
                    return;
                }
                return;
            }
            string s = "修改用户信息失败";
            switch (num)
            {
                case 11:
                    s = s + "用户名有误,请尝试换一个用户名？";
                    break;

                case 7:
                    s = s + "用户名或邮箱已经注册";
                    goto Label_00AB;

                case 10:
                    s = s + "推荐人用户名错误";
                    goto Label_00AB;
            }
        Label_00AB:
            this.ShowAccountTip(s);
        }

        private int HandleGateAuth(WorldPacket recvPacket)
        {
            if (recvPacket != null)
            {
                int code = recvPacket.ReadInt32();
                string str = string.Empty;
                int num2 = 0;
                if (code == 0)
                {
                    uint accountID = recvPacket.ReadUInt32();
                    uint accountTime = recvPacket.ReadUInt32();
                    int num5 = recvPacket.ReadInt32();
                    for (int i = 0; i < num5; i++)
                    {
                        str = recvPacket.ReadString();
                        num2 = recvPacket.ReadInt32();
                    }
                    this.IP = str;
                    this.Port = num2;
                    this.LoginUtil.LoginSuccess(accountID, this._gateSession.Account, accountTime, this.GateSession.GateSocket.K);
                    this.Init2();
                }
                else
                {
                    this.SetReLogin(code);
                    this.ClearPackets();
                }
            }
            return 1;
        }

        private int HandleGatePlayerReg(WorldPacket recvPacket)
        {
            if ((recvPacket != null) && (recvPacket.ReadInt32() == 0))
            {
                this.ResetLoginUser();
                this._gateSession.GateSocket.Account = this._gateSession.Account = this.LoginUtil.Account = recvPacket.ReadString();
                this._gateSession.GateSocket.AccountTime = this.LoginUtil.AccountTime = recvPacket.ReadUInt32();
                this._gateSession.GateSocket.K = this.LoginUtil.K = string.Empty;
                this._gateSession.GateSocket.SendGateAuth();
                this.ResetNetTime();
            }
            return 0;
        }

        private void HandleGatePlayerRegUser(WorldPacket recvPacket)
        {
            if (recvPacket == null)
            {
                return;
            }
            int num = recvPacket.ReadInt32();
            if (num == 0)
            {
                this.LoginUtil.Account = recvPacket.ReadString();
                this.LoginUtil.AccountID = recvPacket.ReadUInt32();
                recvPacket.ReadInt32();
                this.LoginUtil.K = this.K;
                this.LoginUtil.AccountTime = 0;
                this.LoginUtil.SaveSetting();
                this.ResetLoginUser();
                this.UpdateAccount();
                return;
            }
            string s = "修改用户信息失败";
            switch (num)
            {
                case 11:
                    s = s + "用户名有误,请尝试换一个用户名？";
                    break;

                case 7:
                    s = s + "用户名或邮箱已经注册";
                    goto Label_00A8;

                case 10:
                    s = s + "推荐人用户名错误";
                    goto Label_00A8;
            }
        Label_00A8:
            this.ShowAccountTip(s);
            this.ClearPackets();
        }

        public void HandleGetTask(WorldPacket recvPacket)
        {
            if (recvPacket != null)
            {
                MyTask task = new MyTask();
                if (recvPacket.ReadInt32() == 0)
                {
                    task._id = recvPacket.ReadUInt32();
                    uint num2 = recvPacket.ReadUInt32();
                    for (uint i = 0; i < num2; i++)
                    {
                        string str = recvPacket.ReadString();
                        string str2 = recvPacket.ReadString();
                        string str3 = recvPacket.ReadString();
                        string str4 = recvPacket.ReadString();
                        string str5 = recvPacket.ReadString();
                        TaskInfo info = new TaskInfo(str, str2, str3, str4, str5);
                        task._taskItems.Add(info);
                    }
                    this.addGetTask(task);
                }
            }
        }

        public void HandleMyTask(WorldPacket recvPacket)
        {
            if (recvPacket != null)
            {
                MyTask task = new MyTask {
                    _id = recvPacket.ReadUInt32(),
                    _publishTime = recvPacket.ReadUInt32(),
                    _isRun = recvPacket.ReadUInt32() == 1
                };
                uint num = recvPacket.ReadUInt32();
                for (uint i = 0; i < num; i++)
                {
                    string str = recvPacket.ReadString();
                    string str2 = recvPacket.ReadString();
                    string str3 = recvPacket.ReadString();
                    string str4 = recvPacket.ReadString();
                    string str5 = recvPacket.ReadString();
                    TaskInfo info = new TaskInfo(str, str2, str3, str4, str5);
                    task._taskItems.Add(info);
                }
                int num3 = recvPacket.ReadInt32();
                int num4 = recvPacket.ReadInt32();
                int num5 = 0;
                ArrayList areaClicks = new ArrayList();
                num5 = 0;
                while (num5 < 0x1f)
                {
                    areaClicks.Add((num3 & (((int) 1) << num5)) != 0);
                    num5++;
                }
                while (num5 < 0x23)
                {
                    areaClicks.Add((num4 & (((int) 1) << (num5 - 0x1f))) != 0);
                    num5++;
                }
                task.SetAreaClick(areaClicks);
                ArrayList hourClicks = new ArrayList();
                for (num5 = 0; num5 < 0x30; num5++)
                {
                    uint num6 = recvPacket.ReadUInt32();
                    hourClicks.Add(num6);
                }
                task.SetHourClick(hourClicks);
                task._validClickCount = recvPacket.ReadUInt32();
                task._invalidClickCount = recvPacket.ReadUInt32();
                task._todayValidClickCount = recvPacket.ReadUInt32();
                task._todayInvalidClickCount = recvPacket.ReadUInt32();
                task._yesterdayValidClickCount = recvPacket.ReadUInt32();
                task._yesterdayInvalidClickCount = recvPacket.ReadUInt32();
                task.Note = recvPacket.ReadString();
                this.AddTask(task);
                this.UpdateAccount();
            }
        }

        public void HandleRemoveTask(WorldPacket recvPacket)
        {
            if ((recvPacket != null) && (recvPacket.ReadInt32() == 0))
            {
                uint taskID = recvPacket.ReadUInt32();
                this.RemoveTask(taskID);
            }
        }

        public void HandleResumeTask(WorldPacket recvPacket)
        {
            if ((recvPacket != null) && (recvPacket.ReadInt32() == 0))
            {
                uint taskID = recvPacket.ReadUInt32();
                this.ResumeTask(taskID);
            }
        }

        public int HandleServerAuth(WorldPacket recvPacket)
        {
            int num = 1;
            this._serverSession.Authed = false;
            if (recvPacket != null)
            {
                int code = recvPacket.ReadInt32();
                if (code == 0)
                {
                    num = 0;
                    this.LoginUtil.Account = recvPacket.ReadString();
                    this._serverSession.Authed = true;
                    this.UpdateAccount();
                    return num;
                }
                this.SetReLogin(code);
                this.ClearPackets();
            }
            return num;
        }

        public void HandleServerCheckAccount(WorldPacket recvPacket)
        {
            if (recvPacket != null)
            {
                int ret = recvPacket.ReadInt32();
                if (this.CreateAccountForm != null)
                {
                    this.CreateAccountForm.CheckAccount(ret);
                }
            }
        }

        public void HandleStopTask(WorldPacket recvPacket)
        {
            if ((recvPacket != null) && (recvPacket.ReadInt32() == 0))
            {
                uint taskID = recvPacket.ReadUInt32();
                this.StopTasks(taskID);
            }
        }

        public void Init(bool sendLoginInfo = true)
        {
            this.DisConnect();
            this._gateSession.Init(this.LoginUtil.Account, this.LoginUtil.Password, this.LoginUtil.K, this.LoginUtil.AccountTime, this.SetIP);
            this._status = NetManagerStatus.NETMANAGER_STATUS_CONNECTING_GATE;
            if (sendLoginInfo && !this._isInitialisedMyTask)
            {
                this.SendGetLoginInfo(true);
            }
            this._serverSession.Authed = false;
            this.ResetNetTime();
        }

        public void Init2()
        {
            string setIP;
            if (!string.IsNullOrEmpty(this.SetIP))
            {
                setIP = this.SetIP;
            }
            else
            {
                setIP = this._ip;
            }
            this._serverSession.Init(setIP, this._port, this.LoginUtil.AccountID, this.LoginUtil.K, this.LoginUtil.AccountTime);
            this._status = NetManagerStatus.NETMANAGER_STATUS_CONNECTING_SERVER;
            foreach (PacketProcessInfo info in this._packets)
            {
                if (!info.IsProcessed())
                {
                    info.Reset();
                }
            }
            this.ResetNetTime();
        }

        private void InitEditTaskItem(int posindex)
        {
            if ((posindex < 0) || (posindex >= this._task._taskItems.Count))
            {
                return;
            }
            this.listBoxTaskItems.SelectedIndex = posindex;
            this._modifyTaskItemIndex = posindex;
            TaskInfo info = (TaskInfo) this._task._taskItems[posindex];
            TaskCommand command = (TaskCommand) WindowUtil.StringToInt(info._param1);
            this.SetTaskItem(command);
            switch (command)
            {
                case TaskCommand.Task_Wait:
                    this.textBoxWaitTime.Text = info._param2;
                    this.textBoxWaitTime.Focus();
                    this.OnlyShowPanel(4);
                    goto Label_06EB;

                case TaskCommand.Task_InputText:
                    this.textBoxInputBoxName.Text = info._param2;
                    this.textBoxInputText.Text = info._param3;
                    if (info._param4 != string.Empty)
                    {
                        this.textBoxInputBoxName.Tag = (ElementTag) WindowUtil.StringToInt(info._param4);
                    }
                    if (info._param5 != string.Empty)
                    {
                        this._inputIndex = WindowUtil.StringToInt(info._param5);
                    }
                    this.textBoxInputBoxName.Focus();
                    this.OnlyShowPanel(1);
                    goto Label_06EB;

                case TaskCommand.Task_ClickButton:
                    this.textBoxButtonID.Text = info._param2;
                    if (info._param3 != string.Empty)
                    {
                        this.textBoxButtonID.Tag = (ElementTag) WindowUtil.StringToInt(info._param3);
                    }
                    if (info._param4 != string.Empty)
                    {
                        this._buttonIndex = WindowUtil.StringToInt(info._param4);
                    }
                    this.textBoxButtonID.Focus();
                    this.OnlyShowPanel(2);
                    goto Label_06EB;

                case TaskCommand.Task_ClickLink:
                    this.textBoxLink.Text = info._param2;
                    if (!string.IsNullOrEmpty(info._param3))
                    {
                        this.textBoxLinkKeyword.Text = info._param3;
                    }
                    if (info._param4 != string.Empty)
                    {
                        this.textBoxLink.Tag = (ElementTag) WindowUtil.StringToInt(info._param4);
                    }
                    if (info._param5 != string.Empty)
                    {
                        this._linkIndex = WindowUtil.StringToInt(info._param5);
                    }
                    this.textBoxLink.Focus();
                    this.OnlyShowPanel(3);
                    goto Label_06EB;

                case TaskCommand.Task_Navigate:
                    this.textBoxWebSite.Text = info._param2;
                    this.textBoxWebSiteRefer.Text = info._param3;
                    this.textBoxWebSite.Focus();
                    this.OnlyShowPanel(0);
                    goto Label_06EB;

                case TaskCommand.Task_DeepClick:
                    this.textBoxDeepClickTimes.Text = info._param2;
                    this.textBoxDeepClickWaitTime.Text = info._param3;
                    this.textBoxDeepClickText.Text = info._param4;
                    this.textBoxDeepClickTimes.Focus();
                    this.OnlyShowPanel(5);
                    goto Label_06EB;

                case TaskCommand.Task_ClearCookie:
                    if (!(info._param2 == "1"))
                    {
                        if (info._param2 == "2")
                        {
                            this.radioButtonCookie.Checked = true;
                        }
                        else if (info._param2 == "8")
                        {
                            this.radioButtonTempFile.Checked = true;
                        }
                        else if (info._param2 == "48")
                        {
                            this.radioButtonPassword.Checked = true;
                        }
                        else
                        {
                            this.radioButtonAll.Checked = true;
                        }
                        break;
                    }
                    this.radioButtonHistory.Checked = true;
                    break;

                case TaskCommand.Task_FindLinkLinkPage1:
                case TaskCommand.Task_FindLinkHrefPage1:
                case TaskCommand.Task_FindHrefLinkPage1:
                case TaskCommand.Task_FindHrefHrefPage1:
                case TaskCommand.Task_FindLinkSrcPage1:
                case TaskCommand.Task_FindHrefSrcPage1:
                case TaskCommand.Task_FindSrcLinkPage1:
                case TaskCommand.Task_FindSrcHrefPage1:
                case TaskCommand.Task_FindSrcSrcPage1:
                    this.textBoxFindPage.Text = info._param2;
                    this.textBoxFindPageNext.Text = info._param3;
                    if (command != TaskCommand.Task_FindLinkLinkPage1)
                    {
                        switch (command)
                        {
                            case TaskCommand.Task_FindLinkHrefPage1:
                                this.textBoxFindPage.Tag = ElementTag.outerText;
                                this.textBoxFindPageNext.Tag = ElementTag.href;
                                goto Label_055A;

                            case TaskCommand.Task_FindHrefLinkPage1:
                                this.textBoxFindPage.Tag = ElementTag.href;
                                this.textBoxFindPageNext.Tag = ElementTag.outerText;
                                goto Label_055A;

                            case TaskCommand.Task_FindHrefHrefPage1:
                                this.textBoxFindPage.Tag = this.textBoxFindPageNext.Tag = ElementTag.href;
                                goto Label_055A;

                            case TaskCommand.Task_FindLinkSrcPage1:
                                this.textBoxFindPage.Tag = ElementTag.outerText;
                                this.textBoxFindPageNext.Tag = ElementTag.src;
                                goto Label_055A;

                            case TaskCommand.Task_FindHrefSrcPage1:
                                this.textBoxFindPage.Tag = ElementTag.href;
                                this.textBoxFindPageNext.Tag = ElementTag.src;
                                goto Label_055A;

                            case TaskCommand.Task_FindSrcLinkPage1:
                                this.textBoxFindPage.Tag = ElementTag.src;
                                this.textBoxFindPageNext.Tag = ElementTag.outerText;
                                goto Label_055A;

                            case TaskCommand.Task_FindSrcHrefPage1:
                                this.textBoxFindPage.Tag = ElementTag.src;
                                this.textBoxFindPageNext.Tag = ElementTag.href;
                                goto Label_055A;
                        }
                        this.textBoxFindPage.Tag = this.textBoxFindPageNext.Tag = ElementTag.src;
                    }
                    else
                    {
                        this.textBoxFindPage.Tag = this.textBoxFindPageNext.Tag = ElementTag.outerText;
                    }
                    goto Label_055A;

                case TaskCommand.Task_Fresh:
                    this.buttonInsertFresh.Focus();
                    this.OnlyShowPanel(8);
                    goto Label_06EB;

                case TaskCommand.Task_PressKey:
                    this.buttonInsertPressKey.Focus();
                    this.OnlyShowPanel(9);
                    goto Label_06EB;

                case TaskCommand.Task_ClickRadio:
                    this.textBoxRadioID.Text = info._param2;
                    if (!string.IsNullOrEmpty(info._param3))
                    {
                        this.textBoxRadioID.Tag = (ElementTag) WindowUtil.StringToInt(info._param3);
                    }
                    if (!string.IsNullOrEmpty(info._param4))
                    {
                        this._radioIndex = WindowUtil.StringToInt(info._param4);
                    }
                    this.textBoxRadioID.Focus();
                    this.OnlyShowPanel(10);
                    goto Label_06EB;

                case TaskCommand.Task_ClickChecked:
                    this.textBoxCheckedID.Text = info._param2;
                    if (!string.IsNullOrEmpty(info._param3))
                    {
                        this.textBoxCheckedID.Tag = (ElementTag) WindowUtil.StringToInt(info._param3);
                    }
                    if (!string.IsNullOrEmpty(info._param4))
                    {
                        this._checkedIndex = WindowUtil.StringToInt(info._param4);
                    }
                    this.textBoxCheckedID.Focus();
                    this.OnlyShowPanel(11);
                    goto Label_06EB;

                default:
                    goto Label_06EB;
            }
            this.buttonInsertClearCookie.Focus();
            this.OnlyShowPanel(6);
            goto Label_06EB;
        Label_055A:
            if (!string.IsNullOrEmpty(info._param5))
            {
                this.textBoxFindKeyword.Text = info._param5;
            }
            string[] strArray = info._param4.Split(new char[] { ',' });
            this.textBoxFindMaxPage.Text = strArray[0];
            if (strArray.Length >= 2)
            {
                this._findLinkIndex = WindowUtil.StringToInt(strArray[1]);
            }
            if (strArray.Length >= 3)
            {
                this._findNextIndex = WindowUtil.StringToInt(strArray[2]);
            }
            this.textBoxFindPage.Focus();
            this.OnlyShowPanel(7);
        Label_06EB:
            this.ShowPanels();
        }

        private void InitHtmlElementManager()
        {
            this._findTimer.Tick += new EventHandler(this.FindThreadFunc);
            this._findTimer.Interval = 100;
            this._findTimer.Enabled = false;
        }

        private void InitialisePanelSize()
        {
            PanelInfo info = new PanelInfo {
                _size = new Size(this.groupBoxNavigate.Width, this.groupBoxNavigate.Height),
                _show = false,
                _panel = this.groupBoxNavigate
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxInputText.Width, this.groupBoxInputText.Height),
                _show = false,
                _panel = this.groupBoxInputText
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxClickButton.Width, this.groupBoxClickButton.Height),
                _panel = this.groupBoxClickButton,
                _show = false
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxClickLink.Width, this.groupBoxClickLink.Height),
                _panel = this.groupBoxClickLink,
                _show = false
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxWait.Width, this.groupBoxWait.Height),
                _panel = this.groupBoxWait,
                _show = false
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxDeepClick.Width, this.groupBoxDeepClick.Height),
                _panel = this.groupBoxDeepClick,
                _show = false
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxClearCookie.Width, this.groupBoxClearCookie.Height),
                _panel = this.groupBoxClearCookie,
                _show = false
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxFindPage.Width, this.groupBoxFindPage.Height),
                _panel = this.groupBoxFindPage,
                _show = false
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxFresh.Width, this.groupBoxFresh.Height),
                _panel = this.groupBoxFresh,
                _show = false
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupPressKey.Width, this.groupPressKey.Height),
                _panel = this.groupPressKey,
                _show = false
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxClickRadio.Width, this.groupBoxClickRadio.Height),
                _panel = this.groupBoxClickRadio,
                _show = false
            };
            this._panels.Add(info);
            info = new PanelInfo {
                _size = new Size(this.groupBoxClickChecked.Width, this.groupBoxClickChecked.Height),
                _panel = this.groupBoxClickChecked,
                _show = false
            };
            this._panels.Add(info);
            for (int i = 0; i < this._panels.Count; i++)
            {
                info = (PanelInfo) this._panels[i];
            }
            base.Invalidate();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ManageForm));
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new ToolStripMenuItem();
            this.toolStripMenuItem1 = new ToolStripMenuItem();
            this.toolStripMenuItem2 = new ToolStripMenuItem();
            this.toolStripMenuItem3 = new ToolStripMenuItem();
            this.labelScore = new Label();
            this.buttonPay = new Button();
            this.tabControl1 = new TabControl();
            this.tabPageUser = new TabPage();
            this.webBrowser1 = new WebBrowser();
            this.groupBox6 = new GroupBox();
            this.comboBoxTaskBrowserCount = new ComboBox();
            this.label11 = new Label();
            this.labelDayCompleteNetTasksCount = new Label();
            this.labelServerTasksCount = new Label();
            this.labelGetTotalScore = new Label();
            this.labelGetTotalTask = new Label();
            this.labelDayNetTaskScore = new Label();
            this.groupBox3 = new GroupBox();
            this.labelPublishTotalScore = new Label();
            this.labelPublishTotalTask = new Label();
            this.labelRunTasksCount = new Label();
            this.labelTasksCount = new Label();
            this.labelDayCosumeScore = new Label();
            this.labelDayCompleteTask = new Label();
            this.groupBox4 = new GroupBox();
            this.label10 = new Label();
            this.labelAccountTip = new Label();
            this.buttonReLogin = new Button();
            this.buttonEditAccount = new Button();
            this.buttonChangeUser = new Button();
            this.buttonBuyVIP = new Button();
            this.labelUserName = new Label();
            this.tabPageCommonTemplate = new TabPage();
            this.splitContainer2 = new SplitContainer();
            this.listBoxTemplateCategory = new ListBox();
            this.dataGridViewTemplate = new DataGridView();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column12 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.tabPageCustonTemplate = new TabPage();
            this.tabControl4 = new TabControl();
            this.tabPage6 = new TabPage();
            this.panel6 = new Panel();
            this.panel8 = new Panel();
            this.splitContainer1 = new SplitContainer();
            this.listBoxTaskItemLine = new ListBox();
            this.listBoxTaskItems = new ListBox();
            this.panel7 = new Panel();
            this.panel3 = new Panel();
            this.groupPressKey = new GroupBox();
            this.buttonInsertPressKey = new Button();
            this.buttonPressKey = new Button();
            this.groupBoxFresh = new GroupBox();
            this.buttonInsertFresh = new Button();
            this.buttonFresh = new Button();
            this.groupBoxWait = new GroupBox();
            this.label1 = new Label();
            this.buttonWait = new Button();
            this.textBoxWaitTime = new TextBox();
            this.buttonInsertWait = new Button();
            this.groupBoxDeepClick = new GroupBox();
            this.textBoxDeepClickText = new TextBox();
            this.label62 = new Label();
            this.buttonInsertDeepClick = new Button();
            this.label25 = new Label();
            this.label24 = new Label();
            this.label23 = new Label();
            this.textBoxDeepClickWaitTime = new TextBox();
            this.label22 = new Label();
            this.textBoxDeepClickTimes = new TextBox();
            this.button3 = new Button();
            this.groupBoxFindPage = new GroupBox();
            this.textBoxFindKeyword = new TextBox();
            this.label65 = new Label();
            this.textBoxFindMaxPage = new TextBox();
            this.label61 = new Label();
            this.button2 = new Button();
            this.buttonFindPage = new Button();
            this.textBoxFindPageNext = new TextBox();
            this.label60 = new Label();
            this.textBoxFindPage = new TextBox();
            this.label64 = new Label();
            this.buttonInsertFindPage = new Button();
            this.button1 = new Button();
            this.groupBoxClickLink = new GroupBox();
            this.buttonInsertClickLink = new Button();
            this.textBoxLinkKeyword = new TextBox();
            this.textBoxLink = new TextBox();
            this.buttonClickLink = new Button();
            this.buttonFindLink = new Button();
            this.label66 = new Label();
            this.groupBoxClickChecked = new GroupBox();
            this.buttonInsertClickChecked = new Button();
            this.textBoxCheckedID = new TextBox();
            this.buttonFindChecked = new Button();
            this.buttonClickChecked = new Button();
            this.groupBoxClickRadio = new GroupBox();
            this.buttonInsertClickRadio = new Button();
            this.textBoxRadioID = new TextBox();
            this.buttonFindRadio = new Button();
            this.buttonClickRadio = new Button();
            this.groupBoxClickButton = new GroupBox();
            this.buttonInsertClickButton = new Button();
            this.textBoxButtonID = new TextBox();
            this.buttonFindButton = new Button();
            this.buttonClickButton = new Button();
            this.groupBoxInputText = new GroupBox();
            this.buttonInsertInputText = new Button();
            this.textBoxInputText = new TextBox();
            this.buttonEnterInputText = new Button();
            this.textBoxInputBoxName = new TextBox();
            this.buttonFindInputText = new Button();
            this.groupBoxNavigate = new GroupBox();
            this.label2 = new Label();
            this.buttonInsertWebSite = new Button();
            this.textBoxWebSiteRefer = new TextBox();
            this.textBoxWebSite = new TextBox();
            this.buttonEnterWebSite = new Button();
            this.groupBoxClearCookie = new GroupBox();
            this.radioButtonPassword = new RadioButton();
            this.radioButtonTempFile = new RadioButton();
            this.radioButtonAll = new RadioButton();
            this.radioButtonCookie = new RadioButton();
            this.radioButtonHistory = new RadioButton();
            this.buttonClearCookie = new Button();
            this.buttonInsertClearCookie = new Button();
            this.tabPage9 = new TabPage();
            this.tabControl3 = new TabControl();
            this.tabPage3 = new TabPage();
            this.groupBox8 = new GroupBox();
            this.checkBoxMoveMouse = new CheckBox();
            this.checkBoxAllowScrollWhileWait = new CheckBox();
            this.checkBoxAllowScrollWhileLoad = new CheckBox();
            this.label69 = new Label();
            this.textBoxFindWaitTime2 = new TextBox();
            this.label70 = new Label();
            this.label33 = new Label();
            this.textBoxDocWaitTime2 = new TextBox();
            this.label68 = new Label();
            this.label30 = new Label();
            this.textBoxClickIntervalTime = new TextBox();
            this.label31 = new Label();
            this.groupBox7 = new GroupBox();
            this.label71 = new Label();
            this.textBoxTaskIntervalTimeMin = new TextBox();
            this.label28 = new Label();
            this.textBoxClickPerPC = new TextBox();
            this.label29 = new Label();
            this.label16 = new Label();
            this.textBoxClickPerIp = new TextBox();
            this.label15 = new Label();
            this.label21 = new Label();
            this.textBoxTaskIntervalTimeMax = new TextBox();
            this.label72 = new Label();
            this.tabPage7 = new TabPage();
            this.textBoxCustomClock47 = new TextBox();
            this.textBoxCustomClock39 = new TextBox();
            this.label88 = new Label();
            this.label89 = new Label();
            this.textBoxCustomClock46 = new TextBox();
            this.textBoxCustomClock38 = new TextBox();
            this.label90 = new Label();
            this.label91 = new Label();
            this.textBoxCustomClock45 = new TextBox();
            this.textBoxCustomClock37 = new TextBox();
            this.textBoxCustomClock44 = new TextBox();
            this.textBoxCustomClock36 = new TextBox();
            this.textBoxCustomClock43 = new TextBox();
            this.textBoxCustomClock35 = new TextBox();
            this.textBoxCustomClock42 = new TextBox();
            this.textBoxCustomClock34 = new TextBox();
            this.textBoxCustomClock41 = new TextBox();
            this.textBoxCustomClock33 = new TextBox();
            this.textBoxCustomClock40 = new TextBox();
            this.textBoxCustomClock32 = new TextBox();
            this.label92 = new Label();
            this.label93 = new Label();
            this.label94 = new Label();
            this.label95 = new Label();
            this.label96 = new Label();
            this.label97 = new Label();
            this.label98 = new Label();
            this.label99 = new Label();
            this.label100 = new Label();
            this.label101 = new Label();
            this.label102 = new Label();
            this.label103 = new Label();
            this.textBoxCustomClock31 = new TextBox();
            this.textBoxCustomClock23 = new TextBox();
            this.textBoxCustomClock15 = new TextBox();
            this.textBoxCustomClock7 = new TextBox();
            this.label104 = new Label();
            this.label105 = new Label();
            this.label106 = new Label();
            this.label107 = new Label();
            this.textBoxCustomClock30 = new TextBox();
            this.textBoxCustomClock22 = new TextBox();
            this.textBoxCustomClock14 = new TextBox();
            this.textBoxCustomClock6 = new TextBox();
            this.label108 = new Label();
            this.label109 = new Label();
            this.label110 = new Label();
            this.label111 = new Label();
            this.textBoxCustomClock29 = new TextBox();
            this.textBoxCustomClock21 = new TextBox();
            this.textBoxCustomClock13 = new TextBox();
            this.textBoxCustomClock5 = new TextBox();
            this.textBoxCustomClock28 = new TextBox();
            this.textBoxCustomClock20 = new TextBox();
            this.textBoxCustomClock12 = new TextBox();
            this.textBoxCustomClock4 = new TextBox();
            this.textBoxCustomClock27 = new TextBox();
            this.textBoxCustomClock19 = new TextBox();
            this.textBoxCustomClock11 = new TextBox();
            this.textBoxCustomClock3 = new TextBox();
            this.textBoxCustomClock26 = new TextBox();
            this.textBoxCustomClock18 = new TextBox();
            this.textBoxCustomClock10 = new TextBox();
            this.textBoxCustomClock2 = new TextBox();
            this.textBoxCustomClock25 = new TextBox();
            this.textBoxCustomClock17 = new TextBox();
            this.textBoxCustomClock9 = new TextBox();
            this.textBoxCustomClock1 = new TextBox();
            this.textBoxCustomClock24 = new TextBox();
            this.textBoxCustomClock16 = new TextBox();
            this.textBoxCustomClock8 = new TextBox();
            this.textBoxCustomClock0 = new TextBox();
            this.label125 = new Label();
            this.label126 = new Label();
            this.label127 = new Label();
            this.label128 = new Label();
            this.label129 = new Label();
            this.label130 = new Label();
            this.label131 = new Label();
            this.label132 = new Label();
            this.label133 = new Label();
            this.label134 = new Label();
            this.label135 = new Label();
            this.label136 = new Label();
            this.label137 = new Label();
            this.label138 = new Label();
            this.label139 = new Label();
            this.label140 = new Label();
            this.label141 = new Label();
            this.label142 = new Label();
            this.label143 = new Label();
            this.label144 = new Label();
            this.label145 = new Label();
            this.label146 = new Label();
            this.label147 = new Label();
            this.label148 = new Label();
            this.textBoxCustomClickPerDay = new TextBox();
            this.buttonCustomSystemDistribute = new Button();
            this.tabPage8 = new TabPage();
            this.checkBoxCustomOtherProvince = new CheckBox();
            this.checkBoxCustomAoMen = new CheckBox();
            this.checkBoxCustomXiangGan = new CheckBox();
            this.checkBoxCustomGuangXi = new CheckBox();
            this.checkBoxCustomNingXia = new CheckBox();
            this.checkBoxCustomXinJiang = new CheckBox();
            this.checkBoxCustomTianJin = new CheckBox();
            this.checkBoxCustomXiZang = new CheckBox();
            this.checkBoxCustomBeiJing = new CheckBox();
            this.checkBoxCustomNeiMengGu = new CheckBox();
            this.checkBoxCustomChongQing = new CheckBox();
            this.checkBoxCustomTaiWan = new CheckBox();
            this.checkBoxCustomShangHai = new CheckBox();
            this.checkBoxCustomQingHai = new CheckBox();
            this.checkBoxCustomGanSu = new CheckBox();
            this.checkBoxCustomShanXi = new CheckBox();
            this.checkBoxCustomGuangDong = new CheckBox();
            this.checkBoxCustomYunNan = new CheckBox();
            this.checkBoxCustomHuNan = new CheckBox();
            this.checkBoxCustomGuiZhou = new CheckBox();
            this.checkBoxCustomSiChuang = new CheckBox();
            this.checkBoxCustomHuBei = new CheckBox();
            this.checkBoxCustomHaiNan = new CheckBox();
            this.checkBoxCustomHeNan = new CheckBox();
            this.checkBoxCustomSanDong = new CheckBox();
            this.checkBoxCustomJiangXi = new CheckBox();
            this.checkBoxCustomHeiLongjiang = new CheckBox();
            this.checkBoxCustomFuJian = new CheckBox();
            this.checkBoxCustomJiLin = new CheckBox();
            this.checkBoxCustomAnHui = new CheckBox();
            this.checkBoxCustomZheJiang = new CheckBox();
            this.checkBoxCustomLiaoNing = new CheckBox();
            this.checkBoxCustomJiangSu = new CheckBox();
            this.checkBoxCustomSanXi = new CheckBox();
            this.checkBoxCustomHeBei = new CheckBox();
            this.buttonCustomAreaSelectNone = new Button();
            this.buttonCustomAreaSelectAll = new Button();
            this.panel5 = new Panel();
            this.textBoxTaskNote = new TextBox();
            this.label9 = new Label();
            this.labelCustomTotalScore = new Label();
            this.buttonCustomTemplateSaveTask = new Button();
            this.buttonCustonTemplateAdd = new Button();
            this.buttonCustonTemplateTest = new Button();
            this.buttonClearTaskItems = new Button();
            this.tabPageTaskManage = new TabPage();
            this.dataGridViewTasks = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column13 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column14 = new DataGridViewTextBoxColumn();
            this.Column16 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.toolStrip1 = new ToolStrip();
            this.toolStripButtonStartTask = new ToolStripButton();
            this.toolStripButtonOpenBrowser = new ToolStripButton();
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.label3 = new Label();
            this.linkLabel1 = new LinkLabel();
            this.notifyIcon1 = new NotifyIcon(this.components);
            this.contextMenuStrip2 = new ContextMenuStrip(this.components);
            this.ToolStripMenuItem5 = new ToolStripMenuItem();
            this.ToolStripMenuItem6 = new ToolStripMenuItem();
            this.ToolStripMenuItem7 = new ToolStripMenuItem();
            this.ToolStripMenuItem8 = new ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageUser.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPageCommonTemplate.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((ISupportInitialize) this.dataGridViewTemplate).BeginInit();
            this.tabPageCustonTemplate.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupPressKey.SuspendLayout();
            this.groupBoxFresh.SuspendLayout();
            this.groupBoxWait.SuspendLayout();
            this.groupBoxDeepClick.SuspendLayout();
            this.groupBoxFindPage.SuspendLayout();
            this.groupBoxClickLink.SuspendLayout();
            this.groupBoxClickChecked.SuspendLayout();
            this.groupBoxClickRadio.SuspendLayout();
            this.groupBoxClickButton.SuspendLayout();
            this.groupBoxInputText.SuspendLayout();
            this.groupBoxNavigate.SuspendLayout();
            this.groupBoxClearCookie.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPageTaskManage.SuspendLayout();
            ((ISupportInitialize) this.dataGridViewTasks).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            base.SuspendLayout();
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripMenuItem4, this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3 });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x95, 0x5c);
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new Size(0x94, 0x16);
            this.toolStripMenuItem4.Text = "指令编辑";
            this.toolStripMenuItem4.Click += new EventHandler(this.TaskItemEdit);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(0x94, 0x16);
            this.toolStripMenuItem1.Text = "指令删除";
            this.toolStripMenuItem1.Click += new EventHandler(this.TaskItemDelete);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(0x94, 0x16);
            this.toolStripMenuItem2.Text = "指令上移一行";
            this.toolStripMenuItem2.Click += new EventHandler(this.TaskItemMoveUp);
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new Size(0x94, 0x16);
            this.toolStripMenuItem3.Text = "指令下移一行";
            this.toolStripMenuItem3.Click += new EventHandler(this.TaskItemMoveDown);
            this.labelScore.AutoSize = true;
            this.labelScore.Location = new Point(0x42, 0x30);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new Size(0x41, 12);
            this.labelScore.TabIndex = 7;
            this.labelScore.Text = "当前金币：";
            this.buttonPay.Location = new Point(0x11e, 0x2b);
            this.buttonPay.Name = "buttonPay";
            this.buttonPay.Size = new Size(0x4b, 0x17);
            this.buttonPay.TabIndex = 8;
            this.buttonPay.Text = "金币充值";
            this.buttonPay.UseVisualStyleBackColor = true;
            this.buttonPay.Click += new EventHandler(this.buttonPay_Click);
            this.tabControl1.Controls.Add(this.tabPageUser);
            this.tabControl1.Controls.Add(this.tabPageCommonTemplate);
            this.tabControl1.Controls.Add(this.tabPageCustonTemplate);
            this.tabControl1.Controls.Add(this.tabPageTaskManage);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.ItemSize = new Size(200, 30);
            this.tabControl1.Location = new Point(5, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new Point(6, 6);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x32c, 0x202);
            this.tabControl1.TabIndex = 9;
            this.tabPageUser.Controls.Add(this.webBrowser1);
            this.tabPageUser.Controls.Add(this.groupBox6);
            this.tabPageUser.Controls.Add(this.groupBox3);
            this.tabPageUser.Controls.Add(this.groupBox4);
            this.tabPageUser.Location = new Point(4, 0x22);
            this.tabPageUser.Name = "tabPageUser";
            this.tabPageUser.Padding = new Padding(10);
            this.tabPageUser.Size = new Size(0x324, 0x1dc);
            this.tabPageUser.TabIndex = 4;
            this.tabPageUser.Text = "用户中心";
            this.tabPageUser.UseVisualStyleBackColor = true;
            this.webBrowser1.Dock = DockStyle.Fill;
            this.webBrowser1.Location = new Point(10, 0x12e);
            this.webBrowser1.MinimumSize = new Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new Size(0x310, 0xa4);
            this.webBrowser1.TabIndex = 20;
            this.webBrowser1.Url = new Uri("http://www.djjlseo.com/public/softpage", UriKind.Absolute);
            this.groupBox6.Controls.Add(this.comboBoxTaskBrowserCount);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.labelDayCompleteNetTasksCount);
            this.groupBox6.Controls.Add(this.labelServerTasksCount);
            this.groupBox6.Controls.Add(this.labelGetTotalScore);
            this.groupBox6.Controls.Add(this.labelGetTotalTask);
            this.groupBox6.Controls.Add(this.labelDayNetTaskScore);
            this.groupBox6.Dock = DockStyle.Top;
            this.groupBox6.Location = new Point(10, 0xc3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x310, 0x6b);
            this.groupBox6.TabIndex = 0x13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "挂机任务";
            this.comboBoxTaskBrowserCount.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxTaskBrowserCount.FormattingEnabled = true;
            this.comboBoxTaskBrowserCount.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            this.comboBoxTaskBrowserCount.Location = new Point(0x89, 0x4a);
            this.comboBoxTaskBrowserCount.Name = "comboBoxTaskBrowserCount";
            this.comboBoxTaskBrowserCount.Size = new Size(0x38, 20);
            this.comboBoxTaskBrowserCount.TabIndex = 14;
            this.comboBoxTaskBrowserCount.SelectedIndexChanged += new EventHandler(this.comboBoxTaskBrowserCount_SelectedIndexChanged);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x36, 0x4d);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x4d, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "挂机进程数：";
            this.labelDayCompleteNetTasksCount.AutoSize = true;
            this.labelDayCompleteNetTasksCount.Location = new Point(0x117, 0x33);
            this.labelDayCompleteNetTasksCount.Name = "labelDayCompleteNetTasksCount";
            this.labelDayCompleteNetTasksCount.Size = new Size(0x65, 12);
            this.labelDayCompleteNetTasksCount.TabIndex = 10;
            this.labelDayCompleteNetTasksCount.Text = "今日挂机任务数：";
            this.labelServerTasksCount.AutoSize = true;
            this.labelServerTasksCount.Location = new Point(0x204, 0x1b);
            this.labelServerTasksCount.Name = "labelServerTasksCount";
            this.labelServerTasksCount.Size = new Size(0x65, 12);
            this.labelServerTasksCount.TabIndex = 10;
            this.labelServerTasksCount.Text = "服务器任务总数：";
            this.labelGetTotalScore.AutoSize = true;
            this.labelGetTotalScore.Location = new Point(0x117, 0x1b);
            this.labelGetTotalScore.Name = "labelGetTotalScore";
            this.labelGetTotalScore.Size = new Size(0x65, 12);
            this.labelGetTotalScore.TabIndex = 10;
            this.labelGetTotalScore.Text = "挂机获得总金币：";
            this.labelGetTotalTask.AutoSize = true;
            this.labelGetTotalTask.Location = new Point(0x2a, 0x1b);
            this.labelGetTotalTask.Name = "labelGetTotalTask";
            this.labelGetTotalTask.Size = new Size(0x59, 12);
            this.labelGetTotalTask.TabIndex = 10;
            this.labelGetTotalTask.Text = "挂机总任务数：";
            this.labelDayNetTaskScore.AutoSize = true;
            this.labelDayNetTaskScore.Location = new Point(0x12, 0x33);
            this.labelDayNetTaskScore.Name = "labelDayNetTaskScore";
            this.labelDayNetTaskScore.Size = new Size(0x71, 12);
            this.labelDayNetTaskScore.TabIndex = 10;
            this.labelDayNetTaskScore.Text = "今日挂机获得金币：";
            this.groupBox3.Controls.Add(this.labelPublishTotalScore);
            this.groupBox3.Controls.Add(this.labelPublishTotalTask);
            this.groupBox3.Controls.Add(this.labelRunTasksCount);
            this.groupBox3.Controls.Add(this.labelTasksCount);
            this.groupBox3.Controls.Add(this.labelDayCosumeScore);
            this.groupBox3.Controls.Add(this.labelDayCompleteTask);
            this.groupBox3.Dock = DockStyle.Top;
            this.groupBox3.Location = new Point(10, 0x6f);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x310, 0x54);
            this.groupBox3.TabIndex = 0x12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "我的任务";
            this.labelPublishTotalScore.AutoSize = true;
            this.labelPublishTotalScore.Location = new Point(0x204, 0x33);
            this.labelPublishTotalScore.Name = "labelPublishTotalScore";
            this.labelPublishTotalScore.Size = new Size(0x65, 12);
            this.labelPublishTotalScore.TabIndex = 9;
            this.labelPublishTotalScore.Text = "任务总消耗积分：";
            this.labelPublishTotalTask.AutoSize = true;
            this.labelPublishTotalTask.Location = new Point(0x210, 0x1b);
            this.labelPublishTotalTask.Name = "labelPublishTotalTask";
            this.labelPublishTotalTask.Size = new Size(0x59, 12);
            this.labelPublishTotalTask.TabIndex = 9;
            this.labelPublishTotalTask.Text = "发布总任务数：";
            this.labelRunTasksCount.AutoSize = true;
            this.labelRunTasksCount.Location = new Point(0x117, 0x1b);
            this.labelRunTasksCount.Name = "labelRunTasksCount";
            this.labelRunTasksCount.Size = new Size(0x65, 12);
            this.labelRunTasksCount.TabIndex = 9;
            this.labelRunTasksCount.Text = "正在运行任务数：";
            this.labelTasksCount.AutoSize = true;
            this.labelTasksCount.Location = new Point(0x36, 0x1b);
            this.labelTasksCount.Name = "labelTasksCount";
            this.labelTasksCount.Size = new Size(0x4d, 12);
            this.labelTasksCount.TabIndex = 9;
            this.labelTasksCount.Text = "当前任务数：";
            this.labelDayCosumeScore.AutoSize = true;
            this.labelDayCosumeScore.Location = new Point(0x10b, 0x33);
            this.labelDayCosumeScore.Name = "labelDayCosumeScore";
            this.labelDayCosumeScore.Size = new Size(0x71, 12);
            this.labelDayCosumeScore.TabIndex = 10;
            this.labelDayCosumeScore.Text = "今日任务消耗金币：";
            this.labelDayCompleteTask.AutoSize = true;
            this.labelDayCompleteTask.Location = new Point(0x2a, 0x33);
            this.labelDayCompleteTask.Name = "labelDayCompleteTask";
            this.labelDayCompleteTask.Size = new Size(0x59, 12);
            this.labelDayCompleteTask.TabIndex = 10;
            this.labelDayCompleteTask.Text = "今日完成任务：";
            this.groupBox4.BackColor = Color.Transparent;
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.labelAccountTip);
            this.groupBox4.Controls.Add(this.buttonReLogin);
            this.groupBox4.Controls.Add(this.buttonEditAccount);
            this.groupBox4.Controls.Add(this.buttonChangeUser);
            this.groupBox4.Controls.Add(this.buttonBuyVIP);
            this.groupBox4.Controls.Add(this.buttonPay);
            this.groupBox4.Controls.Add(this.labelUserName);
            this.groupBox4.Controls.Add(this.labelScore);
            this.groupBox4.Dock = DockStyle.Top;
            this.groupBox4.Location = new Point(10, 10);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x310, 0x65);
            this.groupBox4.TabIndex = 0x11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "个人信息";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x2a, 0x4c);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x12b, 12);
            this.label10.TabIndex = 0x13;
            this.label10.Text = "(金币用来发布任务，金币由挂机获得，或者充值获得）";
            this.labelAccountTip.BackColor = Color.Transparent;
            this.labelAccountTip.ForeColor = Color.Black;
            this.labelAccountTip.Location = new Point(0x1ca, 0x18);
            this.labelAccountTip.Name = "labelAccountTip";
            this.labelAccountTip.Size = new Size(0x131, 0x40);
            this.labelAccountTip.TabIndex = 0x12;
            this.buttonReLogin.Location = new Point(0xcd, 0x12);
            this.buttonReLogin.Name = "buttonReLogin";
            this.buttonReLogin.Size = new Size(0x4b, 0x17);
            this.buttonReLogin.TabIndex = 0x11;
            this.buttonReLogin.Text = "重新登录";
            this.buttonReLogin.UseVisualStyleBackColor = true;
            this.buttonReLogin.Click += new EventHandler(this.buttonReLogin_Click);
            this.buttonEditAccount.Location = new Point(0x16f, 0x12);
            this.buttonEditAccount.Name = "buttonEditAccount";
            this.buttonEditAccount.Size = new Size(0x4b, 0x17);
            this.buttonEditAccount.TabIndex = 0x11;
            this.buttonEditAccount.Text = "注册";
            this.buttonEditAccount.UseVisualStyleBackColor = true;
            this.buttonEditAccount.Click += new EventHandler(this.buttonEditAccount_Click);
            this.buttonChangeUser.Location = new Point(0x11e, 0x12);
            this.buttonChangeUser.Name = "buttonChangeUser";
            this.buttonChangeUser.Size = new Size(0x4b, 0x17);
            this.buttonChangeUser.TabIndex = 0x11;
            this.buttonChangeUser.Text = "登录";
            this.buttonChangeUser.UseVisualStyleBackColor = true;
            this.buttonChangeUser.Click += new EventHandler(this.buttonChangeUser_Click);
            this.buttonBuyVIP.Location = new Point(0x16f, 0x2b);
            this.buttonBuyVIP.Name = "buttonBuyVIP";
            this.buttonBuyVIP.Size = new Size(0x4b, 0x17);
            this.buttonBuyVIP.TabIndex = 8;
            this.buttonBuyVIP.Text = "购买VIP";
            this.buttonBuyVIP.UseVisualStyleBackColor = true;
            this.buttonBuyVIP.Click += new EventHandler(this.buttonBuyVIP_Click);
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new Point(0x2c, 0x18);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new Size(0x59, 12);
            this.labelUserName.TabIndex = 7;
            this.labelUserName.Text = "正在连接服务器";
            this.tabPageCommonTemplate.Controls.Add(this.splitContainer2);
            this.tabPageCommonTemplate.Location = new Point(4, 0x22);
            this.tabPageCommonTemplate.Name = "tabPageCommonTemplate";
            this.tabPageCommonTemplate.Padding = new Padding(5);
            this.tabPageCommonTemplate.Size = new Size(0x324, 0x1dc);
            this.tabPageCommonTemplate.TabIndex = 0;
            this.tabPageCommonTemplate.Text = "通用模板";
            this.tabPageCommonTemplate.UseVisualStyleBackColor = true;
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.Location = new Point(5, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Panel1.Controls.Add(this.listBoxTemplateCategory);
            this.splitContainer2.Panel2.Controls.Add(this.dataGridViewTemplate);
            this.splitContainer2.Size = new Size(0x31a, 0x1d2);
            this.splitContainer2.SplitterDistance = 0xcd;
            this.splitContainer2.TabIndex = 1;
            this.listBoxTemplateCategory.Dock = DockStyle.Fill;
            this.listBoxTemplateCategory.DrawMode = DrawMode.OwnerDrawVariable;
            this.listBoxTemplateCategory.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listBoxTemplateCategory.ForeColor = SystemColors.WindowText;
            this.listBoxTemplateCategory.FormattingEnabled = true;
            this.listBoxTemplateCategory.HorizontalScrollbar = true;
            this.listBoxTemplateCategory.ItemHeight = 12;
            this.listBoxTemplateCategory.Location = new Point(0, 0);
            this.listBoxTemplateCategory.Name = "listBoxTemplateCategory";
            this.listBoxTemplateCategory.Size = new Size(0xcd, 0x1d2);
            this.listBoxTemplateCategory.TabIndex = 0;
            this.listBoxTemplateCategory.MouseClick += new MouseEventHandler(this.ListBoxTemplateCategoryClick);
            this.listBoxTemplateCategory.DrawItem += new DrawItemEventHandler(this.listBoxTemplateCategory_DrawItem);
            this.listBoxTemplateCategory.MeasureItem += new MeasureItemEventHandler(this.listBoxTemplateCategory_MeasureItem);
            this.dataGridViewTemplate.AllowUserToAddRows = false;
            this.dataGridViewTemplate.AllowUserToDeleteRows = false;
            this.dataGridViewTemplate.AllowUserToResizeColumns = false;
            this.dataGridViewTemplate.AllowUserToResizeRows = false;
            this.dataGridViewTemplate.CellBorderStyle = DataGridViewCellBorderStyle.None;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style.WrapMode = DataGridViewTriState.True;
            this.dataGridViewTemplate.ColumnHeadersDefaultCellStyle = style;
            this.dataGridViewTemplate.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTemplate.Columns.AddRange(new DataGridViewColumn[] { this.Column8, this.Column12, this.Column9 });
            this.dataGridViewTemplate.Dock = DockStyle.Fill;
            this.dataGridViewTemplate.Location = new Point(0, 0);
            this.dataGridViewTemplate.MultiSelect = false;
            this.dataGridViewTemplate.Name = "dataGridViewTemplate";
            this.dataGridViewTemplate.ReadOnly = true;
            style2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style2.WrapMode = DataGridViewTriState.True;
            this.dataGridViewTemplate.RowHeadersDefaultCellStyle = style2;
            this.dataGridViewTemplate.RowHeadersVisible = false;
            this.dataGridViewTemplate.RowTemplate.Height = 0x17;
            this.dataGridViewTemplate.ScrollBars = ScrollBars.Horizontal;
            this.dataGridViewTemplate.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTemplate.Size = new Size(0x249, 0x1d2);
            this.dataGridViewTemplate.TabIndex = 1;
            this.dataGridViewTemplate.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridViewTemplate_CellDoubleClick);
            style3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Column8.DefaultCellStyle = style3;
            this.Column8.HeaderText = "模板";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = DataGridViewTriState.False;
            this.Column8.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 120;
            this.Column12.HeaderText = "金币";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = DataGridViewTriState.False;
            this.Column12.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column12.Width = 80;
            this.Column9.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            style4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Column9.DefaultCellStyle = style4;
            this.Column9.HeaderText = "描述";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = DataGridViewTriState.False;
            this.Column9.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.tabPageCustonTemplate.Controls.Add(this.tabControl4);
            this.tabPageCustonTemplate.Controls.Add(this.panel5);
            this.tabPageCustonTemplate.Location = new Point(4, 0x22);
            this.tabPageCustonTemplate.Name = "tabPageCustonTemplate";
            this.tabPageCustonTemplate.Padding = new Padding(3, 10, 3, 3);
            this.tabPageCustonTemplate.Size = new Size(0x324, 0x1dc);
            this.tabPageCustonTemplate.TabIndex = 1;
            this.tabPageCustonTemplate.Text = "自定义模板";
            this.tabPageCustonTemplate.UseVisualStyleBackColor = true;
            this.tabControl4.Controls.Add(this.tabPage6);
            this.tabControl4.Controls.Add(this.tabPage9);
            this.tabControl4.Dock = DockStyle.Fill;
            this.tabControl4.Location = new Point(3, 10);
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new Size(0x31e, 0x184);
            this.tabControl4.TabIndex = 0x13;
            this.tabPage6.Controls.Add(this.panel6);
            this.tabPage6.Location = new Point(4, 0x16);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new Padding(3);
            this.tabPage6.Size = new Size(790, 0x16a);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "基本设置";
            this.tabPage6.UseVisualStyleBackColor = true;
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = DockStyle.Fill;
            this.panel6.Location = new Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new Size(0x310, 0x164);
            this.panel6.TabIndex = 0x10;
            this.panel8.Controls.Add(this.splitContainer1);
            this.panel8.Dock = DockStyle.Fill;
            this.panel8.Location = new Point(0xe0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new Padding(5);
            this.panel8.Size = new Size(560, 0x164);
            this.panel8.TabIndex = 12;
            this.splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.listBoxTaskItemLine);
            this.splitContainer1.Panel2.Controls.Add(this.listBoxTaskItems);
            this.splitContainer1.Size = new Size(550, 0x15a);
            this.splitContainer1.SplitterDistance = 0x2b;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 10;
            this.listBoxTaskItemLine.BorderStyle = BorderStyle.None;
            this.listBoxTaskItemLine.Dock = DockStyle.Fill;
            this.listBoxTaskItemLine.DrawMode = DrawMode.OwnerDrawVariable;
            this.listBoxTaskItemLine.Enabled = false;
            this.listBoxTaskItemLine.FormattingEnabled = true;
            this.listBoxTaskItemLine.ItemHeight = 12;
            this.listBoxTaskItemLine.Location = new Point(0, 0);
            this.listBoxTaskItemLine.Name = "listBoxTaskItemLine";
            this.listBoxTaskItemLine.Size = new Size(0x29, 0x158);
            this.listBoxTaskItemLine.TabIndex = 0;
            this.listBoxTaskItemLine.DrawItem += new DrawItemEventHandler(this.listBoxTaskItemLine_DrawItem);
            this.listBoxTaskItemLine.MeasureItem += new MeasureItemEventHandler(this.listBoxTemplateCategory_MeasureItem);
            this.listBoxTaskItems.BorderStyle = BorderStyle.None;
            this.listBoxTaskItems.Dock = DockStyle.Fill;
            this.listBoxTaskItems.DrawMode = DrawMode.OwnerDrawVariable;
            this.listBoxTaskItems.FormattingEnabled = true;
            this.listBoxTaskItems.ItemHeight = 12;
            this.listBoxTaskItems.Location = new Point(0, 0);
            this.listBoxTaskItems.Name = "listBoxTaskItems";
            this.listBoxTaskItems.Size = new Size(0x1f8, 0x158);
            this.listBoxTaskItems.TabIndex = 0;
            this.listBoxTaskItems.DrawItem += new DrawItemEventHandler(this.listBoxTaskItems_DrawItem);
            this.listBoxTaskItems.MeasureItem += new MeasureItemEventHandler(this.listBoxTemplateCategory_MeasureItem);
            this.listBoxTaskItems.SelectedIndexChanged += new EventHandler(this.TaskItemSelected);
            this.listBoxTaskItems.MouseDoubleClick += new MouseEventHandler(this.TaskItemMouseDoubleClick);
            this.listBoxTaskItems.MouseUp += new MouseEventHandler(this.TaskItemMouseUp);
            this.panel7.Controls.Add(this.panel3);
            this.panel7.Dock = DockStyle.Left;
            this.panel7.Location = new Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new Padding(5);
            this.panel7.Size = new Size(0xe0, 0x164);
            this.panel7.TabIndex = 11;
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.groupPressKey);
            this.panel3.Controls.Add(this.groupBoxFresh);
            this.panel3.Controls.Add(this.groupBoxWait);
            this.panel3.Controls.Add(this.groupBoxDeepClick);
            this.panel3.Controls.Add(this.groupBoxFindPage);
            this.panel3.Controls.Add(this.groupBoxClickLink);
            this.panel3.Controls.Add(this.groupBoxClickChecked);
            this.panel3.Controls.Add(this.groupBoxClickRadio);
            this.panel3.Controls.Add(this.groupBoxClickButton);
            this.panel3.Controls.Add(this.groupBoxInputText);
            this.panel3.Controls.Add(this.groupBoxNavigate);
            this.panel3.Controls.Add(this.groupBoxClearCookie);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new Point(5, 5);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new Padding(3, 3, 0, 3);
            this.panel3.Size = new Size(0xd6, 0x15a);
            this.panel3.TabIndex = 6;
            this.groupPressKey.Controls.Add(this.buttonInsertPressKey);
            this.groupPressKey.Controls.Add(this.buttonPressKey);
            this.groupPressKey.Dock = DockStyle.Top;
            this.groupPressKey.Location = new Point(3, 0x527);
            this.groupPressKey.Name = "groupPressKey";
            this.groupPressKey.Size = new Size(0xc0, 0x3a);
            this.groupPressKey.TabIndex = 12;
            this.groupPressKey.TabStop = false;
            this.buttonInsertPressKey.Location = new Point(0x37, 0x1f);
            this.buttonInsertPressKey.Name = "buttonInsertPressKey";
            this.buttonInsertPressKey.Size = new Size(0x4b, 0x17);
            this.buttonInsertPressKey.TabIndex = 1;
            this.buttonInsertPressKey.Text = "插入>>";
            this.buttonInsertPressKey.UseVisualStyleBackColor = true;
            this.buttonInsertPressKey.Click += new EventHandler(this.buttonInsertPressKeyClick);
            this.buttonPressKey.Location = new Point(7, 2);
            this.buttonPressKey.Name = "buttonPressKey";
            this.buttonPressKey.Size = new Size(0xb9, 0x17);
            this.buttonPressKey.TabIndex = 0;
            this.buttonPressKey.Text = "输入回车键";
            this.buttonPressKey.UseVisualStyleBackColor = true;
            this.buttonPressKey.Click += new EventHandler(this.buttonPressKeyClick);
            this.groupBoxFresh.Controls.Add(this.buttonInsertFresh);
            this.groupBoxFresh.Controls.Add(this.buttonFresh);
            this.groupBoxFresh.Dock = DockStyle.Top;
            this.groupBoxFresh.Location = new Point(3, 0x4ec);
            this.groupBoxFresh.Name = "groupBoxFresh";
            this.groupBoxFresh.Size = new Size(0xc0, 0x3b);
            this.groupBoxFresh.TabIndex = 11;
            this.groupBoxFresh.TabStop = false;
            this.buttonInsertFresh.Location = new Point(0x37, 30);
            this.buttonInsertFresh.Name = "buttonInsertFresh";
            this.buttonInsertFresh.Size = new Size(0x4b, 0x17);
            this.buttonInsertFresh.TabIndex = 1;
            this.buttonInsertFresh.Text = "插入>>";
            this.buttonInsertFresh.UseVisualStyleBackColor = true;
            this.buttonInsertFresh.Click += new EventHandler(this.buttonInsertFreshClick);
            this.buttonFresh.Location = new Point(7, 2);
            this.buttonFresh.Name = "buttonFresh";
            this.buttonFresh.Size = new Size(0xb9, 0x17);
            this.buttonFresh.TabIndex = 0;
            this.buttonFresh.Text = "刷新";
            this.buttonFresh.UseVisualStyleBackColor = true;
            this.buttonFresh.Click += new EventHandler(this.buttonFreshClick);
            this.groupBoxWait.Controls.Add(this.label1);
            this.groupBoxWait.Controls.Add(this.buttonWait);
            this.groupBoxWait.Controls.Add(this.textBoxWaitTime);
            this.groupBoxWait.Controls.Add(this.buttonInsertWait);
            this.groupBoxWait.Dock = DockStyle.Top;
            this.groupBoxWait.Location = new Point(3, 0x497);
            this.groupBoxWait.Name = "groupBoxWait";
            this.groupBoxWait.Size = new Size(0xc0, 0x55);
            this.groupBoxWait.TabIndex = 11;
            this.groupBoxWait.TabStop = false;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x79, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "秒";
            this.buttonWait.Location = new Point(6, 2);
            this.buttonWait.Name = "buttonWait";
            this.buttonWait.Size = new Size(0xbb, 0x17);
            this.buttonWait.TabIndex = 10;
            this.buttonWait.Text = "等待";
            this.buttonWait.UseVisualStyleBackColor = true;
            this.buttonWait.Click += new EventHandler(this.buttonWaitClick);
            this.textBoxWaitTime.Location = new Point(10, 0x1c);
            this.textBoxWaitTime.Name = "textBoxWaitTime";
            this.textBoxWaitTime.Size = new Size(100, 0x15);
            this.textBoxWaitTime.TabIndex = 1;
            this.textBoxWaitTime.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxWaitTime.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.buttonInsertWait.Location = new Point(0x37, 0x37);
            this.buttonInsertWait.Name = "buttonInsertWait";
            this.buttonInsertWait.Size = new Size(0x4b, 0x17);
            this.buttonInsertWait.TabIndex = 0;
            this.buttonInsertWait.Text = "插入>>";
            this.buttonInsertWait.UseVisualStyleBackColor = true;
            this.buttonInsertWait.Click += new EventHandler(this.buttonInsertWaitClick);
            this.groupBoxDeepClick.Controls.Add(this.textBoxDeepClickText);
            this.groupBoxDeepClick.Controls.Add(this.label62);
            this.groupBoxDeepClick.Controls.Add(this.buttonInsertDeepClick);
            this.groupBoxDeepClick.Controls.Add(this.label25);
            this.groupBoxDeepClick.Controls.Add(this.label24);
            this.groupBoxDeepClick.Controls.Add(this.label23);
            this.groupBoxDeepClick.Controls.Add(this.textBoxDeepClickWaitTime);
            this.groupBoxDeepClick.Controls.Add(this.label22);
            this.groupBoxDeepClick.Controls.Add(this.textBoxDeepClickTimes);
            this.groupBoxDeepClick.Controls.Add(this.button3);
            this.groupBoxDeepClick.Dock = DockStyle.Top;
            this.groupBoxDeepClick.Location = new Point(3, 0x3fa);
            this.groupBoxDeepClick.Name = "groupBoxDeepClick";
            this.groupBoxDeepClick.Size = new Size(0xc0, 0x9d);
            this.groupBoxDeepClick.TabIndex = 1;
            this.groupBoxDeepClick.TabStop = false;
            this.textBoxDeepClickText.ImeMode = ImeMode.On;
            this.textBoxDeepClickText.Location = new Point(10, 0x2e);
            this.textBoxDeepClickText.Name = "textBoxDeepClickText";
            this.textBoxDeepClickText.Size = new Size(0xb3, 0x15);
            this.textBoxDeepClickText.TabIndex = 11;
            this.label62.AutoSize = true;
            this.label62.Location = new Point(0x47, 0x1d);
            this.label62.Name = "label62";
            this.label62.Size = new Size(0x29, 12);
            this.label62.TabIndex = 10;
            this.label62.Text = "关键字";
            this.buttonInsertDeepClick.Location = new Point(0x37, 0x80);
            this.buttonInsertDeepClick.Name = "buttonInsertDeepClick";
            this.buttonInsertDeepClick.Size = new Size(0x4b, 0x17);
            this.buttonInsertDeepClick.TabIndex = 1;
            this.buttonInsertDeepClick.Text = "插入>>";
            this.buttonInsertDeepClick.UseVisualStyleBackColor = true;
            this.buttonInsertDeepClick.Click += new EventHandler(this.buttonInsertDeepClickClick);
            this.label25.AutoSize = true;
            this.label25.Location = new Point(8, 0x68);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x41, 12);
            this.label25.TabIndex = 9;
            this.label25.Text = "点击前等待";
            this.label24.AutoSize = true;
            this.label24.Location = new Point(20, 0x4d);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x35, 12);
            this.label24.TabIndex = 9;
            this.label24.Text = "深入点击";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0x7c, 0x68);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x11, 12);
            this.label23.TabIndex = 8;
            this.label23.Text = "秒";
            this.textBoxDeepClickWaitTime.Location = new Point(0x4c, 0x65);
            this.textBoxDeepClickWaitTime.Name = "textBoxDeepClickWaitTime";
            this.textBoxDeepClickWaitTime.Size = new Size(0x2a, 0x15);
            this.textBoxDeepClickWaitTime.TabIndex = 7;
            this.textBoxDeepClickWaitTime.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxDeepClickWaitTime.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.label22.AutoSize = true;
            this.label22.Location = new Point(0x7c, 0x4d);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x11, 12);
            this.label22.TabIndex = 6;
            this.label22.Text = "次";
            this.textBoxDeepClickTimes.Location = new Point(0x4c, 0x4a);
            this.textBoxDeepClickTimes.Name = "textBoxDeepClickTimes";
            this.textBoxDeepClickTimes.Size = new Size(0x2a, 0x15);
            this.textBoxDeepClickTimes.TabIndex = 5;
            this.textBoxDeepClickTimes.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxDeepClickTimes.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.button3.Location = new Point(7, 2);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0xb9, 0x17);
            this.button3.TabIndex = 0;
            this.button3.Text = "深入点击";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.buttonDeepClickClick);
            this.groupBoxFindPage.Controls.Add(this.textBoxFindKeyword);
            this.groupBoxFindPage.Controls.Add(this.label65);
            this.groupBoxFindPage.Controls.Add(this.textBoxFindMaxPage);
            this.groupBoxFindPage.Controls.Add(this.label61);
            this.groupBoxFindPage.Controls.Add(this.button2);
            this.groupBoxFindPage.Controls.Add(this.buttonFindPage);
            this.groupBoxFindPage.Controls.Add(this.textBoxFindPageNext);
            this.groupBoxFindPage.Controls.Add(this.label60);
            this.groupBoxFindPage.Controls.Add(this.textBoxFindPage);
            this.groupBoxFindPage.Controls.Add(this.label64);
            this.groupBoxFindPage.Controls.Add(this.buttonInsertFindPage);
            this.groupBoxFindPage.Controls.Add(this.button1);
            this.groupBoxFindPage.Dock = DockStyle.Top;
            this.groupBoxFindPage.Location = new Point(3, 0x321);
            this.groupBoxFindPage.Name = "groupBoxFindPage";
            this.groupBoxFindPage.Size = new Size(0xc0, 0xd9);
            this.groupBoxFindPage.TabIndex = 11;
            this.groupBoxFindPage.TabStop = false;
            this.textBoxFindKeyword.ImeMode = ImeMode.On;
            this.textBoxFindKeyword.Location = new Point(0x47, 80);
            this.textBoxFindKeyword.Name = "textBoxFindKeyword";
            this.textBoxFindKeyword.Size = new Size(0x71, 0x15);
            this.textBoxFindKeyword.TabIndex = 10;
            this.label65.AutoSize = true;
            this.label65.Location = new Point(4, 0x53);
            this.label65.Name = "label65";
            this.label65.Size = new Size(0x41, 12);
            this.label65.TabIndex = 11;
            this.label65.Text = "网址关键字";
            this.textBoxFindMaxPage.Location = new Point(0x61, 0xa1);
            this.textBoxFindMaxPage.Name = "textBoxFindMaxPage";
            this.textBoxFindMaxPage.Size = new Size(0x4f, 0x15);
            this.textBoxFindMaxPage.TabIndex = 9;
            this.textBoxFindMaxPage.Text = "20";
            this.textBoxFindMaxPage.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxFindMaxPage.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.label61.AutoSize = true;
            this.label61.Location = new Point(0x13, 0xa4);
            this.label61.Name = "label61";
            this.label61.Size = new Size(0x4d, 12);
            this.label61.TabIndex = 8;
            this.label61.Text = "最大查找页数";
            this.button2.Location = new Point(0x5e, 0x69);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 7;
            this.button2.Text = "抓取超链接";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseDown += new MouseEventHandler(this.FindPageNextMouseDown);
            this.button2.MouseUp += new MouseEventHandler(this.FindMouseUp);
            this.buttonFindPage.Location = new Point(0x61, 0x1b);
            this.buttonFindPage.Name = "buttonFindPage";
            this.buttonFindPage.Size = new Size(0x4b, 0x17);
            this.buttonFindPage.TabIndex = 6;
            this.buttonFindPage.Text = "抓取超链接";
            this.buttonFindPage.UseVisualStyleBackColor = true;
            this.buttonFindPage.MouseDown += new MouseEventHandler(this.FindDestPageMouseDown);
            this.buttonFindPage.MouseUp += new MouseEventHandler(this.FindMouseUp);
            this.textBoxFindPageNext.ImeMode = ImeMode.On;
            this.textBoxFindPageNext.Location = new Point(10, 0x83);
            this.textBoxFindPageNext.Name = "textBoxFindPageNext";
            this.textBoxFindPageNext.Size = new Size(0xb3, 0x15);
            this.textBoxFindPageNext.TabIndex = 5;
            this.label60.AutoSize = true;
            this.label60.Location = new Point(0x18, 0x6f);
            this.label60.Name = "label60";
            this.label60.Size = new Size(0x41, 12);
            this.label60.TabIndex = 4;
            this.label60.Text = "下一页链接";
            this.textBoxFindPage.ImeMode = ImeMode.On;
            this.textBoxFindPage.Location = new Point(10, 0x36);
            this.textBoxFindPage.Name = "textBoxFindPage";
            this.textBoxFindPage.Size = new Size(0xb3, 0x15);
            this.textBoxFindPage.TabIndex = 3;
            this.label64.AutoSize = true;
            this.label64.Location = new Point(5, 0x20);
            this.label64.Name = "label64";
            this.label64.Size = new Size(0x59, 12);
            this.label64.TabIndex = 2;
            this.label64.Text = "需要点击的链接";
            this.buttonInsertFindPage.Location = new Point(0x37, 0xbc);
            this.buttonInsertFindPage.Name = "buttonInsertFindPage";
            this.buttonInsertFindPage.Size = new Size(0x4b, 0x17);
            this.buttonInsertFindPage.TabIndex = 1;
            this.buttonInsertFindPage.Text = "插入>>";
            this.buttonInsertFindPage.UseVisualStyleBackColor = true;
            this.buttonInsertFindPage.Click += new EventHandler(this.buttonInsertFindPageClick);
            this.button1.Location = new Point(7, 0);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0xb8, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "查找页面";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.buttonFindPageClick);
            this.groupBoxClickLink.Controls.Add(this.buttonInsertClickLink);
            this.groupBoxClickLink.Controls.Add(this.textBoxLinkKeyword);
            this.groupBoxClickLink.Controls.Add(this.textBoxLink);
            this.groupBoxClickLink.Controls.Add(this.buttonClickLink);
            this.groupBoxClickLink.Controls.Add(this.buttonFindLink);
            this.groupBoxClickLink.Controls.Add(this.label66);
            this.groupBoxClickLink.Dock = DockStyle.Top;
            this.groupBoxClickLink.Location = new Point(3, 0x29a);
            this.groupBoxClickLink.Name = "groupBoxClickLink";
            this.groupBoxClickLink.Size = new Size(0xc0, 0x87);
            this.groupBoxClickLink.TabIndex = 9;
            this.groupBoxClickLink.TabStop = false;
            this.buttonInsertClickLink.Location = new Point(0x37, 0x6b);
            this.buttonInsertClickLink.Name = "buttonInsertClickLink";
            this.buttonInsertClickLink.Size = new Size(0x4b, 0x17);
            this.buttonInsertClickLink.TabIndex = 1;
            this.buttonInsertClickLink.Text = "插入>>";
            this.buttonInsertClickLink.UseVisualStyleBackColor = true;
            this.buttonInsertClickLink.Click += new EventHandler(this.buttonInsertClickLinkClick);
            this.textBoxLinkKeyword.ImeMode = ImeMode.On;
            this.textBoxLinkKeyword.Location = new Point(0x47, 0x36);
            this.textBoxLinkKeyword.Name = "textBoxLinkKeyword";
            this.textBoxLinkKeyword.Size = new Size(0x71, 0x15);
            this.textBoxLinkKeyword.TabIndex = 0;
            this.textBoxLink.ImeMode = ImeMode.On;
            this.textBoxLink.Location = new Point(10, 30);
            this.textBoxLink.Name = "textBoxLink";
            this.textBoxLink.Size = new Size(0xb5, 0x15);
            this.textBoxLink.TabIndex = 0;
            this.buttonClickLink.Location = new Point(6, 2);
            this.buttonClickLink.Name = "buttonClickLink";
            this.buttonClickLink.Size = new Size(0xbb, 0x17);
            this.buttonClickLink.TabIndex = 8;
            this.buttonClickLink.Text = "点击链接";
            this.buttonClickLink.UseVisualStyleBackColor = true;
            this.buttonClickLink.Click += new EventHandler(this.buttonClickLinkClick);
            this.buttonFindLink.Location = new Point(0x37, 0x51);
            this.buttonFindLink.Name = "buttonFindLink";
            this.buttonFindLink.Size = new Size(0x4b, 0x17);
            this.buttonFindLink.TabIndex = 0;
            this.buttonFindLink.Text = "抓取超链接";
            this.buttonFindLink.UseVisualStyleBackColor = true;
            this.buttonFindLink.MouseDown += new MouseEventHandler(this.FindLinkMouseDown);
            this.buttonFindLink.MouseUp += new MouseEventHandler(this.FindMouseUp);
            this.label66.AutoSize = true;
            this.label66.Location = new Point(4, 0x39);
            this.label66.Name = "label66";
            this.label66.Size = new Size(0x41, 12);
            this.label66.TabIndex = 2;
            this.label66.Text = "网址关键字";
            this.groupBoxClickChecked.Controls.Add(this.buttonInsertClickChecked);
            this.groupBoxClickChecked.Controls.Add(this.textBoxCheckedID);
            this.groupBoxClickChecked.Controls.Add(this.buttonFindChecked);
            this.groupBoxClickChecked.Controls.Add(this.buttonClickChecked);
            this.groupBoxClickChecked.Dock = DockStyle.Top;
            this.groupBoxClickChecked.Location = new Point(3, 0x23e);
            this.groupBoxClickChecked.Name = "groupBoxClickChecked";
            this.groupBoxClickChecked.Size = new Size(0xc0, 0x5c);
            this.groupBoxClickChecked.TabIndex = 14;
            this.groupBoxClickChecked.TabStop = false;
            this.buttonInsertClickChecked.Location = new Point(0x37, 0x41);
            this.buttonInsertClickChecked.Name = "buttonInsertClickChecked";
            this.buttonInsertClickChecked.Size = new Size(0x4b, 0x17);
            this.buttonInsertClickChecked.TabIndex = 1;
            this.buttonInsertClickChecked.Text = "插入>>";
            this.buttonInsertClickChecked.UseVisualStyleBackColor = true;
            this.buttonInsertClickChecked.Click += new EventHandler(this.buttonInsertClickCheckedClick);
            this.textBoxCheckedID.ImeMode = ImeMode.On;
            this.textBoxCheckedID.Location = new Point(10, 0x25);
            this.textBoxCheckedID.Name = "textBoxCheckedID";
            this.textBoxCheckedID.Size = new Size(100, 0x15);
            this.textBoxCheckedID.TabIndex = 0;
            this.buttonFindChecked.Location = new Point(0x74, 0x24);
            this.buttonFindChecked.Name = "buttonFindChecked";
            this.buttonFindChecked.Size = new Size(0x4b, 0x17);
            this.buttonFindChecked.TabIndex = 0;
            this.buttonFindChecked.Text = "抓取复选框";
            this.buttonFindChecked.UseVisualStyleBackColor = true;
            this.buttonFindChecked.MouseDown += new MouseEventHandler(this.FindCheckedMouseDown);
            this.buttonFindChecked.MouseUp += new MouseEventHandler(this.FindMouseUp);
            this.buttonClickChecked.Location = new Point(6, 3);
            this.buttonClickChecked.Name = "buttonClickChecked";
            this.buttonClickChecked.Size = new Size(0xbb, 0x17);
            this.buttonClickChecked.TabIndex = 6;
            this.buttonClickChecked.Text = "点击复选框";
            this.buttonClickChecked.UseVisualStyleBackColor = true;
            this.buttonClickChecked.Click += new EventHandler(this.buttonClickCheckedClick);
            this.groupBoxClickRadio.Controls.Add(this.buttonInsertClickRadio);
            this.groupBoxClickRadio.Controls.Add(this.textBoxRadioID);
            this.groupBoxClickRadio.Controls.Add(this.buttonFindRadio);
            this.groupBoxClickRadio.Controls.Add(this.buttonClickRadio);
            this.groupBoxClickRadio.Dock = DockStyle.Top;
            this.groupBoxClickRadio.Location = new Point(3, 0x1e2);
            this.groupBoxClickRadio.Name = "groupBoxClickRadio";
            this.groupBoxClickRadio.Size = new Size(0xc0, 0x5c);
            this.groupBoxClickRadio.TabIndex = 13;
            this.groupBoxClickRadio.TabStop = false;
            this.buttonInsertClickRadio.Location = new Point(0x37, 0x41);
            this.buttonInsertClickRadio.Name = "buttonInsertClickRadio";
            this.buttonInsertClickRadio.Size = new Size(0x4b, 0x17);
            this.buttonInsertClickRadio.TabIndex = 1;
            this.buttonInsertClickRadio.Text = "插入>>";
            this.buttonInsertClickRadio.UseVisualStyleBackColor = true;
            this.buttonInsertClickRadio.Click += new EventHandler(this.buttonInsertClickRadioClick);
            this.textBoxRadioID.ImeMode = ImeMode.On;
            this.textBoxRadioID.Location = new Point(10, 0x25);
            this.textBoxRadioID.Name = "textBoxRadioID";
            this.textBoxRadioID.Size = new Size(100, 0x15);
            this.textBoxRadioID.TabIndex = 0;
            this.buttonFindRadio.Location = new Point(0x74, 0x24);
            this.buttonFindRadio.Name = "buttonFindRadio";
            this.buttonFindRadio.Size = new Size(0x4b, 0x17);
            this.buttonFindRadio.TabIndex = 0;
            this.buttonFindRadio.Text = "抓取单选框";
            this.buttonFindRadio.UseVisualStyleBackColor = true;
            this.buttonFindRadio.MouseDown += new MouseEventHandler(this.FindRadioMouseDown);
            this.buttonFindRadio.MouseUp += new MouseEventHandler(this.FindMouseUp);
            this.buttonClickRadio.Location = new Point(6, 3);
            this.buttonClickRadio.Name = "buttonClickRadio";
            this.buttonClickRadio.Size = new Size(0xbb, 0x17);
            this.buttonClickRadio.TabIndex = 6;
            this.buttonClickRadio.Text = "点击单选框";
            this.buttonClickRadio.UseVisualStyleBackColor = true;
            this.buttonClickRadio.Click += new EventHandler(this.buttonClickRadioClick);
            this.groupBoxClickButton.Controls.Add(this.buttonInsertClickButton);
            this.groupBoxClickButton.Controls.Add(this.textBoxButtonID);
            this.groupBoxClickButton.Controls.Add(this.buttonFindButton);
            this.groupBoxClickButton.Controls.Add(this.buttonClickButton);
            this.groupBoxClickButton.Dock = DockStyle.Top;
            this.groupBoxClickButton.Location = new Point(3, 390);
            this.groupBoxClickButton.Name = "groupBoxClickButton";
            this.groupBoxClickButton.Size = new Size(0xc0, 0x5c);
            this.groupBoxClickButton.TabIndex = 7;
            this.groupBoxClickButton.TabStop = false;
            this.buttonInsertClickButton.Location = new Point(0x37, 0x41);
            this.buttonInsertClickButton.Name = "buttonInsertClickButton";
            this.buttonInsertClickButton.Size = new Size(0x4b, 0x17);
            this.buttonInsertClickButton.TabIndex = 1;
            this.buttonInsertClickButton.Text = "插入>>";
            this.buttonInsertClickButton.UseVisualStyleBackColor = true;
            this.buttonInsertClickButton.Click += new EventHandler(this.buttonInsertClickButtonClick);
            this.textBoxButtonID.ImeMode = ImeMode.On;
            this.textBoxButtonID.Location = new Point(10, 0x25);
            this.textBoxButtonID.Name = "textBoxButtonID";
            this.textBoxButtonID.Size = new Size(100, 0x15);
            this.textBoxButtonID.TabIndex = 0;
            this.buttonFindButton.Location = new Point(0x74, 0x24);
            this.buttonFindButton.Name = "buttonFindButton";
            this.buttonFindButton.Size = new Size(0x4b, 0x17);
            this.buttonFindButton.TabIndex = 0;
            this.buttonFindButton.Text = "抓取按钮";
            this.buttonFindButton.UseVisualStyleBackColor = true;
            this.buttonFindButton.MouseDown += new MouseEventHandler(this.FindButtonMouseDown);
            this.buttonFindButton.MouseUp += new MouseEventHandler(this.FindMouseUp);
            this.buttonClickButton.Location = new Point(6, 3);
            this.buttonClickButton.Name = "buttonClickButton";
            this.buttonClickButton.Size = new Size(0xbb, 0x17);
            this.buttonClickButton.TabIndex = 6;
            this.buttonClickButton.Text = "点击按钮";
            this.buttonClickButton.UseVisualStyleBackColor = true;
            this.buttonClickButton.Click += new EventHandler(this.buttonClickButtonClick);
            this.groupBoxInputText.Controls.Add(this.buttonInsertInputText);
            this.groupBoxInputText.Controls.Add(this.textBoxInputText);
            this.groupBoxInputText.Controls.Add(this.buttonEnterInputText);
            this.groupBoxInputText.Controls.Add(this.textBoxInputBoxName);
            this.groupBoxInputText.Controls.Add(this.buttonFindInputText);
            this.groupBoxInputText.Dock = DockStyle.Top;
            this.groupBoxInputText.Location = new Point(3, 0x100);
            this.groupBoxInputText.Name = "groupBoxInputText";
            this.groupBoxInputText.Size = new Size(0xc0, 0x86);
            this.groupBoxInputText.TabIndex = 4;
            this.groupBoxInputText.TabStop = false;
            this.buttonInsertInputText.Location = new Point(0x37, 0x69);
            this.buttonInsertInputText.Name = "buttonInsertInputText";
            this.buttonInsertInputText.Size = new Size(0x4b, 0x17);
            this.buttonInsertInputText.TabIndex = 1;
            this.buttonInsertInputText.Text = "插入>>";
            this.buttonInsertInputText.UseVisualStyleBackColor = true;
            this.buttonInsertInputText.Click += new EventHandler(this.buttonInsertInputTextClick);
            this.textBoxInputText.ImeMode = ImeMode.On;
            this.textBoxInputText.Location = new Point(10, 0x3f);
            this.textBoxInputText.Multiline = true;
            this.textBoxInputText.Name = "textBoxInputText";
            this.textBoxInputText.Size = new Size(0xb5, 0x24);
            this.textBoxInputText.TabIndex = 2;
            this.buttonEnterInputText.Location = new Point(7, 2);
            this.buttonEnterInputText.Name = "buttonEnterInputText";
            this.buttonEnterInputText.Size = new Size(0xb8, 0x17);
            this.buttonEnterInputText.TabIndex = 5;
            this.buttonEnterInputText.Text = "输入文本";
            this.buttonEnterInputText.UseVisualStyleBackColor = true;
            this.buttonEnterInputText.Click += new EventHandler(this.buttonInputTextClick);
            this.textBoxInputBoxName.ImeMode = ImeMode.On;
            this.textBoxInputBoxName.Location = new Point(10, 0x25);
            this.textBoxInputBoxName.Name = "textBoxInputBoxName";
            this.textBoxInputBoxName.Size = new Size(100, 0x15);
            this.textBoxInputBoxName.TabIndex = 1;
            this.buttonFindInputText.Location = new Point(0x74, 0x22);
            this.buttonFindInputText.Name = "buttonFindInputText";
            this.buttonFindInputText.Size = new Size(0x4b, 0x17);
            this.buttonFindInputText.TabIndex = 0;
            this.buttonFindInputText.Text = "抓取文本框";
            this.buttonFindInputText.UseVisualStyleBackColor = true;
            this.buttonFindInputText.MouseDown += new MouseEventHandler(this.FindTextMouseDown);
            this.buttonFindInputText.MouseUp += new MouseEventHandler(this.FindMouseUp);
            this.groupBoxNavigate.Controls.Add(this.label2);
            this.groupBoxNavigate.Controls.Add(this.buttonInsertWebSite);
            this.groupBoxNavigate.Controls.Add(this.textBoxWebSiteRefer);
            this.groupBoxNavigate.Controls.Add(this.textBoxWebSite);
            this.groupBoxNavigate.Controls.Add(this.buttonEnterWebSite);
            this.groupBoxNavigate.Dock = DockStyle.Top;
            this.groupBoxNavigate.Location = new Point(3, 0x86);
            this.groupBoxNavigate.Name = "groupBoxNavigate";
            this.groupBoxNavigate.Size = new Size(0xc0, 0x7a);
            this.groupBoxNavigate.TabIndex = 0;
            this.groupBoxNavigate.TabStop = false;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x3f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "来路：";
            this.buttonInsertWebSite.Location = new Point(0x37, 0x59);
            this.buttonInsertWebSite.Name = "buttonInsertWebSite";
            this.buttonInsertWebSite.Size = new Size(0x4b, 0x17);
            this.buttonInsertWebSite.TabIndex = 1;
            this.buttonInsertWebSite.Text = "插入>>";
            this.buttonInsertWebSite.UseVisualStyleBackColor = true;
            this.buttonInsertWebSite.Click += new EventHandler(this.buttonInsertWebSiteClick);
            this.textBoxWebSiteRefer.ImeMode = ImeMode.On;
            this.textBoxWebSiteRefer.Location = new Point(0x30, 60);
            this.textBoxWebSiteRefer.Name = "textBoxWebSiteRefer";
            this.textBoxWebSiteRefer.Size = new Size(0x8f, 0x15);
            this.textBoxWebSiteRefer.TabIndex = 0;
            this.textBoxWebSite.ImeMode = ImeMode.On;
            this.textBoxWebSite.Location = new Point(10, 0x21);
            this.textBoxWebSite.Name = "textBoxWebSite";
            this.textBoxWebSite.Size = new Size(0xb5, 0x15);
            this.textBoxWebSite.TabIndex = 0;
            this.buttonEnterWebSite.Location = new Point(7, 2);
            this.buttonEnterWebSite.Name = "buttonEnterWebSite";
            this.buttonEnterWebSite.Size = new Size(0xb8, 0x17);
            this.buttonEnterWebSite.TabIndex = 3;
            this.buttonEnterWebSite.Text = "输入网址";
            this.buttonEnterWebSite.UseVisualStyleBackColor = true;
            this.buttonEnterWebSite.Click += new EventHandler(this.buttonEnterWebSiteClick);
            this.groupBoxClearCookie.Controls.Add(this.radioButtonPassword);
            this.groupBoxClearCookie.Controls.Add(this.radioButtonTempFile);
            this.groupBoxClearCookie.Controls.Add(this.radioButtonAll);
            this.groupBoxClearCookie.Controls.Add(this.radioButtonCookie);
            this.groupBoxClearCookie.Controls.Add(this.radioButtonHistory);
            this.groupBoxClearCookie.Controls.Add(this.buttonClearCookie);
            this.groupBoxClearCookie.Controls.Add(this.buttonInsertClearCookie);
            this.groupBoxClearCookie.Dock = DockStyle.Top;
            this.groupBoxClearCookie.Location = new Point(3, 3);
            this.groupBoxClearCookie.Name = "groupBoxClearCookie";
            this.groupBoxClearCookie.Size = new Size(0xc0, 0x83);
            this.groupBoxClearCookie.TabIndex = 12;
            this.groupBoxClearCookie.TabStop = false;
            this.radioButtonPassword.AutoSize = true;
            this.radioButtonPassword.Location = new Point(100, 0x37);
            this.radioButtonPassword.Name = "radioButtonPassword";
            this.radioButtonPassword.Size = new Size(0x53, 0x10);
            this.radioButtonPassword.TabIndex = 14;
            this.radioButtonPassword.TabStop = true;
            this.radioButtonPassword.Text = "表单和密码";
            this.radioButtonPassword.UseVisualStyleBackColor = true;
            this.radioButtonTempFile.AutoSize = true;
            this.radioButtonTempFile.Location = new Point(10, 0x1f);
            this.radioButtonTempFile.Name = "radioButtonTempFile";
            this.radioButtonTempFile.Size = new Size(0x47, 0x10);
            this.radioButtonTempFile.TabIndex = 13;
            this.radioButtonTempFile.TabStop = true;
            this.radioButtonTempFile.Text = "临时文件";
            this.radioButtonTempFile.UseVisualStyleBackColor = true;
            this.radioButtonAll.AutoSize = true;
            this.radioButtonAll.Location = new Point(10, 0x4d);
            this.radioButtonAll.Name = "radioButtonAll";
            this.radioButtonAll.Size = new Size(0x47, 0x10);
            this.radioButtonAll.TabIndex = 12;
            this.radioButtonAll.TabStop = true;
            this.radioButtonAll.Text = "所有数据";
            this.radioButtonAll.UseVisualStyleBackColor = true;
            this.radioButtonCookie.AutoSize = true;
            this.radioButtonCookie.Location = new Point(100, 0x1f);
            this.radioButtonCookie.Name = "radioButtonCookie";
            this.radioButtonCookie.Size = new Size(0x3b, 0x10);
            this.radioButtonCookie.TabIndex = 12;
            this.radioButtonCookie.TabStop = true;
            this.radioButtonCookie.Text = "Cookie";
            this.radioButtonCookie.UseVisualStyleBackColor = true;
            this.radioButtonHistory.AutoSize = true;
            this.radioButtonHistory.Location = new Point(10, 0x37);
            this.radioButtonHistory.Name = "radioButtonHistory";
            this.radioButtonHistory.Size = new Size(0x47, 0x10);
            this.radioButtonHistory.TabIndex = 11;
            this.radioButtonHistory.TabStop = true;
            this.radioButtonHistory.Text = "历史记录";
            this.radioButtonHistory.UseVisualStyleBackColor = true;
            this.buttonClearCookie.Location = new Point(6, 2);
            this.buttonClearCookie.Name = "buttonClearCookie";
            this.buttonClearCookie.Size = new Size(0xbb, 0x17);
            this.buttonClearCookie.TabIndex = 10;
            this.buttonClearCookie.Text = "清除Cookie和缓存";
            this.buttonClearCookie.UseVisualStyleBackColor = true;
            this.buttonClearCookie.Click += new EventHandler(this.buttonClearCookieClick);
            this.buttonInsertClearCookie.Location = new Point(0x37, 0x63);
            this.buttonInsertClearCookie.Name = "buttonInsertClearCookie";
            this.buttonInsertClearCookie.Size = new Size(0x4b, 0x17);
            this.buttonInsertClearCookie.TabIndex = 0;
            this.buttonInsertClearCookie.Text = "插入>>";
            this.buttonInsertClearCookie.UseVisualStyleBackColor = true;
            this.buttonInsertClearCookie.Click += new EventHandler(this.buttonInsertClearCookieClick);
            this.tabPage9.Controls.Add(this.tabControl3);
            this.tabPage9.Location = new Point(4, 0x16);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new Padding(10);
            this.tabPage9.Size = new Size(790, 0x16a);
            this.tabPage9.TabIndex = 1;
            this.tabPage9.Text = "高级设置";
            this.tabPage9.UseVisualStyleBackColor = true;
            this.tabControl3.Controls.Add(this.tabPage3);
            this.tabControl3.Controls.Add(this.tabPage7);
            this.tabControl3.Controls.Add(this.tabPage8);
            this.tabControl3.Dock = DockStyle.Fill;
            this.tabControl3.Location = new Point(10, 10);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new Size(770, 0x156);
            this.tabControl3.TabIndex = 0;
            this.tabPage3.Controls.Add(this.groupBox8);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Location = new Point(4, 0x16);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new Size(0x2fa, 0x13c);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "优化设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.groupBox8.Anchor = AnchorStyles.None;
            this.groupBox8.Controls.Add(this.checkBoxMoveMouse);
            this.groupBox8.Controls.Add(this.checkBoxAllowScrollWhileWait);
            this.groupBox8.Controls.Add(this.checkBoxAllowScrollWhileLoad);
            this.groupBox8.Controls.Add(this.label69);
            this.groupBox8.Controls.Add(this.textBoxFindWaitTime2);
            this.groupBox8.Controls.Add(this.label70);
            this.groupBox8.Controls.Add(this.label33);
            this.groupBox8.Controls.Add(this.textBoxDocWaitTime2);
            this.groupBox8.Controls.Add(this.label68);
            this.groupBox8.Controls.Add(this.label30);
            this.groupBox8.Controls.Add(this.textBoxClickIntervalTime);
            this.groupBox8.Controls.Add(this.label31);
            this.groupBox8.Location = new Point(390, 0x2b);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.RightToLeft = RightToLeft.No;
            this.groupBox8.Size = new Size(350, 230);
            this.groupBox8.TabIndex = 6;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "点击设置";
            this.checkBoxMoveMouse.AutoSize = true;
            this.checkBoxMoveMouse.Location = new Point(90, 200);
            this.checkBoxMoveMouse.Name = "checkBoxMoveMouse";
            this.checkBoxMoveMouse.Size = new Size(180, 0x10);
            this.checkBoxMoveMouse.TabIndex = 0x10;
            this.checkBoxMoveMouse.Text = "等待过程中是否随机移动鼠标";
            this.checkBoxMoveMouse.UseVisualStyleBackColor = true;
            this.checkBoxAllowScrollWhileWait.AutoSize = true;
            this.checkBoxAllowScrollWhileWait.Location = new Point(0xc1, 0x9c);
            this.checkBoxAllowScrollWhileWait.Name = "checkBoxAllowScrollWhileWait";
            this.checkBoxAllowScrollWhileWait.Size = new Size(0x84, 0x10);
            this.checkBoxAllowScrollWhileWait.TabIndex = 15;
            this.checkBoxAllowScrollWhileWait.Text = "等待过程中是否滚屏";
            this.checkBoxAllowScrollWhileWait.UseVisualStyleBackColor = true;
            this.checkBoxAllowScrollWhileLoad.AutoSize = true;
            this.checkBoxAllowScrollWhileLoad.Location = new Point(0x1a, 0x9c);
            this.checkBoxAllowScrollWhileLoad.Name = "checkBoxAllowScrollWhileLoad";
            this.checkBoxAllowScrollWhileLoad.Size = new Size(0x84, 0x10);
            this.checkBoxAllowScrollWhileLoad.TabIndex = 14;
            this.checkBoxAllowScrollWhileLoad.Text = "网页加载时是否滚屏";
            this.checkBoxAllowScrollWhileLoad.UseVisualStyleBackColor = true;
            this.label69.AutoSize = true;
            this.label69.Location = new Point(0xc2, 0x74);
            this.label69.Name = "label69";
            this.label69.Size = new Size(0x83, 12);
            this.label69.TabIndex = 13;
            this.label69.Text = "秒　(填写0－60的数字)";
            this.textBoxFindWaitTime2.Location = new Point(0x77, 0x71);
            this.textBoxFindWaitTime2.Name = "textBoxFindWaitTime2";
            this.textBoxFindWaitTime2.Size = new Size(0x45, 0x15);
            this.textBoxFindWaitTime2.TabIndex = 12;
            this.textBoxFindWaitTime2.Text = "30";
            this.label70.AutoSize = true;
            this.label70.Location = new Point(0x18, 0x74);
            this.label70.Name = "label70";
            this.label70.Size = new Size(0x59, 12);
            this.label70.TabIndex = 11;
            this.label70.Text = "查找网页超时：";
            this.label33.AutoSize = true;
            this.label33.Location = new Point(0xc2, 0x4c);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0x83, 12);
            this.label33.TabIndex = 10;
            this.label33.Text = "秒　(填写0－60的数字)";
            this.textBoxDocWaitTime2.Location = new Point(0x77, 0x49);
            this.textBoxDocWaitTime2.Name = "textBoxDocWaitTime2";
            this.textBoxDocWaitTime2.Size = new Size(0x45, 0x15);
            this.textBoxDocWaitTime2.TabIndex = 9;
            this.textBoxDocWaitTime2.Text = "30";
            this.label68.AutoSize = true;
            this.label68.Location = new Point(0x18, 0x4c);
            this.label68.Name = "label68";
            this.label68.Size = new Size(0x59, 12);
            this.label68.TabIndex = 8;
            this.label68.Text = "网页加载超时：";
            this.label30.AutoSize = true;
            this.label30.Location = new Point(0xc2, 0x24);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x83, 12);
            this.label30.TabIndex = 7;
            this.label30.Text = "秒　(填写0－60的数字)";
            this.textBoxClickIntervalTime.Location = new Point(0x77, 0x21);
            this.textBoxClickIntervalTime.Name = "textBoxClickIntervalTime";
            this.textBoxClickIntervalTime.Size = new Size(0x45, 0x15);
            this.textBoxClickIntervalTime.TabIndex = 6;
            this.textBoxClickIntervalTime.Text = "0";
            this.label31.AutoSize = true;
            this.label31.Location = new Point(0x18, 0x24);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x59, 12);
            this.label31.TabIndex = 5;
            this.label31.Text = "点击间隔时间：";
            this.groupBox7.Anchor = AnchorStyles.None;
            this.groupBox7.Controls.Add(this.label71);
            this.groupBox7.Controls.Add(this.textBoxTaskIntervalTimeMin);
            this.groupBox7.Controls.Add(this.label28);
            this.groupBox7.Controls.Add(this.textBoxClickPerPC);
            this.groupBox7.Controls.Add(this.label29);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.textBoxClickPerIp);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.textBoxTaskIntervalTimeMax);
            this.groupBox7.Controls.Add(this.label72);
            this.groupBox7.Location = new Point(0x16, 0x2b);
            this.groupBox7.Margin = new Padding(5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(350, 230);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "任务设置";
            this.label71.AutoSize = true;
            this.label71.Location = new Point(0x88, 0x24);
            this.label71.Name = "label71";
            this.label71.Size = new Size(0x11, 12);
            this.label71.TabIndex = 12;
            this.label71.Text = "－";
            this.textBoxTaskIntervalTimeMin.Location = new Point(90, 0x21);
            this.textBoxTaskIntervalTimeMin.Name = "textBoxTaskIntervalTimeMin";
            this.textBoxTaskIntervalTimeMin.Size = new Size(0x2d, 0x15);
            this.textBoxTaskIntervalTimeMin.TabIndex = 11;
            this.textBoxTaskIntervalTimeMin.Text = "0";
            this.label28.AutoSize = true;
            this.label28.Location = new Point(0xb0, 0x6c);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x83, 12);
            this.label28.TabIndex = 10;
            this.label28.Text = "次　(填写1－50的数字)";
            this.textBoxClickPerPC.Location = new Point(0x65, 0x69);
            this.textBoxClickPerPC.Name = "textBoxClickPerPC";
            this.textBoxClickPerPC.Size = new Size(0x45, 0x15);
            this.textBoxClickPerPC.TabIndex = 9;
            this.textBoxClickPerPC.Text = "1";
            this.label29.AutoSize = true;
            this.label29.Location = new Point(6, 0x6c);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x59, 12);
            this.label29.TabIndex = 8;
            this.label29.Text = "同PC点击次数：";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(0xb0, 0x48);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x83, 12);
            this.label16.TabIndex = 7;
            this.label16.Text = "次　(填写1－50的数字)";
            this.textBoxClickPerIp.Location = new Point(0x65, 0x45);
            this.textBoxClickPerIp.Name = "textBoxClickPerIp";
            this.textBoxClickPerIp.Size = new Size(0x45, 0x15);
            this.textBoxClickPerIp.TabIndex = 6;
            this.textBoxClickPerIp.Text = "1";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(6, 0x48);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x59, 12);
            this.label15.TabIndex = 5;
            this.label15.Text = "同IP点击次数：";
            this.label21.AutoSize = true;
            this.label21.Location = new Point(0xcf, 0x24);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x8f, 12);
            this.label21.TabIndex = 4;
            this.label21.Text = "秒　(填写0－1000的数字)";
            this.textBoxTaskIntervalTimeMax.Location = new Point(0x9c, 0x21);
            this.textBoxTaskIntervalTimeMax.Name = "textBoxTaskIntervalTimeMax";
            this.textBoxTaskIntervalTimeMax.Size = new Size(0x2d, 0x15);
            this.textBoxTaskIntervalTimeMax.TabIndex = 3;
            this.textBoxTaskIntervalTimeMax.Text = "30";
            this.label72.AutoSize = true;
            this.label72.Location = new Point(6, 0x24);
            this.label72.Name = "label72";
            this.label72.Size = new Size(0x59, 12);
            this.label72.TabIndex = 2;
            this.label72.Text = "任务间隔时间：";
            this.tabPage7.Controls.Add(this.textBoxCustomClock47);
            this.tabPage7.Controls.Add(this.textBoxCustomClock39);
            this.tabPage7.Controls.Add(this.label88);
            this.tabPage7.Controls.Add(this.label89);
            this.tabPage7.Controls.Add(this.textBoxCustomClock46);
            this.tabPage7.Controls.Add(this.textBoxCustomClock38);
            this.tabPage7.Controls.Add(this.label90);
            this.tabPage7.Controls.Add(this.label91);
            this.tabPage7.Controls.Add(this.textBoxCustomClock45);
            this.tabPage7.Controls.Add(this.textBoxCustomClock37);
            this.tabPage7.Controls.Add(this.textBoxCustomClock44);
            this.tabPage7.Controls.Add(this.textBoxCustomClock36);
            this.tabPage7.Controls.Add(this.textBoxCustomClock43);
            this.tabPage7.Controls.Add(this.textBoxCustomClock35);
            this.tabPage7.Controls.Add(this.textBoxCustomClock42);
            this.tabPage7.Controls.Add(this.textBoxCustomClock34);
            this.tabPage7.Controls.Add(this.textBoxCustomClock41);
            this.tabPage7.Controls.Add(this.textBoxCustomClock33);
            this.tabPage7.Controls.Add(this.textBoxCustomClock40);
            this.tabPage7.Controls.Add(this.textBoxCustomClock32);
            this.tabPage7.Controls.Add(this.label92);
            this.tabPage7.Controls.Add(this.label93);
            this.tabPage7.Controls.Add(this.label94);
            this.tabPage7.Controls.Add(this.label95);
            this.tabPage7.Controls.Add(this.label96);
            this.tabPage7.Controls.Add(this.label97);
            this.tabPage7.Controls.Add(this.label98);
            this.tabPage7.Controls.Add(this.label99);
            this.tabPage7.Controls.Add(this.label100);
            this.tabPage7.Controls.Add(this.label101);
            this.tabPage7.Controls.Add(this.label102);
            this.tabPage7.Controls.Add(this.label103);
            this.tabPage7.Controls.Add(this.textBoxCustomClock31);
            this.tabPage7.Controls.Add(this.textBoxCustomClock23);
            this.tabPage7.Controls.Add(this.textBoxCustomClock15);
            this.tabPage7.Controls.Add(this.textBoxCustomClock7);
            this.tabPage7.Controls.Add(this.label104);
            this.tabPage7.Controls.Add(this.label105);
            this.tabPage7.Controls.Add(this.label106);
            this.tabPage7.Controls.Add(this.label107);
            this.tabPage7.Controls.Add(this.textBoxCustomClock30);
            this.tabPage7.Controls.Add(this.textBoxCustomClock22);
            this.tabPage7.Controls.Add(this.textBoxCustomClock14);
            this.tabPage7.Controls.Add(this.textBoxCustomClock6);
            this.tabPage7.Controls.Add(this.label108);
            this.tabPage7.Controls.Add(this.label109);
            this.tabPage7.Controls.Add(this.label110);
            this.tabPage7.Controls.Add(this.label111);
            this.tabPage7.Controls.Add(this.textBoxCustomClock29);
            this.tabPage7.Controls.Add(this.textBoxCustomClock21);
            this.tabPage7.Controls.Add(this.textBoxCustomClock13);
            this.tabPage7.Controls.Add(this.textBoxCustomClock5);
            this.tabPage7.Controls.Add(this.textBoxCustomClock28);
            this.tabPage7.Controls.Add(this.textBoxCustomClock20);
            this.tabPage7.Controls.Add(this.textBoxCustomClock12);
            this.tabPage7.Controls.Add(this.textBoxCustomClock4);
            this.tabPage7.Controls.Add(this.textBoxCustomClock27);
            this.tabPage7.Controls.Add(this.textBoxCustomClock19);
            this.tabPage7.Controls.Add(this.textBoxCustomClock11);
            this.tabPage7.Controls.Add(this.textBoxCustomClock3);
            this.tabPage7.Controls.Add(this.textBoxCustomClock26);
            this.tabPage7.Controls.Add(this.textBoxCustomClock18);
            this.tabPage7.Controls.Add(this.textBoxCustomClock10);
            this.tabPage7.Controls.Add(this.textBoxCustomClock2);
            this.tabPage7.Controls.Add(this.textBoxCustomClock25);
            this.tabPage7.Controls.Add(this.textBoxCustomClock17);
            this.tabPage7.Controls.Add(this.textBoxCustomClock9);
            this.tabPage7.Controls.Add(this.textBoxCustomClock1);
            this.tabPage7.Controls.Add(this.textBoxCustomClock24);
            this.tabPage7.Controls.Add(this.textBoxCustomClock16);
            this.tabPage7.Controls.Add(this.textBoxCustomClock8);
            this.tabPage7.Controls.Add(this.textBoxCustomClock0);
            this.tabPage7.Controls.Add(this.label125);
            this.tabPage7.Controls.Add(this.label126);
            this.tabPage7.Controls.Add(this.label127);
            this.tabPage7.Controls.Add(this.label128);
            this.tabPage7.Controls.Add(this.label129);
            this.tabPage7.Controls.Add(this.label130);
            this.tabPage7.Controls.Add(this.label131);
            this.tabPage7.Controls.Add(this.label132);
            this.tabPage7.Controls.Add(this.label133);
            this.tabPage7.Controls.Add(this.label134);
            this.tabPage7.Controls.Add(this.label135);
            this.tabPage7.Controls.Add(this.label136);
            this.tabPage7.Controls.Add(this.label137);
            this.tabPage7.Controls.Add(this.label138);
            this.tabPage7.Controls.Add(this.label139);
            this.tabPage7.Controls.Add(this.label140);
            this.tabPage7.Controls.Add(this.label141);
            this.tabPage7.Controls.Add(this.label142);
            this.tabPage7.Controls.Add(this.label143);
            this.tabPage7.Controls.Add(this.label144);
            this.tabPage7.Controls.Add(this.label145);
            this.tabPage7.Controls.Add(this.label146);
            this.tabPage7.Controls.Add(this.label147);
            this.tabPage7.Controls.Add(this.label148);
            this.tabPage7.Controls.Add(this.textBoxCustomClickPerDay);
            this.tabPage7.Controls.Add(this.buttonCustomSystemDistribute);
            this.tabPage7.Location = new Point(4, 0x16);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new Size(0x2fa, 0x13c);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "点击量设置";
            this.tabPage7.UseVisualStyleBackColor = true;
            this.tabPage7.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock47.Location = new Point(0x2a2, 0xfe);
            this.textBoxCustomClock47.Name = "textBoxCustomClock47";
            this.textBoxCustomClock47.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock47.TabIndex = 0x121;
            this.textBoxCustomClock47.Text = "0";
            this.textBoxCustomClock47.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock47.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock39.Location = new Point(0x2a2, 0x92);
            this.textBoxCustomClock39.Name = "textBoxCustomClock39";
            this.textBoxCustomClock39.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock39.TabIndex = 0x11f;
            this.textBoxCustomClock39.Text = "0";
            this.textBoxCustomClock39.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock39.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.label88.AutoSize = true;
            this.label88.Location = new Point(0x290, 0x101);
            this.label88.Name = "label88";
            this.label88.Size = new Size(11, 12);
            this.label88.TabIndex = 290;
            this.label88.Text = "/";
            this.label89.AutoSize = true;
            this.label89.Location = new Point(0x290, 0x95);
            this.label89.Name = "label89";
            this.label89.Size = new Size(11, 12);
            this.label89.TabIndex = 0x120;
            this.label89.Text = "/";
            this.textBoxCustomClock46.Location = new Point(0x24f, 0xfe);
            this.textBoxCustomClock46.Name = "textBoxCustomClock46";
            this.textBoxCustomClock46.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock46.TabIndex = 0x11d;
            this.textBoxCustomClock46.Text = "0";
            this.textBoxCustomClock46.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock46.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock38.Location = new Point(0x24f, 0x92);
            this.textBoxCustomClock38.Name = "textBoxCustomClock38";
            this.textBoxCustomClock38.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock38.TabIndex = 0x11b;
            this.textBoxCustomClock38.Text = "0";
            this.textBoxCustomClock38.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock38.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.label90.AutoSize = true;
            this.label90.Location = new Point(540, 0x101);
            this.label90.Name = "label90";
            this.label90.Size = new Size(0x35, 12);
            this.label90.TabIndex = 0x11e;
            this.label90.Text = "23-0点：";
            this.label91.AutoSize = true;
            this.label91.Location = new Point(0x216, 0x95);
            this.label91.Name = "label91";
            this.label91.Size = new Size(0x3b, 12);
            this.label91.TabIndex = 0x11c;
            this.label91.Text = "19-20点：";
            this.textBoxCustomClock45.Location = new Point(0x2a2, 0xe3);
            this.textBoxCustomClock45.Name = "textBoxCustomClock45";
            this.textBoxCustomClock45.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock45.TabIndex = 0x119;
            this.textBoxCustomClock45.Text = "0";
            this.textBoxCustomClock45.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock45.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock37.Location = new Point(0x2a2, 0x77);
            this.textBoxCustomClock37.Name = "textBoxCustomClock37";
            this.textBoxCustomClock37.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock37.TabIndex = 0x117;
            this.textBoxCustomClock37.Text = "0";
            this.textBoxCustomClock37.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock37.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock44.Location = new Point(0x24f, 0xe3);
            this.textBoxCustomClock44.Name = "textBoxCustomClock44";
            this.textBoxCustomClock44.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock44.TabIndex = 0x115;
            this.textBoxCustomClock44.Text = "0";
            this.textBoxCustomClock44.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock44.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock36.Location = new Point(0x24f, 0x77);
            this.textBoxCustomClock36.Name = "textBoxCustomClock36";
            this.textBoxCustomClock36.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock36.TabIndex = 0x113;
            this.textBoxCustomClock36.Text = "0";
            this.textBoxCustomClock36.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock36.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock43.Location = new Point(0x2a2, 200);
            this.textBoxCustomClock43.Name = "textBoxCustomClock43";
            this.textBoxCustomClock43.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock43.TabIndex = 0x111;
            this.textBoxCustomClock43.Text = "0";
            this.textBoxCustomClock43.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock43.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock35.Location = new Point(0x2a2, 0x5c);
            this.textBoxCustomClock35.Name = "textBoxCustomClock35";
            this.textBoxCustomClock35.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock35.TabIndex = 0x10f;
            this.textBoxCustomClock35.Text = "0";
            this.textBoxCustomClock35.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock35.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock42.Location = new Point(0x24f, 200);
            this.textBoxCustomClock42.Name = "textBoxCustomClock42";
            this.textBoxCustomClock42.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock42.TabIndex = 0x10d;
            this.textBoxCustomClock42.Text = "0";
            this.textBoxCustomClock42.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock42.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock34.Location = new Point(0x24f, 0x5c);
            this.textBoxCustomClock34.Name = "textBoxCustomClock34";
            this.textBoxCustomClock34.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock34.TabIndex = 0x10b;
            this.textBoxCustomClock34.Text = "0";
            this.textBoxCustomClock34.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock34.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock41.Location = new Point(0x2a2, 0xad);
            this.textBoxCustomClock41.Name = "textBoxCustomClock41";
            this.textBoxCustomClock41.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock41.TabIndex = 0x109;
            this.textBoxCustomClock41.Text = "0";
            this.textBoxCustomClock41.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock41.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock33.Location = new Point(0x2a2, 0x41);
            this.textBoxCustomClock33.Name = "textBoxCustomClock33";
            this.textBoxCustomClock33.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock33.TabIndex = 0x107;
            this.textBoxCustomClock33.Text = "0";
            this.textBoxCustomClock33.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock33.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock40.Location = new Point(0x24f, 0xad);
            this.textBoxCustomClock40.Name = "textBoxCustomClock40";
            this.textBoxCustomClock40.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock40.TabIndex = 0x105;
            this.textBoxCustomClock40.Text = "0";
            this.textBoxCustomClock40.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock40.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock32.Location = new Point(0x24f, 0x41);
            this.textBoxCustomClock32.Name = "textBoxCustomClock32";
            this.textBoxCustomClock32.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock32.TabIndex = 0x103;
            this.textBoxCustomClock32.Text = "0";
            this.textBoxCustomClock32.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock32.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.label92.AutoSize = true;
            this.label92.Location = new Point(0x290, 230);
            this.label92.Name = "label92";
            this.label92.Size = new Size(11, 12);
            this.label92.TabIndex = 0x11a;
            this.label92.Text = "/";
            this.label93.AutoSize = true;
            this.label93.Location = new Point(0x290, 0x7a);
            this.label93.Name = "label93";
            this.label93.Size = new Size(11, 12);
            this.label93.TabIndex = 280;
            this.label93.Text = "/";
            this.label94.AutoSize = true;
            this.label94.Location = new Point(0x216, 230);
            this.label94.Name = "label94";
            this.label94.Size = new Size(0x3b, 12);
            this.label94.TabIndex = 0x116;
            this.label94.Text = "22-23点：";
            this.label95.AutoSize = true;
            this.label95.Location = new Point(0x216, 0x7a);
            this.label95.Name = "label95";
            this.label95.Size = new Size(0x3b, 12);
            this.label95.TabIndex = 0x114;
            this.label95.Text = "18-19点：";
            this.label96.AutoSize = true;
            this.label96.Location = new Point(0x290, 0xcb);
            this.label96.Name = "label96";
            this.label96.Size = new Size(11, 12);
            this.label96.TabIndex = 0x112;
            this.label96.Text = "/";
            this.label97.AutoSize = true;
            this.label97.Location = new Point(0x290, 0x5f);
            this.label97.Name = "label97";
            this.label97.Size = new Size(11, 12);
            this.label97.TabIndex = 0x110;
            this.label97.Text = "/";
            this.label98.AutoSize = true;
            this.label98.Location = new Point(0x216, 0xcb);
            this.label98.Name = "label98";
            this.label98.Size = new Size(0x3b, 12);
            this.label98.TabIndex = 270;
            this.label98.Text = "21-22点：";
            this.label99.AutoSize = true;
            this.label99.Location = new Point(0x216, 0x5f);
            this.label99.Name = "label99";
            this.label99.Size = new Size(0x3b, 12);
            this.label99.TabIndex = 0x10c;
            this.label99.Text = "17-18点：";
            this.label100.AutoSize = true;
            this.label100.Location = new Point(0x290, 0xb0);
            this.label100.Name = "label100";
            this.label100.Size = new Size(11, 12);
            this.label100.TabIndex = 0x10a;
            this.label100.Text = "/";
            this.label101.AutoSize = true;
            this.label101.Location = new Point(0x290, 0x44);
            this.label101.Name = "label101";
            this.label101.Size = new Size(11, 12);
            this.label101.TabIndex = 0x108;
            this.label101.Text = "/";
            this.label102.AutoSize = true;
            this.label102.Location = new Point(0x216, 0xb0);
            this.label102.Name = "label102";
            this.label102.Size = new Size(0x3b, 12);
            this.label102.TabIndex = 0x106;
            this.label102.Text = "20-21点：";
            this.label103.AutoSize = true;
            this.label103.Location = new Point(0x216, 0x44);
            this.label103.Name = "label103";
            this.label103.Size = new Size(0x3b, 12);
            this.label103.TabIndex = 260;
            this.label103.Text = "16-17点：";
            this.textBoxCustomClock31.Location = new Point(0x19c, 0xfe);
            this.textBoxCustomClock31.Name = "textBoxCustomClock31";
            this.textBoxCustomClock31.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock31.TabIndex = 0x101;
            this.textBoxCustomClock31.Text = "0";
            this.textBoxCustomClock31.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock31.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock23.Location = new Point(0x19c, 0x92);
            this.textBoxCustomClock23.Name = "textBoxCustomClock23";
            this.textBoxCustomClock23.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock23.TabIndex = 0xff;
            this.textBoxCustomClock23.Text = "0";
            this.textBoxCustomClock23.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock23.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock15.Location = new Point(160, 0xfe);
            this.textBoxCustomClock15.Name = "textBoxCustomClock15";
            this.textBoxCustomClock15.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock15.TabIndex = 0xfd;
            this.textBoxCustomClock15.Text = "0";
            this.textBoxCustomClock15.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock15.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock7.Location = new Point(160, 0x92);
            this.textBoxCustomClock7.Name = "textBoxCustomClock7";
            this.textBoxCustomClock7.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock7.TabIndex = 0xfb;
            this.textBoxCustomClock7.Text = "0";
            this.textBoxCustomClock7.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock7.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.label104.AutoSize = true;
            this.label104.Location = new Point(0x18a, 0x101);
            this.label104.Name = "label104";
            this.label104.Size = new Size(11, 12);
            this.label104.TabIndex = 0x102;
            this.label104.Text = "/";
            this.label105.AutoSize = true;
            this.label105.Location = new Point(0x18a, 0x95);
            this.label105.Name = "label105";
            this.label105.Size = new Size(11, 12);
            this.label105.TabIndex = 0x100;
            this.label105.Text = "/";
            this.label106.AutoSize = true;
            this.label106.Location = new Point(0x8e, 0x101);
            this.label106.Name = "label106";
            this.label106.Size = new Size(11, 12);
            this.label106.TabIndex = 0xfe;
            this.label106.Text = "/";
            this.label107.AutoSize = true;
            this.label107.Location = new Point(0x8e, 0x95);
            this.label107.Name = "label107";
            this.label107.Size = new Size(11, 12);
            this.label107.TabIndex = 0xfc;
            this.label107.Text = "/";
            this.textBoxCustomClock30.Location = new Point(0x149, 0xfe);
            this.textBoxCustomClock30.Name = "textBoxCustomClock30";
            this.textBoxCustomClock30.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock30.TabIndex = 0xf9;
            this.textBoxCustomClock30.Text = "0";
            this.textBoxCustomClock30.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock30.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock22.Location = new Point(0x149, 0x92);
            this.textBoxCustomClock22.Name = "textBoxCustomClock22";
            this.textBoxCustomClock22.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock22.TabIndex = 0xf7;
            this.textBoxCustomClock22.Text = "0";
            this.textBoxCustomClock22.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock22.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock14.Location = new Point(0x4d, 0xfe);
            this.textBoxCustomClock14.Name = "textBoxCustomClock14";
            this.textBoxCustomClock14.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock14.TabIndex = 0xf5;
            this.textBoxCustomClock14.Text = "0";
            this.textBoxCustomClock14.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock14.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock6.Location = new Point(0x4d, 0x92);
            this.textBoxCustomClock6.Name = "textBoxCustomClock6";
            this.textBoxCustomClock6.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock6.TabIndex = 0xf3;
            this.textBoxCustomClock6.Text = "0";
            this.textBoxCustomClock6.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock6.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.label108.AutoSize = true;
            this.label108.Location = new Point(0x110, 0x101);
            this.label108.Name = "label108";
            this.label108.Size = new Size(0x3b, 12);
            this.label108.TabIndex = 250;
            this.label108.Text = "15-16点：";
            this.label109.AutoSize = true;
            this.label109.Location = new Point(0x110, 0x95);
            this.label109.Name = "label109";
            this.label109.Size = new Size(0x3b, 12);
            this.label109.TabIndex = 0xf8;
            this.label109.Text = "11-12点：";
            this.label110.AutoSize = true;
            this.label110.Location = new Point(0x20, 0x101);
            this.label110.Name = "label110";
            this.label110.Size = new Size(0x2f, 12);
            this.label110.TabIndex = 0xf6;
            this.label110.Text = "7-8点：";
            this.label111.AutoSize = true;
            this.label111.Location = new Point(0x20, 0x95);
            this.label111.Name = "label111";
            this.label111.Size = new Size(0x2f, 12);
            this.label111.TabIndex = 0xf4;
            this.label111.Text = "3-4点：";
            this.textBoxCustomClock29.Location = new Point(0x19c, 0xe3);
            this.textBoxCustomClock29.Name = "textBoxCustomClock29";
            this.textBoxCustomClock29.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock29.TabIndex = 0xf1;
            this.textBoxCustomClock29.Text = "0";
            this.textBoxCustomClock29.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock29.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock21.Location = new Point(0x19c, 0x77);
            this.textBoxCustomClock21.Name = "textBoxCustomClock21";
            this.textBoxCustomClock21.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock21.TabIndex = 0xef;
            this.textBoxCustomClock21.Text = "0";
            this.textBoxCustomClock21.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock21.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock13.Location = new Point(160, 0xe3);
            this.textBoxCustomClock13.Name = "textBoxCustomClock13";
            this.textBoxCustomClock13.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock13.TabIndex = 0xed;
            this.textBoxCustomClock13.Text = "0";
            this.textBoxCustomClock13.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock13.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock5.Location = new Point(160, 0x77);
            this.textBoxCustomClock5.Name = "textBoxCustomClock5";
            this.textBoxCustomClock5.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock5.TabIndex = 0xeb;
            this.textBoxCustomClock5.Text = "0";
            this.textBoxCustomClock5.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock5.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock28.Location = new Point(0x149, 0xe3);
            this.textBoxCustomClock28.Name = "textBoxCustomClock28";
            this.textBoxCustomClock28.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock28.TabIndex = 0xe9;
            this.textBoxCustomClock28.Text = "0";
            this.textBoxCustomClock28.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock28.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock20.Location = new Point(0x149, 0x77);
            this.textBoxCustomClock20.Name = "textBoxCustomClock20";
            this.textBoxCustomClock20.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock20.TabIndex = 0xe7;
            this.textBoxCustomClock20.Text = "0";
            this.textBoxCustomClock20.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock20.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock12.Location = new Point(0x4d, 0xe3);
            this.textBoxCustomClock12.Name = "textBoxCustomClock12";
            this.textBoxCustomClock12.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock12.TabIndex = 0xe5;
            this.textBoxCustomClock12.Text = "0";
            this.textBoxCustomClock12.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock12.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock4.Location = new Point(0x4d, 0x77);
            this.textBoxCustomClock4.Name = "textBoxCustomClock4";
            this.textBoxCustomClock4.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock4.TabIndex = 0xe3;
            this.textBoxCustomClock4.Text = "0";
            this.textBoxCustomClock4.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock4.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock27.Location = new Point(0x19c, 200);
            this.textBoxCustomClock27.Name = "textBoxCustomClock27";
            this.textBoxCustomClock27.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock27.TabIndex = 0xe1;
            this.textBoxCustomClock27.Text = "0";
            this.textBoxCustomClock27.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock27.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock19.Location = new Point(0x19c, 0x5c);
            this.textBoxCustomClock19.Name = "textBoxCustomClock19";
            this.textBoxCustomClock19.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock19.TabIndex = 0xdf;
            this.textBoxCustomClock19.Text = "0";
            this.textBoxCustomClock19.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock19.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock11.Location = new Point(160, 200);
            this.textBoxCustomClock11.Name = "textBoxCustomClock11";
            this.textBoxCustomClock11.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock11.TabIndex = 0xdd;
            this.textBoxCustomClock11.Text = "0";
            this.textBoxCustomClock11.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock11.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock3.Location = new Point(160, 0x5c);
            this.textBoxCustomClock3.Name = "textBoxCustomClock3";
            this.textBoxCustomClock3.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock3.TabIndex = 0xdb;
            this.textBoxCustomClock3.Text = "0";
            this.textBoxCustomClock3.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock3.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock26.Location = new Point(0x149, 200);
            this.textBoxCustomClock26.Name = "textBoxCustomClock26";
            this.textBoxCustomClock26.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock26.TabIndex = 0xd9;
            this.textBoxCustomClock26.Text = "0";
            this.textBoxCustomClock26.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock26.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock18.Location = new Point(0x149, 0x5c);
            this.textBoxCustomClock18.Name = "textBoxCustomClock18";
            this.textBoxCustomClock18.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock18.TabIndex = 0xd7;
            this.textBoxCustomClock18.Text = "0";
            this.textBoxCustomClock18.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock18.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock10.Location = new Point(0x4d, 200);
            this.textBoxCustomClock10.Name = "textBoxCustomClock10";
            this.textBoxCustomClock10.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock10.TabIndex = 0xd5;
            this.textBoxCustomClock10.Text = "0";
            this.textBoxCustomClock10.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock10.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock2.Location = new Point(0x4d, 0x5c);
            this.textBoxCustomClock2.Name = "textBoxCustomClock2";
            this.textBoxCustomClock2.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock2.TabIndex = 0xd3;
            this.textBoxCustomClock2.Text = "0";
            this.textBoxCustomClock2.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock2.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock25.Location = new Point(0x19c, 0xad);
            this.textBoxCustomClock25.Name = "textBoxCustomClock25";
            this.textBoxCustomClock25.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock25.TabIndex = 0xd1;
            this.textBoxCustomClock25.Text = "0";
            this.textBoxCustomClock25.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock25.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock17.Location = new Point(0x19c, 0x41);
            this.textBoxCustomClock17.Name = "textBoxCustomClock17";
            this.textBoxCustomClock17.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock17.TabIndex = 0xcf;
            this.textBoxCustomClock17.Text = "0";
            this.textBoxCustomClock17.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock17.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock9.Location = new Point(160, 0xad);
            this.textBoxCustomClock9.Name = "textBoxCustomClock9";
            this.textBoxCustomClock9.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock9.TabIndex = 0xcd;
            this.textBoxCustomClock9.Text = "0";
            this.textBoxCustomClock9.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock9.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock1.Location = new Point(160, 0x41);
            this.textBoxCustomClock1.Name = "textBoxCustomClock1";
            this.textBoxCustomClock1.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock1.TabIndex = 0xcb;
            this.textBoxCustomClock1.Text = "0";
            this.textBoxCustomClock1.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock1.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock24.Location = new Point(0x149, 0xad);
            this.textBoxCustomClock24.Name = "textBoxCustomClock24";
            this.textBoxCustomClock24.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock24.TabIndex = 0xc9;
            this.textBoxCustomClock24.Text = "0";
            this.textBoxCustomClock24.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock24.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock16.Location = new Point(0x149, 0x41);
            this.textBoxCustomClock16.Name = "textBoxCustomClock16";
            this.textBoxCustomClock16.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock16.TabIndex = 0xc7;
            this.textBoxCustomClock16.Text = "0";
            this.textBoxCustomClock16.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock16.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock8.Location = new Point(0x4d, 0xad);
            this.textBoxCustomClock8.Name = "textBoxCustomClock8";
            this.textBoxCustomClock8.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock8.TabIndex = 0xc5;
            this.textBoxCustomClock8.Text = "0";
            this.textBoxCustomClock8.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock8.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.textBoxCustomClock0.Location = new Point(0x4d, 0x41);
            this.textBoxCustomClock0.Name = "textBoxCustomClock0";
            this.textBoxCustomClock0.Size = new Size(0x38, 0x15);
            this.textBoxCustomClock0.TabIndex = 0xc3;
            this.textBoxCustomClock0.Text = "0";
            this.textBoxCustomClock0.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClock0.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.label125.AutoSize = true;
            this.label125.Location = new Point(0x18a, 230);
            this.label125.Name = "label125";
            this.label125.Size = new Size(11, 12);
            this.label125.TabIndex = 0xf2;
            this.label125.Text = "/";
            this.label126.AutoSize = true;
            this.label126.Location = new Point(0x18a, 0x7a);
            this.label126.Name = "label126";
            this.label126.Size = new Size(11, 12);
            this.label126.TabIndex = 240;
            this.label126.Text = "/";
            this.label127.AutoSize = true;
            this.label127.Location = new Point(0x8e, 230);
            this.label127.Name = "label127";
            this.label127.Size = new Size(11, 12);
            this.label127.TabIndex = 0xee;
            this.label127.Text = "/";
            this.label128.AutoSize = true;
            this.label128.Location = new Point(0x8e, 0x7a);
            this.label128.Name = "label128";
            this.label128.Size = new Size(11, 12);
            this.label128.TabIndex = 0xec;
            this.label128.Text = "/";
            this.label129.AutoSize = true;
            this.label129.Location = new Point(0x110, 230);
            this.label129.Name = "label129";
            this.label129.Size = new Size(0x3b, 12);
            this.label129.TabIndex = 0xea;
            this.label129.Text = "14-15点：";
            this.label130.AutoSize = true;
            this.label130.Location = new Point(0x110, 0x7a);
            this.label130.Name = "label130";
            this.label130.Size = new Size(0x3b, 12);
            this.label130.TabIndex = 0xe8;
            this.label130.Text = "10-11点：";
            this.label131.AutoSize = true;
            this.label131.Location = new Point(0x20, 230);
            this.label131.Name = "label131";
            this.label131.Size = new Size(0x2f, 12);
            this.label131.TabIndex = 230;
            this.label131.Text = "6-7点：";
            this.label132.AutoSize = true;
            this.label132.Location = new Point(0x20, 0x7a);
            this.label132.Name = "label132";
            this.label132.Size = new Size(0x2f, 12);
            this.label132.TabIndex = 0xe4;
            this.label132.Text = "2-3点：";
            this.label133.AutoSize = true;
            this.label133.Location = new Point(0x18a, 0xcb);
            this.label133.Name = "label133";
            this.label133.Size = new Size(11, 12);
            this.label133.TabIndex = 0xe2;
            this.label133.Text = "/";
            this.label134.AutoSize = true;
            this.label134.Location = new Point(0x18a, 0x5f);
            this.label134.Name = "label134";
            this.label134.Size = new Size(11, 12);
            this.label134.TabIndex = 0xe0;
            this.label134.Text = "/";
            this.label135.AutoSize = true;
            this.label135.Location = new Point(0x8e, 0xcb);
            this.label135.Name = "label135";
            this.label135.Size = new Size(11, 12);
            this.label135.TabIndex = 0xde;
            this.label135.Text = "/";
            this.label136.AutoSize = true;
            this.label136.Location = new Point(0x8e, 0x5f);
            this.label136.Name = "label136";
            this.label136.Size = new Size(11, 12);
            this.label136.TabIndex = 220;
            this.label136.Text = "/";
            this.label137.AutoSize = true;
            this.label137.Location = new Point(0x110, 0xcb);
            this.label137.Name = "label137";
            this.label137.Size = new Size(0x3b, 12);
            this.label137.TabIndex = 0xda;
            this.label137.Text = "13-14点：";
            this.label138.AutoSize = true;
            this.label138.Location = new Point(0x116, 0x5f);
            this.label138.Name = "label138";
            this.label138.Size = new Size(0x35, 12);
            this.label138.TabIndex = 0xd8;
            this.label138.Text = "9-10点：";
            this.label139.AutoSize = true;
            this.label139.Location = new Point(0x20, 0xcb);
            this.label139.Name = "label139";
            this.label139.Size = new Size(0x2f, 12);
            this.label139.TabIndex = 0xd6;
            this.label139.Text = "5-6点：";
            this.label140.AutoSize = true;
            this.label140.Location = new Point(0x20, 0x5f);
            this.label140.Name = "label140";
            this.label140.Size = new Size(0x2f, 12);
            this.label140.TabIndex = 0xd4;
            this.label140.Text = "1-2点：";
            this.label141.AutoSize = true;
            this.label141.Location = new Point(0x18a, 0xb0);
            this.label141.Name = "label141";
            this.label141.Size = new Size(11, 12);
            this.label141.TabIndex = 210;
            this.label141.Text = "/";
            this.label142.AutoSize = true;
            this.label142.Location = new Point(0x18a, 0x44);
            this.label142.Name = "label142";
            this.label142.Size = new Size(11, 12);
            this.label142.TabIndex = 0xd0;
            this.label142.Text = "/";
            this.label143.AutoSize = true;
            this.label143.Location = new Point(0x8e, 0xb0);
            this.label143.Name = "label143";
            this.label143.Size = new Size(11, 12);
            this.label143.TabIndex = 0xce;
            this.label143.Text = "/";
            this.label144.AutoSize = true;
            this.label144.Location = new Point(0x8e, 0x44);
            this.label144.Name = "label144";
            this.label144.Size = new Size(11, 12);
            this.label144.TabIndex = 0xcc;
            this.label144.Text = "/";
            this.label145.AutoSize = true;
            this.label145.Location = new Point(0x110, 0xb0);
            this.label145.Name = "label145";
            this.label145.Size = new Size(0x3b, 12);
            this.label145.TabIndex = 0xca;
            this.label145.Text = "12-13点：";
            this.label146.AutoSize = true;
            this.label146.Location = new Point(0x11c, 0x44);
            this.label146.Name = "label146";
            this.label146.Size = new Size(0x2f, 12);
            this.label146.TabIndex = 200;
            this.label146.Text = "8-9点：";
            this.label147.AutoSize = true;
            this.label147.Location = new Point(0x20, 0xb0);
            this.label147.Name = "label147";
            this.label147.Size = new Size(0x2f, 12);
            this.label147.TabIndex = 0xc6;
            this.label147.Text = "4-5点：";
            this.label148.AutoSize = true;
            this.label148.Location = new Point(0x20, 0x44);
            this.label148.Name = "label148";
            this.label148.Size = new Size(0x2f, 12);
            this.label148.TabIndex = 0xc4;
            this.label148.Text = "0-1点：";
            this.textBoxCustomClickPerDay.Location = new Point(0x13b, 0x1b);
            this.textBoxCustomClickPerDay.Name = "textBoxCustomClickPerDay";
            this.textBoxCustomClickPerDay.Size = new Size(0x47, 0x15);
            this.textBoxCustomClickPerDay.TabIndex = 0xc2;
            this.textBoxCustomClickPerDay.Text = "200";
            this.textBoxCustomClickPerDay.TextChanged += new EventHandler(this.LimitNumRange);
            this.textBoxCustomClickPerDay.KeyPress += new KeyPressEventHandler(this.AllowNumKeyPress);
            this.buttonCustomSystemDistribute.Location = new Point(0x188, 0x19);
            this.buttonCustomSystemDistribute.Name = "buttonCustomSystemDistribute";
            this.buttonCustomSystemDistribute.Size = new Size(0x4b, 0x17);
            this.buttonCustomSystemDistribute.TabIndex = 0xc1;
            this.buttonCustomSystemDistribute.Text = "系统分配";
            this.buttonCustomSystemDistribute.UseVisualStyleBackColor = true;
            this.buttonCustomSystemDistribute.Click += new EventHandler(this.buttonSystemDistribute_Click);
            this.tabPage8.Controls.Add(this.checkBoxCustomOtherProvince);
            this.tabPage8.Controls.Add(this.checkBoxCustomAoMen);
            this.tabPage8.Controls.Add(this.checkBoxCustomXiangGan);
            this.tabPage8.Controls.Add(this.checkBoxCustomGuangXi);
            this.tabPage8.Controls.Add(this.checkBoxCustomNingXia);
            this.tabPage8.Controls.Add(this.checkBoxCustomXinJiang);
            this.tabPage8.Controls.Add(this.checkBoxCustomTianJin);
            this.tabPage8.Controls.Add(this.checkBoxCustomXiZang);
            this.tabPage8.Controls.Add(this.checkBoxCustomBeiJing);
            this.tabPage8.Controls.Add(this.checkBoxCustomNeiMengGu);
            this.tabPage8.Controls.Add(this.checkBoxCustomChongQing);
            this.tabPage8.Controls.Add(this.checkBoxCustomTaiWan);
            this.tabPage8.Controls.Add(this.checkBoxCustomShangHai);
            this.tabPage8.Controls.Add(this.checkBoxCustomQingHai);
            this.tabPage8.Controls.Add(this.checkBoxCustomGanSu);
            this.tabPage8.Controls.Add(this.checkBoxCustomShanXi);
            this.tabPage8.Controls.Add(this.checkBoxCustomGuangDong);
            this.tabPage8.Controls.Add(this.checkBoxCustomYunNan);
            this.tabPage8.Controls.Add(this.checkBoxCustomHuNan);
            this.tabPage8.Controls.Add(this.checkBoxCustomGuiZhou);
            this.tabPage8.Controls.Add(this.checkBoxCustomSiChuang);
            this.tabPage8.Controls.Add(this.checkBoxCustomHuBei);
            this.tabPage8.Controls.Add(this.checkBoxCustomHaiNan);
            this.tabPage8.Controls.Add(this.checkBoxCustomHeNan);
            this.tabPage8.Controls.Add(this.checkBoxCustomSanDong);
            this.tabPage8.Controls.Add(this.checkBoxCustomJiangXi);
            this.tabPage8.Controls.Add(this.checkBoxCustomHeiLongjiang);
            this.tabPage8.Controls.Add(this.checkBoxCustomFuJian);
            this.tabPage8.Controls.Add(this.checkBoxCustomJiLin);
            this.tabPage8.Controls.Add(this.checkBoxCustomAnHui);
            this.tabPage8.Controls.Add(this.checkBoxCustomZheJiang);
            this.tabPage8.Controls.Add(this.checkBoxCustomLiaoNing);
            this.tabPage8.Controls.Add(this.checkBoxCustomJiangSu);
            this.tabPage8.Controls.Add(this.checkBoxCustomSanXi);
            this.tabPage8.Controls.Add(this.checkBoxCustomHeBei);
            this.tabPage8.Controls.Add(this.buttonCustomAreaSelectNone);
            this.tabPage8.Controls.Add(this.buttonCustomAreaSelectAll);
            this.tabPage8.Location = new Point(4, 0x16);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new Size(0x2fa, 0x13c);
            this.tabPage8.TabIndex = 3;
            this.tabPage8.Text = "点击地区设置";
            this.tabPage8.UseVisualStyleBackColor = true;
            this.checkBoxCustomOtherProvince.AutoSize = true;
            this.checkBoxCustomOtherProvince.Checked = true;
            this.checkBoxCustomOtherProvince.CheckState = CheckState.Checked;
            this.checkBoxCustomOtherProvince.Location = new Point(660, 0xf9);
            this.checkBoxCustomOtherProvince.Name = "checkBoxCustomOtherProvince";
            this.checkBoxCustomOtherProvince.Size = new Size(0x30, 0x10);
            this.checkBoxCustomOtherProvince.TabIndex = 0x43;
            this.checkBoxCustomOtherProvince.Text = "其他";
            this.checkBoxCustomOtherProvince.UseVisualStyleBackColor = true;
            this.checkBoxCustomAoMen.AutoSize = true;
            this.checkBoxCustomAoMen.Checked = true;
            this.checkBoxCustomAoMen.CheckState = CheckState.Checked;
            this.checkBoxCustomAoMen.Location = new Point(660, 210);
            this.checkBoxCustomAoMen.Name = "checkBoxCustomAoMen";
            this.checkBoxCustomAoMen.Size = new Size(0x30, 0x10);
            this.checkBoxCustomAoMen.TabIndex = 0x44;
            this.checkBoxCustomAoMen.Text = "澳门";
            this.checkBoxCustomAoMen.UseVisualStyleBackColor = true;
            this.checkBoxCustomXiangGan.AutoSize = true;
            this.checkBoxCustomXiangGan.Checked = true;
            this.checkBoxCustomXiangGan.CheckState = CheckState.Checked;
            this.checkBoxCustomXiangGan.Location = new Point(660, 0xab);
            this.checkBoxCustomXiangGan.Name = "checkBoxCustomXiangGan";
            this.checkBoxCustomXiangGan.Size = new Size(0x30, 0x10);
            this.checkBoxCustomXiangGan.TabIndex = 0x40;
            this.checkBoxCustomXiangGan.Text = "香港";
            this.checkBoxCustomXiangGan.UseVisualStyleBackColor = true;
            this.checkBoxCustomGuangXi.AutoSize = true;
            this.checkBoxCustomGuangXi.Checked = true;
            this.checkBoxCustomGuangXi.CheckState = CheckState.Checked;
            this.checkBoxCustomGuangXi.Location = new Point(660, 0x84);
            this.checkBoxCustomGuangXi.Name = "checkBoxCustomGuangXi";
            this.checkBoxCustomGuangXi.Size = new Size(0x30, 0x10);
            this.checkBoxCustomGuangXi.TabIndex = 0x42;
            this.checkBoxCustomGuangXi.Text = "广西";
            this.checkBoxCustomGuangXi.UseVisualStyleBackColor = true;
            this.checkBoxCustomNingXia.AutoSize = true;
            this.checkBoxCustomNingXia.Checked = true;
            this.checkBoxCustomNingXia.CheckState = CheckState.Checked;
            this.checkBoxCustomNingXia.Location = new Point(660, 0x5d);
            this.checkBoxCustomNingXia.Name = "checkBoxCustomNingXia";
            this.checkBoxCustomNingXia.Size = new Size(0x30, 0x10);
            this.checkBoxCustomNingXia.TabIndex = 0x41;
            this.checkBoxCustomNingXia.Text = "宁夏";
            this.checkBoxCustomNingXia.UseVisualStyleBackColor = true;
            this.checkBoxCustomXinJiang.AutoSize = true;
            this.checkBoxCustomXinJiang.Checked = true;
            this.checkBoxCustomXinJiang.CheckState = CheckState.Checked;
            this.checkBoxCustomXinJiang.Location = new Point(0x22f, 0xf9);
            this.checkBoxCustomXinJiang.Name = "checkBoxCustomXinJiang";
            this.checkBoxCustomXinJiang.Size = new Size(0x30, 0x10);
            this.checkBoxCustomXinJiang.TabIndex = 0x3a;
            this.checkBoxCustomXinJiang.Text = "新疆";
            this.checkBoxCustomXinJiang.UseVisualStyleBackColor = true;
            this.checkBoxCustomTianJin.AutoSize = true;
            this.checkBoxCustomTianJin.Checked = true;
            this.checkBoxCustomTianJin.CheckState = CheckState.Checked;
            this.checkBoxCustomTianJin.Location = new Point(0x1ca, 0xf9);
            this.checkBoxCustomTianJin.Name = "checkBoxCustomTianJin";
            this.checkBoxCustomTianJin.Size = new Size(0x30, 0x10);
            this.checkBoxCustomTianJin.TabIndex = 0x39;
            this.checkBoxCustomTianJin.Text = "天津";
            this.checkBoxCustomTianJin.UseVisualStyleBackColor = true;
            this.checkBoxCustomXiZang.AutoSize = true;
            this.checkBoxCustomXiZang.Checked = true;
            this.checkBoxCustomXiZang.CheckState = CheckState.Checked;
            this.checkBoxCustomXiZang.Location = new Point(0x22f, 210);
            this.checkBoxCustomXiZang.Name = "checkBoxCustomXiZang";
            this.checkBoxCustomXiZang.Size = new Size(0x30, 0x10);
            this.checkBoxCustomXiZang.TabIndex = 0x37;
            this.checkBoxCustomXiZang.Text = "西藏";
            this.checkBoxCustomXiZang.UseVisualStyleBackColor = true;
            this.checkBoxCustomBeiJing.AutoSize = true;
            this.checkBoxCustomBeiJing.Checked = true;
            this.checkBoxCustomBeiJing.CheckState = CheckState.Checked;
            this.checkBoxCustomBeiJing.Location = new Point(0x1ca, 210);
            this.checkBoxCustomBeiJing.Name = "checkBoxCustomBeiJing";
            this.checkBoxCustomBeiJing.Size = new Size(0x30, 0x10);
            this.checkBoxCustomBeiJing.TabIndex = 0x38;
            this.checkBoxCustomBeiJing.Text = "北京";
            this.checkBoxCustomBeiJing.UseVisualStyleBackColor = true;
            this.checkBoxCustomNeiMengGu.AutoSize = true;
            this.checkBoxCustomNeiMengGu.Checked = true;
            this.checkBoxCustomNeiMengGu.CheckState = CheckState.Checked;
            this.checkBoxCustomNeiMengGu.Location = new Point(0x22f, 0xab);
            this.checkBoxCustomNeiMengGu.Name = "checkBoxCustomNeiMengGu";
            this.checkBoxCustomNeiMengGu.Size = new Size(60, 0x10);
            this.checkBoxCustomNeiMengGu.TabIndex = 0x36;
            this.checkBoxCustomNeiMengGu.Text = "内蒙古";
            this.checkBoxCustomNeiMengGu.UseVisualStyleBackColor = true;
            this.checkBoxCustomChongQing.AutoSize = true;
            this.checkBoxCustomChongQing.Checked = true;
            this.checkBoxCustomChongQing.CheckState = CheckState.Checked;
            this.checkBoxCustomChongQing.Location = new Point(0x22f, 0x84);
            this.checkBoxCustomChongQing.Name = "checkBoxCustomChongQing";
            this.checkBoxCustomChongQing.Size = new Size(0x30, 0x10);
            this.checkBoxCustomChongQing.TabIndex = 0x3f;
            this.checkBoxCustomChongQing.Text = "重庆";
            this.checkBoxCustomChongQing.UseVisualStyleBackColor = true;
            this.checkBoxCustomTaiWan.AutoSize = true;
            this.checkBoxCustomTaiWan.Checked = true;
            this.checkBoxCustomTaiWan.CheckState = CheckState.Checked;
            this.checkBoxCustomTaiWan.Location = new Point(0x1ca, 0xab);
            this.checkBoxCustomTaiWan.Name = "checkBoxCustomTaiWan";
            this.checkBoxCustomTaiWan.Size = new Size(0x30, 0x10);
            this.checkBoxCustomTaiWan.TabIndex = 0x3e;
            this.checkBoxCustomTaiWan.Text = "台湾";
            this.checkBoxCustomTaiWan.UseVisualStyleBackColor = true;
            this.checkBoxCustomShangHai.AutoSize = true;
            this.checkBoxCustomShangHai.Checked = true;
            this.checkBoxCustomShangHai.CheckState = CheckState.Checked;
            this.checkBoxCustomShangHai.Location = new Point(0x22f, 0x5d);
            this.checkBoxCustomShangHai.Name = "checkBoxCustomShangHai";
            this.checkBoxCustomShangHai.Size = new Size(0x30, 0x10);
            this.checkBoxCustomShangHai.TabIndex = 60;
            this.checkBoxCustomShangHai.Text = "上海";
            this.checkBoxCustomShangHai.UseVisualStyleBackColor = true;
            this.checkBoxCustomQingHai.AutoSize = true;
            this.checkBoxCustomQingHai.Checked = true;
            this.checkBoxCustomQingHai.CheckState = CheckState.Checked;
            this.checkBoxCustomQingHai.Location = new Point(0x1ca, 0x84);
            this.checkBoxCustomQingHai.Name = "checkBoxCustomQingHai";
            this.checkBoxCustomQingHai.Size = new Size(0x30, 0x10);
            this.checkBoxCustomQingHai.TabIndex = 0x3b;
            this.checkBoxCustomQingHai.Text = "青海";
            this.checkBoxCustomQingHai.UseVisualStyleBackColor = true;
            this.checkBoxCustomGanSu.AutoSize = true;
            this.checkBoxCustomGanSu.Checked = true;
            this.checkBoxCustomGanSu.CheckState = CheckState.Checked;
            this.checkBoxCustomGanSu.Location = new Point(0x1ca, 0x5d);
            this.checkBoxCustomGanSu.Name = "checkBoxCustomGanSu";
            this.checkBoxCustomGanSu.Size = new Size(0x30, 0x10);
            this.checkBoxCustomGanSu.TabIndex = 0x3d;
            this.checkBoxCustomGanSu.Text = "甘肃";
            this.checkBoxCustomGanSu.UseVisualStyleBackColor = true;
            this.checkBoxCustomShanXi.AutoSize = true;
            this.checkBoxCustomShanXi.Checked = true;
            this.checkBoxCustomShanXi.CheckState = CheckState.Checked;
            this.checkBoxCustomShanXi.Location = new Point(0x165, 0xf9);
            this.checkBoxCustomShanXi.Name = "checkBoxCustomShanXi";
            this.checkBoxCustomShanXi.Size = new Size(0x30, 0x10);
            this.checkBoxCustomShanXi.TabIndex = 50;
            this.checkBoxCustomShanXi.Text = "陕西";
            this.checkBoxCustomShanXi.UseVisualStyleBackColor = true;
            this.checkBoxCustomGuangDong.AutoSize = true;
            this.checkBoxCustomGuangDong.Checked = true;
            this.checkBoxCustomGuangDong.CheckState = CheckState.Checked;
            this.checkBoxCustomGuangDong.Location = new Point(0x100, 0xf9);
            this.checkBoxCustomGuangDong.Name = "checkBoxCustomGuangDong";
            this.checkBoxCustomGuangDong.Size = new Size(0x30, 0x10);
            this.checkBoxCustomGuangDong.TabIndex = 0x31;
            this.checkBoxCustomGuangDong.Text = "广东";
            this.checkBoxCustomGuangDong.UseVisualStyleBackColor = true;
            this.checkBoxCustomYunNan.AutoSize = true;
            this.checkBoxCustomYunNan.Checked = true;
            this.checkBoxCustomYunNan.CheckState = CheckState.Checked;
            this.checkBoxCustomYunNan.Location = new Point(0x165, 210);
            this.checkBoxCustomYunNan.Name = "checkBoxCustomYunNan";
            this.checkBoxCustomYunNan.Size = new Size(0x30, 0x10);
            this.checkBoxCustomYunNan.TabIndex = 0x33;
            this.checkBoxCustomYunNan.Text = "云南";
            this.checkBoxCustomYunNan.UseVisualStyleBackColor = true;
            this.checkBoxCustomHuNan.AutoSize = true;
            this.checkBoxCustomHuNan.Checked = true;
            this.checkBoxCustomHuNan.CheckState = CheckState.Checked;
            this.checkBoxCustomHuNan.Location = new Point(0x100, 210);
            this.checkBoxCustomHuNan.Name = "checkBoxCustomHuNan";
            this.checkBoxCustomHuNan.Size = new Size(0x30, 0x10);
            this.checkBoxCustomHuNan.TabIndex = 0x35;
            this.checkBoxCustomHuNan.Text = "湖南";
            this.checkBoxCustomHuNan.UseVisualStyleBackColor = true;
            this.checkBoxCustomGuiZhou.AutoSize = true;
            this.checkBoxCustomGuiZhou.Checked = true;
            this.checkBoxCustomGuiZhou.CheckState = CheckState.Checked;
            this.checkBoxCustomGuiZhou.Location = new Point(0x165, 0xab);
            this.checkBoxCustomGuiZhou.Name = "checkBoxCustomGuiZhou";
            this.checkBoxCustomGuiZhou.Size = new Size(0x30, 0x10);
            this.checkBoxCustomGuiZhou.TabIndex = 0x34;
            this.checkBoxCustomGuiZhou.Text = "贵州";
            this.checkBoxCustomGuiZhou.UseVisualStyleBackColor = true;
            this.checkBoxCustomSiChuang.AutoSize = true;
            this.checkBoxCustomSiChuang.Checked = true;
            this.checkBoxCustomSiChuang.CheckState = CheckState.Checked;
            this.checkBoxCustomSiChuang.Location = new Point(0x165, 0x84);
            this.checkBoxCustomSiChuang.Name = "checkBoxCustomSiChuang";
            this.checkBoxCustomSiChuang.Size = new Size(0x30, 0x10);
            this.checkBoxCustomSiChuang.TabIndex = 0x2d;
            this.checkBoxCustomSiChuang.Text = "四川";
            this.checkBoxCustomSiChuang.UseVisualStyleBackColor = true;
            this.checkBoxCustomHuBei.AutoSize = true;
            this.checkBoxCustomHuBei.Checked = true;
            this.checkBoxCustomHuBei.CheckState = CheckState.Checked;
            this.checkBoxCustomHuBei.Location = new Point(0x100, 0xab);
            this.checkBoxCustomHuBei.Name = "checkBoxCustomHuBei";
            this.checkBoxCustomHuBei.Size = new Size(0x30, 0x10);
            this.checkBoxCustomHuBei.TabIndex = 0x2c;
            this.checkBoxCustomHuBei.Text = "湖北";
            this.checkBoxCustomHuBei.UseVisualStyleBackColor = true;
            this.checkBoxCustomHaiNan.AutoSize = true;
            this.checkBoxCustomHaiNan.Checked = true;
            this.checkBoxCustomHaiNan.CheckState = CheckState.Checked;
            this.checkBoxCustomHaiNan.Location = new Point(0x165, 0x5d);
            this.checkBoxCustomHaiNan.Name = "checkBoxCustomHaiNan";
            this.checkBoxCustomHaiNan.Size = new Size(0x30, 0x10);
            this.checkBoxCustomHaiNan.TabIndex = 0x2e;
            this.checkBoxCustomHaiNan.Text = "海南";
            this.checkBoxCustomHaiNan.UseVisualStyleBackColor = true;
            this.checkBoxCustomHeNan.AutoSize = true;
            this.checkBoxCustomHeNan.Checked = true;
            this.checkBoxCustomHeNan.CheckState = CheckState.Checked;
            this.checkBoxCustomHeNan.Location = new Point(0x100, 0x84);
            this.checkBoxCustomHeNan.Name = "checkBoxCustomHeNan";
            this.checkBoxCustomHeNan.Size = new Size(0x30, 0x10);
            this.checkBoxCustomHeNan.TabIndex = 0x30;
            this.checkBoxCustomHeNan.Text = "河南";
            this.checkBoxCustomHeNan.UseVisualStyleBackColor = true;
            this.checkBoxCustomSanDong.AutoSize = true;
            this.checkBoxCustomSanDong.Checked = true;
            this.checkBoxCustomSanDong.CheckState = CheckState.Checked;
            this.checkBoxCustomSanDong.Location = new Point(0x100, 0x5d);
            this.checkBoxCustomSanDong.Name = "checkBoxCustomSanDong";
            this.checkBoxCustomSanDong.Size = new Size(0x30, 0x10);
            this.checkBoxCustomSanDong.TabIndex = 0x2f;
            this.checkBoxCustomSanDong.Text = "山东";
            this.checkBoxCustomSanDong.UseVisualStyleBackColor = true;
            this.checkBoxCustomJiangXi.AutoSize = true;
            this.checkBoxCustomJiangXi.Checked = true;
            this.checkBoxCustomJiangXi.CheckState = CheckState.Checked;
            this.checkBoxCustomJiangXi.Location = new Point(0x9b, 0xf9);
            this.checkBoxCustomJiangXi.Name = "checkBoxCustomJiangXi";
            this.checkBoxCustomJiangXi.Size = new Size(0x30, 0x10);
            this.checkBoxCustomJiangXi.TabIndex = 0x25;
            this.checkBoxCustomJiangXi.Text = "江西";
            this.checkBoxCustomJiangXi.UseVisualStyleBackColor = true;
            this.checkBoxCustomHeiLongjiang.AutoSize = true;
            this.checkBoxCustomHeiLongjiang.Checked = true;
            this.checkBoxCustomHeiLongjiang.CheckState = CheckState.Checked;
            this.checkBoxCustomHeiLongjiang.Location = new Point(0x36, 0xf9);
            this.checkBoxCustomHeiLongjiang.Name = "checkBoxCustomHeiLongjiang";
            this.checkBoxCustomHeiLongjiang.Size = new Size(60, 0x10);
            this.checkBoxCustomHeiLongjiang.TabIndex = 0x26;
            this.checkBoxCustomHeiLongjiang.Text = "黑龙江";
            this.checkBoxCustomHeiLongjiang.UseVisualStyleBackColor = true;
            this.checkBoxCustomFuJian.AutoSize = true;
            this.checkBoxCustomFuJian.Checked = true;
            this.checkBoxCustomFuJian.CheckState = CheckState.Checked;
            this.checkBoxCustomFuJian.Location = new Point(0x9b, 210);
            this.checkBoxCustomFuJian.Name = "checkBoxCustomFuJian";
            this.checkBoxCustomFuJian.Size = new Size(0x30, 0x10);
            this.checkBoxCustomFuJian.TabIndex = 0x24;
            this.checkBoxCustomFuJian.Text = "福建";
            this.checkBoxCustomFuJian.UseVisualStyleBackColor = true;
            this.checkBoxCustomJiLin.AutoSize = true;
            this.checkBoxCustomJiLin.Checked = true;
            this.checkBoxCustomJiLin.CheckState = CheckState.Checked;
            this.checkBoxCustomJiLin.Location = new Point(0x36, 210);
            this.checkBoxCustomJiLin.Name = "checkBoxCustomJiLin";
            this.checkBoxCustomJiLin.Size = new Size(0x30, 0x10);
            this.checkBoxCustomJiLin.TabIndex = 0x22;
            this.checkBoxCustomJiLin.Text = "吉林";
            this.checkBoxCustomJiLin.UseVisualStyleBackColor = true;
            this.checkBoxCustomAnHui.AutoSize = true;
            this.checkBoxCustomAnHui.Checked = true;
            this.checkBoxCustomAnHui.CheckState = CheckState.Checked;
            this.checkBoxCustomAnHui.Location = new Point(0x9b, 0xab);
            this.checkBoxCustomAnHui.Name = "checkBoxCustomAnHui";
            this.checkBoxCustomAnHui.Size = new Size(0x30, 0x10);
            this.checkBoxCustomAnHui.TabIndex = 0x23;
            this.checkBoxCustomAnHui.Text = "安徽";
            this.checkBoxCustomAnHui.UseVisualStyleBackColor = true;
            this.checkBoxCustomZheJiang.AutoSize = true;
            this.checkBoxCustomZheJiang.Checked = true;
            this.checkBoxCustomZheJiang.CheckState = CheckState.Checked;
            this.checkBoxCustomZheJiang.Location = new Point(0x9b, 0x84);
            this.checkBoxCustomZheJiang.Name = "checkBoxCustomZheJiang";
            this.checkBoxCustomZheJiang.Size = new Size(0x30, 0x10);
            this.checkBoxCustomZheJiang.TabIndex = 0x2a;
            this.checkBoxCustomZheJiang.Text = "浙江";
            this.checkBoxCustomZheJiang.UseVisualStyleBackColor = true;
            this.checkBoxCustomLiaoNing.AutoSize = true;
            this.checkBoxCustomLiaoNing.Checked = true;
            this.checkBoxCustomLiaoNing.CheckState = CheckState.Checked;
            this.checkBoxCustomLiaoNing.Location = new Point(0x36, 0xab);
            this.checkBoxCustomLiaoNing.Name = "checkBoxCustomLiaoNing";
            this.checkBoxCustomLiaoNing.Size = new Size(0x30, 0x10);
            this.checkBoxCustomLiaoNing.TabIndex = 0x2b;
            this.checkBoxCustomLiaoNing.Text = "辽宁";
            this.checkBoxCustomLiaoNing.UseVisualStyleBackColor = true;
            this.checkBoxCustomJiangSu.AutoSize = true;
            this.checkBoxCustomJiangSu.Checked = true;
            this.checkBoxCustomJiangSu.CheckState = CheckState.Checked;
            this.checkBoxCustomJiangSu.Location = new Point(0x9b, 0x5d);
            this.checkBoxCustomJiangSu.Name = "checkBoxCustomJiangSu";
            this.checkBoxCustomJiangSu.Size = new Size(0x30, 0x10);
            this.checkBoxCustomJiangSu.TabIndex = 0x29;
            this.checkBoxCustomJiangSu.Text = "江苏";
            this.checkBoxCustomJiangSu.UseVisualStyleBackColor = true;
            this.checkBoxCustomSanXi.AutoSize = true;
            this.checkBoxCustomSanXi.Checked = true;
            this.checkBoxCustomSanXi.CheckState = CheckState.Checked;
            this.checkBoxCustomSanXi.Location = new Point(0x36, 0x84);
            this.checkBoxCustomSanXi.Name = "checkBoxCustomSanXi";
            this.checkBoxCustomSanXi.Size = new Size(0x30, 0x10);
            this.checkBoxCustomSanXi.TabIndex = 0x27;
            this.checkBoxCustomSanXi.Text = "山西";
            this.checkBoxCustomSanXi.UseVisualStyleBackColor = true;
            this.checkBoxCustomHeBei.AutoSize = true;
            this.checkBoxCustomHeBei.Checked = true;
            this.checkBoxCustomHeBei.CheckState = CheckState.Checked;
            this.checkBoxCustomHeBei.Location = new Point(0x36, 0x5d);
            this.checkBoxCustomHeBei.Name = "checkBoxCustomHeBei";
            this.checkBoxCustomHeBei.Size = new Size(0x30, 0x10);
            this.checkBoxCustomHeBei.TabIndex = 40;
            this.checkBoxCustomHeBei.Text = "河北";
            this.checkBoxCustomHeBei.UseVisualStyleBackColor = true;
            this.buttonCustomAreaSelectNone.Location = new Point(0x196, 0x23);
            this.buttonCustomAreaSelectNone.Name = "buttonCustomAreaSelectNone";
            this.buttonCustomAreaSelectNone.Size = new Size(0x4b, 0x17);
            this.buttonCustomAreaSelectNone.TabIndex = 0x20;
            this.buttonCustomAreaSelectNone.Text = "全不选";
            this.buttonCustomAreaSelectNone.UseVisualStyleBackColor = true;
            this.buttonCustomAreaSelectNone.Click += new EventHandler(this.buttonAreaSelectNone_Click);
            this.buttonCustomAreaSelectAll.Location = new Point(0x132, 0x23);
            this.buttonCustomAreaSelectAll.Name = "buttonCustomAreaSelectAll";
            this.buttonCustomAreaSelectAll.Size = new Size(0x4b, 0x17);
            this.buttonCustomAreaSelectAll.TabIndex = 0x21;
            this.buttonCustomAreaSelectAll.Text = "全选";
            this.buttonCustomAreaSelectAll.UseVisualStyleBackColor = true;
            this.buttonCustomAreaSelectAll.Click += new EventHandler(this.buttonAreaSelectAll_Click);
            this.panel5.Controls.Add(this.textBoxTaskNote);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.labelCustomTotalScore);
            this.panel5.Controls.Add(this.buttonCustomTemplateSaveTask);
            this.panel5.Controls.Add(this.buttonCustonTemplateAdd);
            this.panel5.Controls.Add(this.buttonCustonTemplateTest);
            this.panel5.Controls.Add(this.buttonClearTaskItems);
            this.panel5.Dock = DockStyle.Bottom;
            this.panel5.Location = new Point(3, 0x18e);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(0x31e, 0x4b);
            this.panel5.TabIndex = 0x12;
            this.textBoxTaskNote.ImeMode = ImeMode.On;
            this.textBoxTaskNote.Location = new Point(0x114, 12);
            this.textBoxTaskNote.Name = "textBoxTaskNote";
            this.textBoxTaskNote.Size = new Size(0xdb, 0x15);
            this.textBoxTaskNote.TabIndex = 1;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xd5, 15);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "任务备注：";
            this.labelCustomTotalScore.AutoSize = true;
            this.labelCustomTotalScore.Location = new Point(30, 15);
            this.labelCustomTotalScore.Name = "labelCustomTotalScore";
            this.labelCustomTotalScore.Size = new Size(0x65, 12);
            this.labelCustomTotalScore.TabIndex = 14;
            this.labelCustomTotalScore.Text = "任务共消耗金币: ";
            this.buttonCustomTemplateSaveTask.Anchor = AnchorStyles.None;
            this.buttonCustomTemplateSaveTask.Location = new Point(0x14c, 0x2a);
            this.buttonCustomTemplateSaveTask.Name = "buttonCustomTemplateSaveTask";
            this.buttonCustomTemplateSaveTask.Size = new Size(0x37, 0x17);
            this.buttonCustomTemplateSaveTask.TabIndex = 13;
            this.buttonCustomTemplateSaveTask.Text = "保存";
            this.buttonCustomTemplateSaveTask.UseVisualStyleBackColor = true;
            this.buttonCustomTemplateSaveTask.Click += new EventHandler(this.buttonCustonTemplateSaveClick);
            this.buttonCustonTemplateAdd.Anchor = AnchorStyles.None;
            this.buttonCustonTemplateAdd.Location = new Point(0x102, 0x2a);
            this.buttonCustonTemplateAdd.Name = "buttonCustonTemplateAdd";
            this.buttonCustonTemplateAdd.Size = new Size(0x37, 0x17);
            this.buttonCustonTemplateAdd.TabIndex = 11;
            this.buttonCustonTemplateAdd.Text = "添加";
            this.buttonCustonTemplateAdd.UseVisualStyleBackColor = true;
            this.buttonCustonTemplateAdd.Click += new EventHandler(this.buttonCustonTemplateAddClick);
            this.buttonCustonTemplateTest.Anchor = AnchorStyles.None;
            this.buttonCustonTemplateTest.Location = new Point(480, 0x2a);
            this.buttonCustonTemplateTest.Name = "buttonCustonTemplateTest";
            this.buttonCustonTemplateTest.Size = new Size(0x37, 0x17);
            this.buttonCustonTemplateTest.TabIndex = 12;
            this.buttonCustonTemplateTest.Text = "测试";
            this.buttonCustonTemplateTest.UseVisualStyleBackColor = true;
            this.buttonCustonTemplateTest.Click += new EventHandler(this.buttonCustonTemplateTestClick);
            this.buttonClearTaskItems.Anchor = AnchorStyles.None;
            this.buttonClearTaskItems.Location = new Point(0x196, 0x2a);
            this.buttonClearTaskItems.Name = "buttonClearTaskItems";
            this.buttonClearTaskItems.Size = new Size(0x37, 0x17);
            this.buttonClearTaskItems.TabIndex = 12;
            this.buttonClearTaskItems.Text = "清空";
            this.buttonClearTaskItems.UseVisualStyleBackColor = true;
            this.buttonClearTaskItems.Click += new EventHandler(this.buttonClearTask);
            this.tabPageTaskManage.Controls.Add(this.dataGridViewTasks);
            this.tabPageTaskManage.Location = new Point(4, 0x22);
            this.tabPageTaskManage.Name = "tabPageTaskManage";
            this.tabPageTaskManage.Padding = new Padding(5);
            this.tabPageTaskManage.Size = new Size(0x324, 0x1dc);
            this.tabPageTaskManage.TabIndex = 2;
            this.tabPageTaskManage.Text = "任务管理";
            this.tabPageTaskManage.UseVisualStyleBackColor = true;
            this.dataGridViewTasks.AllowUserToAddRows = false;
            this.dataGridViewTasks.AllowUserToDeleteRows = false;
            this.dataGridViewTasks.AllowUserToResizeRows = false;
            style5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style5.BackColor = SystemColors.Control;
            style5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style5.ForeColor = SystemColors.WindowText;
            style5.SelectionBackColor = SystemColors.Highlight;
            style5.SelectionForeColor = SystemColors.HighlightText;
            style5.WrapMode = DataGridViewTriState.True;
            this.dataGridViewTasks.ColumnHeadersDefaultCellStyle = style5;
            this.dataGridViewTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTasks.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column7, this.Column13, this.Column2, this.Column5, this.Column14, this.Column16, this.Column6 });
            this.dataGridViewTasks.Dock = DockStyle.Fill;
            this.dataGridViewTasks.Location = new Point(5, 5);
            this.dataGridViewTasks.MultiSelect = false;
            this.dataGridViewTasks.Name = "dataGridViewTasks";
            this.dataGridViewTasks.ReadOnly = true;
            this.dataGridViewTasks.RowHeadersVisible = false;
            this.dataGridViewTasks.RowTemplate.Height = 0x17;
            this.dataGridViewTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTasks.Size = new Size(0x31a, 0x1d2);
            this.dataGridViewTasks.TabIndex = 1;
            this.dataGridViewTasks.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.dataGridViewTasks_CellMouseDoubleClick);
            this.dataGridViewTasks.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dataGridViewTasks_CellMouseUp);
            this.dataGridViewTasks.CellPainting += new DataGridViewCellPaintingEventHandler(this.dataGridViewTasks_CellPainting);
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 80;
            this.Column7.HeaderText = "备注";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 150;
            this.Column13.HeaderText = "金币";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column13.Width = 80;
            style6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = style6;
            this.Column2.HeaderText = "状态";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 60;
            this.Column5.HeaderText = "总点击量";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 80;
            this.Column14.HeaderText = "今日点击量";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column14.Width = 90;
            this.Column16.HeaderText = "昨天点击量";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column16.Width = 90;
            this.Column6.HeaderText = "日限点击";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 80;
            this.toolStrip1.ImageScalingSize = new Size(0x24, 0x24);
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripButtonStartTask, this.toolStripButtonOpenBrowser });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x336, 0x2b);
            this.toolStrip1.TabIndex = 13;
            this.toolStripButtonStartTask.Image = (Image) manager.GetObject("toolStripButtonStartTask.Image");
            this.toolStripButtonStartTask.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonStartTask.Name = "toolStripButtonStartTask";
            this.toolStripButtonStartTask.Size = new Size(40, 40);
            this.toolStripButtonStartTask.Click += new EventHandler(this.buttonStartMyTasks_Click);
            this.toolStripButtonOpenBrowser.Image = (Image) manager.GetObject("toolStripButtonOpenBrowser.Image");
            this.toolStripButtonOpenBrowser.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonOpenBrowser.Name = "toolStripButtonOpenBrowser";
            this.toolStripButtonOpenBrowser.Size = new Size(40, 40);
            this.toolStripButtonOpenBrowser.Click += new EventHandler(this.buttonOpenTaskForm_Click);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0x2b);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(5, 0, 5, 5);
            this.panel1.Size = new Size(0x336, 0x207);
            this.panel1.TabIndex = 10;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x336, 0x2b);
            this.panel2.TabIndex = 0x11;
            this.label3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x292, 9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x6b, 12);
            this.label3.TabIndex = 0x11;
            this.label3.Text = "客服QQ:2747481971";
            this.linkLabel1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new Point(0x30b, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new Size(0x1d, 12);
            this.linkLabel1.TabIndex = 0x10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "官网";
            this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            this.notifyIcon1.Icon = (Icon) manager.GetObject("notifyIcon1.Icon");
            this.notifyIcon1.Text = "点击精灵";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new MouseEventHandler(this.notifyIcon1_MouseClick);
            this.contextMenuStrip2.Items.AddRange(new ToolStripItem[] { this.ToolStripMenuItem5, this.ToolStripMenuItem6, this.ToolStripMenuItem7, this.ToolStripMenuItem8 });
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new Size(0x65, 0x5c);
            this.ToolStripMenuItem5.Name = "ToolStripMenuItem5";
            this.ToolStripMenuItem5.Size = new Size(100, 0x16);
            this.ToolStripMenuItem5.Text = "设置";
            this.ToolStripMenuItem5.Click += new EventHandler(this.TaskEditClick);
            this.ToolStripMenuItem6.Name = "ToolStripMenuItem6";
            this.ToolStripMenuItem6.Size = new Size(100, 0x16);
            this.ToolStripMenuItem6.Text = "删除";
            this.ToolStripMenuItem6.Click += new EventHandler(this.TaskDeleteClick);
            this.ToolStripMenuItem7.Name = "ToolStripMenuItem7";
            this.ToolStripMenuItem7.Size = new Size(100, 0x16);
            this.ToolStripMenuItem7.Text = "暂停";
            this.ToolStripMenuItem7.Click += new EventHandler(this.TaskStopClick);
            this.ToolStripMenuItem8.Name = "ToolStripMenuItem8";
            this.ToolStripMenuItem8.Size = new Size(100, 0x16);
            this.ToolStripMenuItem8.Text = "运行";
            this.ToolStripMenuItem8.Click += new EventHandler(this.TaskRunClick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x336, 0x232);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ManageForm";
            base.FormClosing += new FormClosingEventHandler(this.FormMainClosing);
            base.Load += new EventHandler(this.FormEnter);
            base.SizeChanged += new EventHandler(this.ManageForm_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageUser.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPageCommonTemplate.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridViewTemplate).EndInit();
            this.tabPageCustonTemplate.ResumeLayout(false);
            this.tabControl4.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupPressKey.ResumeLayout(false);
            this.groupBoxFresh.ResumeLayout(false);
            this.groupBoxWait.ResumeLayout(false);
            this.groupBoxWait.PerformLayout();
            this.groupBoxDeepClick.ResumeLayout(false);
            this.groupBoxDeepClick.PerformLayout();
            this.groupBoxFindPage.ResumeLayout(false);
            this.groupBoxFindPage.PerformLayout();
            this.groupBoxClickLink.ResumeLayout(false);
            this.groupBoxClickLink.PerformLayout();
            this.groupBoxClickChecked.ResumeLayout(false);
            this.groupBoxClickChecked.PerformLayout();
            this.groupBoxClickRadio.ResumeLayout(false);
            this.groupBoxClickRadio.PerformLayout();
            this.groupBoxClickButton.ResumeLayout(false);
            this.groupBoxClickButton.PerformLayout();
            this.groupBoxInputText.ResumeLayout(false);
            this.groupBoxInputText.PerformLayout();
            this.groupBoxNavigate.ResumeLayout(false);
            this.groupBoxNavigate.PerformLayout();
            this.groupBoxClearCookie.ResumeLayout(false);
            this.groupBoxClearCookie.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPageTaskManage.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridViewTasks).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void InitNetManager()
        {
            this._loginUtil = new client.LoginUtil();
            this._getNetTaskTime = DateTime.Now;
        }

        private void InitSetting()
        {
            this.Text = "点击精灵1.0.0.37";
            this.textBoxFindMaxPage.Text = FINDMAXPAGE.ToString();
            this.comboBoxTaskBrowserCount.SelectedIndex = this._maxTaskThreadsCount - 1;
            this.InitialisePanelSize();
            this.ShowPanels();
            this.SetAllAreaClicks(true);
            this.ResetHourClicksText();
        }

        private void InitTaskManager()
        {
            this.LoadTemplates();
            this._taskTimer.Tick += new EventHandler(this.TaskThreadFunc);
            this._taskTimer.Interval = THREADINTERVAL;
            this._taskTimer.Enabled = false;
            this._taskTimer.Start();
        }

        public bool IsConnected()
        {
            return this._serverSession.Authed;
        }

        public bool IsConnecting()
        {
            if (!this._gateSession.IsConnecting())
            {
                return this._serverSession.IsConnecting();
            }
            return true;
        }

        private void LimitNumRange(object sender, EventArgs e)
        {
            int num = 0;
            if (sender.Equals(this.textBoxDeepClickTimes))
            {
                if (!string.IsNullOrEmpty(this.textBoxDeepClickTimes.Text))
                {
                    num = WindowUtil.StringToInt(this.textBoxDeepClickTimes.Text);
                    if (num > 10)
                    {
                        num = 10;
                    }
                }
                this.textBoxDeepClickTimes.Text = num.ToString();
            }
            else if (sender.Equals(this.textBoxDeepClickWaitTime))
            {
                if (!string.IsNullOrEmpty(this.textBoxDeepClickWaitTime.Text))
                {
                    num = WindowUtil.StringToInt(this.textBoxDeepClickWaitTime.Text);
                    if (num > 300)
                    {
                        num = 300;
                    }
                }
                this.textBoxDeepClickWaitTime.Text = num.ToString();
            }
            else if (sender.Equals(this.textBoxWaitTime))
            {
                if (!string.IsNullOrEmpty(this.textBoxWaitTime.Text))
                {
                    num = WindowUtil.StringToInt(this.textBoxWaitTime.Text);
                    if (num > 300)
                    {
                        num = 300;
                    }
                }
                this.textBoxWaitTime.Text = num.ToString();
            }
            else
            {
                TextBox box = sender as TextBox;
                if (box != null)
                {
                    if (!string.IsNullOrEmpty(box.Text))
                    {
                        num = WindowUtil.StringToInt(box.Text);
                        if (num > 0x186a0)
                        {
                            num = 0x186a0;
                        }
                    }
                    box.Text = num.ToString();
                }
            }
            int tabIndex = 0;
            if (this.tabControl1.SelectedIndex == 2)
            {
                tabIndex = 1;
            }
            this.CalculateScore(tabIndex);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.djjlseo.com/" + this.GetUrlSessionStr());
        }

        private void listBoxTaskItemLine_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((e.Index >= 0) && (e.Index < this.listBoxTaskItemLine.Items.Count))
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                float num = (e.Bounds.Height - e.Font.Height) / 2;
                RectangleF layoutRectangle = new RectangleF((float) e.Bounds.X, e.Bounds.Y + num, (float) e.Bounds.Width, (float) e.Font.Height);
                e.Graphics.DrawString(this.listBoxTaskItemLine.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), layoutRectangle);
            }
        }

        private void listBoxTaskItems_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((e.Index >= 0) && (e.Index < this.listBoxTaskItems.Items.Count))
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                float num = (e.Bounds.Height - e.Font.Height) / 2;
                RectangleF layoutRectangle = new RectangleF((float) e.Bounds.X, e.Bounds.Y + num, (float) e.Bounds.Width, (float) e.Font.Height);
                e.Graphics.DrawString(this.listBoxTaskItems.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), layoutRectangle);
            }
        }

        private void listBoxTemplateCategory_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((e.Index >= 0) && (e.Index < this.listBoxTemplateCategory.Items.Count))
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                float num = (e.Bounds.Height - e.Font.Height) / 2;
                RectangleF layoutRectangle = new RectangleF((float) e.Bounds.X, e.Bounds.Y + num, (float) e.Bounds.Width, (float) e.Font.Height);
                e.Graphics.DrawString(this.listBoxTemplateCategory.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), layoutRectangle);
            }
        }

        private void listBoxTemplateCategory_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 20;
        }

        private void ListBoxTemplateCategoryClick(object sender, MouseEventArgs e)
        {
            int index = this.listBoxTemplateCategory.IndexFromPoint(new Point(e.X, e.Y));
            if ((index >= 0) && (index < this.listBoxTemplateCategory.Items.Count))
            {
                this.listBoxTemplateCategory.ContextMenu = null;
                this.ReloadTemplate(index);
            }
        }

        private void LoadSetting()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load("setting.xml");
                foreach (XmlNode node2 in document.SelectSingleNode("root").ChildNodes)
                {
                    if (node2.Name.Equals("ip"))
                    {
                        this._setIp = node2.InnerText;
                    }
                    if (node2.Name.Equals("browserThreadCount") && !string.IsNullOrEmpty(node2.InnerText))
                    {
                        int num = WindowUtil.StringToInt(node2.InnerText);
                        if ((num > 0) && (num <= 5))
                        {
                            this._maxTaskThreadsCount = num;
                        }
                    }
                    if (node2.Name.Equals("runningTask") && !string.IsNullOrEmpty(node2.InnerText))
                    {
                        this._runningTask = WindowUtil.StringToInt(node2.InnerText);
                    }
                }
                if (this._runningTask != 0)
                {
                    this.SaveSetting(false);
                }
            }
            catch (Exception)
            {
            }
        }

        private void LoadTemplates()
        {
            int num = 0;
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load("template.xml");
                foreach (XmlNode node2 in document.SelectSingleNode("root").ChildNodes)
                {
                    if (node2.Name.Equals("tasks"))
                    {
                        foreach (XmlNode node3 in node2.ChildNodes)
                        {
                            if (node3.Name.Equals("task"))
                            {
                                MyTask template = new MyTask();
                                foreach (XmlNode node4 in node3.ChildNodes)
                                {
                                    if (node4.Name.Equals("name"))
                                    {
                                        template.Name = node4.InnerText;
                                    }
                                    else
                                    {
                                        if (node4.Name.Equals("category"))
                                        {
                                            template.Category = node4.InnerText;
                                            continue;
                                        }
                                        if (node4.Name.Equals("note"))
                                        {
                                            template.Note = node4.InnerText;
                                            continue;
                                        }
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
                                                TaskInfo task = new TaskInfo(s, innerText, str3, str4, str5);
                                                template.Insert(task, -1);
                                            }
                                        }
                                    }
                                }
                                if (template._taskItems.Count > 0)
                                {
                                    this._engineManager.Add(template.Category, template);
                                }
                            }
                        }
                        continue;
                    }
                }
            }
            catch (Exception)
            {
                num = -1;
            }
            if (num == 0)
            {
                foreach (EngineCaterogy caterogy in this._engineManager.Caterogy)
                {
                    if (string.IsNullOrEmpty(caterogy.Name))
                    {
                        this.listBoxTemplateCategory.Items.Add("未分类");
                    }
                    else
                    {
                        this.listBoxTemplateCategory.Items.Add(caterogy.Name);
                    }
                }
                this.ReloadTemplate(0);
            }
        }

        private void ManageForm_SizeChanged(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Minimized)
            {
                base.Hide();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            base.Show();
            base.WindowState = FormWindowState.Normal;
            base.ShowInTaskbar = true;
            base.Activate();
        }

        private void OnlyShowPanel(int index)
        {
            for (int i = 0; i < this._panels.Count; i++)
            {
                if (index == i)
                {
                    ((PanelInfo) this._panels[i])._show = true;
                }
                else
                {
                    ((PanelInfo) this._panels[i])._show = false;
                }
            }
        }

        private void ParseMessage(int opcode, ref string data)
        {
            if (opcode == 1)
            {
                this.ProcessCompleteTask(ref data);
            }
            else if (opcode == 2)
            {
                this.StopTasks();
            }
        }

        private void ProcessCompleteTask(ref string data)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                uint id = 0;
                int taskStep = 0;
                bool completed = false;
                string errorString = string.Empty;
                document.LoadXml(data);
                foreach (XmlNode node2 in document.SelectSingleNode("root").ChildNodes)
                {
                    if (node2.Name.Equals("taskid"))
                    {
                        if (!string.IsNullOrEmpty(node2.InnerText))
                        {
                            id = WindowUtil.StringToUint(node2.InnerText);
                        }
                    }
                    else
                    {
                        if (node2.Name.Equals("isCompleted"))
                        {
                            if (!string.IsNullOrEmpty(node2.InnerText))
                            {
                                completed = Convert.ToBoolean(node2.InnerText);
                            }
                            continue;
                        }
                        if (node2.Name.Equals("taskStep"))
                        {
                            if (!string.IsNullOrEmpty(node2.InnerText))
                            {
                                taskStep = WindowUtil.StringToInt(node2.InnerText);
                            }
                            continue;
                        }
                        if (node2.Name.Equals("errorString") && !string.IsNullOrEmpty(node2.InnerText))
                        {
                            errorString = node2.InnerText;
                        }
                    }
                }
                this.CompleteTask(id, completed, taskStep, errorString);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        public void ProcessGateData()
        {
            WorldPacket recvPacket = null;
            while ((recvPacket = this._gateSession.GateSocket.GetPacket()) != null)
            {
                GateMsg opcode = (GateMsg) recvPacket.Opcode;
                try
                {
                    switch (opcode)
                    {
                        case GateMsg.GATE_SMSG_AUTH:
                            this.HandleGateAuth(recvPacket);
                            break;

                        case GateMsg.GATE_SMSG_PLAYERREG:
                            this.HandleGatePlayerReg(recvPacket);
                            break;

                        case GateMsg.GATE_SMSG_PLAYERREGUSER:
                            this.HandleGatePlayerRegUser(recvPacket);
                            break;

                        case GateMsg.GATE_SMSG_CHECKACCOUNT:
                            this.HandleCheckAccount(recvPacket);
                            break;
                    }
                    this.ResetNetTime();
                    continue;
                }
                catch (Exception exception)
                {
                    Logger.Error(exception);
                    continue;
                }
            }
        }

        public void ProcessServerData()
        {
            WorldPacket recvPacket = null;
            while ((recvPacket = this._serverSession.ServerSocket.GetPacket()) != null)
            {
                ServerMsg opcode = (ServerMsg) recvPacket.Opcode;
                try
                {
                    switch (opcode)
                    {
                        case ServerMsg.SERVER_SMSG_STOPTASK:
                            this.HandleStopTask(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_RESUMETASK:
                            this.HandleResumeTask(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_REMOVETASK:
                            this.HandleRemoveTask(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_COMPLETETASK:
                            this.HandleCompleteTask(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_CHECKACCOUNT:
                            this.HandleServerCheckAccount(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_EDITACCOUNT:
                            this.HandleEditAccount(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_GETTASK:
                            this.HandleGetTask(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_AUTH:
                            this.HandleServerAuth(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_ACCOUNTINFO:
                            this.HandleAccountInfo(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_MYTASK:
                            this.HandleMyTask(recvPacket);
                            break;

                        case ServerMsg.SERVER_SMSG_GETLOGININFO:
                            this._isInitialisedMyTask = true;
                            break;
                    }
                    this.UpdatePackets(opcode);
                    this.ResetNetTime();
                    continue;
                }
                catch (Exception exception)
                {
                    Logger.Error(exception);
                    continue;
                }
            }
        }

        public void ReloadTask(bool resetCurIndex = false)
        {
            string item = string.Empty;
            this.listBoxTaskItems.Items.Clear();
            this.listBoxTaskItemLine.Items.Clear();
            for (int i = 0; i < this._task._taskItems.Count; i++)
            {
                int num;
                TaskInfo info = (TaskInfo) this._task._taskItems[i];
                switch (WindowUtil.StringToInt(info._param1))
                {
                    case 0:
                        item = "等待 " + info._param2 + " 秒";
                        goto Label_05ED;

                    case 1:
                    {
                        num = 0;
                        if (!string.IsNullOrEmpty(info._param5))
                        {
                            num = WindowUtil.StringToInt(info._param5);
                        }
                        string[] strArray2 = new string[] { "在第", (num + 1).ToString(), "个文本框 \"", info._param2, "\" 输入 \"", info._param3, "\"" };
                        item = string.Concat(strArray2);
                        goto Label_05ED;
                    }
                    case 2:
                    {
                        num = 0;
                        if (!string.IsNullOrEmpty(info._param4))
                        {
                            num = WindowUtil.StringToInt(info._param4);
                        }
                        string[] strArray3 = new string[] { "点击第", (num + 1).ToString(), "个按钮 \"", info._param2, "\"" };
                        item = string.Concat(strArray3);
                        goto Label_05ED;
                    }
                    case 3:
                        num = 0;
                        if (!string.IsNullOrEmpty(info._param5))
                        {
                            num = WindowUtil.StringToInt(info._param5);
                        }
                        if (string.IsNullOrEmpty(info._param3))
                        {
                            string[] strArray4 = new string[] { "点击第", (num + 1).ToString(), "个链接 \"", info._param2, "\"" };
                            item = string.Concat(strArray4);
                        }
                        else
                        {
                            string[] strArray5 = new string[] { "点击第", (num + 1).ToString(), "个关键字为 \"", info._param3, "\" 的链接 \"", info._param2, "\"" };
                            item = string.Concat(strArray5);
                        }
                        goto Label_05ED;

                    case 4:
                        item = "输入网址 \"" + info._param2 + "\"";
                        if (!string.IsNullOrEmpty(info._param3))
                        {
                            item = item + "，来路: \"" + info._param3 + "\"";
                        }
                        goto Label_05ED;

                    case 5:
                        item = "深入点击关键字为" + info._param4 + " 的超链接 " + info._param2 + " 次，点击前等待时间为 " + info._param3 + " 秒";
                        goto Label_05ED;

                    case 6:
                        if (!(info._param2 == "1"))
                        {
                            break;
                        }
                        item = "清除历史记录";
                        goto Label_05ED;

                    case 7:
                    case 13:
                    case 15:
                    case 0x11:
                    case 0x13:
                    case 0x15:
                    case 0x17:
                    case 0x19:
                    case 0x1b:
                    {
                        num = 0;
                        string[] strArray = info._param4.Split(new char[] { ',' });
                        int num3 = 0;
                        if (strArray.Length >= 2)
                        {
                            num = WindowUtil.StringToInt(strArray[1]);
                        }
                        if (strArray.Length >= 3)
                        {
                            num3 = WindowUtil.StringToInt(strArray[2]);
                        }
                        if (string.IsNullOrEmpty(info._param5))
                        {
                            string[] strArray7 = new string[] { "查找第", (num + 1).ToString(), "个 \"", info._param2, "\" 下一页第", (num3 + 1).ToString(), "个 \"", info._param3, "\" 最多查找 ", strArray[0], "次" };
                            item = string.Concat(strArray7);
                        }
                        else
                        {
                            string[] strArray8 = new string[] { "查找关键字为 \"", info._param5, "\" 的 \"", info._param2, "\" 下一页第", (num3 + 1).ToString(), "个 \"", info._param3, "\" 最多查找 ", strArray[0], "次" };
                            item = string.Concat(strArray8);
                        }
                        goto Label_05ED;
                    }
                    case 9:
                        item = "刷新当前页面";
                        goto Label_05ED;

                    case 10:
                        item = "输入回车键";
                        goto Label_05ED;

                    case 11:
                    {
                        num = 0;
                        if (!string.IsNullOrEmpty(info._param4))
                        {
                            num = WindowUtil.StringToInt(info._param4);
                        }
                        string[] strArray9 = new string[] { "点击第", (num + 1).ToString(), "个单选框 \"", info._param2, "\"" };
                        item = string.Concat(strArray9);
                        goto Label_05ED;
                    }
                    case 12:
                    {
                        num = 0;
                        if (!string.IsNullOrEmpty(info._param4))
                        {
                            num = WindowUtil.StringToInt(info._param4);
                        }
                        string[] strArray10 = new string[] { "点击第", (num + 1).ToString(), "个复选框 \"", info._param2, "\"" };
                        item = string.Concat(strArray10);
                        goto Label_05ED;
                    }
                    default:
                        goto Label_05ED;
                }
                if (info._param2 == "2")
                {
                    item = "清除Cookie";
                }
                else if (info._param2 == "8")
                {
                    item = "清除临时文件";
                }
                else if (info._param2 == "48")
                {
                    item = "清除表单和密码";
                }
                else
                {
                    item = "清除所有记录";
                }
            Label_05ED:
                this.listBoxTaskItems.Items.Add(item);
                this.listBoxTaskItemLine.Items.Add(Convert.ToString((int) (i + 1)));
            }
            this.listBoxTaskItems.Items.Add("");
            this.listBoxTaskItemLine.Items.Add(Convert.ToString((int) (this._task._taskItems.Count + 1)));
            if (resetCurIndex || (this._curTaskIndex >= this._task._taskItems.Count))
            {
                this._curTaskIndex = this._task._taskItems.Count;
            }
            this.ReloadTaskSetting();
            this.listBoxTaskItems.SelectedIndex = this._curTaskIndex;
            if (resetCurIndex)
            {
                this.textBoxTaskNote.Text = this._task.Note;
            }
            this.CalculateScore(1);
        }

        public void ReloadTasks()
        {
            foreach (DataGridViewRow row in (IEnumerable) this.dataGridViewTasks.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Dispose();
                }
                row.Dispose();
            }
            this.dataGridViewTasks.Rows.Clear();
            for (int i = 0; i < this.Tasks.Count; i++)
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                DataGridViewTextBoxCell dataGridViewCell = null;
                MyTask task = this.Tasks[i] as MyTask;
                dataGridViewCell = new DataGridViewTextBoxCell {
                    Value = task._id.ToString()
                };
                dataGridViewRow.Cells.Add(dataGridViewCell);
                dataGridViewCell = new DataGridViewTextBoxCell {
                    Value = task.Note
                };
                dataGridViewRow.Cells.Add(dataGridViewCell);
                dataGridViewCell = new DataGridViewTextBoxCell {
                    Value = task.CalculateScore()
                };
                dataGridViewRow.Cells.Add(dataGridViewCell);
                dataGridViewCell = new DataGridViewTextBoxCell {
                    Value = task._isRun ? "运行中" : "暂停中"
                };
                dataGridViewRow.Cells.Add(dataGridViewCell);
                dataGridViewCell = new DataGridViewTextBoxCell {
                    Value = task._validClickCount + task._invalidClickCount
                };
                dataGridViewRow.Cells.Add(dataGridViewCell);
                dataGridViewCell = new DataGridViewTextBoxCell {
                    Value = task._todayValidClickCount + task._todayInvalidClickCount
                };
                dataGridViewRow.Cells.Add(dataGridViewCell);
                dataGridViewCell = new DataGridViewTextBoxCell {
                    Value = task._yesterdayValidClickCount + task._yesterdayInvalidClickCount
                };
                dataGridViewRow.Cells.Add(dataGridViewCell);
                dataGridViewCell = new DataGridViewTextBoxCell {
                    Value = task._totalDayClicks
                };
                dataGridViewRow.Cells.Add(dataGridViewCell);
                this.dataGridViewTasks.Rows.Add(dataGridViewRow);
            }
        }

        private void ReloadTaskSetting()
        {
            if (this._task != null)
            {
                if (this._task._AreaClicks == null)
                {
                    this.SetAllAreaClicks(true);
                }
                else
                {
                    this.SetAreaClicksText(this._task);
                }
                if (this._task._HourClicks == null)
                {
                    this.ResetHourClicksText();
                }
                else
                {
                    this.SetHourClicksText(this._task);
                }
            }
        }

        public void ReloadTemplate(int index = 0)
        {
            EngineCaterogy caterogy = this._engineManager.GetCaterogy(index);
            if (caterogy != null)
            {
                this._engineManager.SelectedIndex = index;
                foreach (DataGridViewRow row in (IEnumerable) this.dataGridViewTemplate.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Dispose();
                    }
                    row.Dispose();
                }
                this.dataGridViewTemplate.Rows.Clear();
                foreach (MyTask task in caterogy.Templates)
                {
                    DataGridViewRow dataGridViewRow = new DataGridViewRow();
                    DataGridViewTextBoxCell dataGridViewCell = null;
                    dataGridViewCell = new DataGridViewTextBoxCell();
                    if (string.IsNullOrEmpty(task.Name))
                    {
                        dataGridViewCell.Value = "未命名";
                    }
                    else
                    {
                        dataGridViewCell.Value = task.Name;
                    }
                    dataGridViewRow.Cells.Add(dataGridViewCell);
                    dataGridViewCell = new DataGridViewTextBoxCell {
                        Value = task.CalculateScore()
                    };
                    dataGridViewRow.Cells.Add(dataGridViewCell);
                    dataGridViewCell = new DataGridViewTextBoxCell {
                        Value = task.Note
                    };
                    dataGridViewRow.Cells.Add(dataGridViewCell);
                    this.dataGridViewTemplate.Rows.Add(dataGridViewRow);
                }
            }
        }

        public void RemoveTask(uint taskID)
        {
            int index = 0;
            index = 0;
            while (index < this._tasks.Count)
            {
                if (((MyTask) this._tasks[index])._id == taskID)
                {
                    break;
                }
                index++;
            }
            if (index < this._tasks.Count)
            {
                this._tasks.RemoveAt(index);
                if (index == this._modifyTaskIndex)
                {
                    this._modifyTaskIndex = -1;
                    this._task = null;
                    this._task = new MyTask();
                    this.ReloadTask(false);
                }
                else if (index < this._modifyTaskIndex)
                {
                    this._modifyTaskIndex--;
                }
                this.UpdateAccount();
            }
            this.ReloadTasks();
        }

        public void ResetCustonTemplate()
        {
            this.textBoxWebSite.Text = "";
            this.textBoxInputBoxName.Text = "";
            this.textBoxInputText.Text = "";
            this.textBoxButtonID.Text = "";
            this.textBoxLink.Text = "";
            this.textBoxLinkKeyword.Text = "";
            this.textBoxWaitTime.Text = "";
            this.textBoxFindPage.Text = "";
            this.textBoxFindKeyword.Text = "";
            this.textBoxFindPageNext.Text = "";
            this.textBoxFindMaxPage.Text = FINDPAGE.ToString();
            for (int i = 0; i < this._panels.Count; i++)
            {
                PanelInfo info = (PanelInfo) this._panels[i];
                info._show = false;
            }
            this.ShowPanels();
        }

        private void ResetHourClicksText()
        {
            this.textBoxCustomClickPerDay.Text = CLICKPERDAY.ToString();
            this.SystemDistribute(CLICKPERDAY);
        }

        public void ResetLoginUser()
        {
            this._tasks.Clear();
            this._task = new MyTask();
            this._modifyTaskIndex = -1;
            this.IsInitialisedMyTask = false;
            this.ReloadTask(false);
            this.ReloadTasks();
        }

        public void ResetNetTime()
        {
            this._getNetTaskTime = DateTime.Now;
        }

        public void ResetTemplateComponent()
        {
            this.ResetCustonTemplate();
            this.ReloadTask(true);
            this.ReloadTasks();
        }

        public void ResumeTask(uint taskID)
        {
            for (int i = 0; i < this._tasks.Count; i++)
            {
                if (((MyTask) this._tasks[i])._id == taskID)
                {
                    ((MyTask) this._tasks[i])._isRun = true;
                    this.ReloadTasks();
                    return;
                }
            }
        }

        private void RunTask(MyTask task)
        {
            this.SendRunTask(task);
        }

        public void SaveSetting(bool saveRunTask = false)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlElement newChild = document.CreateElement("root");
                if (!string.IsNullOrEmpty(this._setIp))
                {
                    XmlElement element2 = document.CreateElement("ip");
                    element2.InnerText = this._setIp;
                    newChild.AppendChild(element2);
                }
                if (this._maxTaskThreadsCount != 1)
                {
                    XmlElement element3 = document.CreateElement("browserThreadCount");
                    element3.InnerText = this._maxTaskThreadsCount.ToString();
                    newChild.AppendChild(element3);
                }
                if ((this._runningTask != 0) && saveRunTask)
                {
                    XmlElement element4 = document.CreateElement("runningTask");
                    element4.InnerText = this._runningTask.ToString();
                    newChild.AppendChild(element4);
                }
                document.AppendChild(newChild);
                document.Save("setting.xml");
            }
            catch (Exception)
            {
            }
        }

        public void SendAddTask(MyTask task)
        {
            WorldPacket ms = new WorldPacket();
            ms.Initialize(13);
            this.GetTaskPacket(task, ms);
            this.AddPacket(ms);
        }

        public void SendCheckAccountName(string account)
        {
            if (this.IsConnected())
            {
                WorldPacket packet = new WorldPacket();
                packet.Initialize(0x1b);
                packet.InputString(account, 0x20);
                this.AddPacket(packet);
            }
            else
            {
                WorldPacket packet2 = new WorldPacket();
                packet2.Initialize(11);
                packet2.InputString(account, 0x20);
                this._gateSession.GateSocket.AddPacket(packet2);
            }
        }

        public void SendCompletTask(uint taskID, int taskSteps)
        {
            WorldPacket packet = new WorldPacket();
            packet.Initialize(0x19);
            packet.InputUInt32(taskID);
            packet.InputInt(taskSteps);
            this.AddPacket(packet);
        }

        public void SendGetLoginInfo(bool getTask = true)
        {
            WorldPacket packet = new WorldPacket();
            packet.Initialize(11);
            if (getTask)
            {
                packet.InputUInt32(1);
            }
            else
            {
                packet.InputUInt32(0);
            }
            this.AddPacket(packet);
        }

        public void SendGetTask()
        {
            WorldPacket packet = new WorldPacket();
            packet.Initialize(15);
            this.AddPacket(packet);
        }

        public void SendModifyAccount(string account, string email, string password, string phone, string referee)
        {
            this._K = client.LoginUtil.GenerateOrginalKey(account, password);
            if (this.IsConnected())
            {
                WorldPacket packet = new WorldPacket();
                packet.Initialize(0x1d);
                packet.InputString(account, 0x20);
                packet.InputString(email, 0x40);
                packet.InputString(this._K, 0x40);
                packet.InputString(phone, 0x40);
                packet.InputString(referee, 0x20);
                this.AddPacket(packet);
            }
            else
            {
                WorldPacket packet2 = new WorldPacket();
                packet2.Initialize(9);
                packet2.InputString(account, 0x20);
                packet2.InputString(email, 0x40);
                packet2.InputString(this._K, 0x40);
                packet2.InputString(phone, 0x40);
                packet2.InputString(referee, 0x20);
                this._gateSession.GateSocket.AddPacket(packet2);
            }
        }

        public void SendModifyTask(MyTask task)
        {
            WorldPacket ms = new WorldPacket();
            ms.Initialize(0x11);
            ms.InputUInt32(task._id);
            this.GetTaskPacket(task, ms);
            this.AddPacket(ms);
        }

        public void SendNetTask()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this._getNetTaskTime);
            if ((span.TotalSeconds >= 10.0) && !this.IsConnecting())
            {
                this.SendGetTask();
            }
        }

        public void SendRemoveTask(uint taskID)
        {
            WorldPacket packet = new WorldPacket();
            packet.Initialize(0x17);
            packet.InputUInt32(taskID);
            this.AddPacket(packet);
        }

        public void SendResumeTask(MyTask task)
        {
            WorldPacket packet = new WorldPacket();
            packet.Initialize(0x15);
            packet.InputUInt32(task._id);
            this.AddPacket(packet);
        }

        public void SendRunTask(MyTask mytask)
        {
            try
            {
                client.COPYDATASTRUCT copydatastruct;
                XmlDocument document = new XmlDocument();
                XmlElement newChild = document.CreateElement("root");
                XmlElement element2 = document.CreateElement("tasks");
                XmlElement element4 = document.CreateElement("task");
                XmlElement element5 = document.CreateElement("params");
                foreach (TaskInfo info in mytask._taskItems)
                {
                    XmlElement element6 = document.CreateElement("taskinfo");
                    XmlElement element7 = document.CreateElement("param1");
                    XmlElement element8 = document.CreateElement("param2");
                    XmlElement element9 = document.CreateElement("param3");
                    XmlElement element10 = document.CreateElement("param4");
                    XmlElement element11 = document.CreateElement("param5");
                    element7.InnerText = info._param1;
                    element6.AppendChild(element7);
                    if ((info._param2 != string.Empty) && (info._param2 != ""))
                    {
                        element8.InnerText = info._param2;
                        element6.AppendChild(element8);
                    }
                    if (info._param3 != string.Empty)
                    {
                        element9.InnerText = info._param3;
                        element6.AppendChild(element9);
                    }
                    if (info._param4 != string.Empty)
                    {
                        element10.InnerText = info._param4;
                        element6.AppendChild(element10);
                    }
                    if (info._param5 != string.Empty)
                    {
                        element11.InnerText = info._param5;
                        element6.AppendChild(element11);
                    }
                    element4.AppendChild(element6);
                }
                XmlElement element3 = document.CreateElement("taskid");
                element3.InnerText = mytask._id.ToString();
                element5.AppendChild(element3);
                element3 = null;
                element4.AppendChild(element5);
                element2.AppendChild(element4);
                newChild.AppendChild(element2);
                XmlElement element12 = document.CreateElement("setting");
                if (this.WaitFindTime != 0)
                {
                    element3 = document.CreateElement("waitFindTime");
                    element3.InnerText = this.WaitFindTime.ToString();
                    element12.AppendChild(element3);
                    element3 = null;
                }
                if (this.WaitDocCompleteTime != 0)
                {
                    element3 = document.CreateElement("waitDocCompleteTime");
                    element3.InnerText = this.WaitDocCompleteTime.ToString();
                    element12.AppendChild(element3);
                    element3 = null;
                }
                if (!string.IsNullOrEmpty(this._curAddress))
                {
                    element3 = document.CreateElement("curAddress");
                    element3.InnerText = this._curAddress;
                    element12.AppendChild(element3);
                    element3 = null;
                }
                newChild.AppendChild(element12);
                document.AppendChild(newChild);
                string outerXml = document.OuterXml;
                int length = Encoding.Unicode.GetBytes(outerXml).Length;
                copydatastruct.dwData = IntPtr.Zero;
                copydatastruct.lpData = outerXml;
                copydatastruct.cbDaat = length + 2;
                this.BrowserManager.RunTask(mytask._id, copydatastruct);
            }
            catch (Exception)
            {
            }
        }

        public void SendStopTask(MyTask task)
        {
            WorldPacket packet = new WorldPacket();
            packet.Initialize(0x13);
            packet.InputUInt32(task._id);
            this.AddPacket(packet);
        }

        private void SetAllAreaClicks(bool isChecked)
        {
            this.checkBoxCustomHeBei.Checked = isChecked;
            this.checkBoxCustomSanXi.Checked = isChecked;
            this.checkBoxCustomLiaoNing.Checked = isChecked;
            this.checkBoxCustomJiLin.Checked = isChecked;
            this.checkBoxCustomHeiLongjiang.Checked = isChecked;
            this.checkBoxCustomJiangSu.Checked = isChecked;
            this.checkBoxCustomZheJiang.Checked = isChecked;
            this.checkBoxCustomAnHui.Checked = isChecked;
            this.checkBoxCustomFuJian.Checked = isChecked;
            this.checkBoxCustomJiangXi.Checked = isChecked;
            this.checkBoxCustomSanDong.Checked = isChecked;
            this.checkBoxCustomHeNan.Checked = isChecked;
            this.checkBoxCustomHuBei.Checked = isChecked;
            this.checkBoxCustomHuNan.Checked = isChecked;
            this.checkBoxCustomGuangDong.Checked = isChecked;
            this.checkBoxCustomHaiNan.Checked = isChecked;
            this.checkBoxCustomSiChuang.Checked = isChecked;
            this.checkBoxCustomGuiZhou.Checked = isChecked;
            this.checkBoxCustomYunNan.Checked = isChecked;
            this.checkBoxCustomShanXi.Checked = isChecked;
            this.checkBoxCustomGanSu.Checked = isChecked;
            this.checkBoxCustomQingHai.Checked = isChecked;
            this.checkBoxCustomTaiWan.Checked = isChecked;
            this.checkBoxCustomBeiJing.Checked = isChecked;
            this.checkBoxCustomTianJin.Checked = isChecked;
            this.checkBoxCustomShangHai.Checked = isChecked;
            this.checkBoxCustomChongQing.Checked = isChecked;
            this.checkBoxCustomNeiMengGu.Checked = isChecked;
            this.checkBoxCustomXiZang.Checked = isChecked;
            this.checkBoxCustomXinJiang.Checked = isChecked;
            this.checkBoxCustomNingXia.Checked = isChecked;
            this.checkBoxCustomGuangXi.Checked = isChecked;
            this.checkBoxCustomXiangGan.Checked = isChecked;
            this.checkBoxCustomAoMen.Checked = isChecked;
            this.checkBoxCustomOtherProvince.Checked = isChecked;
        }

        private void SetAreaClicksText(MyTask task)
        {
            if (((task != null) && (task._AreaClicks != null)) && (task._AreaClicks.Count == 0x23))
            {
                int num = 0;
                this.checkBoxCustomHeBei.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomSanXi.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomLiaoNing.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomJiLin.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomHeiLongjiang.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomJiangSu.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomZheJiang.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomAnHui.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomFuJian.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomJiangXi.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomSanDong.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomHeNan.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomHuBei.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomHuNan.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomGuangDong.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomHaiNan.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomSiChuang.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomGuiZhou.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomYunNan.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomShanXi.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomGanSu.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomQingHai.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomTaiWan.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomBeiJing.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomTianJin.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomShangHai.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomChongQing.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomNeiMengGu.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomXiZang.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomXinJiang.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomNingXia.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomGuangXi.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomXiangGan.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomAoMen.Checked = (bool) task._AreaClicks[num++];
                this.checkBoxCustomOtherProvince.Checked = (bool) task._AreaClicks[num++];
            }
        }

        private void SetHourClicksText(MyTask task)
        {
            if (((task != null) && (task._HourClicks != null)) && (task._HourClicks.Count == HOURSCOUNT))
            {
                int num = 0;
                for (int i = 0; i < task._HourClicks.Count; i++)
                {
                    num += Convert.ToInt32(task._HourClicks[i]);
                }
                this.textBoxCustomClickPerDay.Text = num.ToString();
                this.textBoxCustomClock0.Text = Convert.ToInt32(task._HourClicks[0]).ToString();
                this.textBoxCustomClock1.Text = Convert.ToInt32(task._HourClicks[1]).ToString();
                this.textBoxCustomClock2.Text = Convert.ToInt32(task._HourClicks[2]).ToString();
                this.textBoxCustomClock3.Text = Convert.ToInt32(task._HourClicks[3]).ToString();
                this.textBoxCustomClock4.Text = Convert.ToInt32(task._HourClicks[4]).ToString();
                this.textBoxCustomClock5.Text = Convert.ToInt32(task._HourClicks[5]).ToString();
                this.textBoxCustomClock6.Text = Convert.ToInt32(task._HourClicks[6]).ToString();
                this.textBoxCustomClock7.Text = Convert.ToInt32(task._HourClicks[7]).ToString();
                this.textBoxCustomClock8.Text = Convert.ToInt32(task._HourClicks[8]).ToString();
                this.textBoxCustomClock9.Text = Convert.ToInt32(task._HourClicks[9]).ToString();
                this.textBoxCustomClock10.Text = Convert.ToInt32(task._HourClicks[10]).ToString();
                this.textBoxCustomClock11.Text = Convert.ToInt32(task._HourClicks[11]).ToString();
                this.textBoxCustomClock12.Text = Convert.ToInt32(task._HourClicks[12]).ToString();
                this.textBoxCustomClock13.Text = Convert.ToInt32(task._HourClicks[13]).ToString();
                this.textBoxCustomClock14.Text = Convert.ToInt32(task._HourClicks[14]).ToString();
                this.textBoxCustomClock15.Text = Convert.ToInt32(task._HourClicks[15]).ToString();
                this.textBoxCustomClock16.Text = Convert.ToInt32(task._HourClicks[0x10]).ToString();
                this.textBoxCustomClock17.Text = Convert.ToInt32(task._HourClicks[0x11]).ToString();
                this.textBoxCustomClock18.Text = Convert.ToInt32(task._HourClicks[0x12]).ToString();
                this.textBoxCustomClock19.Text = Convert.ToInt32(task._HourClicks[0x13]).ToString();
                this.textBoxCustomClock20.Text = Convert.ToInt32(task._HourClicks[20]).ToString();
                this.textBoxCustomClock21.Text = Convert.ToInt32(task._HourClicks[0x15]).ToString();
                this.textBoxCustomClock22.Text = Convert.ToInt32(task._HourClicks[0x16]).ToString();
                this.textBoxCustomClock23.Text = Convert.ToInt32(task._HourClicks[0x17]).ToString();
                this.textBoxCustomClock24.Text = Convert.ToInt32(task._HourClicks[0x18]).ToString();
                this.textBoxCustomClock25.Text = Convert.ToInt32(task._HourClicks[0x19]).ToString();
                this.textBoxCustomClock26.Text = Convert.ToInt32(task._HourClicks[0x1a]).ToString();
                this.textBoxCustomClock27.Text = Convert.ToInt32(task._HourClicks[0x1b]).ToString();
                this.textBoxCustomClock28.Text = Convert.ToInt32(task._HourClicks[0x1c]).ToString();
                this.textBoxCustomClock29.Text = Convert.ToInt32(task._HourClicks[0x1d]).ToString();
                this.textBoxCustomClock30.Text = Convert.ToInt32(task._HourClicks[30]).ToString();
                this.textBoxCustomClock31.Text = Convert.ToInt32(task._HourClicks[0x1f]).ToString();
                this.textBoxCustomClock32.Text = Convert.ToInt32(task._HourClicks[0x20]).ToString();
                this.textBoxCustomClock33.Text = Convert.ToInt32(task._HourClicks[0x21]).ToString();
                this.textBoxCustomClock34.Text = Convert.ToInt32(task._HourClicks[0x22]).ToString();
                this.textBoxCustomClock35.Text = Convert.ToInt32(task._HourClicks[0x23]).ToString();
                this.textBoxCustomClock36.Text = Convert.ToInt32(task._HourClicks[0x24]).ToString();
                this.textBoxCustomClock37.Text = Convert.ToInt32(task._HourClicks[0x25]).ToString();
                this.textBoxCustomClock38.Text = Convert.ToInt32(task._HourClicks[0x26]).ToString();
                this.textBoxCustomClock39.Text = Convert.ToInt32(task._HourClicks[0x27]).ToString();
                this.textBoxCustomClock40.Text = Convert.ToInt32(task._HourClicks[40]).ToString();
                this.textBoxCustomClock41.Text = Convert.ToInt32(task._HourClicks[0x29]).ToString();
                this.textBoxCustomClock42.Text = Convert.ToInt32(task._HourClicks[0x2a]).ToString();
                this.textBoxCustomClock43.Text = Convert.ToInt32(task._HourClicks[0x2b]).ToString();
                this.textBoxCustomClock44.Text = Convert.ToInt32(task._HourClicks[0x2c]).ToString();
                this.textBoxCustomClock45.Text = Convert.ToInt32(task._HourClicks[0x2d]).ToString();
                this.textBoxCustomClock46.Text = Convert.ToInt32(task._HourClicks[0x2e]).ToString();
                this.textBoxCustomClock47.Text = Convert.ToInt32(task._HourClicks[0x2f]).ToString();
            }
        }

        public void SetReLogin(int code)
        {
            if (code == 1)
            {
                this.labelUserName.Text = "您输入的帐户不存在";
            }
            else if (code == 2)
            {
                this.labelUserName.Text = "您输入的帐户或密码错误";
            }
            else if (code == 3)
            {
                this.labelUserName.Text = "您输入的帐户被禁止";
            }
            else
            {
                if (code == 4)
                {
                    this.labelUserName.Text = "版本过低，请升级客户端";
                    this.SaveSetting(true);
                    this.StopSendMessage();
                    this._tipForm.Init(this, "版本过低，请升级客户端", 10, true, new MessageForm.ButtonClickDelegate(this.ClickUpdate), new MessageForm.ButtonClickDelegate(this.ClickCancleUpdate));
                    return;
                }
                if (code == 5)
                {
                    this.labelUserName.Text = "您输入的帐户有误";
                }
                else if (code == 6)
                {
                    this.labelUserName.Text = "您输入的密码错误";
                }
                else if (code == 8)
                {
                    this.labelUserName.Text = "内部错误";
                }
                else if (code == 9)
                {
                    this.labelUserName.Text = "服务器连接错误";
                }
            }
            this.ShowAccountTip("");
            this.buttonReLogin.Visible = true;
        }

        private void SetTaskItem(TaskCommand command)
        {
            if (command == TaskCommand.Task_Wait)
            {
                this.buttonInsertWait.Text = "修改>>";
            }
            else
            {
                this.textBoxWaitTime.Text = "";
                this.buttonInsertWait.Text = "插入>>";
            }
            if (command == TaskCommand.Task_InputText)
            {
                this.buttonInsertInputText.Text = "修改>>";
            }
            else
            {
                this.textBoxInputBoxName.Text = "";
                this.textBoxInputText.Text = "";
                this.buttonInsertInputText.Text = "插入>>";
            }
            if (command == TaskCommand.Task_ClickButton)
            {
                this.buttonInsertClickButton.Text = "修改>>";
            }
            else
            {
                this.textBoxButtonID.Text = "";
                this.buttonInsertClickButton.Text = "插入>>";
            }
            if (command == TaskCommand.Task_ClickLink)
            {
                this.buttonInsertClickLink.Text = "修改>>";
            }
            else
            {
                this.textBoxLink.Text = "";
                this.textBoxLinkKeyword.Text = "";
                this.buttonInsertClickLink.Text = "插入>>";
            }
            if (command == TaskCommand.Task_Navigate)
            {
                this.buttonInsertWebSite.Text = "修改>>";
            }
            else
            {
                this.textBoxWebSite.Text = "";
                this.textBoxWebSiteRefer.Text = "";
                this.buttonInsertWebSite.Text = "插入>>";
            }
            if (command == TaskCommand.Task_DeepClick)
            {
                this.buttonInsertDeepClick.Text = "修改>>";
            }
            else
            {
                this.textBoxDeepClickTimes.Text = "";
                this.textBoxDeepClickWaitTime.Text = "";
                this.textBoxDeepClickText.Text = "";
                this.buttonInsertDeepClick.Text = "插入>>";
            }
            if ((((command == TaskCommand.Task_FindLinkLinkPage1) || (command == TaskCommand.Task_FindLinkHrefPage1)) || ((command == TaskCommand.Task_FindHrefLinkPage1) || (command == TaskCommand.Task_FindHrefHrefPage1))) || (((command == TaskCommand.Task_FindSrcLinkPage1) || (command == TaskCommand.Task_FindSrcHrefPage1)) || (((command == TaskCommand.Task_FindHrefSrcPage1) || (command == TaskCommand.Task_FindLinkSrcPage1)) || (command == TaskCommand.Task_FindSrcSrcPage1))))
            {
                this.buttonInsertFindPage.Text = "修改>>";
            }
            else
            {
                this.textBoxFindPage.Text = "";
                this.textBoxFindKeyword.Text = "";
                this.textBoxFindPageNext.Text = "";
                this.textBoxFindMaxPage.Text = FINDMAXPAGE.ToString();
                this.buttonInsertFindPage.Text = "插入>>";
            }
            if (command != TaskCommand.Task_ClearCookie)
            {
                this.radioButtonAll.Checked = true;
            }
        }

        public void SetValidClick(uint taskID, uint validClick, uint invalidClick, uint todayValidClick, uint todayInvalidClick, uint yesterdayValidClick, uint yesterdayInvalidClick)
        {
            foreach (MyTask task in this._tasks)
            {
                if (task._id == taskID)
                {
                    task._validClickCount = validClick;
                    task._invalidClickCount = invalidClick;
                    task._todayValidClickCount = todayValidClick;
                    task._todayInvalidClickCount = todayInvalidClick;
                    task._yesterdayValidClickCount = yesterdayValidClick;
                    task._yesterdayInvalidClickCount = yesterdayInvalidClick;
                    break;
                }
            }
        }

        public void ShowAccountTip(string s)
        {
            this.labelAccountTip.Text = s;
        }

        private void ShowPanels()
        {
            for (int i = 0; i < this._panels.Count; i++)
            {
                PanelInfo info = (PanelInfo) this._panels[i];
                if (info._show)
                {
                    info._panel.Size = info._size;
                }
                else
                {
                    info._panel.Size = new Size(info._size.Width, 0x1c);
                }
            }
            base.Invalidate();
        }

        public void StartBrowserProcess(bool forceOpen = false)
        {
            this.BrowserManager.CreateBrowserProcess(!forceOpen && (this.toolStripButtonOpenBrowser.Text == "打开浏览器"), this._mainWnd);
        }

        public void StartTask(MyTask task = new MyTask())
        {
            this.StartTaskThread(task);
        }

        public void StartTaskThread(MyTask task)
        {
            if (task == null)
            {
                this.StopTaskThread();
                this._startNetTask = true;
            }
            else
            {
                this._testTask = (MyTask) task.Clone();
            }
            this._taskThreadRun = true;
        }

        private void StopSendMessage()
        {
            this.ClearPackets();
            this._isStopConnectToServer = true;
        }

        public void StopTasks()
        {
            this.StopTaskThread();
            this.CloseBrowserProcess(true, 0);
            this.toolStripButtonStartTask.Text = STARTTASK;
        }

        public void StopTasks(uint taskID)
        {
            for (int i = 0; i < this._tasks.Count; i++)
            {
                if (((MyTask) this._tasks[i])._id == taskID)
                {
                    ((MyTask) this._tasks[i])._isRun = false;
                    this.ReloadTasks();
                    return;
                }
            }
        }

        public void StopTaskThread()
        {
            this._taskThreadRun = false;
            this._browserManager.StopBrowserTask(true, 0);
            this._startNetTask = false;
        }

        private void SystemDistribute(int total)
        {
            int[] numArray = new int[HOURSCOUNT];
            int[] numArray2 = new int[HOURSCOUNT];
            bool[] flagArray = new bool[HOURSCOUNT];
            int num = 0;
            numArray[0] = 10;
            numArray[1] = 10;
            numArray[2] = 5;
            numArray[3] = 5;
            numArray[4] = 3;
            numArray[5] = 3;
            numArray[6] = 2;
            numArray[7] = 2;
            numArray[8] = 2;
            numArray[9] = 2;
            numArray[10] = 2;
            numArray[11] = 4;
            numArray[12] = 5;
            numArray[13] = 5;
            numArray[14] = 7;
            numArray[15] = 8;
            numArray[0x10] = 15;
            numArray[0x11] = 20;
            numArray[0x12] = 20;
            numArray[0x13] = 0x19;
            numArray[20] = 30;
            numArray[0x15] = 30;
            numArray[0x16] = 0x23;
            numArray[0x17] = 0x23;
            numArray[0x18] = 0x23;
            numArray[0x19] = 0x23;
            numArray[0x1a] = 30;
            numArray[0x1b] = 0x23;
            numArray[0x1c] = 0x23;
            numArray[0x1d] = 40;
            numArray[30] = 40;
            numArray[0x1f] = 40;
            numArray[0x20] = 0x19;
            numArray[0x21] = 0x19;
            numArray[0x22] = 20;
            numArray[0x23] = 0x19;
            numArray[0x24] = 30;
            numArray[0x25] = 30;
            numArray[0x26] = 20;
            numArray[0x27] = 0x19;
            numArray[40] = 0x23;
            numArray[0x29] = 0x23;
            numArray[0x2a] = 30;
            numArray[0x2b] = 30;
            numArray[0x2c] = 0x19;
            numArray[0x2d] = 20;
            numArray[0x2e] = 15;
            numArray[0x2f] = 15;
            for (uint i = 0; i < HOURSCOUNT; i++)
            {
                flagArray[i] = false;
                numArray2[i] = (total * numArray[i]) / 0x3e8;
                num += numArray2[i];
            }
            while (num != total)
            {
                int num3 = 0;
                int index = 0;
                int num5 = 0;
                for (num5 = 0; num5 < HOURSCOUNT; num5++)
                {
                    if ((numArray[num5] > num3) && !flagArray[num5])
                    {
                        num3 = numArray[num5];
                        index = num5;
                    }
                }
                if (index >= HOURSCOUNT)
                {
                    break;
                }
                numArray2[index]++;
                num++;
                flagArray[index] = true;
            }
            this.textBoxCustomClock0.Text = numArray2[0].ToString();
            this.textBoxCustomClock1.Text = numArray2[1].ToString();
            this.textBoxCustomClock2.Text = numArray2[2].ToString();
            this.textBoxCustomClock3.Text = numArray2[3].ToString();
            this.textBoxCustomClock4.Text = numArray2[4].ToString();
            this.textBoxCustomClock5.Text = numArray2[5].ToString();
            this.textBoxCustomClock6.Text = numArray2[6].ToString();
            this.textBoxCustomClock7.Text = numArray2[7].ToString();
            this.textBoxCustomClock8.Text = numArray2[8].ToString();
            this.textBoxCustomClock9.Text = numArray2[9].ToString();
            this.textBoxCustomClock10.Text = numArray2[10].ToString();
            this.textBoxCustomClock11.Text = numArray2[11].ToString();
            this.textBoxCustomClock12.Text = numArray2[12].ToString();
            this.textBoxCustomClock13.Text = numArray2[13].ToString();
            this.textBoxCustomClock14.Text = numArray2[14].ToString();
            this.textBoxCustomClock15.Text = numArray2[15].ToString();
            this.textBoxCustomClock16.Text = numArray2[0x10].ToString();
            this.textBoxCustomClock17.Text = numArray2[0x11].ToString();
            this.textBoxCustomClock18.Text = numArray2[0x12].ToString();
            this.textBoxCustomClock19.Text = numArray2[0x13].ToString();
            this.textBoxCustomClock20.Text = numArray2[20].ToString();
            this.textBoxCustomClock21.Text = numArray2[0x15].ToString();
            this.textBoxCustomClock22.Text = numArray2[0x16].ToString();
            this.textBoxCustomClock23.Text = numArray2[0x17].ToString();
            this.textBoxCustomClock24.Text = numArray2[0x18].ToString();
            this.textBoxCustomClock25.Text = numArray2[0x19].ToString();
            this.textBoxCustomClock26.Text = numArray2[0x1a].ToString();
            this.textBoxCustomClock27.Text = numArray2[0x1b].ToString();
            this.textBoxCustomClock28.Text = numArray2[0x1c].ToString();
            this.textBoxCustomClock29.Text = numArray2[0x1d].ToString();
            this.textBoxCustomClock30.Text = numArray2[30].ToString();
            this.textBoxCustomClock31.Text = numArray2[0x1f].ToString();
            this.textBoxCustomClock32.Text = numArray2[0x20].ToString();
            this.textBoxCustomClock33.Text = numArray2[0x21].ToString();
            this.textBoxCustomClock34.Text = numArray2[0x22].ToString();
            this.textBoxCustomClock35.Text = numArray2[0x23].ToString();
            this.textBoxCustomClock36.Text = numArray2[0x24].ToString();
            this.textBoxCustomClock37.Text = numArray2[0x25].ToString();
            this.textBoxCustomClock38.Text = numArray2[0x26].ToString();
            this.textBoxCustomClock39.Text = numArray2[0x27].ToString();
            this.textBoxCustomClock40.Text = numArray2[40].ToString();
            this.textBoxCustomClock41.Text = numArray2[0x29].ToString();
            this.textBoxCustomClock42.Text = numArray2[0x2a].ToString();
            this.textBoxCustomClock43.Text = numArray2[0x2b].ToString();
            this.textBoxCustomClock44.Text = numArray2[0x2c].ToString();
            this.textBoxCustomClock45.Text = numArray2[0x2d].ToString();
            this.textBoxCustomClock46.Text = numArray2[0x2e].ToString();
            this.textBoxCustomClock47.Text = numArray2[0x2f].ToString();
        }

        private void TaskDeleteClick(object sender, EventArgs e)
        {
            int rowIndex = this.dataGridViewTasks.CurrentCell.RowIndex;
            if ((rowIndex >= 0) && (rowIndex < this._tasks.Count))
            {
                MyTask task = this.Tasks[rowIndex] as MyTask;
                this.SendRemoveTask(task._id);
            }
        }

        private void TaskEditClick(object sender, EventArgs e)
        {
            int rowIndex = this.dataGridViewTasks.CurrentCell.RowIndex;
            if ((rowIndex >= 0) && (rowIndex < this._tasks.Count))
            {
                this._task = (MyTask) ((MyTask) this.Tasks[rowIndex]).Clone();
                this._modifyTaskIndex = rowIndex;
                this.ReloadTask(true);
                this.tabControl1.SelectedTab = this.tabPageCustonTemplate;
            }
        }

        private void TaskItemDelete(object sender, EventArgs e)
        {
            this._task.Remove(this.listBoxTaskItems.SelectedIndex);
            this._modifyTaskItemIndex = -1;
            this.SetTaskItem(TaskCommand.Task_None);
            this.ReloadTask(false);
        }

        private void TaskItemEdit(object sender, EventArgs e)
        {
            this.InitEditTaskItem(this.listBoxTaskItems.SelectedIndex);
        }

        private void TaskItemMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int posindex = this.listBoxTaskItems.IndexFromPoint(new Point(e.X, e.Y));
                if ((posindex >= 0) && (posindex < this.listBoxTaskItems.Items.Count))
                {
                    this.listBoxTaskItems.ContextMenu = null;
                    this.InitEditTaskItem(posindex);
                }
            }
        }

        private void TaskItemMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int num = this.listBoxTaskItems.IndexFromPoint(new Point(e.X, e.Y));
                this.listBoxTaskItems.ContextMenu = null;
                if ((num >= 0) && (num < this.listBoxTaskItems.Items.Count))
                {
                    this.listBoxTaskItems.SelectedIndex = num;
                    this.contextMenuStrip1.Show(this.listBoxTaskItems, new Point(e.X, e.Y));
                }
            }
            this.listBoxTaskItems.Refresh();
        }

        private void TaskItemMoveDown(object sender, EventArgs e)
        {
            this._task.Reverse(this.listBoxTaskItems.SelectedIndex, this.listBoxTaskItems.SelectedIndex + 1);
            this.ReloadTask(false);
        }

        private void TaskItemMoveUp(object sender, EventArgs e)
        {
            this._task.Reverse(this.listBoxTaskItems.SelectedIndex, this.listBoxTaskItems.SelectedIndex - 1);
            this.ReloadTask(false);
        }

        private void TaskItemSelected(object sender, EventArgs e)
        {
            this._curTaskIndex = this.listBoxTaskItems.SelectedIndex;
        }

        private void TaskRunClick(object sender, EventArgs e)
        {
            int rowIndex = this.dataGridViewTasks.CurrentCell.RowIndex;
            if ((rowIndex >= 0) && (rowIndex < this._tasks.Count))
            {
                MyTask task = this.Tasks[rowIndex] as MyTask;
                if (!task._isRun)
                {
                    this.SendResumeTask(task);
                }
            }
        }

        private void TaskStopClick(object sender, EventArgs e)
        {
            int rowIndex = this.dataGridViewTasks.CurrentCell.RowIndex;
            if ((rowIndex >= 0) && (rowIndex < this._tasks.Count))
            {
                MyTask task = this.Tasks[rowIndex] as MyTask;
                if (task._isRun)
                {
                    this.SendStopTask(task);
                }
            }
        }

        public void TaskThreadFunc(object sender, EventArgs ea)
        {
            try
            {
                this.UpdateNetManager();
                this._browserManager.Update();
                ClosePopupWindow();
                if (this._taskThreadRun)
                {
                    this._AddIeItems();
                }
                this.CheckNetTime();
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        public void TaskTimeOut(uint id, int taskStep)
        {
            if (id == 0)
            {
                this.StopTasks();
            }
            else
            {
                this.SendCompletTask(id, taskStep);
            }
        }

        public void UpdateAccount()
        {
            if (this._loginUtil != null)
            {
                this.labelUserName.Text = "您好， " + this._loginUtil.Account;
                if (this._loginUtil.AccountTime == 0)
                {
                    this.ShowAccountTip("");
                }
                else
                {
                    this.ShowAccountTip("您使用的是系统自动注册帐号，为了您的账号安全，请在“注册”中设置您的账号和密码。");
                }
                if (this._loginUtil.VipRemainDays != 0)
                {
                    this.ShowAccountTip("亲爱的VIP会员，您还有" + this._loginUtil.VipRemainDays + "天获取双倍积分时间。");
                }
                this.labelScore.Text = "当前金币：" + this._loginUtil.Score;
                if (this._tasks != null)
                {
                    this.labelTasksCount.Text = "当前任务数：" + this._tasks.Count;
                    this.labelRunTasksCount.Text = "正在运行任务数：" + this.GetRunTasksCount();
                }
                else
                {
                    this.labelTasksCount.Text = "当前任务数：0";
                    this.labelRunTasksCount.Text = "正在运行任务数：0";
                }
                this.labelPublishTotalTask.Text = "发布总任务数：" + this._loginUtil.PublishTotalTask;
                this.labelDayCompleteTask.Text = "今日完成任务：" + this._loginUtil.DayCompleteTask;
                this.labelDayCosumeScore.Text = "今日任务消耗金币：" + this._loginUtil.DayConsumeScore;
                this.labelPublishTotalScore.Text = "任务总消耗积分：" + this._loginUtil.PublishTotalScore;
                this.labelGetTotalTask.Text = "挂机总任务数：" + this._loginUtil.GetTotalTask;
                this.labelGetTotalScore.Text = "挂机获得总金币：" + this._loginUtil.GetTotalScore;
                this.labelDayNetTaskScore.Text = "今日挂机获得金币：" + this._loginUtil.DayNetTaskScore;
                this.labelDayCompleteNetTasksCount.Text = "今日挂机任务数：" + this._loginUtil.DayNetTaskCount;
                this.labelServerTasksCount.Text = "服务器任务总数：" + this._loginUtil.ServerTasksCount;
            }
            this.buttonReLogin.Visible = false;
            this.ReloadTasks();
        }

        public void UpdateNetManager()
        {
            this.ProcessGateData();
            this.ProcessServerData();
            if ((this._packets.Count > 0) || (this._gateSession.GateSocket.Packet != null))
            {
                if (this._status == NetManagerStatus.NETMANAGER_STATUS_NULL)
                {
                    this.Init(this._gateSession.GateSocket.Packet == null);
                }
                else if (NetManagerStatus.NETMANAGER_STATUS_CONNECTING_GATE == this._status)
                {
                    if (!this._gateSession.IsConnecting())
                    {
                        this.Init(this._gateSession.GateSocket.Packet == null);
                    }
                }
                else if (NetManagerStatus.NETMANAGER_STATUS_CONNECTED_GATE == this._status)
                {
                    if (this._packets.Count > 0)
                    {
                        this.Init2();
                    }
                }
                else if (NetManagerStatus.NETMANAGER_STATUS_CONNECTING_SERVER == this._status)
                {
                    if (!this._serverSession.IsConnecting())
                    {
                        this.Init2();
                    }
                    else if (this._serverSession.IsConnected())
                    {
                        this._status = NetManagerStatus.NETMANAGER_STATUS_CONNECTED_SERVER;
                    }
                }
                else if (NetManagerStatus.NETMANAGER_STATUS_CONNECTED_SERVER == this._status)
                {
                    if (this._serverSession.IsConnected())
                    {
                        PacketProcessInfo info = null;
                        if (this._packets.Count > 0)
                        {
                            info = (PacketProcessInfo) this._packets[0];
                            if (!info.IsSend())
                            {
                                this._sendTime = DateTime.Now;
                                info.Send();
                                this._serverSession.ServerSocket.SendPacket(info.Packet);
                                this.ResetNetTime();
                            }
                        }
                    }
                    else
                    {
                        TimeSpan span = (TimeSpan) (DateTime.Now - this._sendTime);
                        if ((span.TotalMinutes >= 1.0) && (this._packets.Count > 0))
                        {
                            this._status = NetManagerStatus.NETMANAGER_STATUS_CONNECTED_GATE;
                        }
                    }
                }
            }
        }

        public void UpdatePackets(ServerMsg opcode)
        {
            if ((ServerMsg.SERVER_MSG_LOGICBEGIN < opcode) && (this._packets.Count > 0))
            {
                PacketProcessInfo info = (PacketProcessInfo) this._packets[0];
                if (((ServerMsg) (info.Packet.Opcode + 1)) == opcode)
                {
                    this._packets.RemoveAt(0);
                    this.DisConnect();
                }
            }
        }

        public client.BrowserManager BrowserManager
        {
            get
            {
                return this._browserManager;
            }
        }

        public client.ChangeUserForm ChangeUserForm
        {
            get
            {
                return this._changeUserForm;
            }
            set
            {
                this._changeUserForm = value;
            }
        }

        public client.CreateAccountForm CreateAccountForm
        {
            get
            {
                return this._createAccountForm;
            }
            set
            {
                this._createAccountForm = value;
            }
        }

        public client.GateSession GateSession
        {
            get
            {
                return this._gateSession;
            }
        }

        public string IP
        {
            get
            {
                return this._ip;
            }
            set
            {
                this._ip = value;
            }
        }

        public bool IsInitialisedMyTask
        {
            get
            {
                return this._isInitialisedMyTask;
            }
            set
            {
                this._isInitialisedMyTask = value;
            }
        }

        public string K
        {
            get
            {
                return this._K;
            }
        }

        public client.LoginUtil LoginUtil
        {
            get
            {
                return this._loginUtil;
            }
        }

        public IntPtr MainWnd
        {
            get
            {
                return this._mainWnd;
            }
        }

        public int MaxTaskThreadsCount
        {
            get
            {
                return this._maxTaskThreadsCount;
            }
        }

        public ArrayList NetTasks
        {
            get
            {
                return this._getTasks;
            }
        }

        public int Port
        {
            get
            {
                return this._port;
            }
            set
            {
                this._port = value;
            }
        }

        public client.ServerSession ServerSession
        {
            get
            {
                return this._serverSession;
            }
        }

        public string SetIP
        {
            get
            {
                return this._setIp;
            }
            set
            {
                this._setIp = value;
            }
        }

        public bool StartNetTask
        {
            get
            {
                return this._startNetTask;
            }
        }

        public NetManagerStatus Status
        {
            set
            {
                this._status = value;
            }
        }

        public ArrayList Tasks
        {
            get
            {
                return this._tasks;
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
            set
            {
                this._waitDocCompleteTime = value;
            }
        }

        public int WaitFindTime
        {
            get
            {
                return this._waitFindTime;
            }
            set
            {
                this._waitFindTime = value;
            }
        }
    }
}

