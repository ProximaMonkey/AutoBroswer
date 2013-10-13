namespace client
{
    using System;
    using System.Diagnostics;

    public class TaskBrowser2
    {
        private bool _isRun;
        private System.Diagnostics.Process _process;
        private DateTime _startTime = new DateTime();
        private uint _taskID;

        public void CreateBrowserProcess(bool isHiden, IntPtr hwnd)
        {
            IntPtr foregroundWindow = WindowUtil.GetForegroundWindow();
            this._process = new System.Diagnostics.Process();
            ProcessStartInfo info = new ProcessStartInfo {
                FileName = "TaskBrowser.exe"
            };
            if (isHiden)
            {
                info.WindowStyle = ProcessWindowStyle.Hidden;
            }
            info.Arguments = hwnd.ToInt64().ToString();
            this._process.StartInfo = info;
            this._process.Start();
            WindowUtil.SetForegroundWindow(foregroundWindow);
        }

        public IntPtr GetMainWindowHandle()
        {
            return WindowUtil.GetWindowHandleFromProcessID(this._process.Id);
        }

        public void HideWindow()
        {
            IntPtr mainWindowHandle = this.GetMainWindowHandle();
            if (mainWindowHandle != IntPtr.Zero)
            {
                WindowUtil.ShowWindow(mainWindowHandle, 0);
            }
        }

        public bool IsEmptyWindow()
        {
            return (!this._isRun && (this.GetMainWindowHandle() != IntPtr.Zero));
        }

        public bool IsProcessExit()
        {
            if (this._process != null)
            {
                return this._process.HasExited;
            }
            return true;
        }

        public bool IsRunTask()
        {
            return this._isRun;
        }

        public bool IsRunTask(uint taskID)
        {
            return (this._isRun && (taskID == this._taskID));
        }

        public bool IsTimeOut()
        {
            if (this._isRun)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this._startTime);
                return (span.TotalMinutes >= 25.0);
            }
            return false;
        }

        public void RunTask(uint taskID)
        {
            this._isRun = true;
            this._taskID = taskID;
            this._startTime = DateTime.Now;
        }

        public void ShowWindow()
        {
            IntPtr mainWindowHandle = this.GetMainWindowHandle();
            if (mainWindowHandle != IntPtr.Zero)
            {
                WindowUtil.ShowWindow(mainWindowHandle, 8);
            }
        }

        public void StopBrowserTask()
        {
            this._isRun = false;
            if ((this._process != null) && !this._process.HasExited)
            {
                this._process.Kill();
            }
            this._process = null;
        }

        public System.Diagnostics.Process Process
        {
            get
            {
                return this._process;
            }
            set
            {
                this._process = value;
            }
        }

        public uint TaskID
        {
            get
            {
                return this._taskID;
            }
        }
    }
}

