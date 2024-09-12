using BookLibrary.Storage.Repositories;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BookLibrary.UI.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : BookLibraryWindow
    {
        private readonly IAccountRepository accountRepository = new AccountRepository();

        private Window _loginWindow;

        public RegistrationWindow(Window loginWindow)
        {
            InitializeComponent();

            _loginWindow = loginWindow;

            btnRegister.Click += btnRegister_Click;

            tbLogin.KeyUp += TextBox_KeyUp;
            tbFirstName.KeyUp += TextBox_KeyUp;
            tbLastName.KeyUp += TextBox_KeyUp;
            tbEmail.KeyUp += TextBox_KeyUp;
            pbPassword.KeyUp += TextBox_KeyUp;
            pbConfirmPassword.KeyUp += TextBox_KeyUp;

            this.Closing += RegistrationWindow_Closing;
        }

        private async Task Register()
        {
            if (!ValidateEmptyInputData())
            {
                Message.Content = "Empty input is not correct";
                RegistrationGrid.Background = RegistrationGridAlertBackground;
                return;
            }
            if (!ConfirmPasswordInputData())
            {
                Message.Content = "Confirm password not correct";
                RegistrationGrid.Background = RegistrationGridAlertBackground;
                return;
            }
            if (await accountRepository.Register(tbLogin.Text, pbPassword.Password, tbFirstName.Text, tbLastName.Text, tbEmail.Text) <= 0)
            {
                Message.Content = "Registration data base error";
                RegistrationGrid.Background = RegistrationGridAlertBackground;
                return;
            }
            this.Close();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            await Register();
        }

        private void RegistrationWindow_Closing(object sender, CancelEventArgs e)
        {
            _loginWindow.Show();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnRegister_Click(sender, e);
            }
            e.Handled = true;
        }

        private bool ValidateEmptyInputData()
        {
            if (
                string.IsNullOrEmpty(tbLogin.Text)
                ||
                string.IsNullOrEmpty(pbPassword.Password)
                ||
                string.IsNullOrEmpty(pbConfirmPassword.Password)
                ||
                string.IsNullOrEmpty(tbFirstName.Text)
                ||
                string.IsNullOrEmpty(tbLastName.Text)
                ||
                string.IsNullOrEmpty(tbEmail.Text)
                )
            {
                return false;
            }
            return true;
        }

        private bool ConfirmPasswordInputData()
        {
            if (string.Compare(pbPassword.Password, pbConfirmPassword.Password) == 0)
            {
                return true;
            }
            return false;
        }
        protected new void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private LinearGradientBrush RegistrationGridAlertBackground
        {
            get
            {
                var lgBackground = new LinearGradientBrush();
                lgBackground.EndPoint = new Point(0.5, 1);
                lgBackground.Opacity = 0.25;
                lgBackground.StartPoint = new Point(0.5, 0);
                lgBackground.GradientStops.Add(
                    new GradientStop(Colors.Black, 0));
                lgBackground.GradientStops.Add(
                    new GradientStop(Colors.Red, 1));
                return lgBackground;
            }
        }

    }
}
