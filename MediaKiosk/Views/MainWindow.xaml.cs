﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal LogInPage loginPage;
        internal PurposePage purposePage;
        internal BrowsePage browsePage;
        internal DonatePage donatePage;
        internal BrowseBooksPage browseBooksPage;

        public MainWindow()
        {
            InitializeComponent();

            this.loginPage = new LogInPage(this);
            this.purposePage = new PurposePage(this);
            this.browsePage = new BrowsePage(this);
            this.donatePage = new DonatePage(this);

            this.mainFrame.Navigate(this.loginPage);

            this.browseBooksPage = new BrowseBooksPage(this);
            this.browsePage.mediaTableFrame.Navigate(this.browseBooksPage);
        }
    }
}
