using BookLibrary.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for UserCabinetPage.xaml
    /// </summary>
    public partial class UserCabinetPage : Page
    {
        public UserCabinetPage()
        {
            InitializeComponent();

            BtnBackward.Background = PagesPropertiesProvider.BackwardImage;

            BtnBackward.Click += BtnBackward_Click;
            BtnChangePassword.Click += BtnChangePassword_Click;
            BtnDeleteAccount.Click += BtnDeleteAccount_Click;

            DataContext = new UserCabinetViewModel();
        }

        private void BtnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangePasswordPage());
        }

        private void BtnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeleteAccountPage());
        }
    }
}
