using BookLibrary.UI.HelperClasses;
using BookLibrary.UI.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for DeleteAccountPage.xaml
    /// </summary>
    public partial class DeleteAccountPage : BookLibraryPage
    {
        private Page _previousPage;

        public DeleteAccountPage(Page previousPage)
        {
            InitializeComponent();
            SetupPage();
            _previousPage = previousPage;

            btnDeleteAccount.Click += btnDeleteAccount_Click;
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_previousPage);
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (DataStore.Account.DeleteAccount(AppUser.GetInstance().AccountId, pbPassword.Password))
            {
                WindowsNavigation.MainWindow?.Hide();
                ResetSession();
                ShowLoginWindow();
            }
            else
            {
                lblMasage.Content = "Can't delete account with this password";
            }
        }

        private void LoginWindow_Closed(object sender, EventArgs e)
        {
            NavigationService?.Navigate(new BookLibraryMainPage());
        }

        private void SetupPage()
        {
            btnBackward.Background = PagesPropertiesProvider.BackwardImage;
            btnBackward.Click += btnBackward_Click;
        }

        private void ShowLoginWindow()
        {
            var loginWindow = new LoginWindow();
            loginWindow.Closed += LoginWindow_Closed;
            loginWindow.Show();
        }

        private void ResetSession()
        {
            AppUser.SetInstance(string.Empty, string.Empty, 0);
            Properties.Settings.Default["Login"] = string.Empty;
            Properties.Settings.Default["Password"] = string.Empty;
            Properties.Settings.Default["AccountId"] = 0;
            Properties.Settings.Default.Save();
        }



    }
}
