namespace client
{
    using System;

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
}

