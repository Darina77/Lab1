using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab1.DBModels;
using Lab1.Properties;
using Lab1.Tools;

namespace Lab1.Models
{
    class Count
    {

        public int FilesCount { get; private set;}
        public int FoldersCount { get; private set; }
        public double VolumeRes { get; private set; }
        public Request.Extension CurrentExtension { get; private set; }
        private Thread myThread;
        private bool finish;

        public Count(string VolumePath)
        {
            this.FilesCount = 0;
            this.FoldersCount = 0;
            this.VolumeRes = 0;
            this.CurrentExtension = Request.Extension.B;
            myThread = new Thread(CountInfo);
            myThread.Start(VolumePath);
            finish = true;
        }

        public bool isEnd()
        {
            return myThread.ThreadState == ThreadState.Stopped;
        }

        private void CountInfo(object path)
        {
            string spath = (string)path;
            var files = Directory.GetFiles(spath);
            var directories = Directory.GetDirectories(spath);
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
                    catch (System.UnauthorizedAccessException e)
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
    }
}
