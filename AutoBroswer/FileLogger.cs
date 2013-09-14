using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AutoBroswer
{
    public class FileLogger
    {
        private static FileLogger singleton;
        private StreamWriter logWriter;

        public static FileLogger Instance
        {
            get { return singleton ?? (singleton = new FileLogger()); }
        }

        public void Open(string filePath, bool append)
        {
            if (logWriter != null)
                throw new InvalidOperationException(
                    "Logger is already open");

            logWriter = new StreamWriter(filePath, append);
            logWriter.AutoFlush = true;
        }

        public void Close()
        {
            if (logWriter != null)
            {
                logWriter.Close();
                logWriter = null;
            }
        }

        public void LogInfo(string entry)
        {
            if (this.logWriter == null)
                throw new InvalidOperationException(
                    "Logger is not open");
            logWriter.WriteLine("{0} - {1}",
                 DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                 entry);
        }
    }
}
