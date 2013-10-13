namespace TaskBrowser
{
    using System;
    using System.IO;

    public class Logger
    {
        private const string ERRORFILE = "browsererror.txt";
        private const string LOGGERFILE = "browserlog.txt";
        private const string TRACEFILE = "browsertrace.txt";

        public static void Clear()
        {
            try
            {
                if (File.Exists("browserlog.txt"))
                {
                    File.Delete("browserlog.txt");
                }
            }
            catch
            {
            }
        }

        public static void Error(Exception e)
        {
            try
            {
                StreamWriter writer = new StreamWriter("browsererror.txt", true);
                writer.Write(e.Message);
                writer.Write(e.StackTrace);
                writer.Write(e.InnerException);
                writer.Write(e.Source);
                writer.Write("\r\n");
                writer.Close();
            }
            catch
            {
            }
        }

        public static void Trace(string str)
        {
            try
            {
                StreamWriter writer = new StreamWriter("browsertrace.txt", true);
                writer.Write(string.Concat(new object[] { DateTime.Now, "\t", str, "\r\n" }));
                writer.Close();
            }
            catch
            {
            }
        }

        public static void Write(int taskID, string str)
        {
            try
            {
                StreamWriter writer = new StreamWriter("browserlog.txt", true);
                writer.Write(string.Concat(new object[] { taskID, "\t", DateTime.Now, "\t", str, "\r\n" }));
                writer.Close();
            }
            catch
            {
            }
        }
    }
}

