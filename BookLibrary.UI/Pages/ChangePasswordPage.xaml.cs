using BookLibrary.Storage.Repositories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        private readonly IAccountRepository accountRepository = new AccountRepository();

        public ChangePasswordPage()
        {
            InitializeComponent();

            BtnBackward.Background = PagesPropertiesProvider.BackwardImage;
            BtnBackward.Click += BtnBackward_Click;
            BtnChangePassword.Click += BtnChangePassword_Click;
        }

        private void BtnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (string.Compare(pbNewPassword.Password, pbConfirmPassword.Password) == 0)
            {
                if (await accountRepository.ChangeAccountPassword(AppUser.GetInstance().AccountId, pbPassword.Password, pbNewPassword.Password))
                {
                    MessageBox.Show("Password changed", "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                lblMasage.Content = "Can't change password with this password";
            }
            else
            {
                lblMasage.Content = "Confirm password not correct";
            }
        }
    }
}
