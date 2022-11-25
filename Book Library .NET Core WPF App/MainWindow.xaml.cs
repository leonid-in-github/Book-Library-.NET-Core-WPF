﻿using Book_Library_.NET_Core_WPF_App.ExtensionMethods;
using Book_Library_.NET_Core_WPF_App.HelperClasses;
using Book_Library_.NET_Core_WPF_App.HelperClasses.Commands;
using Book_Library_.NET_Core_WPF_App.Pages;
using Book_Library_.NET_Core_WPF_App.Windows;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Book_Library_.NET_Core_WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BookLibraryWindow
    {
        private readonly LoginWindow loginWindow = new LoginWindow();

        public ICommand NavigateUserCabinet { get; }

        public ICommand Logout { get; }

        public ICommand Exit { get; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            SetupWindow();

            LoadMainWindow();

            NavigateUserCabinet = new NavigateUserCabinetCommand(MainFrame);

            Logout = new LogoutCommand(loginWindow);
            Exit = new ExitCommand();

            Closing += MainWindow_Closing;
            ContentRendered += MainWindow_Rendered;
        }

        private void SetupWindow()
        {
            Background = WindowsPropertiesProvider.DefaultBackground;
            Icon = WindowsPropertiesProvider.DefaultIcon;
            this.CenterWindowOnScreen();
        }

        private void LoadMainWindow()
        {
            if (String.IsNullOrEmpty(LastSession.Login) || String.IsNullOrEmpty(LastSession.Password))
            {
                WindowsNavigation.MainWindow?.Hide();
                ShowLoginWindow();
            }
            else
            {
                var actualAccountId = DataStore.Account.Login(LastSession.Login, LastSession.Password);

                if (actualAccountId > 0)
                {
                    AppUser.SetInstance(LastSession.Login, LastSession.Password, actualAccountId);
                    MainFrame.Navigate(new BookLibraryMainPage());
                }
                else
                {
                    WindowsNavigation.MainWindow?.Hide();

                    ShowLoginWindow();
                }
            }

        }

        private void ShowLoginWindow()
        {
            loginWindow.Show();
        }

        private void MainWindow_Rendered(object sender, EventArgs e)
        {
            MainFrame.Navigate(new BookLibraryMainPage());
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Exit.Execute(null);
        }
    }
}
