using Lab1.Managers;
using Lab1.Tools;
using Lab1.ViewModels;
using System.Windows.Controls;

namespace Lab1
{
    public partial class MainWindow : IContentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var navigationModel = new NavigationModel(this);
            NavigationManager.Instance.Initialize(navigationModel);
            navigationModel.Navigate(ModesEnum.SignIn);
        }

        public ContentControl ContentControl => _contentControl;
    }
}
