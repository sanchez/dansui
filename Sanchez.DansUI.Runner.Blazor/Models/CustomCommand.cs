using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public class CustomCommand : ICommand
    {
        protected Func<Task> _onExecute;
        protected bool _canExecute = true;

        public CustomCommand(Func<Task> onExecute)
        {
            _onExecute = onExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            _canExecute = false;
            CanExecuteChanged.Invoke(this, new EventArgs());

            await _onExecute();

            _canExecute = true;
            CanExecuteChanged.Invoke(this, new EventArgs());
        }
    }
}
