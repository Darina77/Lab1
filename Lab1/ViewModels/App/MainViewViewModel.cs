using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
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
        #endregion
        #endregion

        #region Properties
        public string VolumePath
        {
            get => _volumePath;
            set
            {
                _volumePath = value;
                OnPropertyChanged();
            }
        }

        public int FilesCount
        {
            get => _filesCount;
            set
            {
                _filesCount = value;
                OnPropertyChanged();
            }
        }

        public int FoldersCount
        {
            get => _folderCount;
            set
            {
                _folderCount = value; 
                OnPropertyChanged();
            }
        }

        public string VolumeResString
        {
            get => _volumeResStr;
            set
            {
                _volumeResStr = value;
                OnPropertyChanged();
            }
        }


        public double VolumeRes
        {
            get => _volumeRes;
            set
            {
                _volumeRes = value;
                OnPropertyChanged();
            }
        }

        public Request.Extension CurrentExtension
        {
            get => _extension;
            set
            {
                _extension = value;
                OnPropertyChanged();
            }
        }
        #region Commands

        public ICommand OpenFolderCommand => _openFolderCommand ?? (_openFolderCommand = new RelayCommand<object>(OpenFolderExecute));

        public ICommand OpenHistoryCommand => _openHistory ?? (_openHistory = new RelayCommand<object>(OpenHistoryExecute));

        #endregion
        #endregion

        #region Constructor
        internal MainViewViewModel()
        {
        }
        #endregion

        private void OpenFolderExecute(object obj)
        { 
            FilesCount = 0;
            FoldersCount = 0;
            VolumeRes = 0;
            CurrentExtension = Request.Extension.B;

            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            VolumePath = folderBrowserDialog.SelectedPath;
            try
            {
                CountInfo(VolumePath);
                VolumeResString = $"{_volumeRes:0.00}";
                Console.WriteLine(VolumeResString);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Resources.Read_foulders_error));
            }
            var req = new Request(VolumePath, FilesCount, FoldersCount, VolumeRes, CurrentExtension);
            StationManager.CurrentUser.Requests.Add(req);
        }

        private void CountInfo(string path)
        {
            var files = Directory.GetFiles(path);
            var directories = Directory.GetDirectories(path);
            FilesCount += files.Length;
            FoldersCount += directories.Length;
            double currentVolume = 0;

            foreach(var file in files)
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
                    catch (System.UnauthorizedAccessException)
                    {
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
            NavigationManager.Instance.Navigate(ModesEnum.History);
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
