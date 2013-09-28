using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using mshtml;

using SHDocVw;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using HtmlAgilityPack;
using System.Net.Sockets;
using Microsoft.Win32; //

namespace AutoBroswer
{
//
    public partial class AutoBroswerForm : Form
    {
        #region winapi

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        [DllImport("User32.Dll")]
        public static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);
        [DllImport("User32.Dll")]
        public static extern void GetClassName(int h, StringBuilder s, int nMaxCount);
        [DllImport("User32.Dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, [Out] StringBuilder lParam);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("User32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, UInt32 nCmdShow);

        [DllImport("User32.Dll")]
        public static extern IntPtr GetWindow(IntPtr hwnd, int wFlag);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }


        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public static extern int SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar,
                                       int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        static extern uint GetLastError();
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport(@"wininet", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "InternetSetOption",
                    CallingConvention = CallingConvention.StdCall)]
        public static extern bool InternetSetOption(int hInternet,
                                                int dmOption,
                                                IntPtr lpBuffer,
                                                int dwBufferLength);

        const UInt32 EM_LINESCROLL = 0x00B6;
        public const UInt32 WM_LBUTTONDOWN = 0x0201;
        public const UInt32 WM_LBUTTONUP = 0x0202;
        public const UInt32 MK_LBUTTON = 0x0001;

        const UInt32 WM_GETTEXT = 0x000D;
        const UInt32 WM_GETTEXTLENGTH = 0x000E;

        const UInt32 BM_CLICK = 0xF5;

        const UInt32 CB_GETCOUNT = 0x0146;
        const UInt32 CB_GETCURSEL = 0x0147;
        const UInt32 CB_GETLBTEXT = 0x0148;
        const UInt32 CB_SETCURSEL = 0x014e;

        const UInt32 LB_GETCOUNT = 0x018B;
        const UInt32 LB_GETTEXT = 0x0189;

        const UInt32 SW_MAXIMIZE = 3;
        const UInt32 SW_HIDE = 0;
        const UInt32 SW_MINIMIZE = 6;
        #endregion

        private string m_uaBroswerPattern = @"(.*)=(.*)";
        Regex m_uaBroswerRegex;

        public struct STBroserInfo
        {
            public string m_uaDesc;
            public string m_uaContent;
        }
        List<string> keywordCollection = new List<string>();
        List<STBroserInfo> broswerUACollection = new List<STBroserInfo>();
        enum EIPSelect
        {
            EIPSelect_91VPN,
            EIPSelect_vipiu,
        }

        private int m_waitMessageTick = 300;//300ms
        private int m_waitVPNConnectTime = 50;//15000ms
        public AutoBroswerForm()
        {
            InitializeComponent();

            m_uaBroswerRegex = new Regex(m_uaBroswerPattern);
            long tick = DateTime.Now.Ticks;
            int randSeed = ((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            rndGenerator = new Random(randSeed);
            visitDeepCB.SelectedIndex = 2;
            ipComboBox.SelectedIndex = 1;
            
        }

        public bool loadKeyWord()
        {
            string keyWorldText = keywordRichTB.Text;

            string[] lines = Regex.Split(keyWorldText, "\n");
            foreach (string line in lines)
            {
                if (line.Trim() != "")
                {
                    keywordCollection.Add(line);
                }
            }
            return keywordCollection.Count != 0;
        }

        public string getRandKeyWord()
        {
            int keywordCnt = keywordCollection.Count;
            if (keywordCnt == 1)
            {
                return keywordCollection[0];
            }
            int randIndex = 0;
            if (keywordCnt != 0)
            {
                randIndex = rndGenerator.Next(0, keywordCnt);
                return keywordCollection[randIndex];
            }
            return "";
        }

        
        public Random rndGenerator;
        StringBuilder curSelectComboboxName = new StringBuilder(256, 256);
        private void button1_Click(object sender, EventArgs e)
        {
            
            keywordCollection.Clear();
            loadKeyWord();
            //runNiuBDASHI();
            
            if (isDebugCB.Checked == false)
            {
                if (isVPNRunning() == false)
                {
                    MessageBox.Show("VPN 没有开启");
                    return;
                }

                if (isCCleanRunning() == false)
                {
                    MessageBox.Show("cclean 没有开启");
                    return;
                }
            }
            
            Control.CheckForIllegalCrossThreadCalls = false;
            nonParameterThread1 = new Thread(SimulateVisitThread);
            //nonParameterThread1.IsBackground = true;
            nonParameterThread1.SetApartmentState(ApartmentState.STA);
            nonParameterThread1.Start();
            tabControl1.SelectedIndex = 1;
            
            
        }
        
        #region 模拟访问线程
        private void SimulateVisitThread()
        {
            bool bRet = false;
            int expireTimer = getExpireTime() * 60 * 1000;
            try
            {
                int loopCnt = Convert.ToInt32(broswerNumTXT.Text.Trim());

                int uaCollectCount = broswerUACollection.Count;
                for (int index = 1; index <= loopCnt; index++)
                {

                    //int for loop
                    string keyWord = getRandKeyWord();
                    if (keyWord == "")
                    {
                        continue;
                    }

                    string uaString = "";
                    string uaCaptionStr = "";
                    if (uaCollectCount != 0)
                    {
                        int uaIndex = rndGenerator.Next(0, uaCollectCount);
                        uaString = broswerUACollection[uaIndex].m_uaContent;
                        uaCaptionStr = broswerUACollection[uaIndex].m_uaDesc;
                    }
                    string searchName = "第 " + index + " 个，" + "关键词:" + keyWord + "";
                    FileLogger.Instance.LogInfo(searchName);
                    if (isDebugCB.Checked == false)
                    {
                        bRet = changeVPN();
                        if (bRet == false)
                        {
                            FileLogger.Instance.LogInfo("切换VPN失败");
                            continue;
                        }
                    }
                    SimulateTab simulateTab = new SimulateTab(keyWord, uaString, this, expireTimer);
                    simulateInfoText.Text = " IP:" + curSelectComboboxName.ToString() + " 系统浏览器[版本号]:" + uaCaptionStr + " " + searchName;
                    Application.Run(simulateTab);
                    GC.Collect();
                    if (isDebugCB.Checked == false)
                    {
                        bRet = disconnectVPN();
                        bRet = runCClean();
                        bRet = runNiuBDASHI();
                    }

                    FileLogger.Instance.LogInfo("cookie清理干净了，下一个任务!");
                    if (simulateTab.isNormalQuit == false)
                    {
                        FileLogger.Instance.LogInfo("手动停止!");
                        break;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message.ToString());
                Console.WriteLine(error.StackTrace);
            }
        }
        #endregion

        #region 扫描相关软件是否打开
        public bool isVPNRunning()
        {
            IntPtr vpnMainHWND = FindWindow(null, "91VPN网游加速器商业版 - 3.5.2");
            if (vpnMainHWND == IntPtr.Zero)
            {
                return false;
            }
            return true;
        }
        public bool isCCleanRunning()
        {
            IntPtr ccleanMainHWND = FindWindow(null, "Piriform CCleaner");
            if (ccleanMainHWND == IntPtr.Zero)
            {
                FileLogger.Instance.LogInfo("cclean 没有开启");
                return false;
            }
            return true;
        }
        public bool isNiuBRunning()
        {
            IntPtr niuBMainHWND = FindWindow(null, "牛B硬件信息修改大师");
            if (niuBMainHWND == IntPtr.Zero)
            {
                FileLogger.Instance.LogInfo("牛逼大师 没有开启");
                return false;
            }
            return true;
        }
        public bool runNiuBDASHI()
        {
            IntPtr niuBMainHWND = FindWindow(null, "牛B硬件信息修改大师");
            if (niuBMainHWND == IntPtr.Zero)
            {
                return false;
            }
            IntPtr beginChangeHWND = FindWindowEx(niuBMainHWND, IntPtr.Zero, "Button", "一键修改");
            if (beginChangeHWND == IntPtr.Zero)
            {
                return false;
            }
            const UInt32 WM_LBUTTONDOWN = 0x0201;
            const UInt32 WM_LBUTTONUP = 0x0202;
            const int MK_LBUTTON = 0x0001;
            // 模拟鼠标按下   
            SetForegroundWindow(beginChangeHWND);
            AutoBroswerForm.PostMessage(beginChangeHWND, WM_LBUTTONDOWN, (int)MK_LBUTTON, 0);
            AutoBroswerForm.PostMessage(beginChangeHWND, WM_LBUTTONUP, (int)MK_LBUTTON, 0);
            //bool ret = PostMessage(beginChangeHWND, BM_CLICK, 0, 0);

            int index = 0;
            int maxTimes = 20;//最多等6秒
            bool isChangeDone = false;
            while (!isChangeDone && index <= maxTimes)
            {
                Thread.Sleep(m_waitMessageTick);
                //check if the beginCleanBtnHWND is enable?
                IntPtr infoDlgHWND = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "#32770", "Information");
                if (infoDlgHWND != IntPtr.Zero)
                {
                    IntPtr okBtnHWND = FindWindowEx(infoDlgHWND, IntPtr.Zero, "Button", "确定");
                    if (okBtnHWND != IntPtr.Zero)
                    {
                        SendMessage(okBtnHWND, BM_CLICK, 0, 0);
                        isChangeDone = true;
                    }
                }
                index++;
            }

            return true;
        }
        public bool runCClean()
        {
            IntPtr ccleanMainHWND = FindWindow(null, "Piriform CCleaner");
            if (ccleanMainHWND == IntPtr.Zero)
            {
                return false;
            }

            IntPtr subDlgHWND = FindWindowEx(ccleanMainHWND, IntPtr.Zero, "#32770", "");
            if (subDlgHWND == IntPtr.Zero)
            {
                return false;
            }

            IntPtr beginCleanBtnHWND = FindWindowEx(subDlgHWND, IntPtr.Zero, "Button", "&Run Cleaner");
            if (beginCleanBtnHWND == IntPtr.Zero)
            {
                return false;
            }

            SendMessage(beginCleanBtnHWND, BM_CLICK, 0, 0);

            bool isCleanDone = false;
            while (!isCleanDone)
            {
                Thread.Sleep(200);//sleep 200ms
                //check if the beginCleanBtnHWND is enable?
                isCleanDone = IsWindowEnabled(beginCleanBtnHWND);
            }

            return true;
        }

        public bool changeVPN()
        {
            
            IntPtr vpnMainHWND = FindWindow(null, "91VPN网游加速器商业版 - 3.5.2");
            if (vpnMainHWND == IntPtr.Zero)
            {
                return false;
            }

            IntPtr subDlgHWND = FindWindowEx(vpnMainHWND, IntPtr.Zero, "#32770", "");
            if (subDlgHWND == IntPtr.Zero)
            {
                return false;
            }

            IntPtr subConnectDlgHWND = FindWindowEx(subDlgHWND, IntPtr.Zero, "#32770", "连接");
            if (subConnectDlgHWND == IntPtr.Zero)
            {
                return false;
            }
            IntPtr disConnectBtnHWND = FindWindowEx(subDlgHWND, IntPtr.Zero, "Button", "断开");
            if (disConnectBtnHWND == IntPtr.Zero)
            {
                return false;
            }

            //IntPtr staticTxt = FindWindowEx(subConnectDlgHWND, IntPtr.Zero, "Static", "");
            //if (staticTxt == IntPtr.Zero)
            //{
            //    return false;
            //}
            //string statusTxt = GetControlText(staticTxt);

            IntPtr regionCombox = FindWindowEx(subConnectDlgHWND, IntPtr.Zero, "ComboBox", "");
            if (regionCombox == IntPtr.Zero)
            {
                return false;
            }
            regionCombox = GetWindow(regionCombox, 2);
            if (regionCombox == IntPtr.Zero)
            {
                return false;
            }
            IntPtr radio = GetWindow(regionCombox, 2);
            radio = GetWindow(radio, 2);
            IntPtr staticTxtHWND = GetWindow(radio, 2);
            if (staticTxtHWND == IntPtr.Zero)
            {
                return false;
            }

            int count = SendMessage(regionCombox, CB_GETCOUNT, 0, 0);

            //int reCode = Marshal.GetLastWin32Error();
            //MessageBox.Show("test", count + "--" + reCode);

            IntPtr connectBtnHWND = FindWindowEx(subDlgHWND, IntPtr.Zero, "Button", "连接");
            if (connectBtnHWND == IntPtr.Zero)
            {
                return false;
            }

            
            bool isConnect = false;
            while (!isConnect)
            {
                string statusTxt = GetControlText(staticTxtHWND);

                if (statusTxt != "Connected!")
                {
                    

                    bool isButtonEnable = false;
                    do
                    {
                        isButtonEnable = IsWindowEnabled(disConnectBtnHWND);
                        if (isButtonEnable)
                        {
                            SendMessage(disConnectBtnHWND, BM_CLICK, 0, 0);
                            //Thread.Sleep(100);//sleep 200ms
                        }
                        isButtonEnable = IsWindowEnabled(disConnectBtnHWND);
                    } while (isButtonEnable);

                    int regionIndex = rndGenerator.Next(0, count);
                    SendMessage(regionCombox, CB_SETCURSEL, regionIndex, 0);
                    SendMessage(regionCombox, CB_GETLBTEXT, regionIndex, curSelectComboboxName);
                    do
                    {
                        isButtonEnable = IsWindowEnabled(connectBtnHWND);
                        if (isButtonEnable)
                        {
                            SendMessage(connectBtnHWND, BM_CLICK, 0, 0);
                            //Thread.Sleep(100);//sleep 200ms
                        }
                        isButtonEnable = IsWindowEnabled(connectBtnHWND);
                    } while (isButtonEnable);

                    Thread.Sleep(1000);
                    int waitIndex = 0;
                    do 
                    {
                        statusTxt = GetControlText(staticTxtHWND);
                        waitIndex++;
                        Thread.Sleep(m_waitMessageTick);
                    } while ((statusTxt.ToLower().Contains("disconnected") || statusTxt.ToLower().Contains("...") || statusTxt.ToLower().Contains("authen") || statusTxt.ToLower().Contains("connecting")
                        || statusTxt.ToLower().Contains("projection") || statusTxt.ToLower() == "") && waitIndex < m_waitVPNConnectTime);
                    FileLogger.Instance.LogInfo("VPNStatus:" + statusTxt);
                    
                }
                else
                {
                    isConnect = true;
                }
            }
            FileLogger.Instance.LogInfo("当前选择线路：" + curSelectComboboxName.ToString());
            return true;
        }

        public bool disconnectVPN()
        {
            IntPtr vpnMainHWND = FindWindow(null, "91VPN网游加速器商业版 - 3.5.2");
            if (vpnMainHWND == IntPtr.Zero)
            {
                return false;
            }

            IntPtr subDlgHWND = FindWindowEx(vpnMainHWND, IntPtr.Zero, "#32770", "");
            if (subDlgHWND == IntPtr.Zero)
            {
                return false;
            }

            IntPtr subConnectDlgHWND = FindWindowEx(subDlgHWND, IntPtr.Zero, "#32770", "连接");
            if (subConnectDlgHWND == IntPtr.Zero)
            {
                return false;
            }
            IntPtr disConnectBtnHWND = FindWindowEx(subDlgHWND, IntPtr.Zero, "Button", "断开");
            if (disConnectBtnHWND == IntPtr.Zero)
            {
                return false;
            }

            IntPtr regionCombox = FindWindowEx(subConnectDlgHWND, IntPtr.Zero, "ComboBox", "");
            if (regionCombox == IntPtr.Zero)
            {
                return false;
            }
            regionCombox = GetWindow(regionCombox, 2);
            if (regionCombox == IntPtr.Zero)
            {
                return false;
            }
            IntPtr radio = GetWindow(regionCombox, 2);
            radio = GetWindow(radio, 2);
            IntPtr staticTxtHWND = GetWindow(radio, 2);
            if (staticTxtHWND == IntPtr.Zero)
            {
                return false;
            }

            bool isDisConnect = false;
            while (!isDisConnect)
            {
                string statusTxt = GetControlText(staticTxtHWND);
                if (statusTxt.ToLower().Trim() != "disconnected!")
                {
                    SendMessage(disConnectBtnHWND, BM_CLICK, 0, 0);
                    System.Threading.Thread.Sleep(50);
                }
                else
                {
                    isDisConnect = true;
                }
            }

            return true;
        }
                
        public string GetControlText(IntPtr hWnd)
        {

            StringBuilder title = new StringBuilder();

            // Get the size of the string required to hold the window title. 
            int size = SendMessage(hWnd, WM_GETTEXTLENGTH, 0, 0);

            // If the return is 0, there is no title. 
            if (size > 0)
            {
                title = new StringBuilder(size + 1);
                SendMessage(hWnd, WM_GETTEXT, title.Capacity, title);
            }
            return title.ToString();
        }
        #endregion

        #region 获取相应设置

        public bool isCompareRandCB()
        {
            return compareCB.Checked;
        }

        public bool isVisitDeepRand()
        {
            return visitDeepRndCheckBox.Checked;
        }
        public string getSellerName()
        {
            return sellerNameTB.Text.Trim();
        }

        public int getMainItemMinTime()
        {
            int value = 0;
            Int32.TryParse(mainItemMinTimeTXT.Text.Trim(), out value);
            return value;
        }
        public int getMainItemMaxTime()
        {
            int value = 1;
            Int32.TryParse(mainItemMaxTimeTXT.Text.Trim(), out value);
            return value;
        }
        public int getOtherItemMinTime()
        {
            int value = 0;
            Int32.TryParse(otherItemMinTimeTXT.Text.Trim(), out value);
            return value;
        }
        public int getOtherItemMaxTime()
        {
            int value = 1;
            Int32.TryParse(otherItemMaxTimeTXT.Text.Trim(), out value);
            return value;
        }
        public int getVisitDeep()
        {
            int value = 1;
            Int32.TryParse(visitDeepCB.SelectedItem.ToString().Trim(), out value);
            return value;
        }

        public int getExpireTime()
        {
            int value = 1;
            Int32.TryParse(jobExpireTimer.Text.Trim(), out value);
            return value;
        }
        #endregion

        private void AutoBroswerForm_Load(object sender, EventArgs e)
        {
            try
            {
                keywordRichTB.Text = File.ReadAllText("KeyWord.txt", Encoding.Default);

                string uaString = File.ReadAllText("myua.txt", Encoding.Default);
                string[] lines = Regex.Split(uaString, "\n");
                foreach (string line in lines)
                {
                    
                    if (line.Trim() != "")
                    {
                        MatchCollection matches = m_uaBroswerRegex.Matches(line); 
                        foreach(Match m in matches)
                        {
                            STBroserInfo uaBroswer;
                            uaBroswer.m_uaContent = m.Groups[2].Value;
                            uaBroswer.m_uaDesc = m.Groups[1].Value;
                            broswerUACollection.Add(uaBroswer);
                        }
                        
                    }
                }
                FileLogger.Instance.Open(@"MyLog"+DateTime.Now.ToString("yyyyMMdd") + ".log", true);
            }
            catch (Exception error)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(error.Message);
            }
        }

        private void AutoBroswerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                File.WriteAllText("KeyWord.txt", keywordRichTB.Text, Encoding.Default);
                FileLogger.Instance.Close();
            }
            catch (Exception error)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(error.Message);
            }
        }

        
        /// <summary>
        /// 匹配指定字符之间的字符串
        /// </summary>
        /// <param name="code">html源码</param>
        /// <param name="wordsBegin">开始字符串</param>
        /// <param name="wordsEnd">结束字符串</param>
        /// <returns></returns>
        List<string> SniffwebCode(string code, string wordsBegin, string wordsEnd)
        {
            List<string> listurl = new List<string>();
            Regex regex1 = new Regex("" + wordsBegin + @"(?<content>[\s\S]+?)" + wordsEnd + "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            for (Match match1 = regex1.Match(code); match1.Success; match1 = match1.NextMatch())
            {
                if (!listurl.Contains(match1.Groups[1].Value))
                {
                    listurl.Add(match1.Groups["content"].ToString());
                }
            }
            return listurl;
        }

        public static bool SoketConnect(string host, int port)
        {
            var is_success = false;
            try
            {
                var connsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                connsock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 200);
                System.Threading.Thread.Sleep(500);
                var hip = IPAddress.Parse(host);
                var ipep = new IPEndPoint(hip, port);
                connsock.Connect(ipep);
                if (connsock.Connected)
                {
                    is_success = true;
                }
                connsock.Close();
            }
            catch (Exception)
            {
                is_success = false;
            }
            return is_success;
        }

        private String verifyProxy(string IP, int port)
        {
            bool isok = true;
            string rs = null;
            while (isok)
            {
                try
                {
                    //设置代理IP
                    WebProxy proxyObject = new WebProxy(IP, port);
                    //向指定地址发送请求
                    HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create("http://www.baidu.com");
                    HttpWReq.Proxy = proxyObject;
                    HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
                    HttpWReq.Timeout = 10000;
                    StreamReader sr = new StreamReader(HttpWResp.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));
                    string xmlContent = sr.ReadToEnd().Trim();
                    sr.Close();
                    HttpWResp.Close();
                    HttpWReq.Abort();
                    rs = xmlContent;
                    isok = false;
                }
                catch (Exception)
                {
                    isok = false;
                    rs = "Error";
                }
            }
            return rs;
        }

        private void getIPBtn_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            nonParameterThread1 = new Thread(GetDataThread);
            nonParameterThread1.IsBackground = true;
            nonParameterThread1.Start();
            this.beginBTN.Enabled = false;
            //timer1.Interval = 1000;
            //timer1.Start();
            
        }
        Thread nonParameterThread1 = null;
        private void GetDataThread()
        {
            HtmlAgilityPack.HtmlWeb hw = new HtmlAgilityPack.HtmlWeb();
            hw.AutoDetectEncoding = false;

            //  HtmlAgilityPack.HtmlDocument doccc = hw.Load("http://www.youdaili.cn/");
            HttpWebRequest req;
            req = WebRequest.Create(new Uri(@"http://www.youdaili.cn/")) as HttpWebRequest;
            req.Method = "GET";

            HttpWebResponse rs = (HttpWebResponse)req.GetResponse();


            System.IO.StreamReader sr = new StreamReader(rs.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));

            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(sr);

                GetHrefs(doc);
                if (iplist.Count <= 10)
                {
                    MessageBox.Show("IP太少了", "infomation");
                }
                saveIPToFile();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message.ToString());
                Console.WriteLine(error.StackTrace);
            }
        }
        List<IpUrl> IpUrlList = new List<IpUrl>();
        List<IpModel> iplist = new List<IpModel>();
        int xxcount = 1;
        private void GetHrefs(HtmlAgilityPack.HtmlDocument _doc)
        {
            string todaydaili = DateTime.Now.ToString("MM-dd");
            string yestodaydaili = DateTime.Now.Date.AddDays(-1).ToString("MM-dd");
            HtmlNodeCollection hrefs = _doc.DocumentNode.SelectNodes("//ul/li/a");
            if (hrefs == null)
                return;

            foreach (HtmlNode href in hrefs)
            {
                if (href.Attributes["title"] != null && href.Attributes["href"] != null)
                {
                    string tilte = href.Attributes["title"].Value;
                    string urll = href.Attributes["href"].Value;
                    if ((tilte.IndexOf(todaydaili) >= 0 || (tilte.IndexOf(yestodaydaili) >= 0 )&& urll.Length > 0))
                    {
                        IpUrl model = new IpUrl();
                        if (urll.IndexOf("guonei") > 0)
                        {
                            model.DaiLi = 0;
                            model.Url = urll;
                            IpUrlList.Add(model);
                        }
                        if (urll.IndexOf("guowai") > 0)
                        {
                            model.DaiLi = 1;
                            model.Url = urll;
                            IpUrlList.Add(model);
                        }
                    }
                }
            }
            string url = "";

            int tmppage = 1;
            foreach (IpUrl urla in IpUrlList)
            {

                for (int startpage = 1; startpage <= 10; startpage++)
                {
                    //this.lb_result.Text = "正在采集第" + tmppage.ToString() + "页IP列表请稍后.........";
                    url = urla.Url.Replace(".html", "");
                    if (tmppage != 1)
                    {
                        url = url + "_" + startpage.ToString() + ".html";
                    }
                    else
                    {
                        url = url + ".html";
                    }
                    CaiJiIp(url, tmppage, urla.DaiLi);
                    tmppage++;
                }
            }
            //this.lb_result.Text = "本次采集采集完毕！";
            this.beginBTN.Enabled = true;
            //this.timer1.Stop();
        }
        private void CaiJiIp(string url, int nowpage, int dailitype)
        {
            #region start 采集IP列表页内容
            WebResponse result = null;
            string resultstring = "";
            try
            {
                WebRequest req = WebRequest.Create(url);
                req.Timeout = 8000;
                result = req.GetResponse();
                //FileLogger.Instance.LogInfo("第" + nowpage.ToString() + "页IP列表网页代码正在GetResponseStream解析.......");
                Stream ReceiveStream = result.GetResponseStream();
                //FileLogger.Instance.LogInfo("第" + nowpage.ToString() + "页IP列表网页代码解析完成.......");
                string strEncod = result.ContentType;
                StreamReader sr = new StreamReader(ReceiveStream, System.Text.Encoding.UTF8);
                //FileLogger.Instance.LogInfo("第" + nowpage.ToString() + "页IP列表网页代码解析后正在读取.......");
                resultstring = sr.ReadToEnd();
                //FileLogger.Instance.LogInfo("第" + nowpage.ToString() + "页IP列表网页代码解析后读取完毕.......");
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
                //FileLogger.Instance.LogInfo("获取IP列表请求超时,请重新尝试采集！");
                //this.btn_start.Enabled = true;
                //this.btn_stop.Enabled = false;
            }
            finally
            {
                if (result != null)
                {
                    result.Close();
                }
            }
            #endregion end采集IP列表页内容

            List<string> listurl = SniffwebCode(resultstring, "<br />", "@HTTP#【匿】");
            foreach (string xx in listurl)
            {
                var strxx = xx.Replace("\r\n", "");
                string[] arr = xx.Split(':');
                IpModel ipmodel = new IpModel();
                ipmodel.Ip = arr[0].Replace("\r", "").Replace("\n", "").Trim();
                ipmodel.IpPort = arr[1];
                int ipPort = 1;
                Int32.TryParse(ipmodel.IpPort.Trim(), out ipPort);
                var ipresult = verifyProxy(ipmodel.Ip, ipPort);
                if (ipresult == null || ipresult == "Error")
                {
                    continue;
                }
                //ComboBoxItem item = cbx_caijitype.SelectedItem as ComboBoxItem;
                ipmodel.IpType = 1;//国内//int.Parse(item.Value.ToString());
                ipmodel.IsUse = 1;
                ipmodel.DaiLiType = dailitype;
                //Createitem(ipmodel);
                FileLogger.Instance.LogInfo("第" + xxcount.ToString() + "个IP正在入库.......");
                //   Maticsoft.BLL.Ip bllip = new Maticsoft.BLL.Ip();
                if (iplist.SingleOrDefault(a => a.Ip == ipmodel.Ip) == null)
                {
                    iplist.Add(ipmodel);
                    //bllip.Add(ipmodel);
                    xxcount++;
                    //FileLogger.Instance.LogInfo("第" + xxcount.ToString() + "个IP入库成功.......");
                    //FileLogger.Instance.LogInfo("已经成功采集" + xxcount.ToString() + "条");
                }
                else
                {
                    FileLogger.Instance.LogInfo("已存在,已经成功采集" + xxcount.ToString() + "条");
                }
            }
            FileLogger.Instance.LogInfo("第" + nowpage.ToString() + "页：开始遍历采集会员信息.......");

        }

        public void SetProxy(string ip_port)
        {
            //打开注册表
            RegistryKey regKey = Registry.CurrentUser;
            string SubKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
            RegistryKey optionKey = regKey.OpenSubKey(SubKeyPath, true);
            //更改健值，设置代理，
            optionKey.SetValue("ProxyEnable", 1);
            if (ip_port.Length == 0)
            {
                optionKey.SetValue("ProxyEnable", 0);
            }
            optionKey.SetValue("ProxyServer", ip_port);

            //激活代理设置
            InternetSetOption(0, 39, IntPtr.Zero, 0);
            InternetSetOption(0, 37, IntPtr.Zero, 0);
        }

        private void stopIPBtn_Click(object sender, EventArgs e)
        {
            if (this.stopIPBtn.Text != "恢复获取IP")
            {
                nonParameterThread1.Suspend();
                this.stopIPBtn.Text = "恢复获取IP";
                this.beginBTN.Enabled = true;
            }
            else
            {
                nonParameterThread1.Resume();
                this.stopIPBtn.Text = "停止获取IP";
                this.beginBTN.Enabled = false;
            }
        }

        private void saveIPToFile()
        {
            using (StreamWriter writer = new StreamWriter("dailiIP.txt", true))
            {
                foreach (IpModel ip in iplist)
                {
                    writer.WriteLine(ip.Ip + ":"+ip.IpPort);
                }
                
            }
            
        }

        private void simulateStopBtn_Click(object sender, EventArgs e)
        {
            if (this.simulateStopBtn.Text != "暂停")
            {
                nonParameterThread1.Resume();
                this.simulateStopBtn.Text = "暂停";
            }
            else
            {
                nonParameterThread1.Suspend();
                this.simulateStopBtn.Text = "恢复";
            }
        }

    }
}
