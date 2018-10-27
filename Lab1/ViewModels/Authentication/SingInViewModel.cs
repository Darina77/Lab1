using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Lab1.Managers;
using Lab1.Models;
using Lab1.Properties;
using Lab1.Tools;

namespace Lab1.ViewModels.Authentication
{
    internal class SingInViewModel
    {
        #region Fields
        private string _password;
        private string _login;

        #region Commands
        private ICommand _closeCommand;
        private ICommand _signInCommand;
        private ICommand _signUpCommand;
        #endregion
        #endregion

        #region Properties
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        #region Commands

        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseExecute));

        public ICommand SignInCommand => _signInCommand ?? (_signInCommand = new RelayCommand<object>(SignInExecute, SignInCanExecute));

        public ICommand SignUpCommand => _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(SignUpExecute));

        #endregion
        #endregion

        #region ConstructorAndInit
        internal SingInViewModel()
        {
        }
        #endregion

        private void SignUpExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.SingUp);
        }

        private void SignInExecute(object obj)
        {
            User currentUser;
            DbManager.InitUser();
            try
            {
                currentUser = DbManager.GetUserByLogin(_login);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,
                    ex.Message));
                return;
            }
            if (currentUser == null)
            {
                MessageBox.Show(string.Format(Resources.SignIn_UserDoesntExist, _login));
                return;
            }
            try
            {
                if (!currentUser.CheckPassword(_password))
                {
                    MessageBox.Show(Resources.SignIn_WrongPassword);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resources.SignIn_FailedToValidatePassword, Environment.NewLine,
                    ex.Message));
                return;
            }
            StationManager.CurrentUser = currentUser;
            NavigationManager.Instance.Navigate(ModesEnum.Main);
        }

        private bool SignInCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);
        }

        private static void CloseExecute(object obj)
        {
            StationManager.CloseApp();
        }

        #region EventsAndHandlers
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion
    }
}
