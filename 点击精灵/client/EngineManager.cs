namespace client
{
    using System;
    using System.Collections;

    internal class EngineManager
    {
        private ArrayList _engineCaterogys = new ArrayList();
        private int _selectedIndex;

        private void Add(EngineCaterogy caterogy)
        {
            this._engineCaterogys.Add(caterogy);
        }

        public void Add(string caterogy, MyTask template)
        {
            foreach (EngineCaterogy caterogy2 in this._engineCaterogys)
            {
                if (caterogy2.Name.Equals(caterogy))
                {
                    caterogy2.Add(template);
                    return;
                }
            }
            EngineCaterogy caterogy3 = new EngineCaterogy(caterogy);
            caterogy3.Add(template);
            this.Add(caterogy3);
        }

        public EngineCaterogy GetCaterogy(int index)
        {
            if ((index >= 0) && (index < this._engineCaterogys.Count))
            {
                return (EngineCaterogy) this._engineCaterogys[index];
            }
            return null;
        }

        public ArrayList Caterogy
        {
            get
            {
                return this._engineCaterogys;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this._selectedIndex;
            }
            set
            {
                this._selectedIndex = value;
            }
        }
    }
}

