using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TenDayBrowser
{
    using System.Drawing;
    using System.Windows.Forms;

    public class ExtendedTabControl : TabControl
    {
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle displayRectangle = base.DisplayRectangle;
                return new Rectangle(displayRectangle.Left - 4, displayRectangle.Top - 4, displayRectangle.Width + 8, displayRectangle.Height + 8);
            }
        }
    }
}
