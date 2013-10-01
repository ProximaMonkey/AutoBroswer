using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoBroswer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutoBroswerForm());
        }

        #region 处理未捕获异常的挂钩函数
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception error = e.Exception as Exception;
            if (error != null)
            {
                FileLogger.Instance.LogInfo(string.Format("出现应用程序未处理的异常\n异常类型：{0}\n异常消息：{1}\n异常位置：{2}\n",
                    error.GetType().Name, error.Message, error.StackTrace));
            }
            else
            {
                FileLogger.Instance.LogInfo(string.Format("Application ThreadError:{0}", e));
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //cn.ccets.papercontrol.FullScreenHandle.ShowTray();
            Exception error = e.ExceptionObject as Exception;
            if (error != null)
            {
                FileLogger.Instance.LogInfo(string.Format("Application UnhandledException:{0};\n堆栈信息:{1}", error.Message, error.StackTrace));
            }
            else
            {
                FileLogger.Instance.LogInfo(string.Format("Application UnhandledError:{0}", e));
            }
        }

        #endregion
    }
}
