using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lab1.Models
{
    [Serializable]
    public class Request
    {
        public enum Extension
        {
            B, Kb, Mb, Gb
        }

        #region Field
        private Guid _guid;
        private string _folderPath;
        private int _filesCount;
        private int _foldersCount;
        private double _fullVolume;
        private Extension _extension;
        private DateTime _requestDate;
        private Guid _userGuid;
        private User _user;
        #endregion

        #region Properties
        [Key]
        public Guid Guid
        {
            get => _guid;
            private set => _guid = value;
        }

        public string FolderPath
        {
            get => _folderPath;
            private set => _folderPath = value;
        }

        public int FilesCount
        {
            get => _filesCount;
            private set => _filesCount = value;
        }

        public int FolderCount
        {
            get => _foldersCount;
            private set => _foldersCount = value;
        }

        public double FullVolume
        {
            get => _fullVolume;
            private set => _fullVolume = value;
        }

        public Extension CurrentExtension
        {
            get => _extension;
            set => _extension = value;
        }

        public DateTime RequestDate
        {
            get => _requestDate;
            set => _requestDate = value;
        }

        
        public Guid UserId
        {
            get => _userGuid;
            set => _userGuid = value;
        }
        [ForeignKey("UserId")]
        public User User
        {
            get => _user;
            private set => _user = value;
        }
        #endregion

        #region Constructor
        public Request(string folderPath, int filesCount, int foldersCount, double fullVolume, Extension extension, User user) : this()
        {
            Guid = Guid.NewGuid();
            FolderPath = folderPath;
            FilesCount = filesCount;
            FolderCount = foldersCount;
            FullVolume = fullVolume;
            CurrentExtension = extension;
            RequestDate = DateTime.Now;
            User = user;
            UserId = user.Guid;
            user.Requests.Add(this);
        }

        private Request()
        {
        }
        #endregion

        #region EntityFrameworkConfiguration
        public class RequestEntityConfiguration : EntityTypeConfiguration<Request>
        {
            public RequestEntityConfiguration()
            {
                ToTable("Requests");
                HasKey(s => s.Guid);

                Property(p => p.Guid)
                    .HasColumnName("Guid")
                    .IsRequired();
                Property(p => p.FolderPath)
                    .HasColumnName("FolderPath")
                    .IsRequired();
                Property(s => s.FilesCount)
                    .HasColumnName("FilesCount")
                    .IsRequired();
                Property(s => s.FolderCount)
                    .HasColumnName("FolderCount")
                    .IsRequired();
                Property(s => s.FolderCount)
                    .HasColumnName("FullVolume")
                    .IsRequired();
                Property(s => s.FolderCount)
                    .HasColumnName("CurrentExtension")
                    .IsRequired();
                Property(s => s.FolderCount)
                    .HasColumnName("RequestDate")
                    .IsRequired();
            }
        }
        #endregion

        public void DeleteDatabaseValues()
        {
            _user = null;
        }
    }
}
