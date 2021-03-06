﻿using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Lab1.DBModels;
using Lab1.Managers;
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
                    Logger.Log("Запит до " + VolumePath);
                    CountInfo(VolumePath);                          
                    VolumeResString = $"{_volumeRes:0.00}";
                }
                catch (Exception e)
                {
                    Logger.Log(Resources.Read_foulders_error + " in file " + VolumePath);
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

        private void CountInfo(string path)
        {
            var files = Directory.GetFiles(path);
            var directories = Directory.GetDirectories(path);
            FilesCount += files.Length;
            FoldersCount += directories.Length;
            double currentVolume = 0;

            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                double size = fi.Length;
                currentVolume += size;
            }

            Parallel.For(0, directories.Length,
                index => {
                    try
                    {
                        CountInfo(directories[index]);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Logger.Log(Resources.Dont_have_access + " to " + directories[index]);
                        Logger.Log(e);
                        MessageBox.Show(string.Format(Resources.Dont_have_access, directories[index]));
                    }
                });

            currentVolume = ChangeExtension(currentVolume);
            VolumeRes += currentVolume;
            SetExtension();
        }

        private void SetExtension()
        {
            if (!(VolumeRes > 1024.0) || CurrentExtension == Request.Extension.Gb) return;
            VolumeRes /= 1024.0;
            CurrentExtension++;
        }

        private double ChangeExtension(double currentVolume)
        {
            switch (CurrentExtension)
            {
                case Request.Extension.Kb:
                    currentVolume /= 1024.0;
                    break;
                case Request.Extension.Mb:
                    currentVolume /= (1024.0 * 1024.0);
                    break;
                case Request.Extension.Gb:
                    currentVolume /= (1024.0 * 1024.0 * 1024.0);
                    break;
                case Request.Extension.B:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return currentVolume;
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
