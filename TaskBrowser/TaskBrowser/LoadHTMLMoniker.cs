namespace TaskBrowser
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    internal class LoadHTMLMoniker : IMoniker
    {
        private string m_sBaseName = string.Empty;
        private IStream m_stream;

        public void InitLoader(string sContent, string sBaseUrl)
        {
            this.m_sBaseName = sBaseUrl;
            if (WindowUtil.CreateStreamOnHGlobal(Marshal.StringToHGlobalAuto(sContent), true, out this.m_stream) == 0)
            {
                IStream stream = this.m_stream;
            }
        }

        void IMoniker.BindToObject(IBindCtx pbc, IMoniker pmkToLeft, ref Guid riidResult, out object ppvResult)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, ref Guid riid, out object ppvObj)
        {
            ppvObj = null;
            if (riid.Equals(Iid_Clsids.IID_IStream))
            {
                ppvObj = this.m_stream;
            }
        }

        void IMoniker.CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.ComposeWith(IMoniker pmkRight, bool fOnlyIfNotGeneric, out IMoniker ppmkComposite)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.Enum(bool fForward, out IEnumMoniker ppenumMoniker)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.GetClassID(out Guid pClassID)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, out string ppszDisplayName)
        {
            ppszDisplayName = this.m_sBaseName;
        }

        void IMoniker.GetSizeMax(out long pcbSize)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out System.Runtime.InteropServices.ComTypes.FILETIME pFileTime)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.Hash(out int pdwHash)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.Inverse(out IMoniker ppmk)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        int IMoniker.IsDirty()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        int IMoniker.IsEqual(IMoniker pmkOtherMoniker)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        int IMoniker.IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        int IMoniker.IsSystemMoniker(out int pdwMksys)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.Load(IStream pStm)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, string pszDisplayName, out int pchEaten, out IMoniker ppmkOut)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IMoniker.Save(IStream pStm, bool fClearDirty)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}

