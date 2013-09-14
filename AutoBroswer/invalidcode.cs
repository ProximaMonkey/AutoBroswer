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
using System.IO; //
namespace AutoBroswer
{
    class invalidcode
    {
        private void button1_Click(object sender, EventArgs e)
        {
            ////// Create a new instance of the Firefox driver.

            ////// Notice that the remainder of the code relies on the interface, 
            ////// not the implementation.

            ////// Further note that other drivers (InternetExplorerDriver,
            ////// ChromeDriver, etc.) will require further configuration 
            ////// before this example will work. See the wiki pages for the
            ////// individual drivers at http://code.google.com/p/selenium/wiki
            ////// for further information.
            ////IWebDriver driver = new FirefoxDriver();

            //////Notice navigation is slightly different than the Java version
            //////This is because 'get' is a keyword in C#
            ////driver.Navigate().GoToUrl("http://www.google.com/");

            ////// Find the text input element by its name
            ////IWebElement query = driver.FindElement(By.Name("q"));

            ////// Enter something to search for
            ////query.SendKeys("Cheese");

            ////// Now submit the form. WebDriver will find the form for us from the element
            ////query.Submit();

            ////// Google's search is rendered dynamically with JavaScript.
            ////// Wait for the page to load, timeout after 10 seconds
            ////WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ////wait.Until((d) => { return d.Title.ToLower().StartsWith("cheese"); });

            ////// Should see: "Cheese - Google Search"
            ////System.Console.WriteLine("Page title is: " + driver.Title);

            //////Close the browser
            ////driver.Quit();
            //string IELocation = @"%ProgramFiles%\Internet Explorer\iexplore.exe";
            //IELocation = System.Environment.ExpandEnvironmentVariables(IELocation);

            //Console.WriteLine("Launching IE ");
            //Process p = Process.Start(IELocation, @"http://www.baidu.com");
            //Thread.Sleep(3000);

            //Console.WriteLine("Attaching to IE ... ");
            //InternetExplorer ie = null;

            //string ieWindowName = "空白页";
            //if (p != null)
            //{
            //    //Console.WriteLine("Process handle is: " + p.MainWindowHandle.ToString());
            //    SHDocVw.ShellWindows allBrowsers = new ShellWindowsClass();
            //    Console.WriteLine("Number of active IEs :" + allBrowsers.Count.ToString());
            //    if (allBrowsers.Count != 0)
            //    {
            //        SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass();
            //        foreach (SHDocVw.InternetExplorer ieVW in shellWindows)
            //        {
            //            // 判斷視窗是否為 iexplore
            //            if (Path.GetFileNameWithoutExtension(ieVW.FullName).ToLower().Equals("iexplore"))
            //            {
            //                ie = ieVW;
            //                break;
            //            }
            //        }


            //        //for (int i = 0; i < allBrowsers.Count; i++)
            //        //{
            //        //    //IWebBrowser2 iwb2 = allBrowsers.Item(i) as IWebBrowser2; 

            //        //    InternetExplorer eitem = (InternetExplorer)allBrowsers.Item(i);
            //        //    int l = eitem.HWND;
            //        //    int j = 0;
            //        //    j++;
            //        //    if (eitem.HWND == (int)p.MainWindowHandle.ToInt32())
            //        //    {
            //        //        ie = eitem;
            //        //        break;
            //        //    }
            //        //}
            //    }
            //    else
            //        throw new Exception("Faul to find IE");
            //}
            //else
            //    throw new Exception("Fail to launch IE");

            ////var ie = new SHDocVw.InternetExplorer();
            ////ie.Visible = true;
            ////object o = null;
            ////Console.WriteLine("打开百度首页...");
            ////ie.Navigate(@"http://www.baidu.com", ref o, ref o, ref o, ref o);
            //Thread.Sleep(1000);

            //HTMLDocument doc = (HTMLDocument)ie.Document;

            //Console.WriteLine("网站标题：" + doc.title);

            //Console.WriteLine("输入搜索关键字：飞苔博客");
            //var keyEle = doc.getElementById("kw");
            //keyEle.setAttribute("value", "飞苔博客", 0);

            //Console.WriteLine("点击[百度一下]按钮，进行搜索");
            //var btnSubmit = doc.getElementById("su");
            //btnSubmit.click();
            //Thread.Sleep(500);

            //Console.WriteLine("网站标题：" + doc.title);
            ////var relatedSearchResultEle = doc.getElementsByTagName("SPAN").Cast<ihtmlelement>().Where(ele => ele.className == "nums").FirstOrDefault();
            //Console.WriteLine("百度相关搜索结果：");
            //p.Close();
            ////Console.WriteLine(relatedSearchResultEle.innerHTML);
            object o = null;
            SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorerClass();
            
            SHDocVw.WebBrowser wb = (SHDocVw.WebBrowser)ie;
            wb.Visible = true;
            wb.FullScreen = true;
            //Do anything else with the window here that you wish
            wb.Navigate("http://www.taobao.com/", ref o, ref o, ref o, ref o);
            while (wb.Busy) { Thread.Sleep(100); }
            HTMLDocument document = ((HTMLDocument)wb.Document);
            IHTMLElement element = document.getElementById("q");
            HTMLInputElementClass email = (HTMLInputElementClass)element;
            email.value = "包装盒子";

            var elements = document.getElementsByTagName("button");
            foreach (HTMLButtonElementClass element1 in elements)
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                if (element1.innerHTML.Contains("搜 索"))
                {
                    element1.click();
                }
            }
            //for (int i = 0; i < count; i++)
            //{

            //    //去抓標籤名字有a的，像是連結<a href=xx>這種

            //    //奇怪的是這個方法如果要抓一些標籤都抓不到，像是<td><tr>那些=.=

            //    //所以那些我是用另外的方法抓的，等下會講
            //    if (document.all[i].TagName.ToLower().Equals("button"))
            //    {

            //        //GetAttribute就是去取得標籤的屬性的內容，例：

            //        //<a href ="我是屬性的內容" target=_blank>，不過有些屬性取不到，像是class

            //        if (doc.All[i].GetAttribute("type").Contains("submit"))
            //        {                             //InnerText就是取得標籤中間的內容，<標籤>內容</標籤>
            //            //richTextContent1.AppendText(doc.All[i].InnerText);
            //            //richTextContent1.AppendText(doc.All[i].GetAttribute("href"));
            //            //richTextContent1.AppendText(Environment.NewLine);

            //            ////另外這個函式可以去更改HTML裡面屬性的內容
            //            //XXX.SetAttribute("value", "Hello");
            //            int j = i;
            //            j++;
            //        }
            //    }
            //}
            IHTMLElement search = document.getElementById(@"tag:BUTTON&value:搜 索");
            HTMLButtonElementClass searchEle = (HTMLButtonElementClass)search;
            searchEle.click();
            //email = null;
            //element = document.getElementById("Passwd");
            //HTMLInputElementClass pass = (HTMLInputElementClass)element;
            //pass.value = "pass";
            //pass = null;
            //element = document.getElementById("signIn");
            //HTMLInputElementClass subm = (HTMLInputElementClass)element;
            //subm.click();
            //subm = null;
            //while (wb.Busy) { Thread.Sleep(100); }
            //wb.Navigate("https://adwords.google.co.uk/o/Targeting/Explorer?", ref o, ref o, ref o, ref o);
            //while (wb.Busy) { Thread.Sleep(100); }
            //string connString = "SERVER=localhost;" +
            //        "DATABASE=test;" +
            //        "UID=root;";

            ////create your mySQL connection
            //MySqlConnection cnMySQL = new MySqlConnection(connString);
            ////create your mySql command object
            //MySqlCommand cmdMySQL = cnMySQL.CreateCommand();
            ////create your mySQL reeader object
            //MySqlDataReader reader;
            ////open the mySQL connection
            //cnMySQL.Open();
            //int j = 1;
            //cmdMySQL.CommandText = "SELECT * FROM `emails1` WHERE `send`>'" + j + "' order by `send` asc limit 0,1";
            //reader = cmdMySQL.ExecuteReader();
            //reader.Read();
            //j = (int)reader.GetValue(1);
            HTMLElementCollection col = default(HTMLElementCollection);
            document = ((HTMLDocument)wb.Document);
            col = (HTMLElementCollection)document.getElementsByTagName("textarea");
            //the error happens here in this typecast


            //Console.WriteLine(j);
            wb.Quit();
        }

        //public Point GetOffset(IHTMLElement el)
        //{
        //    //get element pos
        //    Point pos = new Point(el.OffsetRectangle.Left, el.OffsetRectangle.Top);

        //    //get the parents pos
        //    IHTMLElement tempEl = el.parentElement;//.OffsetParent;
        //    while (tempEl != null)
        //    {
        //        pos.X += tempEl.OffsetRectangle.Left;
        //        pos.Y += tempEl.OffsetRectangle.Top;
        //        tempEl = tempEl.parentElement;//.OffsetParent;
        //    }

        //    return pos;
        //}
        public Point GetOffset(IHTMLElement el)
        {
            //get element pos
            Point pos = new Point(el.offsetLeft, el.offsetTop);

            //get the parents pos
            IHTMLElement tempEl = el.parentElement;//.OffsetParent;
            while (tempEl != null)
            {
                pos.X += tempEl.offsetLeft;
                pos.Y += tempEl.offsetTop;
                tempEl = tempEl.parentElement;//.OffsetParent;
            }

            return pos;
        }
        public bool isItemFound(HTMLDivElement divElement)
        {
            if (divElement == null)
            {
                return false;
            }

            //var itemBoxChilds = divElement.childNodes;
            //foreach (HTMLDivElement el in itemBoxChilds)
            //{
            //    var itemChilds = el.childNodes;
            //    foreach (HTMLDivElement item in itemChilds)
            //    {

            //        object divClassAttr = item.getAttribute("className", 0);
            //        if (divClassAttr == null)
            //        {
            //            continue;
            //        }
            //        string divClassName = (string)divClassAttr.ToString();
            //        if (divClassName == "")
            //        {
            //            continue;
            //        }

            //        object divItemIDAttr = item.getAttribute("nid", 0);
            //        if (divItemIDAttr == null)
            //        {
            //            continue;
            //        }
            //        string divItemID = (string)divClassName.ToString();
            //        if (divItemID == "")
            //        {
            //            continue;
            //        }
            //        if (divItemID == "col item icon-datalink")
            //        {
            //            //tbContentDIV = el;
            //            break;
            //        }
            //    }
            //}
            //foreach (HTMLDivElement item in (IHTMLElementCollection)itemChilds)
            //{
            //    object divClassAttr = item.getAttribute("className", 0);
            //    if (divClassAttr == null)
            //    {
            //        continue;
            //    }
            //    string divClassName = (string)divClassAttr.ToString();
            //    if (divClassName == "")
            //    {
            //        continue;
            //    }

            //    object divItemIDAttr = item.getAttribute("nid", 0);
            //    if (divItemIDAttr == null)
            //    {
            //        continue;
            //    }
            //    string divItemID = (string)divClassName.ToString();
            //    if (divItemID == "")
            //    {
            //        continue;
            //    }
            //    if (divItemID == "col item icon-datalink")
            //    {
            //        //tbContentDIV = el;
            //        break;
            //    }
            //}
            return true;
        }

        
//How to find the position of an element in the Web Browser Control?
//up vote 3 down vote favorite
	

//I have a form with web browser control, which loads a webpage ( Its working fine, the page loads ok)

//Now my issue is, i want to find whether a particular url-link is below the fold or above the fold ( i mean, whether the user have to scroll down to see this link, or not ) whether this v is visible with out scrolling or we need to scroll to see it.. I hope i am clear

//i have done extensive search, but looks like no info is available about find an html elements position (above or below current view)

//Does anybody knows something about this and can point me in right direction please? (i am looking for c# solution - WinForms )

//Update: Big thanks to John Koerner for the code. Really appreciate the time and effort he put in solving my issue.

//And to Jonathan & everybody else also.. I wish i could mark Jonathans reply also as answer, but it allows only one reply to be marked as answer. His comment was also clear and useful hint. THanks you guys are great!!!
//c# winforms webbrowser-control
//share|improve this question
	
//edited Feb 27 at 20:39
//ThinkingStiff
//13.5k945105
	
//asked Feb 9 '12 at 19:41
//ugly hand
//1
	
	
//Metro? WinForms? WPF? Silverlight? MonoTouch? – SLaks Feb 9 '12 at 19:43
	
//WinForms (edited and added) – ugly hand Feb 9 '12 at 19:49
	
//If it is not visible, do you just want to scroll it into view? If so, you can use the ScrollIntoView method to show it. – John Koerner Feb 9 '12 at 19:55
	
//no. i just need to know if this link is visible with out scrolling.. or do we need to scroll to see it. Problem is this link may appear on any place on the page (not fixed). THank u anyway – ugly hand Feb 9 '12 at 20:05
//2 Answers
//active oldest votes
//up vote 2 down vote accepted
	

//Ok, I have tested this on google and stackoverflow and it seems to work:

//private bool isElementVisible(WebBrowser web, string elementID)
//{

//    var element = web.Document.All[elementID];

//    if (element == null)
//        throw new ArgumentException(elementID + " did not return an object from the webbrowser");

//    // Calculate the offset of the element, all the way up through the parent nodes
//    var parent = element.OffsetParent;
//    int xoff = element.OffsetRectangle.X;
//    int yoff = element.OffsetRectangle.Y;

//    while (parent != null)
//    {
//        xoff += parent.OffsetRectangle.X;
//        yoff += parent.OffsetRectangle.Y;
//        parent = parent.OffsetParent;
//    }

//    // Get the scrollbar offsets
//    int scrollBarYPosition = web.Document.GetElementsByTagName("HTML")[0].ScrollTop;
//    int scrollBarXPosition = web.Document.GetElementsByTagName("HTML")[0].ScrollLeft;

//    // Calculate the visible page space
//    Rectangle visibleWindow = new Rectangle(scrollBarXPosition, scrollBarYPosition, web.Width, web.Height);

//    // Calculate the visible area of the element
//    Rectangle elementWindow = new Rectangle(xoff,yoff,element.ClientRectangle.Width, element.ClientRectangle.Height);

//    if (visibleWindow.IntersectsWith(elementWindow))
//    {
//        return true;
//    }
//    else
//    {
//        return false;
//    }
//}

    }
}
