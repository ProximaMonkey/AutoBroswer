using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoBroswer;
using System.Threading;
using System.Runtime.InteropServices;
using System.Timers;
using System.Text.RegularExpressions;
using mshtml;

namespace AutoBroswer
{
    public partial class WebBroswerForm : Form
    {
        System.Windows.Forms.Timer timeDown = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timeUp = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer pageMoniterTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer expireTimer = new System.Windows.Forms.Timer();

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool block);

        string keyWord;
        AutoBroswerForm autoBroswerFrom;
        enum ECurrentStep
        {
            ECurrentStep_Load,
            ECurrentStep_Search,
            ECurrentStep_Visit_Compare,
            ECurrentStep_Visit_Me_Main,//访问主宝贝
            ECurrentStep_Visit_Me_MainPage,//访问主页
            ECurrentStep_Visit_Me_Other//访问其他宝贝
        }

        ECurrentStep m_currentStep = ECurrentStep.ECurrentStep_Load;

        int currentScrolBarPos = 0;
        private string m_uaString;
        public int m_iMainItemStopMin;//主宝贝停留时间
        public int m_iMainItemStopMax;//
        public int m_iOhterItemStopMin;//次宝贝停留时间
        public int m_iOtherItemStopMax;

        public int m_iOtherItemStopTime;//其它家宝贝时间，随机在20-30s
        public bool m_isVisitRand;
        const int millSeconds = 1000;

        ExtendedWebBrowser InitialTabBrowser;
        private List<ExtendedWebBrowser> m_webPages;

        private HtmlElement m_myItemElement;//在搜索页的ELEMENT,
        private HtmlElement m_myMainPageElement;//在在主宝贝页面中的首页
        private List<HtmlElement> m_randItemElement;
        private string[] m_clickLinkItem = { "评价详情", "成交记录", "宝贝详情" };
        private string[] m_clickMainPageItem = { "首页", "查看所有宝贝", "进入店铺"};
        private string[] m_clickSpanItem = { "物流运费", "销　　量", "评　　价", "宝贝类型", "支　　付" };

        private List<HtmlElement> m_mainItemClickElement;//详情页三个模块点击
        private List<HtmlElement> m_mainItemSpanElement;//详情页Span模块点击
        private List<HtmlElement> m_otherItemClickElement;//其它宝贝

        private string m_otherItemPattern = @"http://item.taobao.com/item.htm?(.*)id=(\d{11})$";

        public bool isNormalQuit = false;
        Regex otherItemRegex;

        public bool initValue()
        {
            otherItemRegex = new Regex(m_otherItemPattern);
            m_iMainItemStopMin = autoBroswerFrom.getMainItemMinTime();
            m_iMainItemStopMax = autoBroswerFrom.getMainItemMaxTime();
            m_iOhterItemStopMin = autoBroswerFrom.getOtherItemMinTime();
            m_iOtherItemStopMax = autoBroswerFrom.getOtherItemMaxTime();
            m_isVisitRand = autoBroswerFrom.isVisitDeepRand();

            m_randItemElement = new List<HtmlElement>();
            m_mainItemClickElement = new List<HtmlElement>();
            m_mainItemSpanElement = new List<HtmlElement>();
            m_otherItemClickElement = new List<HtmlElement>();

            m_webPages = new List<ExtendedWebBrowser>();

            pageMoniterTimer.Tick += new EventHandler(PageMoniterTimeEvent);
            //pageMoniterTimer.AutoReset = false;//do only one times
            //pageMoniterTimer.Enabled = true;
            return true;
        }
        public WebBroswerForm(string inputKeyword, string uaString, AutoBroswerForm _AutoBroswer, int expireTime)
        {
            InitializeComponent();

            m_uaString = uaString;
            InitialTabBrowser = new ExtendedWebBrowser()
            {
                Parent = Tabs.TabPages[0],
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true,
                Tag = Tabs.TabPages[0]
            };
            InitialTabBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(InitialTabBrowser_DocumentCompleted);


            timeDown.Interval = 100;
            timeDown.Tick += new EventHandler(timeDown_Tick);
            timeUp.Interval = 100;
            timeUp.Tick += new EventHandler(timeUp_Tick);

            expireTimer.Interval = expireTime;// 15 * 60 * millSeconds;//15minus
            expireTimer.Enabled = true;
            expireTimer.Tick += new EventHandler(expireTimer_Tick);
            expireTimer.Start();

            keyWord = inputKeyword;
            autoBroswerFrom = _AutoBroswer;
            initValue();
            InitializeBrowserEvents(InitialTabBrowser);
            m_webPages.Add(InitialTabBrowser);
        }

        public bool searchBroswer(string keyword)
        {
            HtmlDocument document = ((HtmlDocument)InitialTabBrowser.Document);
            HtmlElement textArea = document.GetElementById("q");
            textArea.InnerText = keyword;

            //InitialTabBrowser.Navigate("javascript:" + wbElementMouseSimulate.clickSearch + "clickSearch();");
            //wb.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(OnDocumentComplete);
            var elements = document.GetElementsByTagName("button");
            foreach (HtmlElement searchBTN in elements)
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                string className = searchBTN.GetAttribute("className");
                if (className == "btn-search" || searchBTN.InnerText == "搜 索")
                {
                    int randX = autoBroswerFrom.rndGenerator.Next(0, searchBTN.OffsetRectangle.Width - 1);
                    int randY = autoBroswerFrom.rndGenerator.Next(0, searchBTN.OffsetRectangle.Height - 1);

                    //Point p = GetOffset(searchBTN);
                    //p.X += randX;
                    //p.Y += randY;
                    //p = InitialTabBrowser.PointToScreen(p);
                    //ClickOnPoint(InitialTabBrowser.Handle, p);
                    //AutoBroswerForm.SetCursorPos(p.X, p.Y);

                    //object[] objArgs = new object[] { searchBTN };
                    //InitialTabBrowser.Document.InvokeScript("TestClick", objArgs);
                    //Thread.Sleep(6000);
                    //searchBTN.InvokeMember("click");//.click();
//Rectangle rect = wbElementMouseSimulate.GetElementRect(document.Body.DomElement as mshtml.IHTMLElement, searchBTN.DomElement as mshtml.IHTMLElement);
//Point p = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
                    //p = InitialTabBrowser.PointToScreen(p);
                    //AutoBroswerForm.SetCursorPos(p.X, p.Y);
//ClickOnPoint(InitialTabBrowser.Handle, p);
                    //searchBTN.ScrollIntoView(true);
                    //Point p = GetOffset(searchBTN);
                    //p.Y -= InitialTabBrowser.Document.GetElementsByTagName("HTML")[0].ScrollTop;
                    //p.X += randX;
                    //p.Y += randY;
                    //p = InitialTabBrowser.PointToScreen(p);
                    //ClickOnPoint(InitialTabBrowser.Handle, p);
                    //AutoBroswerForm.SetCursorPos(p.X, p.Y);
                    //Thread.Sleep(6000);
                    //searchBTN.InvokeMember("click");
                    InitialTabBrowser.Document.InvokeScript(@"simulate", new object[] { searchBTN, "click", "{ pointerX: " + searchBTN.OffsetRectangle.Left + ", pointerY: " +searchBTN.OffsetRectangle.Top+ " }" });                    
                    break;
                }
            }

            m_currentStep = ECurrentStep.ECurrentStep_Search;
            return true;
        }
        private Point GetElementPosition(HtmlElement current_element)
        {
            int x_add = current_element.OffsetRectangle.Width;
            int y_add = current_element.OffsetRectangle.Height;
            int x = current_element.OffsetRectangle.Left;
            int y = current_element.OffsetRectangle.Top;
            while ((current_element = current_element.Parent) != null)
            {
                x += current_element.OffsetRectangle.Left;
                y += current_element.OffsetRectangle.Top;
            }

            y -= (InitialTabBrowser.Location.Y);

            return new Point(x + (x_add / 2), y + (y_add / 2));
        }
        private void SetCursorPos(int p, int p_2)
        {
            throw new NotImplementedException();
        }
        private void Window_Error(object sender,  HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = true;
        }
        private void InitialTabBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
            ExtendedWebBrowser currentBroswerPage = m_webPages[Tabs.SelectedIndex];



            //if (currentBroswerPage.ReadyState != WebBrowserReadyState.Complete) return;
            //if ((e.Url.AbsolutePath == "blank") || (e.Url != currentBroswerPage.Url)) return;
            //if (currentBroswerPage.Document.Body.All.Count < 10) return;

            HtmlElement head = currentBroswerPage.Document.GetElementsByTagName("head")[0];
            HtmlElement testScript = currentBroswerPage.Document.CreateElement("script");
            IHTMLScriptElement element = (IHTMLScriptElement)testScript.DomElement;
            //element.type = @"text/javascript";
            element.text = wbElementMouseSimulate.simulateMouseEvent;

            head.AppendChild(testScript);

            TabPage seltab = this.Tabs.SelectedTab;

            if (m_currentStep == ECurrentStep.ECurrentStep_Load)
            {
                searchBroswer(keyWord);
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Search)
            {
                SetTimerDownEnable(50);

                seltab.Text = "查找宝贝";
                //timeUp.Enabled = true;
                //timeUp.Start();
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Compare)
            {
                SetTimerDownEnable(50);

                seltab.Text = "货比三家";

                m_iOtherItemStopTime = autoBroswerFrom.rndGenerator.Next(20, 30);
                pageMoniterTimer.Interval = m_iOtherItemStopTime * millSeconds;
                pageMoniterTimer.Enabled = true;
                pageMoniterTimer.Start();
                //timeUp.Enabled = true;
                //timeUp.Start();
            }else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_Main)
            {
                SetTimerDownEnable(500);

                seltab.Text = "访问主宝贝";

                int stopTime = autoBroswerFrom.rndGenerator.Next(m_iMainItemStopMin, m_iMainItemStopMax);
 
                string labStr = "主宝贝停留时间:" + stopTime + "S";
                FileLogger.Instance.LogInfo(labStr);
                stopTimeLabel.Text = labStr;

                pageMoniterTimer.Interval = stopTime * millSeconds;
                pageMoniterTimer.Enabled = true;
                pageMoniterTimer.Start();
                getRandClickMainItem();
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_MainPage)
            {
                SetTimerDownEnable(300);

                seltab.Text = "访问首页";

                int stopTime = autoBroswerFrom.rndGenerator.Next(30, 50);
                string labStr = "首页停留时间:" + stopTime + "S";
                FileLogger.Instance.LogInfo(labStr);
                stopTimeLabel.Text = labStr;

                pageMoniterTimer.Interval = stopTime * millSeconds;
                pageMoniterTimer.Enabled = true;
                pageMoniterTimer.Start();
                getRandItemInMainPage();
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_Other)
            {
                SetTimerDownEnable(500);

                seltab.Text = "访问其它随机宝贝";

                int stopTime = autoBroswerFrom.rndGenerator.Next(m_iOhterItemStopMin, m_iOtherItemStopMax);
                string labStr = "其它随机宝贝停留时间:" + stopTime + "S";
                FileLogger.Instance.LogInfo(labStr);
                stopTimeLabel.Text = labStr;

                pageMoniterTimer.Interval = stopTime * millSeconds;
                pageMoniterTimer.Enabled = true;
                pageMoniterTimer.Start();
                //getRandClickMainItem();
            }
        }
        void expireTimer_Tick(object sender, EventArgs e)
        {
            isNormalQuit = true;
            ShutDownWinForms();
        }

        void timeDown_Tick(object sender, EventArgs e)
        {
            ExtendedWebBrowser currentBroswerPage = m_webPages[Tabs.SelectedIndex];
            HtmlDocument doc = (HtmlDocument)currentBroswerPage.Document;
            if (doc.Body == null)
            {
                return;
            }
            int height = doc.Body.ScrollRectangle.Height;
            currentScrolBarPos += height / 30;
            if (currentScrolBarPos >= height)
            {
                currentScrolBarPos = height;
                timeDown.Enabled = false;
                timeUp.Enabled = true;
            }
            doc.Window.ScrollTo(new Point(0, currentScrolBarPos));
        }
        void timeUp_Tick(object sender, EventArgs e)
        {
            TabPage currentPage = Tabs.TabPages[Tabs.SelectedIndex];
            ExtendedWebBrowser currentBroswerPage = m_webPages[Tabs.SelectedIndex];
            HtmlDocument doc = (HtmlDocument)currentBroswerPage.Document;
            int height = doc.Body.ScrollRectangle.Height;
            currentScrolBarPos -= height / 100;
            
            if (currentScrolBarPos <= 0)
            {
                currentScrolBarPos = 0;
                timeUp.Enabled = false;
            }
            doc.Window.ScrollTo(new Point(0, currentScrolBarPos));
            if (currentScrolBarPos <= 0)
            {
                switch (m_currentStep)
                {
                    case ECurrentStep.ECurrentStep_Search:
                        searchInPage();
                        break;
                    case ECurrentStep.ECurrentStep_Visit_Compare:
                        {
                            //等待超时
                        }
                        break;
                    case ECurrentStep.ECurrentStep_Visit_Me_Main:
                    case ECurrentStep.ECurrentStep_Visit_Me_Other:
                        clickItemPage();
                        break;
                }
                
            }
        }
        public bool clickItemPage()
        {
            //详情页面点击
            int alinkCount = m_mainItemClickElement.Count;
            ExtendedWebBrowser webBroswer = m_webPages[Tabs.SelectedIndex];

            if (alinkCount != 0)
            {
                //点击一次，等下一次到timer-up再点击
                HtmlElement element = m_mainItemClickElement[alinkCount - 1];
                ClickItemByItem(webBroswer.Handle, webBroswer.Document, element);
                m_mainItemClickElement.Remove(element);
                timeDown.Enabled = true;
            }
            else
            {
                alinkCount = m_mainItemSpanElement.Count;
                //点击一次，等下一次到timer-up再点击
                HtmlElement element = m_mainItemSpanElement[alinkCount - 1];
                ClickItemByItem(webBroswer.Handle, webBroswer.Document, element);
                
                m_mainItemSpanElement.Remove(element);
            }
            
            return true;
        }
        public bool getRandItemInMainPage()
        {
            //首页随机获取其它宝贝
            ExtendedWebBrowser currentBroswerPage = m_webPages[Tabs.SelectedIndex];
            List<HtmlElement> totalItemLinkList = new List<HtmlElement>();//首页页面里面所有的其它宝贝链接

            var linkElements = currentBroswerPage.Document.GetElementsByTagName("a");
            foreach (HtmlElement linkEle in linkElements)
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                if (linkEle.InnerText == null)
                {
                    continue;
                }
                string hrefAttrName = linkEle.GetAttribute("href");
                if (otherItemRegex.IsMatch(hrefAttrName))
                {
                    totalItemLinkList.Add(linkEle);
                }

            }

            //深度随机
            int totalItemCounts = totalItemLinkList.Count;
            int randItemCounts = autoBroswerFrom.getVisitDeep();
            if (autoBroswerFrom.isVisitDeepRand())
            {
                randItemCounts = autoBroswerFrom.rndGenerator.Next(1, 5);
            }
            FileLogger.Instance.LogInfo("访问深度:" + randItemCounts);
            if (totalItemCounts > randItemCounts)
            {
                for (int i = 0; i < randItemCounts; i++)
                {
                    int randItemIndex = autoBroswerFrom.rndGenerator.Next(0, totalItemCounts);
                    HtmlElement selectItem = totalItemLinkList[randItemIndex];
                    m_otherItemClickElement.Add(selectItem);
                    FileLogger.Instance.LogInfo("其他宝贝:" + selectItem.OuterHtml);
                }
            }
            return true;
        }
        public bool getRandClickMainItem()
        {
            //宝贝详情页面 鼠标点击点
            ExtendedWebBrowser currentBroswerPage = m_webPages[Tabs.SelectedIndex];
            List<HtmlElement> totalItemLinkList = new List<HtmlElement>();//主宝贝页面里面所有的其它宝贝链接
            List<HtmlElement> totalEnterMainPageLinkList = new List<HtmlElement>();//进入首页的方式
            var linkElements = currentBroswerPage.Document.GetElementsByTagName("a");
            foreach (HtmlElement linkEle in linkElements)
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                if (linkEle.InnerText == null)
                {
                    continue;
                }
                for (int i = 0; i < m_clickLinkItem.Length; i++ )
                {
                    if (linkEle.InnerText.Contains(m_clickLinkItem[i]))
                    {
                        m_mainItemClickElement.Add(linkEle);
                        break;
                    }
                }

                for (int i = 0; i < m_clickMainPageItem.Length; i++)
                {
                    if (linkEle.InnerText.ToString().Trim() == (m_clickMainPageItem[i]))
                    {
                        totalEnterMainPageLinkList.Add(linkEle);
                        break;
                    }
                }
                string hrefAttrName = linkEle.GetAttribute("href");
                if (otherItemRegex.IsMatch(hrefAttrName))
                {
                    totalItemLinkList.Add(linkEle);
                }
                
            }

            int totalMainPageCount = totalEnterMainPageLinkList.Count;
            int enterMainPageIndex = 0;
            if (totalMainPageCount > 1)
            {
                enterMainPageIndex = autoBroswerFrom.rndGenerator.Next(0, totalMainPageCount);
            }

            m_myMainPageElement = totalEnterMainPageLinkList[enterMainPageIndex];

            var spanElements = currentBroswerPage.Document.GetElementsByTagName("span");
            foreach (HtmlElement linkEle in spanElements)
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                if (linkEle.InnerText == null)
                {
                    continue;
                }
                for (int i = 0; i < m_clickSpanItem.Length; i++)
                {
                    if (linkEle.InnerText.Contains(m_clickSpanItem[i]))
                    {
                        m_mainItemSpanElement.Add(linkEle);
                        break;
                    }
                }

            }
            return true;
        }
        public bool searchInPage()
        {
            bool isFound = false;
            HtmlElement foundAnchorEle = searchIsFound(ref isFound); 
            if (isFound)
            {
                FileLogger.Instance.LogInfo("在当前页:" + pageInfo + " 找到");
                randSelectOtherItem();
                randVisitOtherItemInSearch();

            }
            else
            {
                m_currentStep = ECurrentStep.ECurrentStep_Search;
                FileLogger.Instance.LogInfo("在当前页:" + pageInfo + ",没有找到，下一页继续");
                gotoNextPage();

            }
            return true;
        }
        
        public HtmlElement getPicElement(HtmlElement itemBoxEle)
        {
            //通过itembox 得到pic element
            if (itemBoxEle == null)
            {
                return null;
            }
            HtmlElement divPicEle = itemBoxEle.All[0];
            if (divPicEle == null)
            {
                return null;
            }
            HtmlElement divPicBoxEle = divPicEle.All[0];
            if (divPicBoxEle == null)
            {
                return null;
            }
            return divPicBoxEle;
        }
        public HtmlElement getItemBoxElement(HtmlElement linkEle)
        {
            if (linkEle == null)
            {
                return null;
            }

            HtmlElement parentEle = linkEle.Parent;//"col seller"
            if (parentEle == null)
            {
                return null;
            }

            HtmlElement grandParentEle = parentEle.Parent;//"class="row""
            if (grandParentEle == null)
            {
                return null;
            }
            HtmlElement grandParentEle2 = grandParentEle.Parent;//"item box"
            if (grandParentEle2 == null)
            {
                return null;
            }
            return (HtmlElement)grandParentEle2;
        }
        //搜索页面随机
        public bool randVisitOtherItemInSearch()
        {
            int leftVisitCount = m_randItemElement.Count;
            if (leftVisitCount == 0)
            {
                FileLogger.Instance.LogInfo("货比三家结束了");
                return false;//访问结束了
            }

            HtmlElement visitItem = m_randItemElement[0];
            m_randItemElement.Remove(visitItem);
            FileLogger.Instance.LogInfo("开始浏览其他家的" + visitItem.OuterHtml);
            //Tabs.SelectTab(0);//返回 默认的Tab

            ClickItemByPicBox(InitialTabBrowser.Handle, visitItem);
            m_currentStep = ECurrentStep.ECurrentStep_Visit_Compare;
            return true;
        }
        public bool randVisitOtherItem()
        {
            int leftVisitCount = m_otherItemClickElement.Count;
            if (leftVisitCount == 0)
            {
                return false;//访问结束了
            }

            HtmlElement visitItem = m_otherItemClickElement[0];
            m_otherItemClickElement.Remove(visitItem);
            //Tabs.SelectTab(0);//返回 默认的Tab

            ExtendedWebBrowser currentWB = m_webPages[Tabs.SelectedIndex];
            ClickItemByItem(currentWB.Handle, currentWB.Document, visitItem);
            m_currentStep = ECurrentStep.ECurrentStep_Visit_Me_Other;
            return true;
        }
        public bool ClickNextPage(IntPtr hwnd, HtmlElement visitItem)
        {
            //滚到最前面会有问题，坐标转换异常
            visitItem.ScrollIntoView(true);
            //visitItem.InvokeMember("onclick");
            //visitItem.InvokeMember("click");

            //Point p = GetOffset(visitItem);
            //ClickOnPoint(hwnd, p);
            Point p = GetOffset(visitItem);
            p.Y -= InitialTabBrowser.Document.GetElementsByTagName("HTML")[0].ScrollTop;
            ClickOnPoint(hwnd, p);
            //Rectangle rect = wbElementMouseSimulate.GetElementRect(InitialTabBrowser.Document.Body.DomElement as mshtml.IHTMLElement, visitItem.DomElement as mshtml.IHTMLElement);
            //Point p = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            //InitialTabBrowser.Document.Window.ScrollTo(p);
            //ClickOnPointInClient(hwnd, p);

            return true;
        }

        //在搜索页点击
        public HtmlElement getHyperLinkByPicBox(HtmlElement visitItem)
        {
            //HtmlElement hyperLink;
            return visitItem.All[0].All[0];
        }

        //在搜索页点击
        public bool ClickItemByPicBox(IntPtr hwnd, HtmlElement visitItem)
        {
            visitItem.ScrollIntoView(true);

            Point p = GetOffset(visitItem);
            //visitItem.InvokeMember("click");
            p.Y = 25;

            int randX = 0;
            int randY = 0;
            if (visitItem.OffsetRectangle.Width - 5 > 0)
            {
                int temp = (visitItem.OffsetRectangle.Width - 5) / 4;
                randX = autoBroswerFrom.rndGenerator.Next(temp, temp * 3);
            }
            if (visitItem.OffsetRectangle.Height - 25 > 0)
            {
                int temp = (visitItem.OffsetRectangle.Height - 25) / 4;
                randY = autoBroswerFrom.rndGenerator.Next(temp, temp * 3);
            }
            p.X += randX;
            p.Y += randY;

            //Thread.Sleep(500);
            ClickOnPoint(hwnd, p);
            //object []objArgs = new object[] { visitItem.All[0].All[0] };
            //InitialTabBrowser.Document.InvokeScript("TestClick", objArgs);
            //HtmlElement aLink = getHyperLinkByPicBox(visitItem);
            //aLink.InvokeMember("click");
            return true;
        }

        //在主页点击其它标记
        public bool ClickItemByItem(IntPtr hwnd, HtmlDocument doc, HtmlElement visitItem)
        {
            visitItem.ScrollIntoView(true);

            Point p = GetOffset(visitItem);
            p.Y -= doc.GetElementsByTagName("HTML")[0].ScrollTop;

            int randX = 0;
            int randY = 0;
            if (visitItem.OffsetRectangle.Width - 1 > 0)
            {
                randX = autoBroswerFrom.rndGenerator.Next(2, visitItem.OffsetRectangle.Width - 1);
            }
            if (visitItem.OffsetRectangle.Height - 3 > 0)
            {
                randY = autoBroswerFrom.rndGenerator.Next(2, visitItem.OffsetRectangle.Height - 3);
            }
            p.X += randX;
            p.Y += randY;
            ClickOnPoint(hwnd, p);
            return true;
        }
        public bool randSelectOtherItem()
        {
            var divCollect = InitialTabBrowser.Document.GetElementsByTagName("div");
            List<HtmlElement> colItemCollect = new List<HtmlElement>();
            foreach (HtmlElement el in divCollect)
            {
                string divClassAttr = el.GetAttribute("className");
                string nidString = el.GetAttribute("nid");
                if (divClassAttr == "col item" && nidString != "")
                {
                    colItemCollect.Add(el);
                }
            }

            //HtmlElementCollection itemChilds = tbContentChildDIV.Children;//item box
            int itemListCount = colItemCollect.Count;
            if (itemListCount == 0)//查找出来的宝贝个数
            {
                return false;
            }

            int visitOtherCnt = autoBroswerFrom.rndGenerator.Next(1, 3);
            for (int i = 0; i < visitOtherCnt; i++)//随机货比三家
            {
                int randIndex = autoBroswerFrom.rndGenerator.Next(0, itemListCount);

                HtmlElement itemElement = colItemCollect[randIndex];
                HtmlElement picBoxElement = getPicElement(itemElement);
                m_randItemElement.Add(picBoxElement);
                
            }

            return true;
        }
        public bool visitMe()
        {
            if (m_myItemElement == null)
            {
                return false;
            }

            HtmlElement itemBoxEle = getItemBoxElement(m_myItemElement);
            if (itemBoxEle == null)
            {
                return false;
            }

            HtmlElement itemPicEle = itemBoxEle.All[0].All[0];
            ClickItemByPicBox(InitialTabBrowser.Handle, itemPicEle);
            m_currentStep = ECurrentStep.ECurrentStep_Visit_Me_Main;
            return true;
        }
        public bool enterMainPage()
        {
            if (m_myMainPageElement == null)
            {
                return false;
            }
            ExtendedWebBrowser webBroswer = m_webPages[Tabs.SelectedIndex];
            ClickItemByItem(webBroswer.Handle, webBroswer.Document, m_myMainPageElement);
            m_currentStep = ECurrentStep.ECurrentStep_Visit_Me_MainPage;
            return true;
        }

        public HtmlElement searchIsFound(ref bool isFound)
        {
            //HtmlElement foundAnchorEle = null;
            var linkElements = InitialTabBrowser.Document.GetElementsByTagName("a");
            foreach (HtmlElement linkEle in linkElements)
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                if (linkEle.InnerText == null)
                {
                    continue;
                }
                if (linkEle.InnerText.ToString().Trim() == (autoBroswerFrom.getSellerName()) && 
                    linkEle.Parent.TagName.ToLower().Trim() == "div")
                {
                    m_myItemElement = linkEle;
                    break;
                }
            }

            if (m_myItemElement == null)
            {
                isFound = false;
                return m_myItemElement;
            }
            isFound = true;
            return m_myItemElement;
        }

        public string pageInfo;
        public string prevPage;
        public string nextPage = "1/100";
        public bool gotoNextPage()
        {
            HtmlElement foundAnchorEle = null;
            var linkElements = InitialTabBrowser.Document.GetElementsByTagName("div");
            foreach (HtmlElement linkEle in linkElements)
            {
                string className = linkEle.GetAttribute("className");
                if (className == "page-top")
                {
                    pageInfo = linkEle.InnerText;
                    foundAnchorEle = linkEle;
                    break;
                }
            }
            HtmlElement nextPageLink = null;
            if (foundAnchorEle == null)
            {
                return false;
            }
            foreach (HtmlElement linkEle in foundAnchorEle.All)
            {
                string className = linkEle.GetAttribute("className");
                if (className == "page-next")
                {
                    HtmlElement pageInfoEle = linkEle.FirstChild;
                    //pageInfo = pageInfoEle.InnerText;
                    nextPageLink = linkEle;
                    break;
                }
            }
            if (nextPage == null || nextPageLink == null)
            {
                FileLogger.Instance.LogInfo("没有找到宝贝！");
                isNormalQuit = true;
                ShutDownWinForms();
                return false;
            }
            //Point p = GetOffset(nextPageLink);

            //p = InitialTabBrowser.PointToClient(p);
            //AutoBroswerForm.SetCursorPos(p.X, p.Y);

            //HtmlDocument doc = (HtmlDocument)InitialTabBrowser.Document;
            //doc.Window.ScrollTo(new Point(0, p.Y));

            ClickNextPage(InitialTabBrowser.Handle, nextPageLink);
            //nextPageLink.InvokeMember("click");//.click();
            return true;
        }
        public Point GetOffset(HtmlElement el)
        {
            //get element pos
            Point pos = new Point(el.OffsetRectangle.Left, el.OffsetRectangle.Top);

            //get the parents pos
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                pos.X += tempEl.OffsetRectangle.Left;
                pos.Y += tempEl.OffsetRectangle.Top;
                tempEl = tempEl.OffsetParent;
            }

            return pos;
        }


        private void WebBroswerForm_Shown(object sender, EventArgs e)
        {
            InitialTabBrowser.Navigate("http://www.taobao.com/");
        }

        private void ClickOnPoint(IntPtr wndHandle, Point clientPoint)
        {
            this.Activate();
            this.WindowState = FormWindowState.Normal;
            SetForegroundWindow(wndHandle);

            FileLogger.Instance.LogInfo("clientX:" + clientPoint.X + ",Y:" + clientPoint.Y);
            Point oldPos = Cursor.Position;
            ClientToScreen(wndHandle, ref clientPoint);
            FileLogger.Instance.LogInfo("screenX:" + clientPoint.X + ",Y:" + clientPoint.Y);
            /// set cursor on coords, and press mouse
            BlockInput(true);
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);
            
            Thread.Sleep(1000);
            mouse_event(0x00000002, 0, 0, 0, UIntPtr.Zero); /// left mouse button down
            mouse_event(0x00000004, 0, 0, 0, UIntPtr.Zero); /// left mouse button up
            Thread.Sleep(50);
            BlockInput(false);
            //Cursor.Position = oldPos;
        }

        private void ClickOnPointInClient(IntPtr wndHandle, Point clientPoint)
        {
            Point oldPos = Cursor.Position;
            //ClientToScreen(wndHandle, ref clientPoint);
            /// set cursor on coords, and press mouse
            //int position = ((clientPoint.X & 0xFFFF) << 16) | (clientPoint.Y & 0xFFFF);

            //AutoBroswerForm.SendMessage(wndHandle, 0x0201, 0, (position));
            //AutoBroswerForm.SendMessage(wndHandle, 0x0202, 0, (position));

            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);
            mouse_event(0x00000002, (uint)clientPoint.X, (uint)clientPoint.Y, 0, UIntPtr.Zero); /// left mouse button down
            mouse_event(0x00000004, (uint)clientPoint.X, (uint)clientPoint.Y, 0, UIntPtr.Zero); /// left mouse button up
            Cursor.Position = oldPos;
        }
        private void InitializeBrowserEvents(ExtendedWebBrowser SourceBrowser)
        {
            SourceBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(InitialTabBrowser_DocumentCompleted);

            SourceBrowser.NewWindow2 += new EventHandler<NewWindow2EventArgs>(SourceBrowser_NewWindow2);
        }

        void SourceBrowser_NewWindow2(object sender, NewWindow2EventArgs e)
        {

            TabPage NewTabPage = new TabPage()
            {
                Text = "Loading..."
            };

            ExtendedWebBrowser NewTabBrowser = new ExtendedWebBrowser()
            {
                Parent = NewTabPage,
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true,
                Tag = NewTabPage
            };

            e.PPDisp = NewTabBrowser.Application;
            InitializeBrowserEvents(NewTabBrowser);

            Tabs.TabPages.Add(NewTabPage);
            m_webPages.Add(NewTabBrowser);
            Tabs.SelectedTab = NewTabPage;

        }

        public void SetTimerUpEnable(int tickInter)
        {
            timeDown.Enabled = false;
            timeDown.Stop();

            timeUp.Interval = tickInter;
            timeUp.Enabled = true;
            timeUp.Start();
        }

        public void SetTimerDownEnable(int tickInter)
        {
            timeUp.Enabled = false;
            timeUp.Stop();

            timeDown.Interval = tickInter;
            timeDown.Enabled = true;
            timeDown.Start();
        }
        public void PageMoniterTimeEvent( object source, EventArgs e)
        {
            timeUp.Enabled = false;
            timeDown.Enabled = false;

            if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Compare)
            {
                TabPage seltab = this.Tabs.SelectedTab;
                int seltabindex = this.Tabs.SelectedIndex;
                ExtendedWebBrowser seltabBroswer = m_webPages[seltabindex];

                Tabs.Controls.Remove(seltab);
                m_webPages.Remove(seltabBroswer);
                Tabs.SelectTab(seltabindex - 1);

                Thread.Sleep(500);
                bool bRandVisitOther = true;
                bRandVisitOther = randVisitOtherItemInSearch();
                if (bRandVisitOther == false)//has visit done
                {
                    visitMe();
                }
                else
                {
                    //
                }
                pageMoniterTimer.Enabled = false;
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_Main)
            {
                //主宝贝不关闭,设置监视其它宝贝定时器
                //timeDown.Interval = 1000;
                enterMainPage();
                FileLogger.Instance.LogInfo("访问主宝贝结束了,进入宝贝首页");
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_MainPage)
            {
                bool bRandVisitOther = true;
                bRandVisitOther = randVisitOtherItem();
                if (bRandVisitOther == false)//has visit done
                {
                    FileLogger.Instance.LogInfo("首页访问结束，找不到其它宝贝？");
                    isNormalQuit = true;
                    ShutDownWinForms();
                }
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_Other)
            {
                TabPage seltab = this.Tabs.SelectedTab;
                int seltabindex = this.Tabs.SelectedIndex;
                ExtendedWebBrowser seltabBroswer = m_webPages[seltabindex];

                FileLogger.Instance.LogInfo("其他宝贝访问结束，关闭当前标签");
                Tabs.Controls.Remove(seltab);
                m_webPages.Remove(seltabBroswer);

                if (seltabindex == 0)
                {
                    FileLogger.Instance.LogInfo("其他宝贝访问结束，只有一个标签不对啦！！！");
                    ShutDownWinForms();
                    return;
                }


                Tabs.SelectTab(seltabindex - 1);//回退到店铺首页tab

                bool bRandVisitOther = true;
                bRandVisitOther = randVisitOtherItem();
                if (bRandVisitOther == false)//has visit done
                {
                    isNormalQuit = true;
                    ShutDownWinForms();
                }
            }
            
        }

        public void ShutDownWinForms()
        {
            timeUp.Enabled = false;
            timeDown.Enabled = false;
            pageMoniterTimer.Enabled = false;
            this.Close(); 
            this.Dispose();
        }
    }
}
