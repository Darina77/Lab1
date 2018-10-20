using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
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
        private long _volumeRes;

        #region Commands
        private ICommand _openFolderCommand;
        private ICommand _openHistory;
        #endregion
        #endregion

        #region Properties
        public string VolumePath
        {
            get { return _volumePath; }
            set
            {
                _volumePath = value;
                OnPropertyChanged();
            }
        }

        public int FilesCount
        {
            get { return _filesCount; }
            set
            {
                _filesCount = value;
                OnPropertyChanged();
            }
        }

        public int FoldersCount
        {
            get { return _folderCount; }
            set
            {
                _folderCount = value; 
                OnPropertyChanged();
            }
        }

        public long VolumeRes
        {
            get { return _volumeRes; }
            set
            {
                _volumeRes = value;
                OnPropertyChanged();
            }
        }
        #region Commands

        public ICommand OpenFolderCommand
        {
            get
            {
                return _openFolderCommand ?? (_openFolderCommand = new RelayCommand<object>(OpenFolderExecute));
            }
        }

        public ICommand OpenHistoryCommand
        {
            get
            {
                return _openHistory ?? (_openHistory = new RelayCommand<object>(OpenHistoryExecute));
            }
        }

        #endregion
        #endregion

        #region ConstructorAndInit
        internal MainViewViewModel()
        {
        }
        #endregion

        private void OpenFolderExecute(object obj)
        {
            FilesCount = 0;
            FoldersCount = 0;
            VolumeRes = 0;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            VolumePath = folderBrowserDialog.SelectedPath;

            CountInfo(VolumePath);
        }

        private void CountInfo(string path)
        {
            String[] files = Directory.GetFiles(path);
            String[] directories = Directory.GetDirectories(path);
            FilesCount += files.Length;
            FoldersCount += directories.Length;
            long currentVolume = 0;

            Parallel.For(0, files.Length,
                index => {
                    FileInfo fi = new FileInfo(files[index]);
                    long size = fi.Length;
                    Interlocked.Add(ref currentVolume, size);
                });

            Parallel.For(0, directories.Length,
                index => {
                    CountInfo(directories[index]);
                });

            VolumeRes += currentVolume;
        }

        private void OpenHistoryExecute(object obj)
        {
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
