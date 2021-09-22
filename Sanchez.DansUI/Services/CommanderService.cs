using Microsoft.JSInterop;

using Sanchez.DansUI.Components.Overlay;
using Sanchez.DansUI.InternalComponents;

using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Services
{
    public class CommanderService : ICommanderService, IDisposable
    {
        protected IJSRuntime _jsRuntime;
        protected IModalService _modalService;

        protected Subject<Unit> _commandTrigger = new();
        protected IDisposable _commandTriggerDisposable;

        public CommanderService(IJSRuntime jsRuntime, IModalService modalService)
        {
            _jsRuntime = jsRuntime;
            _modalService = modalService;

            _commandTriggerDisposable = _commandTrigger
                .Select(x => modalService.Push<CommanderSearch, Command>())
                .Switch()
                .Subscribe();
        }

        public async Task Init()
        {
            await _jsRuntime.InvokeAsync<string>("DansUI.commander.listen", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public void CommanderTriggered()
        {
            _commandTrigger.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _commandTriggerDisposable.Dispose();
        }
    }
}
