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

namespace WindowsFormsApplication1
{
    public partial class AuroBroswer : Form
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        [DllImport("User32.Dll")]
        public static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);
        [DllImport("User32.Dll")]
        public static extern void GetClassName(int h, StringBuilder s, int nMaxCount);
        [DllImport("User32.Dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd,UInt32 Msg,int wParam,int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, [Out] StringBuilder lParam);
        [DllImport("User32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, UInt32 nCmdShow);

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
        private static extern int SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar,
                                       int nPos, bool bRedraw);
        const UInt32 EM_LINESCROLL = 0x00B6;

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

        List<string> keywordCollection = new List<string>();
        int current = 0;
        System.Windows.Forms.Timer timeDown = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timeUp = new System.Windows.Forms.Timer();
        HTMLDocument currentDoc;

        public AuroBroswer()
        {
            InitializeComponent();
            //rndGenerator = new Random(DateTime.Now.Millisecond);
            //timeDown.Interval = 100;
            //timeDown.Tick += new EventHandler(timeDown_Tick);
            //timeUp.Interval = 100;
            //timeUp.Tick += new EventHandler(timeUp_Tick);
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
                randIndex = rndGenerator.Next(0, keywordCnt - 1);
                return keywordCollection[randIndex];
            }
            return "";
        }

        public bool searchBroswer(SHDocVw.WebBrowser wb, string keyword)
        {
            HTMLDocument document = ((HTMLDocument)wb.Document);
            currentDoc = document;
            IHTMLElement element = document.getElementById("q");
            HTMLInputElement searchKW = (HTMLInputElement)element;
            //HTMLInputElementClass searchKW = (HTMLInputElementClass)element;
            searchKW.value = keyword;

            //Point docHeight = new Point(document.body.offsetLeft, document.body.offsetTop);
            //ClientToScreen((IntPtr)wb.HWND, ref docHeight);
            //var rectDoc = document.body.getBoundingClientRect();
            //RECT rct;

            //if (!GetWindowRect((IntPtr)element.HWND, out rct))
            //{
            //    MessageBox.Show("ERROR");
            //    return false;
            //}
            var elements = document.getElementsByTagName("button");
            foreach (HTMLButtonElement searchBTN in elements)
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                if (searchBTN.innerHTML.Contains("搜 索"))
                {
                    int randX = rndGenerator.Next(0, searchBTN.offsetWidth-1);
                    int randY = rndGenerator.Next(0, searchBTN.offsetHeight-1);

                    var rect = searchBTN.getBoundingClientRect();
                    Point p = new Point(rect.left + randX, rect.top + randY);

                    ClientToScreen((IntPtr)wb.HWND, ref p);
                    //p = this.PointToScreen(p); 
                    SetCursorPos(p.X, p.Y);

                    //Thread.Sleep(6000);
                    searchBTN.click();
                    break;
                }
            }

            while (wb.Busy) { Thread.Sleep(100); }

            HTMLAnchorElement foundAnchorEle = null;
            var linkElements = document.getElementsByTagName("a");
            foreach (HTMLAnchorElement linkEle in linkElements)
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                if (linkEle.innerText == null)
                {
                    continue;
                }
                if (linkEle.innerText.Contains(sellerNameTB.Text.Trim()))
                {
                    //int randX = rndGenerator.Next(0, searchBTN.offsetWidth - 1);
                    //int randY = rndGenerator.Next(0, searchBTN.offsetHeight - 1);

                    //var rect = searchBTN.getBoundingClientRect();
                    //Point p = new Point(rect.left + randX, rect.top + randY);

                    //ClientToScreen((IntPtr)wb.HWND, ref p);
                    ////p = this.PointToScreen(p); 
                    //SetCursorPos(p.X, p.Y);

                    ////Thread.Sleep(6000);
                    //searchBTN.click();
                    foundAnchorEle = linkEle;
                    break;
                }
            }

            if (foundAnchorEle == null)
            {
                return false;
            }
            //if found first scroll the page
            if (compareCB.Checked == true)
            {
                //货比三家
                randVisitOther(document);
            }

#region 
            
            //search load

#endregion
            Thread.Sleep(1000);
            return true;
        }

        Random rndGenerator;
        private void button1_Click(object sender, EventArgs e)
        {
            keywordCollection.Clear();
            loadKeyWord();

            int loopCnt = Convert.ToInt32(broswerNumTXT.Text.Trim());
            for (int index = 0; index < loopCnt; index++)
            {
                //int for loop
                string keyWord = getRandKeyWord();
                if (keyWord == "")
                {
                    continue;
                }
                //changeVPN();
                
                object o = null;
                SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorerClass();
                SHDocVw.WebBrowser wb = (SHDocVw.WebBrowser)ie;
                IntPtr wbHWND = (IntPtr)wb.HWND;
                wb.Visible = true;
                //wb.FullScreen = true;
                //ShowWindow(wbHWND, SW_MAXIMIZE);

                //Do anything else with the window here that you wish
                wb.Navigate("http://www.taobao.com/", ref o, ref o, ref o, ref o);
                while (wb.Busy) { Thread.Sleep(100); }
                //ShowWindow(wbHWND, SW_MAXIMIZE);
                
                searchBroswer(wb, keyWord);
                wb.Quit();
            }
            
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

            int reCode = Marshal.GetLastWin32Error();
            MessageBox.Show("test", count + "--" + reCode);

            

            IntPtr connectBtnHWND = FindWindowEx(subDlgHWND, IntPtr.Zero, "Button", "连接");
            if (connectBtnHWND == IntPtr.Zero)
            {
                return false;
            }
            
            bool isConnect = false;
            while (!isConnect)
            {
                int regionIndex = rndGenerator.Next(0, count - 1);
                SendMessage(regionCombox, CB_SETCURSEL, regionIndex, 0);

                string statusTxt = GetControlText(staticTxtHWND);
                if (statusTxt != "Connected!")
                {
                    SendMessage(disConnectBtnHWND, BM_CLICK, 0, 0);
                    System.Threading.Thread.Sleep(5000);
                    SendMessage(connectBtnHWND, BM_CLICK, 0, 0);
                }
                else
                {
                    isConnect = true;
                }
            }
            
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

            bool isConnect = true;
            while (isConnect)
            {
                string statusTxt = GetControlText(staticTxtHWND);
                if (statusTxt == "Connected!")
                {
                    SendMessage(disConnectBtnHWND, BM_CLICK, 0, 0);
                    System.Threading.Thread.Sleep(5000);
                }
                else
                {
                    isConnect = false;
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

        public HTMLDivElement getItemBoxElement(HTMLAnchorElement linkEle)
        {
            if (linkEle == null)
            {
                return null;
            }

            IHTMLElement parentEle = linkEle.parentElement;//"col seller"
            if (parentEle == null)
            {
                return null;
            }

            IHTMLElement grandParentEle = parentEle.parentElement;//"class="row""
            if (grandParentEle == null)
            {
                return null;
            }
            IHTMLElement grandParentEle2 = grandParentEle.parentElement;//"item box"
            if (grandParentEle2 == null)
            {
                return null;
            }
            return (HTMLDivElement)grandParentEle2;
        }

        public bool randVisitOther(HTMLDocument document)
        {
            var divCollect = document.getElementsByTagName("div");

            HTMLDivElement tbContentDIV = null;
            foreach (HTMLDivElement el in divCollect)
            {
                object divClassAttr = el.getAttribute("className", 0);
                if (divClassAttr == null)
                {
                    continue;
                }
                string divClassName = (string)divClassAttr.ToString();
                if (divClassName == "")
                {
                    continue;
                }
                if (divClassName == "tb-content")
                {
                    tbContentDIV = el;
                    break;
                }
            }

            if (tbContentDIV == null)
            {
                return false;
            }
            var tbContentChildDIV = tbContentDIV.all;//.childNodes;
            if (tbContentChildDIV == null)
            {
                return false;
            }
            IHTMLElementCollection tbContentChildDIVCollect = (IHTMLElementCollection)tbContentChildDIV;
            HTMLDivElement childDIVNode = (HTMLDivElement)(tbContentChildDIVCollect.item(null, 0));
            var itemChildsVar = childDIVNode.children;//.all;//.childNodes;//item box

            IHTMLElementCollection itemChilds = (IHTMLElementCollection)itemChildsVar;
            int visitOtherCnt = rndGenerator.Next(1, 3);
            for (int i = 0; i < visitOtherCnt; i++ )//随机货比三家
            {
                int randIndex = rndGenerator.Next(0, itemChilds.length);
            }

            return true;
        }

        void timeDown_Tick(object sender, EventArgs e)
        {
            //HtmlDocument doc = (HtmlDocument)currentDoc;
            //int height = doc.Body.ScrollRectangle.Height;
            //current += height / 100;
            //if (current >= height)
            //{
            //    current = height;
            //    timeDown.Enabled = false;
            //    timeUp.Enabled = true;
            //}
            ////SetScrollPos(myTextBox.Handle,1,current,true);
            ////SendMessage(myTextBox.Handle,EM_LINESCROLL,0,
            ////                 current);
            //doc.Window.ScrollTo(new Point(0, current));
            HTMLDocument doc = currentDoc;
            mshtml.IHTMLElement2 body = (mshtml.IHTMLElement2)currentDoc.body;
            int height = body.scrollHeight;
            current += height / 100;
            if (current <= 0)
            {
                current = 0;
                timeUp.Enabled = false;
            }
            //doc.Window.ScrollTo(new Point(0, current));
            doc.parentWindow.scrollTo(0, current);
        }
        void timeUp_Tick(object sender, EventArgs e)
        {
            HTMLDocument doc = currentDoc;
            mshtml.IHTMLElement2 body = (mshtml.IHTMLElement2)currentDoc.body;
            int height = body.scrollHeight;
            current -= height / 100;
            if (current <= 0)
            {
                current = 0;
                timeUp.Enabled = false;
            }
            //doc.Window.ScrollTo(new Point(0, current));
            doc.parentWindow.scrollTo(0, current);
        }
       
    }
}
