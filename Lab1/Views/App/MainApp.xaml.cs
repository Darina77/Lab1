using Lab1.ViewModels.App;

namespace Lab1.Views.App
{
    /// <summary>
    /// Логика взаимодействия для MainApp.xaml
    /// </summary>
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
