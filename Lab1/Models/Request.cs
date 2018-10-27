using System;

namespace Lab1.Models
{
    public class Request
    {
        public enum Extension
        {
            B, Kb, Mb, Gb
        }

        #region Properties
        public Guid Guid { get; private set; }

        public string FolderPath { get; private set; }

        public int FilesCount { get; private set; }

        public int FolderCount { get; private set; }

        public double FullVolume { get; private set; }

        public Extension CurrentExtension { get; set; }

        private DateTime RequestDate { get; set; }
        #endregion

        #region Constructor
        public Request(string folderPath, int filesCount, int foldersCount, double fullVolume, Extension extension) : this()
        {
            Guid = Guid.NewGuid();
            FolderPath = folderPath;
            FilesCount = filesCount;
            FolderCount = foldersCount;
            FullVolume = fullVolume;
            CurrentExtension = extension;
            RequestDate = new DateTime();
        }

        private Request()
        {
        }
        #endregion
    }
}
