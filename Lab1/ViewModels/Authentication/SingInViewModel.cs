using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Lab1.DBModels;
using Lab1.Managers;
using Lab1.Models;
using Lab1.Properties;
using Lab1.Tools;

namespace Lab1.ViewModels.Authentication
{
    internal class SignInViewModel : INotifyPropertyChanged
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
        #region Commands

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseExecute));
            }
        }

        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(SignInExecute, SignInCanExecute));
            }
        }

        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(SignUpExecute));
            }
        }

        #endregion
        #endregion

        #region ConstructorAndInit

        internal SignInViewModel()
        {
        }

        #endregion

        private void SignUpExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.SingUp);
        }

        private async void SignInExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                User currentUser;
                try
                {
                    currentUser = DbManager.GetUserByLogin(Login);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,
                        ex.Message));
                    Logger.Log(ex);
                    return false;
                }
                if (currentUser == null)
                {
                    MessageBox.Show(string.Format(Resources.SignIn_UserDoesntExist, Login));
                    Logger.Log(string.Format(Resources.SignIn_UserDoesntExist, Login));
                    return false;
                }
                try
                {
                    if (!currentUser.CheckPassword(Password))
                    {
                        MessageBox.Show(Resources.SignIn_WrongPassword);
                        Logger.Log(Resources.SignIn_WrongPassword);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Resources.SignIn_FailedToValidatePassword, Environment.NewLine,
                        ex.Message));
                    Logger.Log(ex);
                    return false;
                }
                StationManager.CurrentUser = currentUser;
                SerializationManager.Serialize(StationManager.CurrentUser, FileFolderHelper.LastUserFilePath);
                return true;
            });
            LoaderManager.Instance.HideLoader();
            Login = "";
            Password = "";
            if (result)
            {
                NavigationManager.Instance.Navigate(ModesEnum.Main);
            }
        }

        private bool SignInCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);
        }

        private void CloseExecute(object obj)
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