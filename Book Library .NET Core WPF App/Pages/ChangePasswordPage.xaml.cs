using System;
using System.Collections.Generic;
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
    /// Interaction logic for ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : BookLibraryPage
    {
        private Page _previousPage;

        public ChangePasswordPage(Page previousPage)
        {
            InitializeComponent();

            _previousPage = previousPage;
            btnBackward.Background = PagesPropertiesProvider.BackwardImage;
            btnBackward.Click += btnBackward_Click;
            btnChangePassword.Click += btnChangePassword_Click;
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
                if (string.Compare(pbNewPassword.Password, pbConfirmPassword.Password) == 0)
                {
                    if (DataStore.Account.ChangeAccountPassword(AppUser.GetInstance().AccountId, pbPassword.Password, pbNewPassword.Password))
                    {
                        MessageBox.Show("Password changed", "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.Navigate(_previousPage);
                    }
                    lblMasage.Content = "Can't change password with this password";
                }
                else
                {
                    lblMasage.Content = "Confirm password not correct";
                }
            });
        }
    }
}
