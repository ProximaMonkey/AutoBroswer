namespace TenDayBrowser
{
    using mshtml;
    using System;

    internal class ClickEvent
    {
        private bool _couldClick;
        private IHTMLElement _element;
        private bool _isClick;

        private bool HtmlElement1_Click(IHTMLEventObj pEvtObj)
        {
            this._isClick = true;
            this._element = null;
            return true;
        }

        public void SetElement(IHTMLElement ele)
        {
            if (this._element != ele)
            {
                this._element = ele;
                this._isClick = false;
                this._couldClick = false;
                HTMLAnchorEvents2_Event event2 = ele as HTMLAnchorEvents2_Event;
                if (event2 != null)
                {
                    this._couldClick = true;
                    event2.onclick += (new HTMLAnchorEvents2_onclickEventHandler(this.HtmlElement1_Click));
                }
                else
                {
                    HTMLInputTextElementEvents2_Event event3 = ele as HTMLInputTextElementEvents2_Event;
                    if (event3 != null)
                    {
                        this._couldClick = true;
                        event3.onclick += (new HTMLInputTextElementEvents2_onclickEventHandler(this.HtmlElement1_Click));
                    }
                    else
                    {
                        HTMLButtonElementEvents2_Event event4 = ele as HTMLButtonElementEvents2_Event;
                        if (event4 != null)
                        {
                            this._couldClick = true;
                            event4.onclick += (new HTMLButtonElementEvents2_onclickEventHandler(this.HtmlElement1_Click));
                        }
                        else
                        {
                            HTMLControlElementEvents2_Event event5 = ele as HTMLControlElementEvents2_Event;
                            if (event5 != null)
                            {
                                this._couldClick = true;
                                event5.onclick += (new HTMLControlElementEvents2_onclickEventHandler(this.HtmlElement1_Click));
                            }
                            else
                            {
                                HTMLImgEvents2_Event event6 = ele as HTMLImgEvents2_Event;
                                if (event6 != null)
                                {
                                    this._couldClick = true;
                                    event6.onclick += (new HTMLImgEvents2_onclickEventHandler(this.HtmlElement1_Click));
                                }
                                else
                                {
                                    HTMLElementEvents2_Event event7 = ele as HTMLElementEvents2_Event;
                                    if (event7 != null)
                                    {
                                        this._couldClick = true;
                                        event7.onclick += (new HTMLElementEvents2_onclickEventHandler(this.HtmlElement1_Click));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool CouldClick
        {
            get
            {
                return this._couldClick;
            }
        }

        public bool IsClick
        {
            get
            {
                return this._isClick;
            }
        }
    }
}
