using Lab1.Tools;
using Lab1.Views.App;
using Lab1.Views.Authentication;
using System;


namespace Lab1.ViewModels
{
    internal enum ModesEnum
    {
        SignIn,
        SingUp,
        Main,
        History
    }

    internal class NavigationModel
    {
        private readonly IContentWindow _contentWindow;
        private SignInView _signInView;
        private SignUpView _signUpView;
        private MainApp _mainView;
        private History _historyView;

        internal NavigationModel(IContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
        }

        internal void Navigate(ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.SignIn:
                    _contentWindow.ContentControl.Content = _signInView ?? (_signInView = new SignInView());
                    break;
                case ModesEnum.SingUp:
                    _contentWindow.ContentControl.Content = _signUpView ?? (_signUpView = new SignUpView());
                    break;
                case ModesEnum.Main:
                    _contentWindow.ContentControl.Content = _mainView ?? (_mainView = new MainApp());
                    break;
                case ModesEnum.History:
                    _contentWindow.ContentControl.Content = _historyView ?? (_historyView = new History() );
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

    }
}
