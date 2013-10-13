namespace TaskBrowser
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    [ComImport, ComVisible(true), Guid("7FD52380-4E07-101B-AE2D-08002B2EC713"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersistStreamInit
    {
        void GetClassID([In, Out] ref Guid pClassID);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int IsDirty();
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Load([In, MarshalAs(UnmanagedType.Interface)] IStream pstm);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Save([In, MarshalAs(UnmanagedType.Interface)] IStream pstm, [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty);
        void GetSizeMax([Out, MarshalAs(UnmanagedType.LPArray)] long pcbSize);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int InitNew();
    }
}

