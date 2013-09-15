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
using System.Text.RegularExpressions; //

namespace AutoBroswer
{
    public partial class AutoBroswerForm : Form
    {
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

        const UInt32 EM_LINESCROLL = 0x00B6;
        public const UInt32 WM_LBUTTONDOWN = 0x0201;

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
        private string m_uaBroswerPattern = @"(.*)=(.*)";
        Regex m_uaBroswerRegex;

        public struct STBroserInfo
        {
            public string m_uaDesc;
            public string m_uaContent;
        }
        List<string> keywordCollection = new List<string>();
        List<STBroserInfo> broswerUACollection = new List<STBroserInfo>();
        //int current = 0;
        
        //HTMLDocument currentDoc;

        
        public AutoBroswerForm()
        {
            InitializeComponent();

            m_uaBroswerRegex = new Regex(m_uaBroswerPattern);
            long tick = DateTime.Now.Ticks;
            int randSeed = ((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            rndGenerator = new Random(randSeed);
            visitDeepCB.SelectedIndex = 3;
            
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
            bool bRet = false;
            keywordCollection.Clear();
            loadKeyWord();
            int expireTimer = getExpireTime() * 60 * 1000;

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
            int loopCnt = Convert.ToInt32(broswerNumTXT.Text.Trim());

            int uaCollectCount = broswerUACollection.Count;
            for (int index = 0; index < loopCnt; index++)
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
                bRet = changeVPN();
                if (bRet == false)
                {
                    FileLogger.Instance.LogInfo("切换VPN失败");
                    continue;
                }


                WebBroswerForm webBroswer = new WebBroswerForm(keyWord, uaString, this, expireTimer);
                webBroswer.Text += " 来源:" + curSelectComboboxName.ToString() + " 系统浏览器:" + uaCaptionStr + " " + searchName;
                webBroswer.ShowDialog();
                GC.Collect();
                bRet = disconnectVPN();
                bRet = runCClean();
                
                FileLogger.Instance.LogInfo("cookie清理干净了，下一个任务!");
            }
            
        }
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
                    int regionIndex = rndGenerator.Next(0, count);
                    SendMessage(regionCombox, CB_SETCURSEL, regionIndex, 0);
                    SendMessage(regionCombox, CB_GETLBTEXT, regionIndex, curSelectComboboxName);

                    bool isButtonEnable = false;
                    do
                    {
                        isButtonEnable = IsWindowEnabled(disConnectBtnHWND);
                        if (isButtonEnable)
                        {
                            SendMessage(disConnectBtnHWND, BM_CLICK, 0, 0);
                            Thread.Sleep(100);//sleep 200ms
                        }
                        isButtonEnable = IsWindowEnabled(disConnectBtnHWND);
                    } while (isButtonEnable);

                    //isButtonEnable = false;
                    do
                    {
                        isButtonEnable = IsWindowEnabled(connectBtnHWND);
                        if (isButtonEnable)
                        {
                            SendMessage(connectBtnHWND, BM_CLICK, 0, 0);
                            Thread.Sleep(100);//sleep 200ms
                        }
                        isButtonEnable = IsWindowEnabled(connectBtnHWND);
                    } while (isButtonEnable);

                    Thread.Sleep(10000);
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
            Int32.TryParse(visitDeepCB.SelectedText.Trim(), out value);
            return value;
        }

        public int getExpireTime()
        {
            int value = 1;
            Int32.TryParse(jobExpireTimer.Text.Trim(), out value);
            return value;
        }

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

    }
}
