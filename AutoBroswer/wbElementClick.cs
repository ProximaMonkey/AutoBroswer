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
        public static string m_simulateJS =
            @"
function simulate(element, eventName)
{
    var options = extend(defaultOptions, arguments[2] || {});
    var oEvent, eventType = null;

    for (var name in eventMatchers)
    {
        if (eventMatchers[name].test(eventName)) { eventType = name; break; }
    }

    if (!eventType)
        throw new SyntaxError('Only HTMLEvents and MouseEvents interfaces are supported');

    if (document.createEvent)
    {
        oEvent = document.createEvent(eventType);
        if (eventType == 'HTMLEvents')
        {
            oEvent.initEvent(eventName, options.bubbles, options.cancelable);
        }
        else
        {
            oEvent.initMouseEvent(eventName, options.bubbles, options.cancelable, document.defaultView,
            options.button, options.pointerX, options.pointerY, options.pointerX, options.pointerY,
            options.ctrlKey, options.altKey, options.shiftKey, options.metaKey, options.button, element);
        }
        element.dispatchEvent(oEvent);
    }
    else
    {
        options.clientX = options.pointerX;
        options.clientY = options.pointerY;
        var evt = document.createEventObject();
        oEvent = extend(evt, options);
        element.fireEvent('on' + eventName, oEvent);
    }
    return element;
}

function extend(destination, source) {
    for (var property in source)
      destination[property] = source[property];
    return destination;
}

var eventMatchers = {
    'HTMLEvents': /^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|scroll)$/,
    'MouseEvents': /^(?:click|dblclick|mouse(?:down|up|over|move|out))$/
}
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
}
";
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
        @"function eventFire(element){alert('hello');element.fireEvent('onmousedown');element.click();}";

    }
}
