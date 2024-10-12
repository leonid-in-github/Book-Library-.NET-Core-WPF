using BookLibrary.Storage.Repositories;
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
    public partial class DeleteAccountPage : Page
    {
        private readonly IAccountRepository accountRepository = new AccountRepository();

        public DeleteAccountPage()
        {
            InitializeComponent();
            SetupPage();

            BtnDeleteAccount.Click += BtnDeleteAccount_Click;
        }

        private void BtnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void BtnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (await accountRepository.DeleteAccount(AppUser.GetInstance().AccountId, pbPassword.Password))
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
            BtnBackward.Background = PagesPropertiesProvider.BackwardImage;
            BtnBackward.Click += BtnBackward_Click;
        }

        private void ShowLoginWindow()
        {
            var loginWindow = new LoginWindow();
            loginWindow.Closed += LoginWindow_Closed;
            loginWindow.Show();
        }

        private void ResetSession()
        {
            AppUser.SetInstance(string.Empty, string.Empty, new Guid());
            Properties.Settings.Default["Login"] = string.Empty;
            Properties.Settings.Default["Password"] = string.Empty;
            Properties.Settings.Default["AccountId"] = new Guid();
            Properties.Settings.Default.Save();
        }



    }
}
