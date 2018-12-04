using System;
using System.ServiceModel;
using Lab1.DBModels;

namespace CountServiceInterface
{
    [ServiceContract]
    public interface ICountContract
    {
        [OperationContract]
        bool UserExists(string login);
        [OperationContract]
        bool UserEmailExists(string login);
        [OperationContract]
        User GetUserByLogin(string login);
        [OperationContract]
        User GetUserByGuid(Guid guid);
        [OperationContract]
        void AddUser(User user);
        [OperationContract]
        void AddRequest(Request request);
    }
}
