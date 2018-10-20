using Lab1.ViewModels.Authentication;

namespace Lab1.Views.Authentication
{
    /// <summary>
    /// Логика взаимодействия для SignUpView.xaml
    /// </summary>
    internal partial class SignUpView
    {
        #region Constructor
        public SignUpView()
        {
            InitializeComponent();
            var signUpViewModel = new SignUpViewModel();
            DataContext = signUpViewModel;
        }
        #endregion
    }
}
