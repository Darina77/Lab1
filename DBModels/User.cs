using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;
using Lab1.Tools;


namespace Lab1.DBModels { 
    [Serializable]
    [DataContract(IsReference = true)]
    public class User
    {

        #region Const
        private const string Key = "sr4l8EwMgPqPhRTK";
        #endregion

        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _firstName;
        [DataMember]
        private string _lastName;
        [DataMember]
        private string _email;
        [DataMember]
        private string _login;
        [DataMember]
        private string _password;
        [DataMember]
        private DateTime _lastLoginDate;
        [DataMember]
        private List<Request> _requests;
        #endregion

        #region Properties
        [Key]
        public Guid Guid
        {
            get => _guid;
            private set => _guid = value;
        }

        private string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        private string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Login
        {
            get => _login;
            private set => _login = value;
        }

        private string Password
        {
            get => _password;
            set => _password = value;
        }

        private DateTime LastLoginDate
        {
            get => _lastLoginDate;
            set => _lastLoginDate = value;
        }

        public List<Request> Requests
        {
            get => _requests;
            set => _requests = value;
        }

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
            Requests = new List<Request>();
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

        #region EntityConfiguration

        public class UserEntityConfiguration : EntityTypeConfiguration<User>
        {
            public UserEntityConfiguration()
            {
                ToTable("Users");
                HasKey(s => s.Guid);

                Property(p => p.Guid)
                    .HasColumnName("Guid")
                    .IsRequired();
                Property(p => p.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired();
                Property(p => p.LastName)
                    .HasColumnName("LastName")
                    .IsRequired();
                Property(p => p.Email)
                    .HasColumnName("Email")
                    .IsOptional();
                Property(p => p.Login)
                    .HasColumnName("Login")
                    .IsRequired();
                Property(p => p.Password)
                    .HasColumnName("Password")
                    .IsRequired();
                Property(p => p.LastLoginDate)
                    .HasColumnName("LastLoginDate")
                    .IsRequired();

                HasMany(s => s.Requests)
                    .WithRequired(r => r.User)
                    .HasForeignKey(r => r.UserId)
                    .WillCascadeOnDelete(true);
            }
        }
        #endregion
    }
}

