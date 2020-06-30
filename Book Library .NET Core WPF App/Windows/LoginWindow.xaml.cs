using Book_Library_.NET_Core_WPF_App.ExtensionMethods;
using Book_Library_.NET_Core_WPF_App.HelperClasses;
using Book_Library_.NET_Core_WPF_App.Models.AccountModels;
using Book_Library_.NET_Core_WPF_App.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Book_Library_.NET_Core_WPF_App.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : BookLibraryWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            SetupWindow();

            btnLogin.Click += btnLogin_Click;
            btnRegistration.Click += btnRegistration_Click;
            tbLogin.KeyUp += TextBox_KeyUp;
            tbPassword.KeyUp += TextBox_KeyUp;

            this.Closing += LoginWindow_Closing;
        }

        private void SetupWindow()
        {
            this.CenterWindowOnScreen();
            Background = WindowsPropertiesProvider.LoginBackground;
            Icon = WindowsPropertiesProvider.DefaultIcon;
        }

        private LinearGradientBrush LoginGridAlertBackground
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

        private void Login()
        {
            var login = tbLogin.Text;
            tbLogin.Text = string.Empty;
            var password = tbPassword.Password;
            tbPassword.Password = string.Empty;

            var actualAccountId = DataStore.Account.Login(login, password);

            if (actualAccountId > 0)
            {
                AppUser.SetInstance(login, password, actualAccountId);
                Hide();
                var mainWindow = WindowsNavigation.MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Refresh();
                    mainWindow.MainFrame.Navigate(new BookLibraryMainPage());
                    mainWindow.Show();
                }
            }
            else
            {
                LoginGrid.Background = LoginGridAlertBackground;

                Message.Content = "Incorrect login or password.";
            }
        }

        private void Registration()
        {
            this.Hide();
            var registrationWindow = new RegistrationWindow(this);
            registrationWindow.Show();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                Login();
            });
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                Registration();
            });
        }

        private void LoginWindow_Closing(object sender, CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
            if (String.IsNullOrEmpty(AppUser.GetInstance().Login))
            {
                Application.Current.Shutdown();
            }
            else
            {
                WindowsNavigation.MainWindow?.Show();
            }
            
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
            e.Handled = true;
        }
    }
}
