using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels.Returns
{
    internal class ReturnsPageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;

        public ReturnsPageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
    }
}
