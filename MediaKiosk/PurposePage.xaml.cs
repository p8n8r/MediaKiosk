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

namespace MediaKiosk
{
    /// <summary>
    /// Interaction logic for PurposePage.xaml
    /// </summary>
    public partial class PurposePage : Page
    {
        private MainWindow mainWindow;

        public PurposePage(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.mainFrame.Navigate(this.mainWindow.browsePage);
        }

        private void DonateButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.mainFrame.Navigate(this.mainWindow.donatePage);
        }
    }
}
