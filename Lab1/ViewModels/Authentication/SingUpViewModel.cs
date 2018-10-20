using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Lab1.Managers;
using Lab1.Models;
using Lab1.Properties;
using Lab1.Tools;

namespace Lab1.ViewModels.Authentication
{
    internal class SignUpViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _password;
        private string _login;
        private string _firstName;
        private string _lastName;
        private string _email;
        #region Commands
        private ICommand _closeCommand;
        private ICommand _signUpCommand;
        private ICommand _signInCommand;
        #endregion
        #endregion

        #region Properties
        #region Command

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseExecute));
            }
        }

        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(SignUpExecute, SignUpCanExecute));
            }
        }
        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(SignInExecute));
            }
        }
        #endregion

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConstructorAndInit
        internal SignUpViewModel()
        {
        }
        #endregion

        private void SignUpExecute(object obj)
        {
            try
            {
                if (!new EmailAddressAttribute().IsValid(_email))
                {
                    MessageBox.Show(String.Format(Resources.SignUp_EmailIsNotValid, _email));
                    return;
                }
                if (DBManager.UserExists(_login))
                {
                    MessageBox.Show(String.Format(Resources.SignUp_UserAlreadyExists, _login));
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Resources.SignUp_FailedToValidateData, Environment.NewLine,
                    ex.Message));
                return;
            }
            try
            {
                var user = new User(_firstName, _lastName, _email, _login, _password);
                DBManager.AddUser(user);
                StationManager.CurrentUser = user;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Resources.SignUp_FailedToCreateUser, Environment.NewLine,
                    ex.Message));
                return;
            }
            MessageBox.Show(String.Format(Resources.SignUp_UserSuccessfulyCreated, _login));
            NavigationManager.Instance.Navigate(ModesEnum.Main);
        }
        private bool SignUpCanExecute(object obj)
        {
            return !String.IsNullOrEmpty(_login) &&
                   !String.IsNullOrEmpty(_password) &&
                   !String.IsNullOrEmpty(_firstName) &&
                   !String.IsNullOrEmpty(_lastName) &&
                   !String.IsNullOrEmpty(_email);
        }
        private void SignInExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.SignIn);
        }
        private void CloseExecute(object obj)
        {
            StationManager.CloseApp();
        }

        #region EventsAndHandlers
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion
    }
}
