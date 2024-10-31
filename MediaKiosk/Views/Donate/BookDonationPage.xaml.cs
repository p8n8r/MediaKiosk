using MediaKiosk.ViewModels;
using MediaKiosk.ViewModels.Browse;
using MediaKiosk.ViewModels.Donate;
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

namespace MediaKiosk.Views.Donate
{
    /// <summary>
    /// Interaction logic for BookDonationPage.xaml
    /// </summary>
    public partial class BookDonationPage : Page
    {
        public BookDonationPage()
        {
            InitializeComponent();

            this.DataContext = new BookDonationPageViewModel();
        }

        private void Control_GotFocus(object sender, RoutedEventArgs e)
        {
            (this.DataContext as BookDonationPageViewModel).controlGotFocusCmd.Execute(sender);
        }
    }
}
