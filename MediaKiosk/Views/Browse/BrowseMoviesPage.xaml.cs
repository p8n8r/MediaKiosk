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
    /// Interaction logic for BrowseMoviesPage.xaml
    /// </summary>
    public partial class BrowseMoviesPage : Page
    {
        public BrowseMoviesPage(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            this.DataContext = new BrowseMoviesPageViewModel(mainWindowViewModel);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as BrowseMoviesPageViewModel).reloadCmd.Execute();
        }
    }
}
