using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels
{
    internal class LogInPageViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindowViewModel;
        private List<User> Users;
        private string username;
        private UserComparer userComparer;
        public RelayCommand loginCmd => new RelayCommand(pw => LogIn(pw), pw => HasEnteredValidUserData(pw));
        public RelayCommand registerCmd => new RelayCommand(pw => Register(pw), pw => HasEnteredValidUserData(pw));

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }

        public LogInPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.Users = mainWindowViewModel.Users;
            this.userComparer = new UserComparer();
        }

        private void LogIn(object passwordBox)
        {
            User user = new User()
            {
                Username = Username,
                Password = (passwordBox as PasswordBox).SecurePassword
            };

            if (!this.Users.Any(u => u.Username == user.Username)
                && !this.Users.Contains(user, this.userComparer))
            {
                this.mainWindowViewModel.HasLoggedIn = true;
                this.mainWindowViewModel.navigateToWelcomePageCmd.Execute();
            }
            else
            {
                Utility.ShowErrorMessageBox($"The username/password was not found. "
                    + "Please try again.");
            }
        }

        private void Register(object passwordBox)
        {
            User user = new User()
            {
                Username = Username,
                Password = (passwordBox as PasswordBox).SecurePassword
            };

            if (!this.Users.Any(u => u.Username == user.Username))
            {
                this.Users.Add(user);
                this.mainWindowViewModel.HasLoggedIn = true;
                this.mainWindowViewModel.navigateToWelcomePageCmd.Execute();
            }
            else
            {
                Utility.ShowErrorMessageBox($"The username \"{user.Username}\" already exists. "
                    + "Please choose another username.");
            }
        }

        private bool HasEnteredValidUserData(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;

            //Ensure username/password aren't empty
            if (string.IsNullOrWhiteSpace(this.Username)
                || string.IsNullOrWhiteSpace(passwordBox.Password))
                return false;

            //Ensure username/password contain only letters and numbers
            bool hasValidChars = true;
            foreach (char c in this.Username)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    hasValidChars = false;
                    break;
                }
            }
            foreach (char c in passwordBox.Password)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    hasValidChars = false;
                    break;
                }
            }

            return hasValidChars;
        }
    }
}
