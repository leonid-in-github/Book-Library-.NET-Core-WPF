using BookLibrary.UI.Pages;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookLibrary.UI.HelperClasses.Commands.MenuCommands
{
    public class NavigateUserCabinetCommand : ICommand
    {
        #region Fields

        private readonly Action _executeMethod;
        private List<WeakReference> _canExecuteChangedHandlers;

        #endregion

        public NavigateUserCabinetCommand(Frame mainFrame)
        {
            _executeMethod = new Action(() =>
            {
                mainFrame.Navigate(new UserCabinetPage());
            });
        }


        public NavigateUserCabinetCommand()
        {
            _executeMethod = new Action(() =>
            {
                WindowsNavigation.MainWindow?.MainFrame.Navigate(new UserCabinetPage());
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
    }
}
