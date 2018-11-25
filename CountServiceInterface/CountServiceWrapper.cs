using System;
using System.ServiceModel;
using Lab1.DBModels;

namespace Lab1.CountServiceInterface
{
    public class CountServiceWrapper
    {
        public static bool UserExists(string login)
        {
            using (var myChannelFactory = new ChannelFactory<ICountContract>("Server"))
            {
                ICountContract client = myChannelFactory.CreateChannel();
                return client.UserExists(login);
            }
        }
        public static bool UserEmailExists(string login)
        {
            using (var myChannelFactory = new ChannelFactory<ICountContract>("Server"))
            {
                ICountContract client = myChannelFactory.CreateChannel();
                return client.UserEmailExists(login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var myChannelFactory = new ChannelFactory<ICountContract>("Server"))
            {
                ICountContract client = myChannelFactory.CreateChannel();
                return client.GetUserByLogin(login);
            }
        }

        public static User GetUserByGuid(Guid guid)
        {
            using (var myChannelFactory = new ChannelFactory<ICountContract>("Server"))
            {
                ICountContract client = myChannelFactory.CreateChannel();
                return client.GetUserByGuid(guid);
            }
        }

        public static void AddUser(User user)
        {
            using (var myChannelFactory = new ChannelFactory<ICountContract>("Server"))
            {
                ICountContract client = myChannelFactory.CreateChannel();
                client.AddUser(user);
            }
        }

        public static void AddRequest(Request wallet)
        {
            using (var myChannelFactory = new ChannelFactory<ICountContract>("Server"))
            {
                ICountContract client = myChannelFactory.CreateChannel();
                client.AddRequest(wallet);
            }
        }



    }
}

