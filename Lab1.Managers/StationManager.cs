﻿using System;
using System.IO;
using System.Windows.Forms;
using Lab1.DBModels;
using Lab1.Tools;
namespace Lab1.Managers
{
    public static class StationManager
    {
        public static User CurrentUser { get; set; }

        static StationManager()
        {
                DeserializeLastUser();     
        }

        private static void DeserializeLastUser()
        {
            User userCandidate;
            try
            {
                FileInfo file = new FileInfo(FileFolderHelper.LastUserFilePath);
                if (file.Exists)
                {
                    userCandidate = SerializationManager.Deserialize<User>(Path.Combine(FileFolderHelper.LastUserFilePath));

                }
                else
                userCandidate = null;
            }
            catch (Exception ex)
            {
                userCandidate = null;
                Logger.Log("Failed to Deserialize last user", ex);
            }
            if (userCandidate == null)
            {
                Logger.Log("User was not deserialized");
                return;
            }
            if (userCandidate.Requests == null)
            {
                Logger.Log("User requests was not deserialized");
                return;
            }
            userCandidate = DbManager.CheckCachedUser(userCandidate);
            if (userCandidate == null)
                Logger.Log("Failed to relogin last user");
            else
                CurrentUser = userCandidate;
            SerializationManager.Serialize(CurrentUser, FileFolderHelper.LastUserFilePath);
        }

        public static void CloseApp()
        {
            MessageBox.Show("Close");
            Logger.Log("Close");
            Environment.Exit(1);
        }
    }
}