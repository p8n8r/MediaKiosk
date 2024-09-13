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
    internal class WelcomePageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;

        public WelcomePageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
    }
}
