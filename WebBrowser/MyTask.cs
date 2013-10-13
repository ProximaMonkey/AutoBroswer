using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenDayBrowser
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    public class MyTask : ICloneable
    {
        public bool _allowScrollWhileWaiting = TenDayBrowser.ALLOWSCROLLWHILEWAITING;
        public ArrayList _AreaClicks;
        public int _clickCountPerIP = TenDayBrowser.CLICKCOUNTPERIP;
        public int _clickIntervalTime = TenDayBrowser.CLICKINTERVALTIME;
        public int _clickRate = TenDayBrowser.CLICKRATE;
        public int _completedCount;
        public int _completeTotalCount;
        public int _failTotalCount;
        public ArrayList _HourClicks;
        public int _id;
        public bool _isRun;
        public ArrayList _taskItems = new ArrayList();
        public uint _totalDayClicks;
        private bool _vipMode;

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
            task._completeTotalCount = this._completeTotalCount;
            task._failTotalCount = this._failTotalCount;
            task._completedCount = this._completedCount;
            task._id = this._id;
            if (this._HourClicks != null)
            {
                task._HourClicks = new ArrayList(this._HourClicks);
            }
            if (this._AreaClicks != null)
            {
                task._AreaClicks = new ArrayList(this._AreaClicks);
            }
            task._totalDayClicks = this._totalDayClicks;
            return task;
        }

        public void Complete(bool completed)
        {
            this._isRun = false;
            if (completed)
            {
                this._completedCount++;
                this._completeTotalCount++;
            }
            else
            {
                this._failTotalCount++;
            }
        }

        ~MyTask()
        {
            if (this._taskItems != null)
            {
                this._taskItems.Clear();
            }
            this._taskItems = null;
        }

        public int GetTaskInfo(ref TaskInfo taskItem, ref int taskIndex, ref bool startLoop, ref bool loop, ref int loopTime)
        {
            if (startLoop && loop)
            {
                taskIndex--;
                loopTime++;
            }
            else
            {
                startLoop = false;
                loop = false;
                loopTime = 0;
            }
            if (taskIndex >= this._taskItems.Count)
            {
                return -1;
            }
            taskItem = (TaskInfo) this._taskItems[taskIndex++];
            switch (WindowUtil.StringToInt(taskItem._param1))
            {
                case 5:
                case 7:
                case 13:
                case 15:
                case 0x11:
                case 0x17:
                case 0x19:
                case 0x13:
                case 0x15:
                case 0x1b:
                    startLoop = loop = true;
                    break;

                default:
                    startLoop = loop = false;
                    break;
            }
            return 0;
        }

        public void Insert(TaskInfo task,  [Optional, DefaultParameterValue(-1)]　int index)
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
            return ((((hour >= 0) && (hour < TenDayBrowser.HOURSCOUNT)) && ((this._HourClicks != null) && (this._HourClicks.Count == TenDayBrowser.HOURSCOUNT))) && (Convert.ToInt32(this._HourClicks[hour]) <= this._completedCount));
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
            if ((hourClicks != null) && (hourClicks.Count != TenDayBrowser.HOURSCOUNT))
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

        public bool VipMode
        {
            get
            {
                return this._vipMode;
            }
            set
            {
                this._vipMode = value;
            }
        }
    }
}
