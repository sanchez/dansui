using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sanchez.DansUI.Commands
{
    public class Command<TParam, TResult> : ICommand<TParam, TResult>, IDisposable
    {
        readonly Func<TParam, bool> _canExecute;
        readonly Func<TParam, Task<TResult>> _execute;
        readonly Subject<TResult> _subject;

        public Command(Func<TParam, bool> canExecute, Func<TParam, Task<TResult>> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
            _subject = new Subject<TResult>();
        }

        public bool CanExecute(TParam param)
        {
            return _canExecute(param);
        }

        public async Task<TResult> ExecuteAsync(TParam param)
        {
            try
            {
                TResult res = await _execute(param);
                _subject.OnNext(res);
                return res;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter == null)
                return _canExecute(default);
            if (parameter is TParam param)
                return _canExecute(param);
            return false;
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter is TParam param)
                Task.Run(() => ExecuteAsync(param));
        }

        IDisposable IObservable<TResult>.Subscribe(IObserver<TResult> observer)
        {
            return _subject.Subscribe(observer);
        }

        void IDisposable.Dispose()
        {
            _subject.Dispose();
        }
    }
}
