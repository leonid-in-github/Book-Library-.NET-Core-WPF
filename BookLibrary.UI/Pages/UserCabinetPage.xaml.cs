using BookLibrary.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
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

            DataContext = new UserCabinetViewModel();
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_previousPage);
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangePasswordPage(this));
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeleteAccountPage(this));
        }
    }
}
