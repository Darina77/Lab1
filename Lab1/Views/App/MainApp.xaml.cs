using Lab1.ViewModels.App;

namespace Lab1.Views.App
{
    internal partial class MainApp
    {
        #region Constructor
        public MainApp()
        {
            InitializeComponent();
            var mainViewView = new MainViewViewModel();
            DataContext = mainViewView;
        }
        #endregion
    }
}
