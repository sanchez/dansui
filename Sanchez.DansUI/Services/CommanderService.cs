using Microsoft.JSInterop;

using Sanchez.DansUI.Components.Overlay;
using Sanchez.DansUI.Extensions;
using Sanchez.DansUI.InternalComponents;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Services
{
    public class CommanderService : ICommanderService, IDisposable
    {
        protected IJSRuntime _jsRuntime;
        protected IModalService _modalService;

        protected LinkedList<Command> _commands = new();

        protected Subject<Command> _commandTrigger = new();
        protected IDisposable _commandTriggerDisposable;

        protected Command _activeCommand;

        public CommanderService(IJSRuntime jsRuntime, IModalService modalService)
        {
            _jsRuntime = jsRuntime;
            _modalService = modalService;

            _commandTriggerDisposable = _commandTrigger
                .Do(x =>
                {
                    _activeCommand = x;
                })
                .Select(x => modalService.Push<CommanderSearch, Command>())
                .Switch()
                .Do(x =>
                {
                    x.OnExecute?.Invoke();

                    if (x.SubCommands != null)
                        _commandTrigger.OnNext(x);
                })
                .Subscribe();
        }

        public async Task Init()
        {
            await _jsRuntime.InvokeAsync<string>("DansUI.commander.listen", DotNetObjectReference.Create(this));
        }

        public IEnumerable<Command> SearchCommands(string name)
        {
            return (_activeCommand?.SubCommands != null ? _activeCommand.SubCommands : _commands)
                .Select(x => (Rating: x.Name.FuzzyMatch(name), Command: x))
                .Where(x => x.Rating >= 0)
                .OrderByDescending(x => x.Rating)
                .Take(5)
                .Select(x => x.Command);
        }

        public IDisposable RegisterCommand(Command command)
        {
            var node = _commands.AddLast(command);
            return Disposable.Create(() =>
            {
                _commands.Remove(node);
            });
        }

        public IDisposable RegisterCommands(IEnumerable<Command> commands)
        {
            var removables = commands.Select(x => RegisterCommand(x)).ToList();
            return Disposable.Create(() =>
            {
                foreach (var item in removables)
                    item.Dispose();
            });
        }

        [JSInvokable]
        public void CommanderTriggered()
        {
            _commandTrigger.OnNext(null);
        }

        public void Dispose()
        {
            _commandTriggerDisposable.Dispose();
        }
    }
}
