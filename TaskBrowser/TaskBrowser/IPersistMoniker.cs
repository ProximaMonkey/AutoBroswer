namespace TaskBrowser
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), ComVisible(true), Guid("79eac9c9-baf9-11ce-8c82-00aa004ba90b")]
    public interface IPersistMoniker
    {
        void GetClassID([In, Out] ref Guid pClassID);
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int IsDirty();
        void Load([In] int fFullyAvailable, [In, MarshalAs(UnmanagedType.Interface)] IMoniker pmk, [In, MarshalAs(UnmanagedType.Interface)] object pbc, [In, MarshalAs(UnmanagedType.U4)] uint grfMode);
        void SaveCompleted([In, MarshalAs(UnmanagedType.Interface)] IMoniker pmk, [In, MarshalAs(UnmanagedType.Interface)] object pbc);
        [return: MarshalAs(UnmanagedType.Interface)]
        IMoniker GetCurMoniker();
    }
}

