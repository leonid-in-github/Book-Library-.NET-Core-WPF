using Book_Library_.NET_Core_WPF_App.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Book_Library_.NET_Core_WPF_App.Pages
{
    /// <summary>
    /// Interaction logic for UserCabinetPage.xaml
    /// </summary>
    public partial class UserCabinetPage : BookLibraryPage
    {
        private Page _previousPage;

        public UserCabinetPage(Page previousPage)
        {
            InitializeComponent();

            _previousPage = previousPage;

            btnBackward.Background = PagesPropertiesProvider.BackwardImage;

            btnBackward.Click += btnBackward_Click;
            btnChangePassword.Click += btnChangePassword_Click;
            btnDeleteAccount.Click += btnDeleteAccount_Click;

            DataContext = new UserCabinetVM();
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                NavigationService.Navigate(_previousPage);
            });
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                NavigationService.Navigate(new ChangePasswordPage(this));
            });
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                NavigationService.Navigate(new DeleteAccountPage(this));
            });
        }
    }
}
