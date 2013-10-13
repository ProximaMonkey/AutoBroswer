using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenDayBrowser
{
    using System;
    using System.Runtime.InteropServices;
    public enum TaskCommand
    {
        Task_ClearCookie = 6,
        Task_ClickButton = 2,
        Task_ClickChecked = 12,
        Task_ClickLink = 3,
        Task_ClickRadio = 11,
        Task_Count = 0x1d,
        Task_DeepClick = 5,
        Task_FindHrefHrefPage1 = 0x11,
        Task_FindHrefHrefPage2 = 0x12,
        Task_FindHrefLinkPage1 = 15,
        Task_FindHrefLinkPage2 = 0x10,
        Task_FindHrefSrcPage1 = 0x15,
        Task_FindHrefSrcPage2 = 0x16,
        Task_FindLinkHrefPage1 = 13,
        Task_FindLinkHrefPage2 = 14,
        Task_FindLinkLinkPage1 = 7,
        Task_FindLinkLinkPage2 = 8,
        Task_FindLinkSrcPage1 = 0x13,
        Task_FindLinkSrcPage2 = 20,
        Task_FindSrcHrefPage1 = 0x19,
        Task_FindSrcHrefPage2 = 0x1a,
        Task_FindSrcLinkPage1 = 0x17,
        Task_FindSrcLinkPage2 = 0x18,
        Task_FindSrcSrcPage1 = 0x1b,
        Task_FindSrcSrcPage2 = 0x1c,
        Task_Fresh = 9,
        Task_InputText = 1,
        Task_Navigate = 4,
        Task_None = -1,
        Task_PressKey = 10,
        Task_Wait = 0
    }

    public class TaskInfo
    {
        public string _param1;
        public string _param2;
        public string _param3;
        public string _param4;
        public string _param5;

        public TaskInfo(string param1,  [Optional, DefaultParameterValue("")]　string param2,  [Optional, DefaultParameterValue("")]　string param3,  [Optional, DefaultParameterValue("bb")] string param4,  [Optional, DefaultParameterValue("bb")]　string param5)
        {
            this._param1 = param1;
            this._param2 = param2;
            this._param3 = param3;
            this._param4 = param4;
            this._param5 = param5;
        }

        public uint CalculateScore()
        {
            uint num = 0;
            switch (((TaskCommand) WindowUtil.StringToInt(this._param1)))
            {
                case TaskCommand.Task_Wait:
                    if (!string.IsNullOrEmpty(this._param2))
                    {
                        num = (WindowUtil.StringToUint(this._param2) + 0x1d) / 30;
                    }
                    return num;

                case TaskCommand.Task_DeepClick:
                    if (!string.IsNullOrEmpty(this._param2))
                    {
                        num = 1 + (WindowUtil.StringToUint(this._param2) * ((WindowUtil.StringToUint(this._param3) + 0x1d) / 30));
                    }
                    return num;

                case TaskCommand.Task_FindLinkLinkPage1:
                case TaskCommand.Task_FindLinkHrefPage1:
                case TaskCommand.Task_FindHrefLinkPage1:
                case TaskCommand.Task_FindHrefHrefPage1:
                case TaskCommand.Task_FindSrcLinkPage1:
                case TaskCommand.Task_FindSrcHrefPage1:
                case TaskCommand.Task_FindHrefSrcPage1:
                case TaskCommand.Task_FindLinkSrcPage1:
                case TaskCommand.Task_FindSrcSrcPage1:
                    if (!string.IsNullOrEmpty(this._param4))
                    {
                        num = WindowUtil.StringToUint(this._param4) + 1;
                    }
                    return num;
            }
            return 1;
        }
    }
}
