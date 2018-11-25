using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Lab1.Managers;
using Lab1.Models;
using Lab1.Properties;
using Lab1.Tools;

namespace Lab1.ViewModels.App
{
    internal class MainViewViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _volumePath;
        private int _filesCount;
        private int _folderCount;
        private double _volumeRes;
        private string _volumeResStr;
        private Request.Extension _extension;

        #region Commands
        private ICommand _openFolderCommand;
        private ICommand _openHistory;
        private ICommand _logOut;
        #endregion
        #endregion

        #region Properties
        public string VolumePath
        {
            get => _volumePath;
            private set
            {
                _volumePath = value;
                OnPropertyChanged();
            }
        }

        public int FilesCount
        {
            get => _filesCount;
            private set
            {
                _filesCount = value;
                OnPropertyChanged();
            }
        }

        public int FoldersCount
        {
            get => _folderCount;
            private set
            {
                _folderCount = value; 
                OnPropertyChanged();
            }
        }

        public string VolumeResString
        {
            get => _volumeResStr;
            private set
            {
                _volumeResStr = value;
                OnPropertyChanged();
            }
        }


        public double VolumeRes
        {
            get => _volumeRes;
            private set
            {
                _volumeRes = value;
                OnPropertyChanged();
            }
        }

        public Request.Extension CurrentExtension
        {
            get => _extension;
            private set
            {
                _extension = value;
                OnPropertyChanged();
            }
        }
        #region Commands

        public ICommand OpenFolderCommand => _openFolderCommand ?? (_openFolderCommand = new RelayCommand<object>(OpenFolderExecute));

        public ICommand OpenHistoryCommand => _openHistory ?? (_openHistory = new RelayCommand<object>(OpenHistoryExecute));
        public ICommand LogOutCommand => _logOut ?? (_logOut = new RelayCommand<object>(LogOut));

        #endregion
        #endregion

        #region Constructor
        internal MainViewViewModel()
        {
        }
        #endregion

        private async void OpenFolderExecute(object obj)
        {
            FilesCount = 0;
            FoldersCount = 0;
            VolumeRes = 0;
            CurrentExtension = Request.Extension.B;

            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            VolumePath = folderBrowserDialog.SelectedPath;

            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    Logger.Log("Запит до " + _volumePath);
                    Count countObj = new Count(_volumePath);
                    while (!countObj.isEnd()){}

                    _volumeRes = countObj.VolumeRes;
                    FoldersCount = countObj.FoldersCount;
                    FilesCount = countObj.FilesCount;
                    CurrentExtension = countObj.CurrentExtension;
                    VolumeResString = $"{_volumeRes:0.00}";
                }
                catch (Exception e)
                {
                    Logger.Log(Resources.Read_foulders_error + " in file " + _volumePath);
                    Logger.Log(e);
                    MessageBox.Show(string.Format(Resources.Read_foulders_error));
                    return false;
                }

                try
                {
                    var req = new Request(_volumePath, _filesCount, _folderCount, _volumeRes, _extension, StationManager.CurrentUser);
                    DbManager.AddRequest(req);
                }
                catch (Exception e)
                {
                    Logger.Log(Resources.Request_FaildToCeate);
                    Logger.Log(e);
                    MessageBox.Show(string.Format(Resources.Request_FaildToCeate));
                    return false;
                }

                return true;
            });
            LoaderManager.Instance.HideLoader();
           
            if (result)
            {
                Logger.Log(Resources.Request_Suссess);
                MessageBox.Show(string.Format(Resources.Request_Suссess, Environment.NewLine));
            }
        }

        private static void OpenHistoryExecute(object obj)
        {
            Logger.Log("Open History");
            NavigationManager.Instance.Navigate(ModesEnum.History);
        }

        private void LogOut(object obj)
        {
            Logger.Log("Log out");
            FileInfo file =new FileInfo(FileFolderHelper.LastUserFilePath);
            file.Delete();
            ClearProperties();
            NavigationManager.Instance.Navigate(ModesEnum.SignIn);
        }

        private void ClearProperties()
        {
            VolumePath = "";
            FoldersCount = 0;
            FilesCount = 0;
            VolumeResString = "";
            VolumeRes = 0;
            CurrentExtension = Request.Extension.B;
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
