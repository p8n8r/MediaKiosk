using MediaKiosk.ViewModels;
using MediaKiosk.Views.Browse;
using MediaKiosk.Views.Donate;
using MediaKiosk.Views.Returns;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel mainWindowViewModel;
        internal LogInPage loginPage;
        internal WelcomePage purposePage;
        internal BrowsePage browsePage;
        internal ReturnsPage returnsPage;
        internal DonatePage donatePage;
        internal BrowseBooksPage browseBooksPage;
        internal BrowseAlbumsPage browseAlbumsPage;
        internal BrowseMoviesPage browseMoviesPage;

        public MainWindow()
        {
            InitializeComponent();

            //Set data context
            this.mainWindowViewModel = new MainWindowViewModel(this);
            this.DataContext = mainWindowViewModel;

            //Construct pages and viewmodels
            this.browseBooksPage = new BrowseBooksPage(mainWindowViewModel);
            this.browseAlbumsPage = new BrowseAlbumsPage(mainWindowViewModel);
            this.browseMoviesPage = new BrowseMoviesPage(mainWindowViewModel);
            this.loginPage = new LogInPage(mainWindowViewModel);
            this.purposePage = new WelcomePage(this);
            this.browsePage = new BrowsePage(this); //Depends on subpages
            this.returnsPage = new ReturnsPage(this);
            this.donatePage = new DonatePage(); //TODO: give mainwindowviewmodel

            //Set initial navigation pages
            this.mainFrame.Navigate(this.loginPage);
            this.browsePage.mediaTableFrame.Navigate(this.browseBooksPage);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.mainWindowViewModel.onCloseCmd.Execute();
        }
    }
}
