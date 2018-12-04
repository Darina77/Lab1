using CountServiceInterface;
using Lab1.DBModels;
namespace Lab1.Managers
{
    public class DbManager
    {

        public static bool UserExists(string login)
        {
            return CountServiceWrapper.UserExists(login);
        }

        public static bool UserEmailExists(string email)
        {
            return CountServiceWrapper.UserEmailExists(email);
        }

        public static User GetUserByLogin(string login)
        {
            return CountServiceWrapper.GetUserByLogin(login);
        }

        public static void AddUser(User user)
        {
            CountServiceWrapper.AddUser(user);
        }

        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = CountServiceWrapper.GetUserByGuid(userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;
        }

        public static void AddRequest(Request request)
        {
            CountServiceWrapper.AddRequest(request);
        }
    }
}