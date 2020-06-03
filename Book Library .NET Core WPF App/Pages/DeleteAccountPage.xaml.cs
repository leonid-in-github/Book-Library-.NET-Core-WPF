using Book_Library_.NET_Core_WPF_App.Windows;
using Book_Library_EF_Core_Proxy_Class_Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Book_Library_.NET_Core_WPF_App.Pages
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
            TryCatchMessageTask(() =>
            {
                NavigationService.Navigate(_previousPage);
            });
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                if (DbBookLibraryRepository.Account.DeleteAccount(AppUser.GetInstance().AccountId, pbPassword.Password))
                {
                    App.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Hide();
                    ResetSession();
                    ShowLoginWindow();
                }
                else
                {
                    lblMasage.Content = "Can't delete account with this password";
                }
            });
        }

        private void LoginWindow_Closed(object sender, EventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                NavigationService.Navigate(new BookLibraryMainPage());
            });
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
