using Book_Library_.NET_Core_WPF_App.ExtensionMethods;
using Book_Library_.NET_Core_WPF_App.Models.AccountModels;
using Book_Library_.NET_Core_WPF_App.Pages;
using Book_Library_.NET_Core_WPF_App.Windows;
using Book_Library_EF_Core_Proxy_Class_Library.Configuration;
using Book_Library_EF_Core_Proxy_Class_Library.Proxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Book_Library_.NET_Core_WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BookLibraryWindow
    {
        private readonly Window loginWindow;

        public MainWindow()
        {
            InitializeComponent();

            loginWindow = new LoginWindow();

            SetupWindow();

            LoadMainWindow();
        }

        private void SetupWindow()
        {
            Background = WindowsPropertiesProvider.DefaultBackground;
            Icon = WindowsPropertiesProvider.DefaultIcon;
            this.CenterWindowOnScreen();
            Closing += MainWindow_Closing;
            ContentRendered += MainWindow_Rendered;
        }

        private void LoadMainWindow()
        {
            BookLibraryProxyConfiguration
                .GetInstanse()
                .SetupBookLibraryProxyConfiguration(Properties.Settings.Default["ConnectionString"].ToString());

            var lastSession = LastSessionConfig;

            if (String.IsNullOrEmpty(lastSession.Login)
                ||
                String.IsNullOrEmpty(lastSession.Password))
            {
                App.Current.MainWindow.Hide();

                ShowLoginWindow();
            }
            else
            {
                TryCatchMessageTask(() =>
                {
                    var actualAccountId = dbBookLibraryProxy.Account.Login(lastSession.Login, lastSession.Password);

                    if (actualAccountId > 0)
                    {
                        AppUser.SetInstance(lastSession.Login, lastSession.Password, actualAccountId);
                        MainFrame.Navigate(new BookLibraryMainPage());
                    }
                    else
                    {
                        App.Current.MainWindow.Hide();

                        ShowLoginWindow();
                    }
                });
            }

        }

        private void ShowLoginWindow()
        {
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

        private void MainWindow_Rendered(object sender, EventArgs e)
        {
            MainFrame.Navigate(new BookLibraryMainPage());
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mnUserCabinetClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MainFrame.Navigate(new UserCabinetPage((Page)MainFrame.Content));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        private void mnLogoutClick(object sender, RoutedEventArgs e)
        {
            try
            {
                App.Current.MainWindow.Hide();
                ResetSession();
                ShowLoginWindow();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        private void mnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private AppUserConfigModel LastSessionConfig
        {
            get
                {
                    return new AppUserConfigModel(Properties.Settings.Default.Login,
                        Properties.Settings.Default.Password,
                        Properties.Settings.Default.AccountId);
                }
        }
    }
}
