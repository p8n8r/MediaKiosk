using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels
{
    internal class BrowsePageViewModel
    {
        private MainWindow mainWindow;

        public BrowsePageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
    }
}
