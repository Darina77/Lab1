using System.Collections.Generic;
using System.Linq;
using Lab1.Models;
using  Lab1.Tools;

namespace Lab1.Managers
{
   internal class DbManager
    {
        private static readonly List<User> Users;
        static DbManager()
        {
            Users = SerializationManager.Deserialize<List<User>>(FileFolderHelper.StorageFilePath) ?? new List<User>();
        }

        internal static bool UserExists(string login)
        {
            return Users.Any(u => u.Login == login);
        }

        internal static User GetUserByLogin(string login)
        {
            return Users.FirstOrDefault(u => u.Login == login);
        }

        internal static void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }
        private static void SaveChanges()
        {
            SerializationManager.Serialize(Users, FileFolderHelper.StorageFilePath);
        }
        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = Users.FirstOrDefault(u => u.Guid == userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;
        }
        public static void UpdateUser(User currentUser)
        {
            SaveChanges();
        }

        internal static void InitUser()
        {
            Users.Add(new User("Name", "Last", "email@m.m", "login", "pass"));
        }
    }
}
