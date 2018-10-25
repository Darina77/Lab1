using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Lab1.Managers;
using Lab1.Models;
using Lab1.Properties;
using Lab1.Tools;

namespace Lab1.ViewModels.App
{
    internal class HistoryViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<Request> _requests;

        #region Commands
        private ICommand _openMainViewCommand;
        private ICommand _cancelCommand;
        #endregion
        #endregion

        #region Properties
        public ObservableCollection<Request> Requests
        {
            get { return _requests; }
        }

        #endregion

        #region Commands

        public ICommand MainViewCommand
        {
            get
            {
                return _openMainViewCommand ?? (_openMainViewCommand = new RelayCommand<object>(OpenMainViewExecute));
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(CencelCommandExecute));
            }
        }

        #endregion

        #region Constructor
        public HistoryViewModel()
        {
            FillRequests();
        }
        #endregion

        private void FillRequests()
        {
            _requests = new ObservableCollection<Request>();
            foreach (var request in StationManager.CurrentUser.Requests)
            {
                _requests.Add(request);
            }
        }

        private void OpenMainViewExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.Main);
        }

        private void CencelCommandExecute(object obj)
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
