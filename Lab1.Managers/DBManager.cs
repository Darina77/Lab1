using Lab1.DBModels;
using Lab1.DBAdapter;
namespace Lab1.Managers
{
    internal class DbManager
    {

        public static bool UserExists(string login)
        {
            return EntityWrapper.UserExists(login);
        }

        public static bool UserEmailExists(string email)
        {
            return EntityWrapper.UserEmailExists(email);
        }

        public static User GetUserByLogin(string login)
        {
            return EntityWrapper.GetUserByLogin(login);
        }

        public static void AddUser(User user)
        {
            EntityWrapper.AddUser(user);
        }

        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = EntityWrapper.GetUserByGuid(userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;
        }

        public static void AddRequest(Request request)
        {
            EntityWrapper.AddRequest(request);
        }
    }
}