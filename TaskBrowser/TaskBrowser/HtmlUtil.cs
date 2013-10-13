namespace TaskBrowser
{
    using mshtml;
    using SHDocVw;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class HtmlUtil
    {
        public static void CheckMousePoint(IntPtr hwnd, ref Point fakeMousePoint, mshtml.IHTMLDocument2 doc)
        {
            int clientWidth = 0;
            int clientHeight = 0;
            int scrollWidth = 0;
            int scrollHeight = 0;
            Random random = new Random();
            if (GetWindowWidthAndHeight(hwnd, doc, ref clientWidth, ref clientHeight, ref scrollWidth, ref scrollHeight) != null)
            {
                if (((fakeMousePoint.X <= 0) || (fakeMousePoint.X >= clientWidth)) && (clientWidth > 8))
                {
                    fakeMousePoint.X = random.Next(4, clientWidth - 8);
                }
                if (((fakeMousePoint.Y <= 0) || (fakeMousePoint.Y >= clientHeight)) && (clientHeight > 8))
                {
                    fakeMousePoint.Y = random.Next(4, clientHeight - 8);
                }
            }
        }

        public static bool ClickButtonRect(IntPtr hwnd, mshtml.IHTMLDocument2 doc, string itemName, string tagStr, string indexStr, ref bool isClick, ref Point fakeMousePoint, ClickEvent clickEvent)
        {
            mshtml.IHTMLElement elem = GetButtonElement(doc, itemName, tagStr, indexStr);
            bool flag = false;
            if (elem != null)
            {
                flag = true;
                Rectangle elementRect = GetElementRect(doc.body, elem);
                isClick = false;
                if ((elementRect.Width > 0) && (elementRect.Height > 0))
                {
                    Random random = new Random();
                    int num = random.Next(elementRect.Width);
                    int num2 = random.Next(elementRect.Height);
                    SetMousePoint(hwnd, ref fakeMousePoint, elementRect.X + num, elementRect.Y + num2, doc);
                    isClick = isClickElement(hwnd, doc, elem, elementRect.X + num, elementRect.Y + num2, clickEvent);
                }
            }
            if (elem != null)
            {
                Marshal.ReleaseComObject(elem);
            }
            return flag;
        }

        public static bool ClickCheckedRect(IntPtr hwnd, mshtml.IHTMLDocument2 doc, string itemName, string tagStr, string indexStr, ref bool isClick, ref Point fakeMousePoint, ClickEvent clickEvent)
        {
            mshtml.IHTMLElement elem = GetCheckedElement(doc, itemName, tagStr, indexStr);
            bool flag = false;
            if (elem != null)
            {
                flag = true;
                Rectangle elementRect = GetElementRect(doc.body, elem);
                isClick = false;
                if ((elementRect.Width > 0) && (elementRect.Height > 0))
                {
                    Random random = new Random();
                    int num = random.Next(elementRect.Width);
                    int num2 = random.Next(elementRect.Height);
                    SetMousePoint(hwnd, ref fakeMousePoint, elementRect.X + num, elementRect.Y + num2, doc);
                    isClick = isClickElement(hwnd, doc, elem, elementRect.X + num, elementRect.Y + num2, clickEvent);
                }
            }
            if (elem != null)
            {
                Marshal.ReleaseComObject(elem);
            }
            return flag;
        }

        public static bool ClickLinkRect(IntPtr hwnd, mshtml.IHTMLDocument2 doc, string itemName, string keyword, string tagStr, string indexStr, ref bool isClick, ref Point fakeMousePoint, ClickEvent clickEvent, ref int clickLinkCount)
        {
            Rectangle elementRect = new Rectangle();
            bool flag = false;
            mshtml.IHTMLElement elem = GetLinkElement(doc, itemName, keyword, tagStr, indexStr);
            if (elem != null)
            {
                flag = true;
                elementRect = GetElementRect(doc.body, elem);
                isClick = false;
                if ((elementRect.Width > 0) && (elementRect.Height > 0))
                {
                    Random random = new Random();
                    int num = random.Next(elementRect.Width);
                    int num2 = random.Next(elementRect.Height);
                    SetMousePoint(hwnd, ref fakeMousePoint, elementRect.X + num, elementRect.Y + num2, doc);
                    isClick = isClickElement(hwnd, doc, elem, elementRect.X + num, elementRect.Y + num2, clickEvent);
                }
                clickLinkCount++;
            }
            return flag;
        }

        public static bool ClickRadioRect(IntPtr hwnd, mshtml.IHTMLDocument2 doc, string itemName, string tagStr, string indexStr, ref bool isClick, ref Point fakeMousePoint, ClickEvent clickEvent)
        {
            mshtml.IHTMLElement elem = GetRadioElement(doc, itemName, tagStr, indexStr);
            bool flag = false;
            if (elem != null)
            {
                flag = true;
                Rectangle elementRect = GetElementRect(doc.body, elem);
                isClick = false;
                if ((elementRect.Width > 0) && (elementRect.Height > 0))
                {
                    Random random = new Random();
                    int num = random.Next(elementRect.Width);
                    int num2 = random.Next(elementRect.Height);
                    SetMousePoint(hwnd, ref fakeMousePoint, elementRect.X + num, elementRect.Y + num2, doc);
                    isClick = isClickElement(hwnd, doc, elem, elementRect.X + num, elementRect.Y + num2, clickEvent);
                }
            }
            if (elem != null)
            {
                Marshal.ReleaseComObject(elem);
            }
            return flag;
        }

        public static mshtml.IHTMLElement GetButtonElement(mshtml.IHTMLDocument2 doc, string itemName, string tagStr, string indexStr)
        {
            mshtml.IHTMLElement element = null;
            ElementTag iD = ElementTag.ID;
            int num = 0;
            int num2 = 0;
            if ((indexStr != string.Empty) && (indexStr != ""))
            {
                num2 = WindowUtil.StringToInt(indexStr);
            }
            if ((tagStr != string.Empty) && (tagStr != ""))
            {
                iD = (ElementTag) WindowUtil.StringToInt(tagStr);
            }
            mshtml.IHTMLElementCollection o = (mshtml.IHTMLElementCollection) doc.all.tags("input");
            foreach (mshtml.IHTMLElement element2 in o)
            {
                if ((IsElementMatchType(element2, "submit") || IsElementMatchType(element2, "button")) && IsElementMatch(element2, iD, itemName, ""))
                {
                    if (num == num2)
                    {
                        element = element2;
                        break;
                    }
                    num++;
                }
            }
            if (o != null)
            {
                Marshal.ReleaseComObject(o);
            }
            o = (mshtml.IHTMLElementCollection) doc.all.tags("button");
            foreach (mshtml.IHTMLElement element3 in o)
            {
                if (IsElementMatch(element3, iD, itemName, ""))
                {
                    if (num == num2)
                    {
                        return element3;
                    }
                    num++;
                }
            }
            return element;
        }

        public static bool GetButtonRect(mshtml.IHTMLDocument2 doc, ref Rectangle rect, string itemName, string tagStr, string indexStr)
        {
            mshtml.IHTMLElement elem = GetButtonElement(doc, itemName, tagStr, indexStr);
            bool flag = false;
            if (elem != null)
            {
                rect = GetElementRect(doc.body, elem);
                flag = true;
            }
            if (elem != null)
            {
                Marshal.ReleaseComObject(elem);
            }
            return flag;
        }

        public static mshtml.IHTMLElement GetCheckedElement(mshtml.IHTMLDocument2 doc, string itemName, string tagStr, string indexStr)
        {
            mshtml.IHTMLElementCollection o = (mshtml.IHTMLElementCollection) doc.all.tags("input");
            mshtml.IHTMLElement element = null;
            ElementTag iD = ElementTag.ID;
            bool flag = false;
            int num = 0;
            int num2 = 0;
            if (!string.IsNullOrEmpty(tagStr))
            {
                iD = (ElementTag) WindowUtil.StringToInt(tagStr);
            }
            if (!string.IsNullOrEmpty(indexStr))
            {
                num = WindowUtil.StringToInt(indexStr);
            }
            foreach (mshtml.IHTMLElement element2 in o)
            {
                if (IsElementMatchType(element2, "checkbox") && IsElementMatch(element2, iD, itemName, ""))
                {
                    if (num == num2)
                    {
                        element = element2;
                        flag = true;
                        break;
                    }
                    num2++;
                }
            }
            if (o != null)
            {
                Marshal.ReleaseComObject(o);
            }
            if (!flag)
            {
                o = (mshtml.IHTMLElementCollection) doc.all.tags("label");
                foreach (mshtml.IHTMLElement element3 in o)
                {
                    if (element3.getAttribute("htmlFor", 0) != null)
                    {
                        string str = element3.getAttribute("htmlFor", 0).ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            mshtml.IHTMLElement ele = doc.all.item(str, Missing.Value) as mshtml.IHTMLElement;
                            if (((ele != null) && IsElementMatchType(ele, "checkbox")) && IsElementMatch(element3, iD, itemName, ""))
                            {
                                if (num == num2)
                                {
                                    element = element3;
                                    flag = true;
                                    break;
                                }
                                num2++;
                            }
                        }
                    }
                }
            }
            if (o != null)
            {
                Marshal.ReleaseComObject(o);
            }
            return element;
        }

        public static bool GetCheckedRect(mshtml.IHTMLDocument2 doc, ref Rectangle rect, string itemName, string tagStr, string indexStr)
        {
            bool flag = false;
            mshtml.IHTMLElement elem = GetCheckedElement(doc, itemName, tagStr, indexStr);
            if (elem != null)
            {
                rect = GetElementRect(doc.body, elem);
                flag = true;
            }
            return flag;
        }

        public static Rectangle GetElementRect(mshtml.IHTMLElement body, mshtml.IHTMLElement elem)
        {
            int top;
            int num3;
            int num4;
            int left = top = num3 = num4 = 0;
            mshtml.IHTMLElement2 element = elem as mshtml.IHTMLElement2;
            IHTMLRect o = element.getBoundingClientRect();
            elem = element as mshtml.IHTMLElement;
            left = o.left;
            top = o.top;
            num3 = o.right - o.left;
            num4 = o.bottom - o.top;
            if (body.document != elem.document)
            {
                List<mshtml.IHTMLDOMNode> frames = new List<mshtml.IHTMLDOMNode>();
                GetEleParentFrames(body as mshtml.IHTMLDOMNode, elem as mshtml.IHTMLDOMNode, frames);
                foreach (mshtml.IHTMLDOMNode node in frames)
                {
                    IHTMLRect rect2 = (node as mshtml.IHTMLElement2).getBoundingClientRect();
                    left += rect2.left;
                    top += rect2.top;
                }
            }
            Rectangle rectangle = new Rectangle {
                X = left,
                Y = top,
                Width = num3,
                Height = num4
            };
            if (o != null)
            {
                Marshal.ReleaseComObject(o);
            }
            return rectangle;
        }

        private static bool GetEleParentFrames(mshtml.IHTMLDOMNode root, mshtml.IHTMLDOMNode node, List<mshtml.IHTMLDOMNode> frames)
        {
            bool flag = false;
            if (root == node)
            {
                return true;
            }
            bool flag2 = false;
            switch (root.nodeName.ToLower())
            {
                case "frame":
                case "iframe":
                    flag2 = true;
                    break;
            }
            IHTMLDOMChildrenCollection childNodes = null;
            if (flag2)
            {
                SHDocVw.IWebBrowser2 browser = root as SHDocVw.IWebBrowser2;
                if (browser != null)
                {
                    mshtml.IHTMLDocument2 document = browser.Document as mshtml.IHTMLDocument2;
                    if (document != null)
                    {
                        mshtml.IHTMLDOMNode parentElement = document.body.parentElement as mshtml.IHTMLDOMNode;
                        childNodes = parentElement.childNodes as IHTMLDOMChildrenCollection;
                    }
                }
            }
            if (childNodes == null)
            {
                childNodes = root.childNodes as IHTMLDOMChildrenCollection;
            }
            if (childNodes == null)
            {
                return false;
            }
            for (int i = 0; i < childNodes.length; i++)
            {
                mshtml.IHTMLDOMNode node3 = childNodes.item(i) as mshtml.IHTMLDOMNode;
                if (GetEleParentFrames(node3, node, frames))
                {
                    if (flag2)
                    {
                        frames.Add(root);
                    }
                    flag = true;
                }
            }
            return flag;
        }

        public static mshtml.IHTMLDocument2 GetIEWindowDocument(IntPtr hwnd)
        {
            object ppvObject = new object();
            int lParam = 0;
            Guid riid = new Guid();
            int wMsg = WindowUtil.RegisterWindowMessage("WM_Html_GETOBJECT");
            WindowUtil.ObjectFromLresult(WindowUtil.SendMessage(hwnd, wMsg, 0, lParam), ref riid, 0, ref ppvObject);
            mshtml.IHTMLDocument2 document = (mshtml.IHTMLDocument2) ppvObject;
            ppvObject = null;
            return document;
        }

        public static mshtml.IHTMLElement GetInputElement(mshtml.IHTMLDocument2 doc, string itemName, string tagStr, string indexStr)
        {
            mshtml.IHTMLElementCollection o = (mshtml.IHTMLElementCollection) doc.all.tags("input");
            mshtml.IHTMLElement element = null;
            ElementTag iD = ElementTag.ID;
            int num = 0;
            int num2 = 0;
            if (!string.IsNullOrEmpty(indexStr))
            {
                num = WindowUtil.StringToInt(indexStr);
            }
            if ((tagStr != string.Empty) && (tagStr != ""))
            {
                iD = (ElementTag) WindowUtil.StringToInt(tagStr);
            }
            foreach (mshtml.IHTMLElement element2 in o)
            {
                if (IsElementMatch(element2, iD, itemName, ""))
                {
                    if (num2 == num)
                    {
                        element = element2;
                        break;
                    }
                    num2++;
                }
            }
            if (o != null)
            {
                Marshal.ReleaseComObject(o);
            }
            return element;
        }

        public static bool GetInputElementRect(mshtml.IHTMLDocument2 doc, ref Rectangle rect, string itemName, string tagStr, string indexStr)
        {
            bool flag = false;
            mshtml.IHTMLElement elem = GetInputElement(doc, itemName, tagStr, indexStr);
            if (elem != null)
            {
                rect = GetElementRect(doc.body, elem);
                flag = true;
            }
            return flag;
        }

        public static mshtml.IHTMLElement GetLinkElement(mshtml.IHTMLDocument2 doc, string itemName, string keyword, string tagStr, string indexStr)
        {
            mshtml.IHTMLElement element = null;
            mshtml.IHTMLElementCollection links;
            ElementTag outerText = ElementTag.outerText;
            int num = 0;
            int num2 = 0;
            if (!string.IsNullOrEmpty(indexStr))
            {
                num = WindowUtil.StringToInt(indexStr);
            }
            if (!string.IsNullOrEmpty(tagStr))
            {
                outerText = (ElementTag) WindowUtil.StringToInt(tagStr);
            }
            if (outerText != ElementTag.src)
            {
                links = doc.links;
                if (links == null)
                {
                    return element;
                }
                keyword = keyword.Trim();
                foreach (mshtml.IHTMLElement element3 in links)
                {
                    if (!IsElementMatch(element3, outerText, itemName, ""))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(keyword))
                    {
                        if (num2 == num)
                        {
                            element = element3;
                            break;
                        }
                        num2++;
                        continue;
                    }
                    if ((element3.getAttribute("href", 0) != null) && (((string) element3.getAttribute("href", 0)).Trim().IndexOf(keyword) != -1))
                    {
                        element = element3;
                        break;
                    }
                }
            }
            else
            {
                links = doc.images;
                if (links != null)
                {
                    foreach (mshtml.IHTMLElement element2 in links)
                    {
                        if (IsElementMatch(element2, outerText, itemName, keyword))
                        {
                            if (num == num2)
                            {
                                element = element2;
                                break;
                            }
                            num2++;
                        }
                    }
                    Marshal.ReleaseComObject(links);
                }
                return element;
            }
            Marshal.ReleaseComObject(links);
            return element;
        }

        public static int GetLinkElementIndex(mshtml.IHTMLDocument2 doc, mshtml.IHTMLElement ele, string itemName, string tagStr)
        {
            mshtml.IHTMLElementCollection links;
            int num = 0;
            bool flag = false;
            ElementTag outerText = ElementTag.outerText;
            if (!string.IsNullOrEmpty(tagStr))
            {
                outerText = (ElementTag) WindowUtil.StringToInt(tagStr);
            }
            if (outerText != ElementTag.src)
            {
                links = doc.links;
                if (links == null)
                {
                    goto Label_00EA;
                }
                foreach (mshtml.IHTMLElement element2 in links)
                {
                    if (IsElementMatch(element2, outerText, itemName, ""))
                    {
                        if (ele == element2)
                        {
                            flag = true;
                            break;
                        }
                        num++;
                    }
                }
            }
            else
            {
                links = doc.images;
                if (links != null)
                {
                    foreach (mshtml.IHTMLElement element in links)
                    {
                        if (IsElementMatch(element, outerText, itemName, ""))
                        {
                            if (ele == element)
                            {
                                flag = true;
                                break;
                            }
                            num++;
                        }
                    }
                    Marshal.ReleaseComObject(links);
                }
                goto Label_00EA;
            }
            Marshal.ReleaseComObject(links);
        Label_00EA:
            if (!flag)
            {
                num = 0;
            }
            return num;
        }

        public static bool GetLinkRect(mshtml.IHTMLDocument2 doc, ref Rectangle rect, string itemName, string keyword, string tagStr, string indexStr)
        {
            bool flag = false;
            mshtml.IHTMLElement elem = GetLinkElement(doc, itemName, keyword, tagStr, indexStr);
            if (elem != null)
            {
                rect = GetElementRect(doc.body, elem);
                flag = true;
            }
            return flag;
        }

        public static mshtml.IHTMLElement GetRadioElement(mshtml.IHTMLDocument2 doc, string itemName, string tagStr, string indexStr)
        {
            mshtml.IHTMLElementCollection o = (mshtml.IHTMLElementCollection) doc.all.tags("input");
            mshtml.IHTMLElement element = null;
            ElementTag iD = ElementTag.ID;
            bool flag = false;
            int num = 0;
            int num2 = 0;
            if (!string.IsNullOrEmpty(tagStr))
            {
                iD = (ElementTag) WindowUtil.StringToInt(tagStr);
            }
            if (!string.IsNullOrEmpty(indexStr))
            {
                num = WindowUtil.StringToInt(indexStr);
            }
            foreach (mshtml.IHTMLElement element2 in o)
            {
                if (IsElementMatchType(element2, "radio") && IsElementMatch(element2, iD, itemName, ""))
                {
                    if (num == num2)
                    {
                        element = element2;
                        flag = true;
                        break;
                    }
                    num2++;
                }
            }
            if (o != null)
            {
                Marshal.ReleaseComObject(o);
            }
            if (!flag)
            {
                o = (mshtml.IHTMLElementCollection) doc.all.tags("label");
                foreach (mshtml.IHTMLElement element3 in o)
                {
                    if (element3.getAttribute("htmlFor", 0) != null)
                    {
                        string str = element3.getAttribute("htmlFor", 0).ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            mshtml.IHTMLElement ele = doc.all.item(str, Missing.Value) as mshtml.IHTMLElement;
                            if (((ele != null) && IsElementMatchType(ele, "radio")) && IsElementMatch(element3, iD, itemName, ""))
                            {
                                if (num == num2)
                                {
                                    element = element3;
                                    flag = true;
                                    break;
                                }
                                num2++;
                            }
                        }
                    }
                }
            }
            if (o != null)
            {
                Marshal.ReleaseComObject(o);
            }
            return element;
        }

        public static bool GetRadioRect(mshtml.IHTMLDocument2 doc, ref Rectangle rect, string itemName, string tagStr, string indexStr)
        {
            bool flag = false;
            mshtml.IHTMLElement elem = GetRadioElement(doc, itemName, tagStr, indexStr);
            if (elem != null)
            {
                rect = GetElementRect(doc.body, elem);
                flag = true;
            }
            return flag;
        }

        public static int GetWindowMessageLParam(IntPtr hwnd, Point mousePoint)
        {
            Rect lpRect = new Rect();
            WindowUtil.GetWindowRect(hwnd, out lpRect);
            return (((lpRect.Top + mousePoint.Y) << 0x10) + (lpRect.Left + mousePoint.X));
        }

        public static mshtml.IHTMLElement2 GetWindowWidthAndHeight(IntPtr hwnd, mshtml.IHTMLDocument2 doc, ref int clientWidth, ref int clientHeight, ref int scrollWidth, ref int scrollHeight)
        {
            HTMLDocumentClass class2 = doc as HTMLDocumentClass;
            mshtml.IHTMLElement2 documentElement = null;
            if (class2 != null)
            {
                documentElement = class2.documentElement as mshtml.IHTMLElement2;
                if (documentElement != null)
                {
                    clientWidth = documentElement.clientWidth;
                    clientHeight = documentElement.clientHeight;
                }
            }
            else
            {
                Rect lpRect = new Rect();
                documentElement = doc.body as mshtml.IHTMLElement2;
                WindowUtil.GetWindowRect(hwnd, out lpRect);
                if (documentElement != null)
                {
                    clientWidth = lpRect.Right - lpRect.Left;
                    clientHeight = lpRect.Bottom - lpRect.Top;
                }
            }
            if (documentElement != null)
            {
                scrollWidth = documentElement.scrollWidth;
                scrollHeight = documentElement.scrollHeight;
            }
            return documentElement;
        }

        public static bool isClickElement(IntPtr hwnd, mshtml.IHTMLDocument2 doc, mshtml.IHTMLElement ele, int x, int y, ClickEvent clickEvent)
        {
            bool flag = false;
            clickEvent.SetElement(ele);
            WindowUtil.ClickMouse(hwnd, x, y);
            if (clickEvent.CouldClick)
            {
                if (clickEvent.IsClick)
                {
                    flag = true;
                }
                return flag;
            }
            mshtml.IHTMLElement parentElement = ele;
            mshtml.IHTMLElement activeElement = doc.activeElement;
            while ((parentElement != null) && (parentElement != doc.body))
            {
                if (parentElement == activeElement)
                {
                    flag = true;
                    break;
                }
                parentElement = parentElement.parentElement;
            }
            if (!flag)
            {
                parentElement = ele;
                for (activeElement = doc.activeElement; (activeElement != null) && (activeElement != doc.body); activeElement = activeElement.parentElement)
                {
                    if (parentElement == activeElement)
                    {
                        return true;
                    }
                }
            }
            return flag;
        }

        public static bool IsElementMatch(mshtml.IHTMLElement ele, ElementTag tag, string itemName, string keyword = "")
        {
            itemName = itemName.Trim();
            if (tag == ElementTag.ID)
            {
                return (!string.IsNullOrEmpty(ele.id) && ele.id.Trim().Equals(itemName));
            }
            if (tag == ElementTag.name)
            {
                return ((ele.getAttribute("name", 0) != null) && ele.getAttribute("name", 0).ToString().Trim().Equals(itemName));
            }
            if (tag == ElementTag.outerText)
            {
                return (!string.IsNullOrEmpty(ele.outerText) && (ele.outerText.Trim().IndexOf(itemName) != -1));
            }
            if (tag == ElementTag.className)
            {
                return (!string.IsNullOrEmpty(ele.className) && ele.className.Trim().Equals(itemName));
            }
            if (tag == ElementTag.outerHTML)
            {
                return (!string.IsNullOrEmpty(ele.outerHTML) && ele.outerHTML.Trim().Equals(itemName));
            }
            if (tag == ElementTag.value)
            {
                return ((ele.getAttribute("value", 0) != null) && ele.getAttribute("value", 0).ToString().Trim().Equals(itemName));
            }
            if (tag == ElementTag.href)
            {
                return ((ele.getAttribute("href", 0) != null) && ele.getAttribute("href", 0).ToString().Trim().Equals(itemName));
            }
            if (tag != ElementTag.src)
            {
                return false;
            }
            if (ele.getAttribute("src", 0) == null)
            {
                return false;
            }
            return (ele.getAttribute("src", 0).ToString().Trim().Equals(itemName) || (!string.IsNullOrEmpty(keyword) && (ele.getAttribute("src", 0).ToString().Trim().IndexOf(keyword) != -1)));
        }

        public static bool IsElementMatchType(mshtml.IHTMLElement ele, string typeName)
        {
            return ((ele.getAttribute("type", 0) != null) && ele.getAttribute("type", 0).ToString().ToLower().Trim().Equals(typeName));
        }

        public static bool MoveToDest(IntPtr hwnd, mshtml.IHTMLDocument2 doc, Point to, ref Point fakeMousePoint)
        {
            bool flag = false;
            int clientWidth = 0;
            int clientHeight = 0;
            int scrollWidth = 0;
            int scrollHeight = 0;
            if ((GetWindowWidthAndHeight(hwnd, doc, ref clientWidth, ref clientHeight, ref scrollWidth, ref scrollHeight) != null) && ((clientWidth != 0) || (clientHeight != 0)))
            {
                int num5 = 50;
                if ((Math.Abs((int) (fakeMousePoint.Y - to.Y)) >= num5) || (Math.Abs((int) (fakeMousePoint.X - to.X)) >= num5))
                {
                    if (((to.X >= 0) && (to.X < clientWidth)) && ((to.Y >= 0) && (to.Y <= clientHeight)))
                    {
                        Random random = new Random();
                        int num6 = 0;
                        int num7 = 0;
                        if ((fakeMousePoint.X - to.X) > 0)
                        {
                            num6 = -random.Next(Math.Max(fakeMousePoint.X - to.X, 50));
                        }
                        else if ((fakeMousePoint.X - to.X) < 0)
                        {
                            num6 = random.Next(Math.Max(to.X - fakeMousePoint.X, 50));
                        }
                        if ((fakeMousePoint.Y - to.Y) > 0)
                        {
                            num7 = -random.Next(Math.Max(fakeMousePoint.Y - to.Y, 50));
                        }
                        else if ((fakeMousePoint.Y - to.Y) < 0)
                        {
                            num7 = random.Next(Math.Max(to.Y - fakeMousePoint.Y, 50));
                        }
                        SetMousePoint(ref fakeMousePoint, fakeMousePoint.X + num6, fakeMousePoint.Y + num7, clientWidth, clientHeight);
                        int lParam = (fakeMousePoint.Y << 0x10) + fakeMousePoint.X;
                        WindowUtil.PostMessage(hwnd, 0x200, 0, lParam);
                    }
                    return flag;
                }
                flag = true;
                if ((fakeMousePoint.Y != to.Y) || (fakeMousePoint.X != to.X))
                {
                    SetMousePoint(ref fakeMousePoint, to.X, to.Y, clientWidth, clientHeight);
                    int num9 = (fakeMousePoint.Y << 0x10) + fakeMousePoint.X;
                    WindowUtil.PostMessage(hwnd, 0x200, 0, num9);
                }
            }
            return flag;
        }

        public static bool MoveToDest(IntPtr hwnd, HtmlElement htmlElement, mshtml.IHTMLDocument2 doc, int x, int y, ref Point fakeMousePoint, bool scrollfast, bool isTimeout)
        {
            bool flag = true;
            int clientWidth = 0;
            int clientHeight = 0;
            int scrollWidth = 0;
            int scrollHeight = 0;
            if ((GetWindowWidthAndHeight(hwnd, doc, ref clientWidth, ref clientHeight, ref scrollWidth, ref scrollHeight) != null) && (htmlElement != null))
            {
                int scrollLeft = htmlElement.ScrollLeft;
                int scrollTop = htmlElement.ScrollTop;
                if ((clientWidth == 0) && (clientHeight == 0))
                {
                    return flag;
                }
                flag = ScrollToAbsolutePoint(hwnd, doc, scrollTop, y + scrollTop, scrollHeight, clientHeight, ref fakeMousePoint, scrollfast, isTimeout, htmlElement);
                if (!flag)
                {
                    return flag;
                }
                flag = false;
                int num7 = 0;
                if (((scrollLeft + x) + (clientWidth / 2)) > scrollWidth)
                {
                    num7 = scrollWidth - clientWidth;
                }
                else if ((scrollLeft + x) < (clientWidth / 2))
                {
                    num7 = 0;
                }
                else
                {
                    num7 = (scrollLeft + x) - (clientWidth / 2);
                }
                htmlElement.ScrollLeft = num7;
                if ((x + scrollLeft) < 0)
                {
                    x = 4;
                }
                else if ((x + scrollLeft) > scrollWidth)
                {
                    x = clientWidth - 8;
                }
                if (((scrollLeft + x) >= num7) && ((scrollLeft + x) <= (num7 + clientWidth)))
                {
                    SetMousePoint(hwnd, ref fakeMousePoint, x, y, doc);
                    int lParam = (fakeMousePoint.Y << 0x10) + fakeMousePoint.X;
                    WindowUtil.PostMessage(hwnd, 0x200, 0, lParam);
                    flag = true;
                }
            }
            return flag;
        }

        public static bool ScrollToAbsolutePoint(IntPtr hwnd, mshtml.IHTMLDocument2 doc, int scrollTop, int y, int scrollHeight, int winHeight, ref Point fakeMousePoint, bool scrollFast, bool isTimeout, HtmlElement htmlElement)
        {
            int num2;
            bool flag = false;
            int num = 0;
            Random random = new Random();
            if (isTimeout)
            {
                if (htmlElement == null)
                {
                    return flag;
                }
                int num3 = y - (winHeight / 2);
                if (num3 < 0)
                {
                    num3 = 0;
                }
                else if (num3 > (scrollHeight - winHeight))
                {
                    num3 = scrollHeight - winHeight;
                }
                htmlElement.ScrollTop = num3;
                return true;
            }
            if (scrollFast)
            {
                num2 = 100;
            }
            else
            {
                num2 = random.Next(5, 10);
            }
            CheckMousePoint(hwnd, ref fakeMousePoint, doc);
            while (!flag && (num < num2))
            {
                int windowMessageLParam = GetWindowMessageLParam(hwnd, fakeMousePoint);
                if ((y <= ((scrollTop + (winHeight / 2)) - 120)) && (scrollTop > 0))
                {
                    scrollTop = ((scrollTop - 120) >= 0) ? (scrollTop - 120) : 0;
                    WindowUtil.PostMessage(hwnd, 0x20a, 0x780000, windowMessageLParam);
                }
                else if ((y >= ((scrollTop + (winHeight / 2)) + 120)) && ((scrollTop + winHeight) < scrollHeight))
                {
                    scrollTop = (((scrollTop + winHeight) + 120) <= scrollHeight) ? (scrollTop + 120) : (scrollHeight - winHeight);
                    WindowUtil.PostMessage(hwnd, 0x20a, -7864320, windowMessageLParam);
                }
                else
                {
                    flag = true;
                }
                num++;
            }
            return flag;
        }

        public static bool ScrollToBottom(IntPtr hwnd, mshtml.IHTMLDocument2 doc, HtmlElement htmlElement, Point fakeMousePoint, int loadPercent, bool isTimeout)
        {
            bool flag = false;
            int clientWidth = 0;
            int clientHeight = 0;
            int scrollWidth = 0;
            int scrollHeight = 0;
            if ((GetWindowWidthAndHeight(hwnd, doc, ref clientWidth, ref clientHeight, ref scrollWidth, ref scrollHeight) == null) || (htmlElement == null))
            {
                return flag;
            }
            int scrollTop = htmlElement.ScrollTop;
            if ((clientWidth == 0) && (clientHeight == 0))
            {
                return flag;
            }
            return ScrollToAbsolutePoint(hwnd, doc, scrollTop, (scrollHeight * loadPercent) / 100, scrollHeight, clientHeight, ref fakeMousePoint, true, isTimeout, htmlElement);
        }

        public static bool ScrollToDest(IntPtr hwnd, mshtml.IHTMLDocument2 doc, HtmlElement htmlElement, int height, Point fakeMousePoint, bool isTimeout)
        {
            bool flag = false;
            int clientWidth = 0;
            int clientHeight = 0;
            int scrollWidth = 0;
            int scrollHeight = 0;
            if ((GetWindowWidthAndHeight(hwnd, doc, ref clientWidth, ref clientHeight, ref scrollWidth, ref scrollHeight) != null) && (htmlElement != null))
            {
                int scrollTop = htmlElement.ScrollTop;
                if ((clientWidth == 0) && (clientHeight == 0))
                {
                    return flag;
                }
                ScrollToAbsolutePoint(hwnd, doc, scrollTop, height, scrollHeight, clientHeight, ref fakeMousePoint, false, isTimeout, htmlElement);
            }
            return flag;
        }

        public static void SetMousePoint(ref Point fakeMousePoint, int x, int y, int maxX, int maxY)
        {
            if ((maxX > 0) && (maxY > 0))
            {
                fakeMousePoint.X = x;
                fakeMousePoint.Y = y;
                if (fakeMousePoint.X < 1)
                {
                    fakeMousePoint.X = 1;
                }
                else if (fakeMousePoint.X > (maxX - 1))
                {
                    fakeMousePoint.X = maxX - 1;
                }
                if (fakeMousePoint.Y < 1)
                {
                    fakeMousePoint.Y = 1;
                }
                else if (fakeMousePoint.Y > (maxY - 1))
                {
                    fakeMousePoint.Y = maxY - 1;
                }
            }
        }

        public static void SetMousePoint(IntPtr hwnd, ref Point fakeMousePoint, int x, int y, mshtml.IHTMLDocument2 doc)
        {
            int clientWidth = 0;
            int clientHeight = 0;
            int scrollWidth = 0;
            int scrollHeight = 0;
            if (GetWindowWidthAndHeight(hwnd, doc, ref clientWidth, ref clientHeight, ref scrollWidth, ref scrollHeight) != null)
            {
                SetMousePoint(ref fakeMousePoint, x, y, clientWidth, clientHeight);
            }
        }
    }
}

