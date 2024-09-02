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
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        private MainWindow mainWindow;

        public LogInPage(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO

            this.mainWindow.mainFrame.Navigate(this.mainWindow.purposePage);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO

            this.mainWindow.mainFrame.Navigate(this.mainWindow.purposePage);
        }
    }
}
