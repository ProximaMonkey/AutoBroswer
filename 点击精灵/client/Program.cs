namespace client
{
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    internal static class Program
    {
        private static void HandleRunningInstance(Process instance)
        {
            WindowUtil.ShowWindowAsync(instance.MainWindowHandle, 4);
            WindowUtil.SetForegroundWindow(instance.MainWindowHandle);
        }

        [STAThread]
        private static void Main()
        {
            try
            {
                Process instance = RunningInstance();
                if (instance == null)
                {
                    RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Styles");
                    key.SetValue("MaxScriptStatements", 0x3b9aca00);
                    key.Close();
                    key = null;
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    ManageForm mainForm = new ManageForm();
                    Application.Run(mainForm);
                }
                else
                {
                    HandleRunningInstance(instance);
                }
            }
            catch
            {
            }
        }

        private static Process RunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process2 in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (process2.Id != currentProcess.Id)
                {
                    return process2;
                }
            }
            return null;
        }
    }
}

