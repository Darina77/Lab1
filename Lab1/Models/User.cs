using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Tools;

namespace Lab1.Models
{
    public class User
    {
        #region Const
        private const string key = "sr4l8EwMgPqPhRTK";
        #endregion

        #region Fields
        private Guid _guid;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _login;
        private string _password;
        private DateTime _lastLoginDate;
        private List<Request> _requests;
        #endregion

        #region Properties
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            private set
            {
                _guid = value;
            }
        }
        private string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }
        private string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }
        private string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public string Login
        {
            get
            {
                return _login;
            }
            private set
            {
                _login = value;
            }
        }
        private string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        private DateTime LastLoginDate
        {
            get
            {
                return _lastLoginDate;
            }
            set
            {
                _lastLoginDate = value;
            }
        }

        public List<Request> Requests
        {
            get
            {
                return _requests;
            }
            private set
            {
                _requests = value;
            }
        }
        #endregion

        #region Constructor

        public User(string firstName, string lastName, string email, string login, string password) : this()
        {
            _guid = Guid.NewGuid();
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _login = login;
            _lastLoginDate = DateTime.Now;

            SetPassword(password);
        }

        private User()
        {
            _requests = new List<Request>();
        }

        #endregion

        private void SetPassword(string password)
        {
            _password = Encrypting.Encrypt(password, key);
        }
        public bool CheckPassword(string password)
        {
            try
            {
                string res = Encrypting.Decrypt(_password, key);
                string res2 = password;
                return res == res2;
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
