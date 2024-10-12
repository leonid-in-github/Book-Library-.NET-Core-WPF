using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BookLibrary.UI.HelperClasses.Commands.MenuCommands
{
    public class LogoutCommand : ICommand
    {
        #region Fields

        private readonly Action _executeMethod;
        private List<WeakReference> _canExecuteChangedHandlers;

        private readonly Window _loginWindow;

        #endregion

        public LogoutCommand(Window loginWindow)
        {
            _loginWindow = loginWindow;
            _executeMethod = new Action(() =>
            {
                WindowsNavigation.MainWindow?.Hide();
                ResetSession();
                ShowLoginWindow();
            });
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManagerHelper.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value);
            }
            remove
            {
                CommandManagerHelper.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value);
            }
        }

        bool ICommand.CanExecute(object parameter) => true;

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        public void Execute()
        {
            _executeMethod?.Invoke();
        }

        private static void ResetSession()
        {
            AppUser.SetInstance(string.Empty, string.Empty, new Guid());
            Properties.Settings.Default["Login"] = string.Empty;
            Properties.Settings.Default["Password"] = string.Empty;
            Properties.Settings.Default["AccountId"] = new Guid();
            Properties.Settings.Default.Save();
        }

        private void ShowLoginWindow()
        {
            _loginWindow.Show();
        }

    }
}
