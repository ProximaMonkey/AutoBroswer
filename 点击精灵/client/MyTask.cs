namespace client
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    public class MyTask : ICloneable
    {
        public ArrayList _AreaClicks;
        private string _category = string.Empty;
        public int _completedCount;
        public ArrayList _HourClicks;
        public uint _id;
        public uint _invalidClickCount;
        public bool _isRun;
        private string _name = string.Empty;
        private string _note = string.Empty;
        public uint _publishTime;
        public ArrayList _taskItems = new ArrayList();
        public uint _todayInvalidClickCount;
        public uint _todayValidClickCount;
        public uint _totalDayClicks;
        public uint _validClickCount;
        public uint _yesterdayInvalidClickCount;
        public uint _yesterdayValidClickCount;

        public uint CalculateScore()
        {
            uint num = 0;
            if (this._taskItems != null)
            {
                foreach (TaskInfo info in this._taskItems)
                {
                    num += info.CalculateScore();
                }
            }
            return num;
        }

        public object Clone()
        {
            MyTask task = new MyTask();
            if (this._taskItems != null)
            {
                task._taskItems = new ArrayList();
                for (int i = 0; i < this._taskItems.Count; i++)
                {
                    TaskInfo info2 = (TaskInfo) this._taskItems[i];
                    TaskInfo info = new TaskInfo(info2._param1, info2._param2, info2._param3, info2._param4, info2._param5);
                    task._taskItems.Add(info);
                }
            }
            task._isRun = this._isRun;
            task._validClickCount = this._validClickCount;
            task._invalidClickCount = this._invalidClickCount;
            task._todayValidClickCount = this._todayValidClickCount;
            task._todayInvalidClickCount = this._todayInvalidClickCount;
            task._yesterdayValidClickCount = this._yesterdayValidClickCount;
            task._yesterdayInvalidClickCount = this._yesterdayInvalidClickCount;
            task._completedCount = this._completedCount;
            task._id = this._id;
            task._name = this._name;
            task._category = this._category;
            if (this._HourClicks != null)
            {
                task._HourClicks = new ArrayList(this._HourClicks);
            }
            if (this._AreaClicks != null)
            {
                task._AreaClicks = new ArrayList(this._AreaClicks);
            }
            task._totalDayClicks = this._totalDayClicks;
            task._note = this._note;
            task._publishTime = this._publishTime;
            return task;
        }

        ~MyTask()
        {
            if (this._taskItems != null)
            {
                this._taskItems.Clear();
            }
            this._taskItems = null;
        }

        public void Insert(TaskInfo task, int index = -1)
        {
            if (index == -1)
            {
                this._taskItems.Add(task);
            }
            else
            {
                this._taskItems.Insert(index, task);
            }
        }

        public bool IsCompleteHourTask(int hour)
        {
            return ((((hour >= 0) && (hour < ManageForm.HOURSCOUNT)) && ((this._HourClicks != null) && (this._HourClicks.Count == ManageForm.HOURSCOUNT))) && (Convert.ToInt32(this._HourClicks[hour]) <= this._completedCount));
        }

        public void Remove(int index)
        {
            if ((index >= 0) && (index < this._taskItems.Count))
            {
                this._taskItems.RemoveAt(index);
            }
        }

        public void ResetCompletedCount()
        {
            this._completedCount = 0;
        }

        public void Reverse(int posFrom, int posTo)
        {
            if (((posFrom >= 0) && (posFrom < this._taskItems.Count)) && ((posTo >= 0) && (posTo < this._taskItems.Count)))
            {
                TaskInfo info = (TaskInfo) this._taskItems[posFrom];
                this._taskItems.RemoveAt(posFrom);
                this._taskItems.Insert(posTo, info);
            }
        }

        public void SetAreaClick(ArrayList areaClicks)
        {
            if (this._AreaClicks != null)
            {
                this._AreaClicks.Clear();
            }
            this._AreaClicks = null;
            if (areaClicks.Count != 0x23)
            {
                areaClicks.Clear();
                areaClicks = null;
            }
            this._AreaClicks = areaClicks;
        }

        public void SetHourClick(ArrayList hourClicks)
        {
            if (this._HourClicks != null)
            {
                this._HourClicks.Clear();
            }
            this._HourClicks = null;
            this._totalDayClicks = 0;
            if ((hourClicks != null) && (hourClicks.Count != ManageForm.HOURSCOUNT))
            {
                hourClicks.Clear();
                hourClicks = null;
            }
            else
            {
                for (int i = 0; i < hourClicks.Count; i++)
                {
                    this._totalDayClicks += Convert.ToUInt32(hourClicks[i]);
                }
            }
            this._HourClicks = hourClicks;
        }

        public string Category
        {
            get
            {
                return this._category;
            }
            set
            {
                this._category = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string Note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
            }
        }
    }
}

