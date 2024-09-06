using MediaKiosk.ViewModels;
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
        internal LogInPage loginPage;
        internal PurposePage purposePage;
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
            this.DataContext = new MainWindowViewModel(this);

            //Construct pages and viewmodels
            this.browseBooksPage = new BrowseBooksPage(this);
            this.browseAlbumsPage = new BrowseAlbumsPage(this);
            this.browseMoviesPage = new BrowseMoviesPage(this);
            this.loginPage = new LogInPage(this);
            this.purposePage = new PurposePage(this);
            this.browsePage = new BrowsePage(this); //Depends on subpages
            this.returnsPage = new ReturnsPage(this);
            this.donatePage = new DonatePage();

            //Set initial navigation pages
            this.mainFrame.Navigate(this.loginPage);
            this.browsePage.mediaTableFrame.Navigate(this.browseBooksPage);
        }
    }
}
