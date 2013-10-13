using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenDayBrowser
{
    internal enum IEStatus
    {
        IEStatus_None,
        IEStatus_SysWait,
        IEStatus_SysComplete,
        IEStatus_Wait,
        IEStatus_MoveToDest
    }
}
