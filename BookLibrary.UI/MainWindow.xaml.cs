using BookLibrary.Storage.Repositories;
using BookLibrary.UI.HelperClasses;
using BookLibrary.UI.HelperClasses.Commands;
using BookLibrary.UI.Pages;
using BookLibrary.UI.Windows;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookLibrary.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BookLibraryWindow
    {
        private readonly AccountRepository accountRepository = new();
        private readonly LoginWindow loginWindow = new();

        public ICommand NavigateUserCabinet { get; }

        public ICommand Logout { get; }

        public ICommand Exit { get; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            LoadMainWindow().GetAwaiter().GetResult();

            NavigateUserCabinet = new NavigateUserCabinetCommand(MainFrame);

            Logout = new LogoutCommand(loginWindow);
            Exit = new ExitCommand();

            Closing += MainWindow_Closing;
        }

        private async Task LoadMainWindow()
        {
            if (String.IsNullOrEmpty(LastSession.Login) || String.IsNullOrEmpty(LastSession.Password))
            {
                WindowsNavigation.MainWindow?.Hide();
                ShowLoginWindow();
            }
            else
            {
                var actualAccountId = await accountRepository.Login(LastSession.Login, LastSession.Password);

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

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Exit.Execute(null);
        }
    }
}
