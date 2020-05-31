using Book_Library_.NET_Core_WPF_App.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Book_Library_.NET_Core_WPF_App.HelperClasses.Commands
{
    public class NavigateUserCabinetCommand : ICommand
    {
        #region Fields

        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;
        private List<WeakReference> _canExecuteChangedHandlers;

        #endregion

        public NavigateUserCabinetCommand(Frame mainFrame)
        {
            _executeMethod = new Action(() =>
            {
                mainFrame.Navigate(new UserCabinetPage((Page)mainFrame.Content));
            });
        }
        

        public NavigateUserCabinetCommand(NavigationService navigationService, Page parentPage)
        {
            _executeMethod = new Action(() =>
            {
                navigationService.Navigate(new UserCabinetPage(parentPage));
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
    }
}
