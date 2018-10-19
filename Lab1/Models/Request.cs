using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class Request
    {
        #region Fields
        private Guid _guid;
        private string _foulderPath;
        private int _filesCount;
        private int _fouldersCount;
        private int _fullVolume;
        #endregion

        #region Properties
        public Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }
        public int FilesCount
        {
            get { return _filesCount; }
            set { _filesCount = value; }
        }
        public int FoulderCount
        {
            get { return _fouldersCount; }
            private set { _fouldersCount = value; }
        }
        public int FullVolume
        {
            get { return _fullVolume; }
            private set { _fullVolume = value; }
        }
        #endregion

        #region Constructor
        public Request(string foulderPath, int filesCount, int fouldersCount, int fullVolume, User user) : this()
        {
            _guid = Guid.NewGuid();
            _foulderPath = foulderPath;
            _fouldersCount = fouldersCount;
            _fullVolume = fullVolume;
            user.Requests.Add(this);
        }
        private Request()
        {
        }
        #endregion
        public override string ToString()
        {
            return _foulderPath + " Файлів: " + _filesCount + " Папок: " + _fouldersCount + " Заагальний об'єм: " + _fullVolume;
        }
    }
}
