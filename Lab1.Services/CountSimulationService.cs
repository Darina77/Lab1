using System;
using CountServiceInterface;
using Lab1.DBAdapter;
using Lab1.DBModels;


namespace Lab1.Services
{

    class CountSimulatorService : ICountContract
    {
        public bool UserExists(string login)
        {
            return EntityWrapper.UserExists(login);
        }
        public bool UserEmailExists(string login)
        {
            return EntityWrapper.UserEmailExists(login);
        }

        public User GetUserByLogin(string login)
        {
            return EntityWrapper.GetUserByLogin(login);
        }

        public User GetUserByGuid(Guid guid)
        {
            return EntityWrapper.GetUserByGuid(guid);
        }

        public void AddUser(User user)
        {
            EntityWrapper.AddUser(user);
        }

        public void AddRequest(Request wallet)
        {
            EntityWrapper.AddRequest(wallet);
        }


    }
}
