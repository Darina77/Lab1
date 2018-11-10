using System;
using System.Collections.Generic;
using Lab1.Tools;

namespace Lab1.Models
{
    [Serializable]
    public class User
    {

        #region Const
        private const string Key = "sr4l8EwMgPqPhRTK";
        #endregion

        #region Fields

        #endregion

        #region Properties
        public Guid Guid { get; private set; }

        private string FirstName { get; set; }

        private string LastName { get; set; }

        private string Email { get; set; }

        public string Login { get; private set; }

        private string Password { get; set; }

        private DateTime LastLoginDate { get; set; }

        public List<Request> Requests { get; set; }

        #endregion

        #region Constructor

        public User(string firstName, string lastName, string email, string login, string password) : this()
        {
            Guid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            LastLoginDate = DateTime.Now;

            SetPassword(password);
        }

        private User()
        {
            Requests = new List<Request>();
        }

        #endregion

        private void SetPassword(string password)
        {
            Password = Encrypting.Encrypt(password, Key);
        }
        public bool CheckPassword(string password)
        {
            try
            {
                var res = Encrypting.Decrypt(Password, Key);
                var res2 = password;
                return res == res2;
            }
            catch (Exception)
            {
               return false;
            }
        }
        public bool CheckPassword(User userCandidate)
        {
            try
            {
                return Password == userCandidate.Password;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
