using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaKiosk.ViewModels
{
    internal class MainWindowViewModel
    {
        private MainWindow mainWindow;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void ShowErrorMessageBox(string message)
        {
            MessageBox.Show(mainWindow, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
