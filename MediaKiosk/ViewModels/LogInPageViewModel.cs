using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaKiosk.ViewModels
{
    internal class LogInPageViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindowViewModel;
        public RelayCommand loginCmd => new RelayCommand(execute => LogIn(), canExecute => CanLogIn());
        public RelayCommand registerCmd => new RelayCommand(execute => Register(), canExecute => CanRegister());

        public LogInPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        private void LogIn()
        {
            //TODO
            mainWindowViewModel.HasLoggedIn = true;
            this.mainWindowViewModel.navigateToWelcomePageCmd.Execute();
        }

        private bool CanLogIn()
        {
            //TODO
            return true;
        }

        private void Register()
        {
            //TODO
            mainWindowViewModel.HasLoggedIn = true;
        }

        private bool CanRegister()
        {
            //TODO
            return true;
        }
    }
}
