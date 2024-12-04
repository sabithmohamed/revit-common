using System;
using System.Windows.Input;

namespace Idibri.RevitPlugin.Common.Infrastructure
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Predicate<Object> _canExecute;
        private readonly Action<Object> _executeAction;

        public RelayCommand(Action<object> executeAction)
            : this(null, executeAction)
        { }

        public RelayCommand(Predicate<Object> canExecute, Action<object> executeAction)
        {
            if (executeAction == null) { throw new ArgumentNullException("executeAction"); }

            _canExecute = canExecute;
            _executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void UpdateCanExecuteState()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public void Execute(object parameter)
        {
            if (_executeAction != null)
            {
                _executeAction(parameter);
            }

            UpdateCanExecuteState();
        }
    }
}
