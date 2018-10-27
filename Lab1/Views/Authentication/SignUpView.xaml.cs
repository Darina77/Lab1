using Lab1.ViewModels.Authentication;

namespace Lab1.Views.Authentication
{
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
