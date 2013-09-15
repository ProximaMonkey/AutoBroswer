using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mshtml;
using System.Drawing;
using SHDocVw;

namespace AutoBroswer
{

    public static class wbElementMouseSimulate
    {
        /// <summary>
        /// 得到node的所有父亲frames
        /// </summary>
        /// <param name="root"></param>
        /// <param name="elem"></param>
        /// <param name="frames"></param>
        /// <returns></returns>
        private static bool _getEleParentFrames(IHTMLDOMNode root, IHTMLDOMNode node, List<IHTMLDOMNode> frames)
        {
            if (root == node)
                return true;

            bool isFrame = false;
            string tag = root.nodeName.ToLower();
            if (tag == "frame" || tag == "iframe")
                isFrame = true;

            IHTMLDOMChildrenCollection cs = null;
            if (isFrame)
            {
                IWebBrowser2 pwb = root as IWebBrowser2;
                if (pwb != null)
                {
                    IHTMLDocument2 pdoc2 = pwb.Document as IHTMLDocument2;
                    if (pdoc2 != null)
                    {
                        IHTMLDOMNode htmlElem = pdoc2.body.parentElement as IHTMLDOMNode;
                        cs = htmlElem.childNodes as IHTMLDOMChildrenCollection;
                    }
                }
            }
            if (cs == null)
            {
                cs = root.childNodes as IHTMLDOMChildrenCollection;
            }

            if (cs == null)
                return false;
            for (int idx = 0; idx < cs.length; idx++)
            {
                IHTMLDOMNode c = cs.item(idx) as IHTMLDOMNode;
                if (_getEleParentFrames(c, node, frames))
                {
                    if (isFrame)
                        frames.Add(root);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 得到元素的位置
        /// </summary>
        /// <param name="elem">元素</param>
        /// <returns></returns>
        public static Rectangle GetElementRect(IHTMLElement body, IHTMLElement elem)
        {
            int x, y, w, h;
            x = y = w = h = 0;

            // 计算元素本身的位置
            IHTMLElement2 elem2 = elem as IHTMLElement2;
            IHTMLRect elemRect = elem2.getBoundingClientRect();
            x = elemRect.left;
            y = elemRect.top;
            w = elemRect.right - elemRect.left;
            h = elemRect.bottom - elemRect.top;

            // TODO: 计算顶端htmlElem(docElem)的位置，一般不用计算，其位置应该为（0,0，xx，xx）

            // 计算父亲iframes
            if (body.document != elem.document)
            {
                List<IHTMLDOMNode> frames = new List<IHTMLDOMNode>();
                _getEleParentFrames(body as IHTMLDOMNode, elem as IHTMLDOMNode, frames);
                foreach (IHTMLDOMNode f in frames)
                {
                    IHTMLElement2 frame2 = f as IHTMLElement2;
                    IHTMLRect frameRect = frame2.getBoundingClientRect();
                    x += frameRect.left;
                    y += frameRect.top;
                }
            }

            Rectangle ret = new Rectangle();
            ret.X = x;
            ret.Y = y;
            ret.Width = w;
            ret.Height = h;

            return ret;
        }
                public static string m_jsScript1 =
            @"
var dispatchMouseEvent = function(target, var_args) {
  var e = document.createEvent('MouseEvents');
  // If you need clientX, clientY, etc., you can call
  // initMouseEvent instead of initEvent
  e.initEvent.apply(e, Array.prototype.slice.call(arguments, 1));
  target.dispatchEvent(e);
};
//dispatchMouseEvent(element, 'mouseover', true, true);
//dispatchMouseEvent(element, 'mousedown', true, true);
//dispatchMouseEvent(element, 'click', true, true);
//dispatchMouseEvent(element, 'mouseup', true, true);
function TestClick(element) {
dispatchMouseEvent(element, 'mousedown', true, true);
dispatchMouseEvent(element, 'mouseup', true, true);
}";

        public static string m_jsScript2 =
        @"function eventFire(el, etype){
  if (el.fireEvent) {
    (el.fireEvent('on' + etype));
  } else {
    var evObj = document.createEvent('Events');
    evObj.initEvent(etype, true, false);
    el.dispatchEvent(evObj);
  }
}";
        //f = mouseevent
        public static string m_jsScript =
//            @"
//var dispatchMouseEvent = function(target, var_args) {
//  var e = document.createEvent('MouseEvents');
//  // If you need clientX, clientY, etc., you can call
//  // initMouseEvent instead of initEvent
//  e.initEvent.apply(e, Array.prototype.slice.call(arguments, 1));
//  target.dispatchEvent(e);
//};
////dispatchMouseEvent(element, 'mouseover', true, true);
////dispatchMouseEvent(element, 'mousedown', true, true);
////dispatchMouseEvent(element, 'click', true, true);
////dispatchMouseEvent(element, 'mouseup', true, true);
//function TestClick(element) {dispatchMouseEvent(element, 'mousedown', true, true);dispatchMouseEvent(element, 'mouseup', true, true);}";
//            @"function eventFire(el, etype){
//  if (el.fireEvent) {
//    (el.fireEvent('on' + etype));
//  } else {
//    var evObj = document.createEvent('Events');
//    evObj.initEvent(etype, true, false);
//    el.dispatchEvent(evObj);
//  }
//}";
        @"function simulate(g, c) {
    var e = extend(defaultOptions, arguments[2] || {});
    var b, f = null;
    for (var d in eventMatchers) {
        if (eventMatchers[d].test(c)) {
            f = d;
            break
        }
    }
    if (!f) {
        throw new SyntaxError('Only HTMLEvents and MouseEvents interfaces are supported')
    }
    if (document.createEvent) {
        b = document.createEvent(f);
        if (f == 'HTMLEvents') {
            b.initEvent(c, e.bubbles, e.cancelable)
        } else {
            b.initMouseEvent(c, e.bubbles, e.cancelable, document.defaultView, e.button, e.pointerX, e.pointerY, e.pointerX, e.pointerY, e.ctrlKey, e.altKey, e.shiftKey, e.metaKey, e.button, g)
        }
        g.dispatchEvent(b)
    } else {
        e.clientX = e.pointerX;
        e.clientY = e.pointerY;
        var a = document.createEventObject();
        b = extend(a, e);
        g.fireEvent('on' + c, b)
    }
    return g
}
function extend(a, c) {
    for (var b in c) {
        a[b] = c[b]
    }
    return a
}
var eventMatchers = {
    HTMLEvents: /^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|scroll)$/,
    MouseEvents: /^(?:click|dblclick|mouse(?:down|up|over|move|out))$/
};
var defaultOptions = {
    pointerX: 0,
    pointerY: 0,
    button: 0,
    ctrlKey: false,
    altKey: false,
    shiftKey: false,
    metaKey: false,
    bubbles: true,
    cancelable: true
};
function TestClick() {
    simulate(document.getElementById('flls').firstChild, 'click')
};

function clickSearch(){
var evt = document.createEvent('MouseEvents');  
            evt.initEvent('click', true, true);  
var elements = document.getElementsByClassName('btn-search');
            for (var i = 0, len = elements.length; i < len; i++) {
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                string className = elements[i].getAttribute('type');
alert(className);
                if (className == 'submit')
                {
elements[i].dispatchEvent(evt);
                }
            }
};
";

        public static string clickSearch =
@"function clickSearch(){
var evt = document.createEvent('MouseEvents');  
            evt.initEvent('click', true, true);  
var elements = document.getElementsByClassName('btn-search');
alert('length:' + elements.length);
            for (var i = 0, len = elements.length; i < len; i++) {
            {
                // If there's more than one button, you can check the
                //element.InnerHTML to see if it's the one you want
                string className = elements[i].getAttribute('type');
alert(className);
                if (className == 'submit')
                {
elements[i].dispatchEvent(evt);
                }
            }
};
";
    }
}


//function simulate(g, c) {
//    var e = extend(defaultOptions, arguments[2] || {});
//    var b, f = null;
//    for (var d in eventMatchers) {
//        if (eventMatchers[d].test(c)) {
//            f = d;
//            break
//        }
//    }
//    if (!f) {
//        throw new SyntaxError('Only HTMLEvents and MouseEvents interfaces are supported')
//    }
//    if (document.createEvent) {
//        b = document.createEvent(f);
//        if (f == 'HTMLEvents') {
//            b.initEvent(c, e.bubbles, e.cancelable)
//        } else {
//            b.initMouseEvent(c, e.bubbles, e.cancelable, document.defaultView, e.button, e.pointerX, e.pointerY, e.pointerX, e.pointerY, e.ctrlKey, e.altKey, e.shiftKey, e.metaKey, e.button, g)
//        }
//        g.dispatchEvent(b)
//    } else {
//        e.clientX = e.pointerX;
//        e.clientY = e.pointerY;
//        var a = document.createEventObject();
//        b = extend(a, e);
//        g.fireEvent('on' + c, b)
//    }
//    return g
//}
//function extend(a, c) {
//    for (var b in c) {
//        a[b] = c[b]
//    }
//    return a
//}
//var eventMatchers = {
//    HTMLEvents: /^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|scroll)$/,
//    MouseEvents: /^(?:click|dblclick|mouse(?:down|up|over|move|out))$/
//};
//var defaultOptions = {
//    pointerX: 0,
//    pointerY: 0,
//    button: 0,
//    ctrlKey: false,
//    altKey: false,
//    shiftKey: false,
//    metaKey: false,
//    bubbles: true,
//    cancelable: true
//};
//function TestClick() {
//    simulate(document.getElementById('flls').firstChild, 'click')
//};

/*
 Licensed to the Apache Software Foundation (ASF) under one
 or more contributor license agreements.  See the NOTICE file
 distributed with this work for additional information
 regarding copyright ownership.  The ASF licenses this file
 to you under the Apache License, Version 2.0 (the
 "License"); you may not use this file except in compliance
 with the License.  You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

 Unless required by applicable law or agreed to in writing,
 software distributed under the License is distributed on an
 "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 KIND, either express or implied.  See the License for the
 specific language governing permissions and limitations
 under the License. 
 */
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;
//using Microsoft.Phone.Controls;
//using System.Windows.Input;
//using System.Diagnostics;
//using System.Windows.Media;
//using System;
//using System.Collections.Generic;

//namespace WP7CordovaClassLib
//{

//    /// <summary>
//    /// Suppresses pinch zoom and optionally scrolling of the WebBrowser control
//    /// </summary>
//    public class BrowserMouseHelper
//    {

//        /**
//         * 
//         * Full Script below, in use it is minified.
//        */
//        /*
//        private static string mouseScript =
//        @"(function(win,doc){
//            Object.defineProperty( MouseEvent.prototype, 'pageX', {
//               configurable: true,
//               get: function(){ return this.clientX }
//            });
//            Object.defineProperty( MouseEvent.prototype, 'pageY', {
//               configurable: true,
//               get: function(){ return this.clientY }
//            });

//            win.onNativeMouseEvent = function(type,x,y){
//                try {
//                    var xMod = screen.logicalXDPI / screen.deviceXDPI;
//                    var yMod = screen.logicalYDPI / screen.deviceYDPI;
//                    var evt = doc.createEvent('MouseEvents');
//                    var xPos =  doc.body.scrollLeft + Math.round(xMod * x);
//                    var yPos =  doc.body.scrollTop + Math.round(yMod * y);
//                    var element = doc.elementFromPoint(xPos,yPos);
//                    evt.initMouseEvent(type, true, true, win, 1, xPos, yPos, xPos, yPos, false, false, false, false, 0, element);
//                    var canceled = element ? !element.dispatchEvent(evt) : !doc.dispatchEvent(evt);
//                    return canceled ? 'true' : 'false';
//                }
//                catch(e) { return e;}
//            }
//        })(window,document);";
//        */

//        private static string MinifiedMouseScript = "(function(f,a){Object.defineProperty(MouseEvent.prototype,'pageX',{configurable:!0,get:function(){return this.clientX}});Object.defineProperty(MouseEvent.prototype,'pageY',{configurable:!0,get:function(){return this.clientY}});f.onNativeMouseEvent=function(g,h,i)" 
//        + "{try{var j=screen.logicalXDPI/screen.deviceXDPI,k=screen.logicalYDPI/screen.deviceYDPI,b=a.createEvent('MouseEvents'),c=a.body.scrollLeft+Math.round(j*h),d=a.body.scrollTop+Math.round(k*i),e=a.elementFromPoint(c,d);b.initMouseEvent(g,!0,!0,"
//        + "f,1,c,d,c,d,!1,!1,!1,!1,0,e);return(e?!e.dispatchEvent(b):!a.dispatchEvent(b))?'true':'false'}catch(l){return l}}})(window,document);";


//        private WebBrowser _browser;

//        /// <summary>
//        /// Gets or sets whether to suppress the scrolling of
//        /// the WebBrowser control;
//        /// </summary>
//        public bool ScrollDisabled { get; set; }

//        private bool userScalable = true;
//        private double maxScale = 2.0;
//        private double minScale = 0.5;
//        protected Border border;
//        private bool firstMouseMove = false;


//        public BrowserMouseHelper(ref WebBrowser browser)
//        {
//            _browser = browser;
//            browser.Loaded += new RoutedEventHandler(browser_Loaded);
//        }

//        private void browser_Loaded(object sender, RoutedEventArgs e)
//        {
//            var border0 = VisualTreeHelper.GetChild(_browser, 0);
//            var border1 = VisualTreeHelper.GetChild(border0, 0);
//            var panZoom = VisualTreeHelper.GetChild(border1, 0);
//            var grid = VisualTreeHelper.GetChild(panZoom, 0);
//            border = VisualTreeHelper.GetChild(grid, 0) as Border;

//            if (border != null)
//            {
//                border.ManipulationStarted += Border_ManipulationStarted;
//                border.ManipulationDelta += Border_ManipulationDelta;
//                border.ManipulationCompleted += Border_ManipulationCompleted;
//                border.DoubleTap += Border_DoubleTap;
//                border.Hold += Border_Hold;
//                border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
//            }

//            _browser.LoadCompleted += Browser_LoadCompleted;

//        }

//        void ParseViewportMeta()
//        {
//            string metaScript = "(function() { return document.querySelector('meta[name=viewport]').content; })()";

//            try
//            {
//                string metaContent = _browser.InvokeScript("eval", new string[] { metaScript }) as string;
//                string[] arr = metaContent.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
//                Dictionary<string, string> metaDictionary = new Dictionary<string, string>();
//                foreach (string val in arr)
//                {
//                    string[] keyVal = val.Split('=');
//                    metaDictionary.Add(keyVal[0], keyVal[1]);
//                }

//                this.userScalable = false; // reset to default
//                if (metaDictionary.ContainsKey("user-scalable"))
//                {
//                    this.userScalable = metaDictionary["user-scalable"] == "yes";
//                }

//                this.maxScale = 2.0;// reset to default
//                if (metaDictionary.ContainsKey("maximum-scale"))
//                {
//                    this.maxScale = double.Parse(metaDictionary["maximum-scale"]);
//                }

//                this.minScale = 0.5;// reset to default
//                if (metaDictionary.ContainsKey("minimum-scale"))
//                {
//                    this.minScale = double.Parse(metaDictionary["minimum-scale"]);
//                }
//            }
//            catch (Exception)
//            {

//            }
//        }

//        void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
//        {
//            ParseViewportMeta();

//            try
//            {
//                _browser.InvokeScript("execScript", MinifiedMouseScript);
//            }
//            catch (Exception)
//            {
//                Debug.WriteLine("BrowserHelper Failed to install mouse script in WebBrowser");
//            }
//        }

//        bool InvokeSimulatedMouseEvent(string eventName, Point pos)
//        {
//            bool bCancelled = false;
//            try
//            {
//                string strCancelled = _browser.InvokeScript("onNativeMouseEvent", new string[] { eventName, pos.X.ToString(), pos.Y.ToString() }) as string;                
//                if (bool.TryParse(strCancelled, out bCancelled))
//                {
//                    return bCancelled;
//                }
//            }
//            catch (Exception)
//            {
//                // script error
//            }

//            return bCancelled;
//        }

//        #region Hold

//        void Border_Hold(object sender, GestureEventArgs e)
//        {
//            Debug.WriteLine("Border_Hold");
//            e.Handled = true;
//        }

//        #endregion

//        #region DoubleTap

//        void Border_DoubleTap(object sender, GestureEventArgs e)
//        {
//            Debug.WriteLine("Border_DoubleTap");
//            e.Handled = true;
//        }

//        #endregion

//        #region MouseEvents

//        void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//        {
//            border.MouseMove += new MouseEventHandler(Border_MouseMove);
//            border.MouseLeftButtonUp += new MouseButtonEventHandler(Border_MouseLeftButtonUp);

//            firstMouseMove = true;

//            Point pos = e.GetPosition(_browser);
//            InvokeSimulatedMouseEvent("mousedown", pos);
//        }

//        void Border_MouseMove(object sender, MouseEventArgs e)
//        {
//            Point pos = e.GetPosition(_browser);
//            bool retVal = InvokeSimulatedMouseEvent("mousemove", pos);
//            // only the return value from the first mouse move event should be used to determine if scrolling is prevented.
//            if (firstMouseMove)
//            {
//                ScrollDisabled = retVal;
//            }
//        }

//        void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
//        {
//            border.MouseMove -= new MouseEventHandler(Border_MouseMove);
//            border.MouseLeftButtonUp -= new MouseButtonEventHandler(Border_MouseLeftButtonUp);
//            Point pos = e.GetPosition(_browser);
//            e.Handled = InvokeSimulatedMouseEvent("mouseup", pos);
//            ScrollDisabled = false;
//        }


//        #endregion

//        #region ManipulationEvents

//        void Border_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
//        {
//            if (ScrollDisabled)
//            {
//                e.Handled = true;
//                e.Complete();
//            }
//        }

//        private void Border_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
//        {
//            // optionally suppress zoom
//            if ((ScrollDisabled || !userScalable) && (e.DeltaManipulation.Scale.X != 0.0 || e.DeltaManipulation.Scale.Y != 0.0))
//            {
//                e.Handled = true;
//                e.Complete();
//            }
//            // optionally suppress scrolling
//            if (ScrollDisabled && (e.DeltaManipulation.Translation.X != 0.0 || e.DeltaManipulation.Translation.Y != 0.0))
//            {
//                e.Handled = true;
//                e.Complete();
//            }
//        }

//        private void Border_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
//        {
//            // suppress zoom
//            if (!userScalable && e.FinalVelocities != null)
//            {
//                if (e.FinalVelocities.ExpansionVelocity.X != 0.0 ||
//                   e.FinalVelocities.ExpansionVelocity.Y != 0.0)
//                {
//                    e.Handled = true;
//                }
//            }
//        }


//        #endregion

//    }
//}
