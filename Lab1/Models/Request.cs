using System;

namespace Lab1.Models
{
    public class Request
    {
        public enum Extension
        {
            b, kb, mb, gb
        }

        #region Fields
        private Guid _guid;
        private string _folderPath;
        private int _filesCount;
        private int _foldersCount;
        private long _fullVolume;
        private Extension _extension;
        #endregion

        #region Properties
        public Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }
        public string FolderPath
        {
            get { return _folderPath; }
            private set { _folderPath = value; }
        }
        public int FilesCount
        {
            get { return _filesCount; }
            private set { _filesCount = value; }
        }
        public int FolderCount
        {
            get { return _foldersCount; }
            private set { _foldersCount = value; }
        }
        public long FullVolume
        {
            get { return _fullVolume; }
            private set { _fullVolume = value; }
        }
        public Extension CurrentExtension
        {
            get { return _extension; }
            set { _extension = value; }
        }
        #endregion

        #region Constructor
        public Request(string folderPath, int filesCount, int foldersCount, long fullVolume, Extension extension) : this()
        {
            _guid = Guid.NewGuid();
            _folderPath = folderPath;
            _filesCount = filesCount;
            _foldersCount = foldersCount;
            _fullVolume = fullVolume;
            _extension = extension;
        }
        private Request()
        {
        }
        #endregion
        public override string ToString()
        {
            return _folderPath + " Файлів: " + _filesCount + " Папок: " + _foldersCount + " Заагальний об'єм: " + _fullVolume;
        }
    }
}
