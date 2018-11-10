using System;
using System.IO;
using System.Windows;
using Lab1.Models;
using  Lab1.Tools;
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
                userCandidate = SerializationManager.Deserialize<User>(Path.Combine(FileFolderHelper.LastUserFilePath));
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
            userCandidate = DbManager.CheckCachedUser(userCandidate);
            if (userCandidate == null)
                Logger.Log("Failed to relogin last user");
            else
                CurrentUser = userCandidate;
        }
        internal static void CloseApp()
        {
            MessageBox.Show("Close");
            Environment.Exit(1);
        }
    }
}
