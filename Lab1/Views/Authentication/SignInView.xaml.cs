using Lab1.ViewModels.Authentication;

namespace Lab1.Views.Authentication
{
    internal partial class SignInView
    {
        #region Constructor
        public SignInView()
        {
            InitializeComponent();
            var signInViewModel = new SingInViewModel();
            DataContext = signInViewModel;
        }
        #endregion      
    }
}
