using System;
using System.IO;
using System.Text;

namespace Lab1.Tools
{
    public static class Logger
    {
        public static void Log(string message)
        {
            lock (FileFolderHelper.LogFilepath)
            {
                StreamWriter writer = null;
                FileStream file = null;
                try
                {
                    FileFolderHelper.CheckAndCreateFile(FileFolderHelper.LogFilepath);
                    file = new FileStream(FileFolderHelper.LogFilepath, FileMode.Append);
                    writer = new StreamWriter(file);
                    writer.WriteLine(DateTime.Now.ToString("HH:mm:ss.ms") + " " + message);
                }
                catch
                {
                }
                finally
                {
                    writer?.Close();
                    file?.Close();
                    writer = null;
                    file = null;
                }
            }
        }
        public static void Log(string message, Exception ex)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(message);
            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            Log(stringBuilder.ToString());
        }

        public static void Log(Exception ex)
        {
            var stringBuilder = new StringBuilder();
            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            Log(stringBuilder.ToString());
        }
    }
}
