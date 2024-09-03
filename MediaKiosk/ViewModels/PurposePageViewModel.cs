using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaKiosk.ViewModels
{
    internal class PurposePageViewModel
    {
        private MainWindow mainWindow;
        public RelayCommand browseCmd => new RelayCommand(execute => Browse());
        public RelayCommand donateCmd => new RelayCommand(execute => Donate());

        public PurposePageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private void Browse()
        {
            this.mainWindow.mainFrame.Navigate(this.mainWindow.browsePage);
        }

        private void Donate()
        {
            this.mainWindow.mainFrame.Navigate(this.mainWindow.donatePage);
        }
    }
}
