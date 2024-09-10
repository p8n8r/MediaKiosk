using MediaKiosk.ViewModels;
using MediaKiosk.ViewModels.Browse;
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

namespace MediaKiosk.Views.Browse
{
    /// <summary>
    /// Interaction logic for BrowseAlbumsPage.xaml
    /// </summary>
    public partial class BrowseAlbumsPage : Page
    {
        internal BrowseAlbumsPage(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            this.DataContext = new BrowseAlbumsPageViewModel(mainWindowViewModel);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as BrowseAlbumsPageViewModel).reloadCmd.Execute();
        }
    }
}
