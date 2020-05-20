using Book_Library_.NET_Core_WPF_App.ExtensionMethods;
using Book_Library_.NET_Core_WPF_App.Models.AccountModels;
using Book_Library_EF_Core_Proxy_Class_Library.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            SetupWindow();
            this.CenterWindowOnScreen();

            btnLogin.Click += btnLogin_Click;
            btnRegistration.Click += btnRegistration_Click;
            loginTextBox.KeyUp += TextBox_KeyUp;
            loginPasswordBox.KeyUp += TextBox_KeyUp;

            this.Closing += LoginWindow_Closing;
        }

        private void SetupWindow()
        {
            Background = WindowsPropertiesProvider.LoginBackground;
            Icon = WindowsPropertiesProvider.DefaultIcon;
        }
        
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = loginTextBox.Text;
                var password = loginPasswordBox.Password;

                //var login = "test";
                //var password = "test";

                var actualAccountId = dbBookLibraryProxy.Account.Login(login, password);

                if (actualAccountId > 0)
                {
                    Properties.Settings.Default["Login"] = login;
                    Properties.Settings.Default["Password"] = password;
                    Properties.Settings.Default["AccountId"] = actualAccountId;
                    Properties.Settings.Default.Save();
                    AppUser.SetInstance(login, password, actualAccountId);
                    this.Close();
                }
                else
                {
                    LoginGrid.Background = LoginGridAlertBackground;

                    Message.Content = "Incorrect login or password.";
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }


            
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                var registrationWindow = new RegistrationWindow(this);
                registrationWindow.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        private void LoginWindow_Closing(object sender, CancelEventArgs e)
        {
            if(String.IsNullOrEmpty(AppUser.GetInstance().Login))
            {
                Application.Current.Shutdown();
            }
            else
            {
                App.Current.MainWindow.Show();
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
    }
}
