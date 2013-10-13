namespace client
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    public class BrowserManager
    {
        private ArrayList _taskBrowsers = new ArrayList();

        public void CreateBrowserProcess(bool isHiden, IntPtr hwnd)
        {
            TaskBrowser2 browser = new TaskBrowser2();
            browser.CreateBrowserProcess(isHiden, hwnd);
            this._taskBrowsers.Add(browser);
        }

        public int GetBrowsersCount()
        {
            return this._taskBrowsers.Count;
        }

        public bool GetEmptyBrowserProcess()
        {
            foreach (TaskBrowser2 browser in this._taskBrowsers)
            {
                if (!browser.IsRunTask())
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetEmptyBrowserWindow()
        {
            foreach (TaskBrowser2 browser in this._taskBrowsers)
            {
                if (browser.IsEmptyWindow())
                {
                    return true;
                }
            }
            return false;
        }

        public void HideWindow()
        {
            foreach (TaskBrowser2 browser in this._taskBrowsers)
            {
                browser.HideWindow();
            }
        }

        public bool IsRunTask()
        {
            foreach (TaskBrowser2 browser in this._taskBrowsers)
            {
                if (browser.IsRunTask())
                {
                    return true;
                }
            }
            return false;
        }

        public void RunTask(uint taskID, COPYDATASTRUCT cds)
        {
            foreach (TaskBrowser2 browser in this._taskBrowsers)
            {
                if (browser.IsEmptyWindow())
                {
                    IntPtr mainWindowHandle = browser.GetMainWindowHandle();
                    if (mainWindowHandle != IntPtr.Zero)
                    {
                        browser.RunTask(taskID);
                        WindowUtil.SendMessage(mainWindowHandle, 0x4a, 0, ref cds);
                    }
                }
            }
        }

        public void ShowWindow()
        {
            foreach (TaskBrowser2 browser in this._taskBrowsers)
            {
                browser.ShowWindow();
            }
        }

        public void StopBrowserTask(bool closeAll = true, uint taskID = 0)
        {
            if (closeAll)
            {
                foreach (TaskBrowser2 browser in this._taskBrowsers)
                {
                    browser.StopBrowserTask();
                }
                this._taskBrowsers.Clear();
            }
            else
            {
                foreach (TaskBrowser2 browser2 in this._taskBrowsers)
                {
                    if (browser2.IsRunTask(taskID))
                    {
                        browser2.StopBrowserTask();
                        this._taskBrowsers.Remove(browser2);
                        break;
                    }
                }
            }
        }

        public void Update()
        {
            foreach (TaskBrowser2 browser in this._taskBrowsers)
            {
                if (browser.IsTimeOut())
                {
                    browser.StopBrowserTask();
                    this._taskBrowsers.Remove(browser);
                    break;
                }
                if (browser.IsProcessExit())
                {
                    browser.StopBrowserTask();
                    this._taskBrowsers.Remove(browser);
                    break;
                }
            }
        }
    }
}

