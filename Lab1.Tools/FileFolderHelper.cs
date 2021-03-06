﻿using System;
using System.IO;

namespace Lab1.Tools
{
    public static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string ClientFolderPath =
            Path.Combine(AppDataPath, "Lab1");

        internal static readonly string LogFolderPath =
            Path.Combine(ClientFolderPath, "Log");

        internal static readonly string LogFilepath = Path.Combine(LogFolderPath,
            "App_" + DateTime.Now.ToString("MM/dd/yyyy") + ".txt");

        internal static readonly string StorageFilePath =
            Path.Combine(ClientFolderPath, "Storage.walsim");

        public static readonly string LastUserFilePath =
            Path.Combine(ClientFolderPath, "LastUser.walsim");
        internal static readonly string LastRequestFilePath =
            Path.Combine(ClientFolderPath, "LastRequests.walsim");

        public static void CheckAndCreateFile(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                if (!file.Exists)
                {
                    file.Create().Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
