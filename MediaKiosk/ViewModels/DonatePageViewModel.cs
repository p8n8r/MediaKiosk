﻿using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels
{
    internal class DonatePageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;

        public DonatePageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
    }
}