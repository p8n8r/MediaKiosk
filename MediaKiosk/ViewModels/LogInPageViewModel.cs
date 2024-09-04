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
        private MainWindow mainWindow;
        public RelayCommand loginCmd => new RelayCommand(execute => LogIn(), canExecute => CanLogIn());
        public RelayCommand registerCmd => new RelayCommand(execute => Register(), canExecute => CanRegister());

        public LogInPageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private void LogIn()
        {
            //TODO

            this.mainWindow.mainFrame.Navigate(this.mainWindow.purposePage);
        }

        private bool CanLogIn()
        {
            //TODO
            return true;
        }

        private void Register()
        {
            //TODO

            this.mainWindow.mainFrame.Navigate(this.mainWindow.purposePage);
        }

        private bool CanRegister()
        {
            //TODO
            return true;
        }
    }
}
