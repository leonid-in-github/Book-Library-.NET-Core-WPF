using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BookLibrary.UI.HelperClasses.Commands
{
    public class ExitCommand : ICommand
    {
        #region Fields

        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;
        private List<WeakReference> _canExecuteChangedHandlers;

        #endregion

        public ExitCommand()
        {
            _executeMethod = new Action(() => { Application.Current.Shutdown(); });
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
