using System;
using System.Data.Entity;
using System.Linq;
using Lab1.Models;

namespace Lab1.Adapter
{
    public static class EntityWrapper
    {
        public static bool UserExists(string login)
        {
            using (var context = new DirFileCounterContext())
            {
                return context.Users.Any(u => u.Login == login);
            }
        }

        public static bool UserEmailExists(string email)
        {
            using (var context = new DirFileCounterContext())
            {
                return context.Users.Any(u => u.Email == email);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var context = new DirFileCounterContext())
            {
                return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Login == login);
            }
        }

        public static User GetUserByGuid(Guid guid)
        {
            using (var context = new DirFileCounterContext())
            {
                return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Guid == guid);
            }
        }

        public static void AddUser(User user)
        {
            using (var context = new DirFileCounterContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void AddRequest(Request request)
        {
            using (var context = new DirFileCounterContext())
            {
                request.DeleteDatabaseValues();
                context.Requests.Add(request);
                context.SaveChanges();
            }
        }

    }
}
