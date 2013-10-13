namespace client
{
    using mshtml;
    using SHDocVw;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal class HtmlUtil
    {
        public static IHTMLElement FindElement(IntPtr fromHandle, IHTMLDocument2 doc, int x, int y)
        {
            IHTMLElement elem = null;
            Rect lpRect = new Rect();
            WindowUtil.GetWindowRect(fromHandle, out lpRect);
            elem = doc.elementFromPoint(x - lpRect.Left, y - lpRect.Top);
            while (((elem != null) && (elem.tagName != null)) && (elem.tagName.ToLower().Equals("iframe") || elem.tagName.ToLower().Equals("frame")))
            {
                Rectangle elementRect = GetElementRect(doc.body, elem);
                IWebBrowser2 o = elem as IWebBrowser2;
                IHTMLDocument2 document = o.Document as IHTMLDocument2;
                IHTMLElement element2 = document.elementFromPoint((x - lpRect.Left) - elementRect.X, (y - lpRect.Top) - elementRect.Y);
                if (element2 != null)
                {
                    elem = element2;
                }
                Marshal.ReleaseComObject(document);
                Marshal.ReleaseComObject(o);
            }
            return elem;
        }

        public static IHTMLElement GetButtonElement(IHTMLDocument2 doc, string itemName, string tagStr, string indexStr)
        {
            IHTMLElement element = null;
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
            IHTMLElementCollection o = (IHTMLElementCollection) doc.all.tags("input");
            foreach (IHTMLElement element2 in o)
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
            o = (IHTMLElementCollection) doc.all.tags("button");
            foreach (IHTMLElement element3 in o)
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

        public static int GetButtonElementIndex(IHTMLDocument2 doc, IHTMLElement ele, string itemName, string tagStr)
        {
            int num = 0;
            bool flag = false;
            if (ele != null)
            {
                ElementTag iD = ElementTag.ID;
                if ((tagStr != string.Empty) && (tagStr != ""))
                {
                    iD = (ElementTag) WindowUtil.StringToInt(tagStr);
                }
                if (iD != ElementTag.ID)
                {
                    IHTMLElementCollection o = (IHTMLElementCollection) doc.all.tags("input");
                    foreach (IHTMLElement element in o)
                    {
                        if ((IsElementMatchType(element, "submit") || IsElementMatchType(element, "button")) && IsElementMatch(element, iD, itemName, ""))
                        {
                            if (ele == element)
                            {
                                flag = true;
                                break;
                            }
                            num++;
                        }
                    }
                    if (o != null)
                    {
                        Marshal.ReleaseComObject(o);
                    }
                    if (!flag)
                    {
                        o = (IHTMLElementCollection) doc.all.tags("button");
                        foreach (IHTMLElement element2 in o)
                        {
                            if (IsElementMatch(element2, iD, itemName, ""))
                            {
                                if (ele == element2)
                                {
                                    flag = true;
                                    break;
                                }
                                num++;
                            }
                        }
                        if (o != null)
                        {
                            Marshal.ReleaseComObject(o);
                        }
                    }
                }
            }
            if (!flag)
            {
                num = 0;
            }
            return num;
        }

        public static bool GetButtonRect(IHTMLDocument2 doc, ref Rectangle rect, string itemName, string tagStr, string indexStr)
        {
            IHTMLElement elem = GetButtonElement(doc, itemName, tagStr, indexStr);
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

        public static int GetCheckedElementIndex(IHTMLDocument2 doc, IHTMLElement ele, string itemName, string tagStr)
        {
            int num = 0;
            bool flag = false;
            if (ele != null)
            {
                ElementTag iD = ElementTag.ID;
                if (!string.IsNullOrEmpty(tagStr))
                {
                    iD = (ElementTag) WindowUtil.StringToInt(tagStr);
                }
                if (iD != ElementTag.ID)
                {
                    IHTMLElementCollection o = (IHTMLElementCollection) doc.all.tags("input");
                    foreach (IHTMLElement element in o)
                    {
                        if (IsElementMatchType(element, "checkbox") && IsElementMatch(element, iD, itemName, ""))
                        {
                            if (ele == element)
                            {
                                flag = true;
                                break;
                            }
                            num++;
                        }
                    }
                    if (o != null)
                    {
                        Marshal.ReleaseComObject(o);
                    }
                    if (!flag)
                    {
                        o = (IHTMLElementCollection) doc.all.tags("label");
                        foreach (IHTMLElement element2 in o)
                        {
                            if (element2.getAttribute("htmlFor", 0) != null)
                            {
                                string str = element2.getAttribute("htmlFor", 0).ToString();
                                if (!string.IsNullOrEmpty(str))
                                {
                                    IHTMLElement element3 = doc.all.item(str, Missing.Value) as IHTMLElement;
                                    if (((element3 != null) && IsElementMatchType(element3, "checkbox")) && IsElementMatch(element2, iD, itemName, ""))
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
                        }
                        if (o != null)
                        {
                            Marshal.ReleaseComObject(o);
                        }
                    }
                }
            }
            if (!flag)
            {
                num = 0;
            }
            return num;
        }

        public static Rectangle GetElementRect(IHTMLElement body, IHTMLElement elem)
        {
            int top;
            int num3;
            int num4;
            int left = top = num3 = num4 = 0;
            IHTMLElement2 element = elem as IHTMLElement2;
            IHTMLRect o = element.getBoundingClientRect();
            elem = element as IHTMLElement;
            left = o.left;
            top = o.top;
            num3 = o.right - o.left;
            num4 = o.bottom - o.top;
            if (body.document != elem.document)
            {
                List<IHTMLDOMNode> frames = new List<IHTMLDOMNode>();
                GetEleParentFrames(body as IHTMLDOMNode, elem as IHTMLDOMNode, frames);
                foreach (IHTMLDOMNode node in frames)
                {
                    IHTMLRect rect2 = (node as IHTMLElement2).getBoundingClientRect();
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

        private static bool GetEleParentFrames(IHTMLDOMNode root, IHTMLDOMNode node, List<IHTMLDOMNode> frames)
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
                IWebBrowser2 browser = root as IWebBrowser2;
                if (browser != null)
                {
                    IHTMLDocument2 document = browser.Document as IHTMLDocument2;
                    if (document != null)
                    {
                        IHTMLDOMNode parentElement = document.body.parentElement as IHTMLDOMNode;
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
                IHTMLDOMNode node3 = childNodes.item(i) as IHTMLDOMNode;
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

        public static IHTMLDocument2 GetIEWindowDocument(IntPtr hwnd)
        {
            object ppvObject = new object();
            int lParam = 0;
            Guid riid = new Guid();
            int wMsg = WindowUtil.RegisterWindowMessage("WM_Html_GETOBJECT");
            WindowUtil.ObjectFromLresult(WindowUtil.SendMessage(hwnd, wMsg, 0, lParam), ref riid, 0, ref ppvObject);
            IHTMLDocument2 document = (IHTMLDocument2) ppvObject;
            ppvObject = null;
            return document;
        }

        public static IHTMLElement GetInputElement(IHTMLDocument2 doc, string itemName, string tagStr, string indexStr)
        {
            IHTMLElementCollection o = (IHTMLElementCollection) doc.all.tags("input");
            IHTMLElement element = null;
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
            foreach (IHTMLElement element2 in o)
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

        public static int GetInputElementIndex(IHTMLDocument2 doc, IHTMLElement ele, string itemName, string tagStr)
        {
            int num = 0;
            bool flag = false;
            if (ele != null)
            {
                IHTMLElementCollection o = (IHTMLElementCollection) doc.all.tags("input");
                ElementTag iD = ElementTag.ID;
                if (!string.IsNullOrEmpty(tagStr))
                {
                    iD = (ElementTag) WindowUtil.StringToInt(tagStr);
                }
                if (iD != ElementTag.ID)
                {
                    foreach (IHTMLElement element in o)
                    {
                        if (IsElementMatch(element, iD, itemName, ""))
                        {
                            if (ele == element)
                            {
                                flag = true;
                                break;
                            }
                            num++;
                        }
                    }
                }
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                }
            }
            if (!flag)
            {
                num = 0;
            }
            return num;
        }

        public static bool GetInputElementRect(IHTMLDocument2 doc, ref Rectangle rect, string itemName, string tagStr, string indexStr)
        {
            bool flag = false;
            IHTMLElement elem = GetInputElement(doc, itemName, tagStr, indexStr);
            if (elem != null)
            {
                rect = GetElementRect(doc.body, elem);
                flag = true;
            }
            return flag;
        }

        public static IHTMLElement GetLinkElement(IHTMLDocument2 doc, string itemName, string keyword, string tagStr, string indexStr)
        {
            IHTMLElement element = null;
            IHTMLElementCollection links;
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
                foreach (IHTMLElement element3 in links)
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
                    foreach (IHTMLElement element2 in links)
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

        public static int GetLinkElementIndex(IHTMLDocument2 doc, IHTMLElement ele, string itemName, string tagStr)
        {
            IHTMLElementCollection links;
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
                foreach (IHTMLElement element2 in links)
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
                    foreach (IHTMLElement element in links)
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

        public static bool GetLinkRect(IHTMLDocument2 doc, ref Rectangle rect, string itemName, string keyword, string tagStr, string indexStr)
        {
            bool flag = false;
            IHTMLElement elem = GetLinkElement(doc, itemName, keyword, tagStr, indexStr);
            if (elem != null)
            {
                rect = GetElementRect(doc.body, elem);
                flag = true;
            }
            return flag;
        }

        public static int GetRadioElementIndex(IHTMLDocument2 doc, IHTMLElement ele, string itemName, string tagStr)
        {
            int num = 0;
            bool flag = false;
            if (ele != null)
            {
                ElementTag iD = ElementTag.ID;
                if (!string.IsNullOrEmpty(tagStr))
                {
                    iD = (ElementTag) WindowUtil.StringToInt(tagStr);
                }
                if (iD != ElementTag.ID)
                {
                    IHTMLElementCollection o = (IHTMLElementCollection) doc.all.tags("input");
                    foreach (IHTMLElement element in o)
                    {
                        if (IsElementMatchType(element, "radio") && IsElementMatch(element, iD, itemName, ""))
                        {
                            if (ele == element)
                            {
                                flag = true;
                                break;
                            }
                            num++;
                        }
                    }
                    if (o != null)
                    {
                        Marshal.ReleaseComObject(o);
                    }
                    if (!flag)
                    {
                        o = (IHTMLElementCollection) doc.all.tags("label");
                        foreach (IHTMLElement element2 in o)
                        {
                            if (element2.getAttribute("htmlFor", 0) != null)
                            {
                                string str = element2.getAttribute("htmlFor", 0).ToString();
                                if (!string.IsNullOrEmpty(str))
                                {
                                    IHTMLElement element3 = doc.all.item(str, Missing.Value) as IHTMLElement;
                                    if (((element3 != null) && IsElementMatchType(element3, "radio")) && IsElementMatch(element2, iD, itemName, ""))
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
                        }
                        if (o != null)
                        {
                            Marshal.ReleaseComObject(o);
                        }
                    }
                }
            }
            if (!flag)
            {
                num = 0;
            }
            return num;
        }

        public static bool IsElementMatch(IHTMLElement ele, ElementTag tag, string itemName, string keyword = "")
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

        public static bool IsElementMatchType(IHTMLElement ele, string typeName)
        {
            return ((ele.getAttribute("type", 0) != null) && ele.getAttribute("type", 0).ToString().ToLower().Trim().Equals(typeName));
        }
    }
}

