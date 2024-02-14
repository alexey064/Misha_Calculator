using System;
using System.Windows.Input;

namespace WindowsDesktop.ViewModels
{
    internal class ExecutableCommand : ICommand
    {
        Action<object> _execute;

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public ExecutableCommand(Action<object> execute)
        {
            _execute = execute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
