using System.Threading;
using Lab1.ViewModels;

namespace Lab1.Managers
{
    internal class NavigationManager
    {
        #region static

        private static readonly object Lock = new object();

        private static NavigationManager _instance;

        public static NavigationManager Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (Lock)
                {
                    return _instance = new NavigationManager();
                }
            }
        }
        #endregion

        private NavigationModel _navigationManager;

        internal void Initialize(NavigationModel navigationModel)
        {
            _navigationManager = navigationModel;
        }

        internal void Navigate(ModesEnum mode)
        {
            _navigationManager?.Navigate(mode);
        }
    }
}
