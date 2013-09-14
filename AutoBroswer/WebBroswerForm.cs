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
        bool m_bLoaded = false;
        bool m_bFound = false;
        int currentScrolBarPos = 0;
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
        private string[] m_clickLinkItem = { "评价详情", "成交记录" };
        private string[] m_clickMainPageItem = { "首页", "查看所有宝贝", "进入店铺"};
        private string[] m_clickSpanItem = { "物流运费", "销　　量", "评　　价", "宝贝类型", "支　　付" };

        private List<HtmlElement> m_mainItemClickElement;//详情页三个模块点击
        private List<HtmlElement> m_mainItemSpanElement;//详情页Span模块点击
        private List<HtmlElement> m_otherItemClickElement;//其它宝贝

        private string m_otherItemPattern = @"http://item.taobao.com/item.htm?(.*)id=(\d{11})$";
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
        public WebBroswerForm(string inputKeyword, AutoBroswerForm _AutoBroswer)
        {
            InitializeComponent();
            InitialTabBrowser = new ExtendedWebBrowser()
            {
                Parent = Tabs.TabPages[0],
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true,
                Tag = Tabs.TabPages[0]
            };
            //InitialTabBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(InitialTabBrowser_DocumentCompleted);


            timeDown.Interval = 100;
            timeDown.Tick += new EventHandler(timeDown_Tick);
            timeUp.Interval = 100;
            timeUp.Tick += new EventHandler(timeUp_Tick);

            expireTimer.Interval = 15 * 60 * 1000;//15minus
            expireTimer.Tick += new EventHandler(expireTimer_Tick);
            keyWord = inputKeyword;
            autoBroswerFrom = _AutoBroswer;
            initValue();
            InitializeBrowserEvents(InitialTabBrowser);
            m_webPages.Add(InitialTabBrowser);

            //this.Location = new Point(0, 0);
            //this.Width = Screen.PrimaryScreen.WorkingArea.Size.Width; 
            //this.Height = Screen.PrimaryScreen.WorkingArea.Size.Height/2; 

            //string initStr = @"http://item.taobao.com/item.htm?spm=2013.1.w4004-3681024833.2.lUGWpP&id=19827789553";
            //string pattern = @"http://item.taobao.com/item.htm?(.*)&id=(\d{11,})";
            //Regex r2 = new Regex(pattern);
            //if (r2.IsMatch(initStr))
            //{
            //    Console.Write("123");
            //}
            //if (r.IsMatch(initStr))
            //{
            //    Console.Write("123");
            //}
            //if (Regex.Match(initStr, m_otherItemPattern).Length != 0)
            //{
            //    Console.Write("123213");
            //}
        }

        public bool begin()
        {
            //wb.FullScreen = true;
            //ShowWindow(wbHWND, SW_MAXIMIZE);
            InitialTabBrowser.Navigate("http://www.taobao.com/");
            //Do anything else with the window here that you wish
            //InitialTabBrowser.Navigate("http://www.taobao.com/", ref o, ref o, ref o, ref o);
            //while (InitialTabBrowser.Busy) { Thread.Sleep(100); }
            //while (InitialTabBrowser.ReadyState != WebBrowserReadyState.Complete) { Thread.Sleep(100); }
            //ShowWindow(wbHWND, SW_MAXIMIZE);
            //Thread.Sleep(5000);
            //searchBroswer(keyWord);
            return true;
        }

        public bool searchBroswer(string keyword)
        {
            HtmlDocument document = ((HtmlDocument)InitialTabBrowser.Document);
            HtmlElement textArea = document.GetElementById("q");
            textArea.InnerText = keyword;

            //wb.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(OnDocumentComplete);
            var elements = document.GetElementsByTagName("button");
            foreach (HtmlElement searchBTN in elements)
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                if (searchBTN.InnerHtml.Contains("搜 索"))
                {
                    int randX = autoBroswerFrom.rndGenerator.Next(0, searchBTN.OffsetRectangle.Width - 1);
                    int randY = autoBroswerFrom.rndGenerator.Next(0, searchBTN.OffsetRectangle.Height - 1);

                    //var rect = searchBTN.getBoundingClientRect();
                    Point p = GetOffset(searchBTN);
                    p.X += randX;
                    p.Y += randY;

                    //ClientToScreen((IntPtr)wb.HWND, ref p);
                    p = InitialTabBrowser.PointToScreen(p);
                    AutoBroswerForm.SetCursorPos(p.X, p.Y);

                    //Thread.Sleep(6000);
                    searchBTN.InvokeMember("click");//.click();
                    break;
                }
            }

            m_currentStep = ECurrentStep.ECurrentStep_Search;
            //while (m_bLoaded == false) { Thread.Sleep(100); }

            //int i = 0;
            //while (m_bLoaded == false && i < 50)
            //{
            //    //visit next page
            //}
            ////if found first scroll the page
            //if (autoBroswerFrom.isCompareRandCB() == true)
            //{
            //    //货比三家
            //    randVisitOther();
            //}

            //Thread.Sleep(10000);
            return true;
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

            if (m_currentStep == ECurrentStep.ECurrentStep_Load)
            {
                searchBroswer(keyWord);
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Search)
            {
                timeDown.Enabled = true;
                timeDown.Start();
                //timeUp.Enabled = true;
                //timeUp.Start();
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Compare)
            {
                timeDown.Enabled = true;
                timeDown.Start();

                m_iOtherItemStopTime = autoBroswerFrom.rndGenerator.Next(20, 30);
                pageMoniterTimer.Interval = m_iOtherItemStopTime * millSeconds;
                pageMoniterTimer.Enabled = true;
                pageMoniterTimer.Start();
                //timeUp.Enabled = true;
                //timeUp.Start();
            }else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_Main)
            {
                timeDown.Interval = 100;
                timeUp.Interval = 100;
                timeDown.Enabled = true;
                timeDown.Start();

                int stopTime = autoBroswerFrom.rndGenerator.Next(m_iMainItemStopMin, m_iMainItemStopMax);
                pageMoniterTimer.Interval = stopTime * millSeconds;
                pageMoniterTimer.Enabled = true;
                pageMoniterTimer.Start();
                getRandClickMainItem();
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_MainPage)
            {
                timeDown.Interval = 100;
                timeUp.Interval = 100;

                timeDown.Enabled = true;
                timeDown.Start();

                int stopTime = autoBroswerFrom.rndGenerator.Next(10, 25);
                pageMoniterTimer.Interval = stopTime * millSeconds;
                pageMoniterTimer.Enabled = true;
                pageMoniterTimer.Start();
                getRandItemInMainPage();
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_Other)
            {
                timeDown.Interval = 100;
                timeUp.Interval = 100;

                timeDown.Enabled = true;
                timeDown.Start();

                int stopTime = autoBroswerFrom.rndGenerator.Next(m_iOhterItemStopMin, m_iOtherItemStopMax);
                pageMoniterTimer.Interval = stopTime * millSeconds;
                pageMoniterTimer.Enabled = true;
                pageMoniterTimer.Start();
                //getRandClickMainItem();
            }
        }
        void expireTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        void timeDown_Tick(object sender, EventArgs e)
        {
            //int timeInterval = 100;
            //switch (m_currentStep)
            //{
            //    case ECurrentStep.ECurrentStep_Search:
            //        break;
            //    case ECurrentStep.ECurrentStep_Visit_Compare:
            //        {
            //            timeInterval = 500;
            //            timeDown.Interval = timeInterval;
            //        }
            //        break;
            //    case ECurrentStep.ECurrentStep_Visit_Me_Main:
            //    case ECurrentStep.ECurrentStep_Visit_Me_Other:
            //        {
            //            timeInterval = 1000;
            //            timeDown.Interval = timeInterval;
            //        }
            //        break;
            //}
            ExtendedWebBrowser currentBroswerPage = m_webPages[Tabs.SelectedIndex];
            HtmlDocument doc = (HtmlDocument)currentBroswerPage.Document;
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
            //int timeInterval = 100;
            //switch (m_currentStep)
            //{
            //    case ECurrentStep.ECurrentStep_Search:
            //        break;
            //    case ECurrentStep.ECurrentStep_Visit_Compare:
            //        {
            //            timeInterval = 500;
            //            timeUp.Interval = timeInterval;
            //        }
            //        break;
            //    case ECurrentStep.ECurrentStep_Visit_Me_Main:
            //    case ECurrentStep.ECurrentStep_Visit_Me_Other:
            //        {
            //            timeInterval = 1000;
            //            timeUp.Interval = timeInterval;
            //        }
            //        break;
            //}
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
                            //查看是否所有的都已经访问完毕，如果是的话，切回search-tab，删除临时的tab
                            //修改为ECurrentStep_Visit_Me
                            if (m_randItemElement.Count == 0)
                            {
                            }
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
                ClickItemByItem(webBroswer.Handle, element);
                Console.WriteLine("Current Click:" + element.InnerText + " left Num:" + alinkCount);

                m_mainItemClickElement.Remove(element);
                timeDown.Enabled = true;
            }
            else
            {
                alinkCount = m_mainItemSpanElement.Count;
                //点击一次，等下一次到timer-up再点击
                HtmlElement element = m_mainItemSpanElement[alinkCount - 1];
                ClickItemByItem(webBroswer.Handle, element);
                Console.WriteLine("Current Click:" + element.InnerText + " left Num:" + alinkCount);

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

            ////深度随机
            //int totalItemCounts = totalItemLinkList.Count;
            //int randItemCounts = autoBroswerFrom.getVisitDeep();
            //if (autoBroswerFrom.isVisitDeepRand())
            //{
            //    randItemCounts = autoBroswerFrom.rndGenerator.Next(1, 5);
            //}
            //FileLogger.Instance.LogInfo("访问深度:" + randItemCounts);
            //if (totalItemCounts > randItemCounts)
            //{
            //    for (int i = 0; i < randItemCounts;i++ )
            //    {
            //        int randItemIndex = autoBroswerFrom.rndGenerator.Next(0, totalItemCounts);
            //        HtmlElement selectItem = totalItemLinkList[randItemIndex];
            //        m_otherItemClickElement.Add(selectItem);
            //        FileLogger.Instance.LogInfo("其他宝贝:" + selectItem.OuterHtml);
            //    }
            //}

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
            //
            if (isFound)
            {
                randSelectOtherItem();
                randVisitOtherItemInSearch();

            }
            else
            {
                m_currentStep = ECurrentStep.ECurrentStep_Search;
                gotoNextPage();

            }
            return true;
        }

        //在搜索页，随机提前其它卖家的宝贝(货比三家)
        public bool getRandItemElement()
        {
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
                return false;//访问结束了
            }

            HtmlElement visitItem = m_randItemElement[0];
            m_randItemElement.Remove(visitItem);
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
            ClickItemByItem(currentWB.Handle, visitItem);
            m_currentStep = ECurrentStep.ECurrentStep_Visit_Me_Other;
            return true;
        }
        public bool ClickNextPage(IntPtr hwnd, HtmlElement visitItem)
        {
            //滚到最前面会有问题，坐标转换异常
            visitItem.ScrollIntoView(false);

            Point p = GetOffset(visitItem);
            ClickOnPoint(hwnd, p);
            return true;
        }
        public bool ClickItemByPicBox(IntPtr hwnd, HtmlElement visitItem)
        {
            visitItem.ScrollIntoView(true);

            Point p = GetOffset(visitItem);
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
            ClickOnPoint(hwnd, p);
            return true;
        }
        public bool ClickItemByItem(IntPtr hwnd, HtmlElement visitItem)
        {
            visitItem.ScrollIntoView(false);

            Point p = GetOffset(visitItem);

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
            visitItem.InvokeMember("click");
            Console.WriteLine("ClickItemByItem CurrentX:" + p.X + ",currentY:" + p.Y);
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
            ClickItemByItem(webBroswer.Handle, m_myMainPageElement);
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
                if (linkEle.InnerText.Contains(autoBroswerFrom.getSellerName()))
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
        public string nextPage;
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
            Point p = GetOffset(nextPageLink);

            p = InitialTabBrowser.PointToClient(p);
            AutoBroswerForm.SetCursorPos(p.X, p.Y);

            HtmlDocument doc = (HtmlDocument)InitialTabBrowser.Document;
            doc.Window.ScrollTo(new Point(0, p.Y));

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
            begin();
        }

        private void ClickOnPoint(IntPtr wndHandle, Point clientPoint)
        {
            //var oldPos = Cursor.Position;

            /// get screen coordinates
            ClientToScreen(wndHandle, ref clientPoint);

            Console.WriteLine("CurrentX:" + clientPoint.X + ",currentY:" + clientPoint.Y);
            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);
            mouse_event(0x00000002, 0, 0, 0, UIntPtr.Zero); /// left mouse button down
            mouse_event(0x00000004, 0, 0, 0, UIntPtr.Zero); /// left mouse button up

            /// return mouse 
            //Cursor.Position = oldPos;
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
        public void PageMoniterTimeEvent( object source, EventArgs e)
        {
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
                pageMoniterTimer.Enabled = false;
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_Main)
            {
                //主宝贝不关闭,设置监视其它宝贝定时器
                timeDown.Enabled = true;
                timeDown.Interval = 1000;
                timeDown.Start();

                timeUp.Enabled = true;
                timeUp.Interval = 1000;
                timeUp.Start();

                //bool bRandVisitOther = true;
                //bRandVisitOther = randVisitOtherItem();
                //if (bRandVisitOther == false)//has visit done
                //{
                //    //visitMe();
                //}
                enterMainPage();
                FileLogger.Instance.LogInfo("访问主宝贝结束了,应该要跳转了");
                //int stopTime = autoBroswerFrom.rndGenerator.Next(m_iMainItemStopMin, m_iMainItemStopMax);
                //pageMoniterTimer.Interval = stopTime * millSeconds;
                //pageMoniterTimer.Enabled = true;
                //pageMoniterTimer.Start();
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_MainPage)
            {
                bool bRandVisitOther = true;
                bRandVisitOther = randVisitOtherItem();
                if (bRandVisitOther == false)//has visit done
                {

                    //shut down the forms
                    //visitMe();
                }
                else
                {

                }
            }
            else if (m_currentStep == ECurrentStep.ECurrentStep_Visit_Me_Other)
            {
                TabPage seltab = this.Tabs.SelectedTab;
                int seltabindex = this.Tabs.SelectedIndex;
                ExtendedWebBrowser seltabBroswer = m_webPages[seltabindex];

                Tabs.Controls.Remove(seltab);
                m_webPages.Remove(seltabBroswer);
                Tabs.SelectTab(seltabindex - 1);

                Thread.Sleep(500);

                bool bRandVisitOther = true;
                bRandVisitOther = randVisitOtherItem();
                if (bRandVisitOther == false)//has visit done
                {
                    ShutDownWinForms();
                }
            }
            
        }

        public void ShutDownWinForms()
        {
            timeUp.Enabled = false;
            timeDown.Enabled = false;
            pageMoniterTimer.Enabled = false;
            this.Dispose();
        }
    }
}
