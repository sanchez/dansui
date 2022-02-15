using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Commands
{
    public static class Command
    {
        public static ICommand<TParam, TResult> Create<TParam, TResult>(Func<TParam, bool> canExecute, Func<TParam, Task<TResult>> execute)
        {
            return new Command<TParam, TResult>(canExecute, execute);
        }

        public static ICommand<TParam, TResult> Create<TParam, TResult>(Func<TParam, bool> canExecute, Func<TParam, IObservable<TResult>> execute)
        {
            return new Command<TParam, TResult>(canExecute, param => execute(param).ToTask());
        }

        public static ICommand<TParam, TResult> Create<TParam, TResult>(Func<TParam, bool> canExecute, Func<TParam, TResult> execute)
        {
            return new Command<TParam, TResult>(canExecute, param => Task.FromResult(execute(param)));
        }

        public static ICommand<TParam, Unit> Create<TParam>(Func<TParam, bool> canExecute, Func<TParam, Task> execute)
        {
            return new Command<TParam, Unit>(canExecute, async (param) =>
            {
                await execute(param);
                return Unit.Default;
            });
        }

        public static ICommand<Unit, Unit> Create(Func<bool> canExecute, Func<Task> execute)
        {
            return new Command<Unit, Unit>(x => canExecute(), async (param) =>
            {
                await execute();
                return Unit.Default;
            });
        }

        public static ICommand<Unit, Unit> Bind<TParam, Unit>(this ICommand<TParam, Unit> command, TParam param)
        {
            return new Command<Unit, Unit>((_) => command.CanExecute(param), (_) => command.ExecuteAsync(param));
        }

        public static ICommand<TParam, TResult> ToCommand<TParam, TResult>(this Func<TParam, Task<TResult>> execute)
        {
            return Create(x => true, execute);
        }

        public static ICommand<TParam, TResult> ToCommand<TParam, TResult>(this Func<TParam, TResult> execute)
        {
            return Create(x => true, execute);
        }

        public static ICommand<TParam, Unit> ToCommand<TParam>(this Func<TParam, Task> execute)
        {
            return Create(x => true, execute);
        }

        public static ICommand<Unit, Unit> ToCommand(this Func<Task> execute)
        {
            return Create(() => true, execute);
        }
    }
}
