using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BookLibrary.UI.HelperClasses.Commands.MenuCommands
{
    public class ExitCommand : ICommand
    {
        #region Fields

        private readonly Action _executeMethod;
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
