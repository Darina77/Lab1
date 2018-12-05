using System;
using System.Data.Entity;
using System.Linq;
using Lab1.DBModels;

namespace Lab1.DBAdapter
{
    public static class EntityWrapper
    {
        public static bool UserExists(string login)
        {
            try
            {
                using (var context = new DirFileCounterContext())
                {
                    return context.Users.Any(u => u.Login == login);
                }
            }
            catch (Exception ex)
            {
                throw new CounterDbException("User exists exception", ex.InnerException);
            }
        }

        public static bool UserEmailExists(string email)
        {
            try
            {
                using (var context = new DirFileCounterContext())
                {
                    return context.Users.Any(u => u.Email == email);
                }
            }
            catch (Exception ex)
            {
                throw new CounterDbException("User email exists exception", ex.InnerException);
            }
        }

        public static User GetUserByLogin(string login)
        {
            try
            {
                using (var context = new DirFileCounterContext())
                {
                    return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Login == login);
                }
            }
            catch (Exception ex)
            {
                throw new CounterDbException("Get user by login exception", ex.InnerException);
            }
        }

        public static User GetUserByGuid(Guid guid)
        {
            try
            {
                using (var context = new DirFileCounterContext())
                {
                    return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Guid == guid);
                }
            }
            catch (Exception ex)
            {
                throw new CounterDbException("Get user by guid exception", ex.InnerException);
            }
        }

        public static void AddUser(User user)
        {
            try
            {
                using (var context = new DirFileCounterContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new CounterDbException("Add user exception", ex.InnerException);
            }
        }

        public static void AddRequest(Request request)
        {
            try
            {
                using (var context = new DirFileCounterContext())
                {
                    request.DeleteDatabaseValues();
                    context.Requests.Add(request);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new CounterDbException("Add request exception", ex.InnerException);
            }
        }
    }

    public class CounterDbException : Exception
    {
        public CounterDbException() {}

        public CounterDbException(string message) : base(message) {}

        public CounterDbException(string message, Exception inner) : base(message, inner) { }

    }
}
