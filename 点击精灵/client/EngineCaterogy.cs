namespace client
{
    using System;
    using System.Collections;

    internal class EngineCaterogy
    {
        private string _name;
        private ArrayList _templates = new ArrayList();

        public EngineCaterogy(string name)
        {
            this._name = name;
        }

        public void Add(MyTask template)
        {
            this._templates.Add(template);
        }

        public MyTask GetTemplate(int index)
        {
            if ((index >= 0) && (index < this._templates.Count))
            {
                return (MyTask) this._templates[index];
            }
            return null;
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public ArrayList Templates
        {
            get
            {
                return this._templates;
            }
        }
    }
}

