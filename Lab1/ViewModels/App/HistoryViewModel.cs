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

        #region Commands
        private ICommand _openMainViewCommand;
        private ICommand _cancelCommand;
        #endregion
        #endregion

        #region Properties
        public ObservableCollection<Request> Requests { get; private set; }

        #endregion

        #region Commands

        public ICommand MainViewCommand => _openMainViewCommand ?? (_openMainViewCommand = new RelayCommand<object>(OpenMainViewExecute));

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(CancelCommandExecute));

        #endregion

        #region Constructor
        public HistoryViewModel()
        {
            FillRequests();
        }
        #endregion

        private void FillRequests()
        {
            Requests = new ObservableCollection<Request>();
            foreach (var request in StationManager.CurrentUser.Requests)
            {
                Requests.Add(request);
            }
        }

        private static void OpenMainViewExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.Main);
        }

        private static void CancelCommandExecute(object obj)
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
