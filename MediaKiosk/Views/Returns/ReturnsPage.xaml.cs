﻿using MediaKiosk.ViewModels;
using MediaKiosk.ViewModels.Returns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaKiosk.Views.Returns
{
    /// <summary>
    /// Interaction logic for ReturnsPage.xaml
    /// </summary>
    public partial class ReturnsPage : Page
    {
        public ReturnsPage(MainWindow mainWindow)
        {
            InitializeComponent();

            this.DataContext = new ReturnsPageViewModel(mainWindow);
        }
    }
}
