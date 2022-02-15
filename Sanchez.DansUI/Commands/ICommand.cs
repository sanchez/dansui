using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sanchez.DansUI.Commands
{
    public interface ICommand<TParam, TResult> : ICommand, IObservable<TResult>
    {
        bool CanExecute(TParam param);
        Task<TResult> ExecuteAsync(TParam param);
    }
}
