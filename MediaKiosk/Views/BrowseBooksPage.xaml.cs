﻿using MediaKiosk.ViewModels;
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

namespace MediaKiosk.Views
{
    /// <summary>
    /// Interaction logic for BooksTable.xaml
    /// </summary>
    public partial class BrowseBooksPage : Page
    {
        public BrowseBooksPage(MainWindow mainWindow)
        {
            InitializeComponent();

            this.DataContext = new BrowseBooksPageViewModel(mainWindow);
        }
    }
}