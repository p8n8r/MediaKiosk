using MediaKiosk.Models;
using MediaKiosk.ViewModels;
using MediaKiosk.ViewModels.Browse;
using MediaKiosk.ViewModels.Donate;
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
        public MainWindowViewModel mainWindowViewModel;
        public LogInPage loginPage;
        public WelcomePage welcomePage;
        public BrowsePage browsePage;
        public ReturnsPage returnsPage;
        public DonatePage donatePage;

        public MainWindow()
        {
            InitializeComponent();

            //Set data context
            this.mainWindowViewModel = new MainWindowViewModel(this);
            this.DataContext = mainWindowViewModel;

            //Construct pages and viewmodels
            this.loginPage = new LogInPage(mainWindowViewModel);
            this.welcomePage = new WelcomePage(mainWindowViewModel);
            this.browsePage = new BrowsePage(mainWindowViewModel); 
            this.returnsPage = new ReturnsPage(mainWindowViewModel);
            this.donatePage = new DonatePage(mainWindowViewModel); 

            //Set initial navigation pages
            this.mainFrame.Navigate(this.loginPage); //Cannot set in xaml with param
            BrowsePageViewModel browsePageViewModel = this.browsePage.DataContext as BrowsePageViewModel;
            this.browsePage.mediaTableFrame.Navigate(browsePageViewModel.browseBooksPage);
            DonatePageViewModel donatePageViewModel=this.donatePage.DataContext as DonatePageViewModel;
            this.donatePage.mediaTableFrame.Navigate(donatePageViewModel.bookDonationPage);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.mainWindowViewModel.onCloseCmd.Execute();
        }
    }
}
