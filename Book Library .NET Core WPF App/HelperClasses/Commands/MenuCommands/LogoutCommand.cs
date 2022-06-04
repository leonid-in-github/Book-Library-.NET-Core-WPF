using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Book_Library_.NET_Core_WPF_App.HelperClasses.Commands
{
    public class LogoutCommand : ICommand
    {
        #region Fields

        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;
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

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        public bool CanExecute()
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod();
            }

            return true;
        }

        public void Execute()
        {
            if (_executeMethod != null)
            {
                _executeMethod();
            }
        }

        private void ResetSession()
        {
            AppUser.SetInstance(string.Empty, string.Empty, 0);
            Properties.Settings.Default["Login"] = string.Empty;
            Properties.Settings.Default["Password"] = string.Empty;
            Properties.Settings.Default["AccountId"] = 0;
            Properties.Settings.Default.Save();
        }

        private void ShowLoginWindow()
        {
            _loginWindow.Show();
        }

    }
}
