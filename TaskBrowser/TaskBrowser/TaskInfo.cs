namespace TaskBrowser
{
    using System;
    using System.Runtime.InteropServices;

    public class TaskInfo
    {
        public string _param1;
        public string _param2;
        public string _param3;
        public string _param4;
        public string _param5;

        public TaskInfo(string param1, string param2 = "", string param3 = "", string param4 = "", string param5 = "")
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

