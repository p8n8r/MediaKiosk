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
    /// Interaction logic for AlbumDonationPage.xaml
    /// </summary>
    public partial class AlbumDonationPage : Page
    {
        public AlbumDonationPage()
        {
            InitializeComponent();

            this.DataContext = new AlbumDonationPageViewModel();
        }

        private void Control_GotFocus(object sender, RoutedEventArgs e)
        {
            (this.DataContext as AlbumDonationPageViewModel).controlGotFocusCmd.Execute(sender);
        }
    }
}
