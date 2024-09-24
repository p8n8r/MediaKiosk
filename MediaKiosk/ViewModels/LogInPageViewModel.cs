using MediaKiosk.DisplayDialogs;
using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels
{
    public class LogInPageViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindowViewModel;
        private List<User> Users;
        private string username;
        private readonly UserComparer userComparer = new UserComparer();
        private readonly IDisplayDialog displayDialog;
        public RelayCommand loginCmd => new RelayCommand(pw => LogIn(pw));
        public RelayCommand registerCmd => new RelayCommand(pw => Register(pw));

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }

        public LogInPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.displayDialog = this.mainWindowViewModel.displayDialog;

            this.Users = mainWindowViewModel.Users;
        }

        private void LogIn(object passwordBox)
        {
            if (HasEnteredValidUserData(passwordBox))
            {
                User user = new User(this.Username, (passwordBox as PasswordBox).Password);

                if (this.Users.Contains(user, this.userComparer))
                {
                    User userCurrent = this.Users.Single(user, this.userComparer);
                    this.mainWindowViewModel.CurrentUser = userCurrent;
                    this.mainWindowViewModel.HasLoggedIn = true;
                    this.mainWindowViewModel.navigateToWelcomePageCmd.Execute();
                }
                else
                {
                    displayDialog.ShowErrorMessageBox($"The username/password was not found. "
                        + "Please try again.");
                }
            }
            else
            {
                displayDialog.ShowErrorMessageBox($"Invalid characters found. "
                    + "Please use only letters and numbers for your username and password.");
            }
        }

        private void Register(object passwordBox)
        {
            if (HasEnteredValidUserData(passwordBox))
            { 
                User user = new User(this.Username, (passwordBox as PasswordBox).Password);

                if (!this.Users.Any(user, this.userComparer))
                {
                    this.Users.Add(user);
                    this.mainWindowViewModel.HasLoggedIn = true;
                    this.mainWindowViewModel.navigateToWelcomePageCmd.Execute();
                }
                else
                {
                    displayDialog.ShowErrorMessageBox($"The username \"{user.Username}\" already exists. "
                        + "Please choose another username.");
                }
            }
            else
            {
                displayDialog.ShowErrorMessageBox($"Invalid characters found. "
                    + "Please use only letters and numbers for your username and password.");
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
