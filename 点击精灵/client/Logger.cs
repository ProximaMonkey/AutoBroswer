namespace client
{
    using System;
    using System.IO;

    public class Logger
    {
        private static string ERRORFILE = "error.txt";
        private static string LOGGERFILE = "log.txt";
        private static string TRACEFILE = "trace.txt";

        public static void Clear()
        {
            try
            {
                if (File.Exists(LOGGERFILE))
                {
                    File.Delete(LOGGERFILE);
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
                StreamWriter writer = new StreamWriter(ERRORFILE, true);
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
                StreamWriter writer = new StreamWriter(TRACEFILE, true);
                writer.Write(string.Concat(new object[] { DateTime.Now, "\t", str, "\r\n" }));
                writer.Close();
            }
            catch
            {
            }
        }

        public static void Write(uint taskID, string str)
        {
            try
            {
                StreamWriter writer = new StreamWriter(LOGGERFILE, true);
                writer.Write(string.Concat(new object[] { taskID, "\t", DateTime.Now, "\t", str, "\r\n" }));
                writer.Close();
            }
            catch
            {
            }
        }
    }
}

