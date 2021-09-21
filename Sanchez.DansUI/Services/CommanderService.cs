using Microsoft.JSInterop;

using Sanchez.DansUI.Components.Overlay;

using System.Threading.Tasks;

namespace Sanchez.DansUI.Services
{
    public class CommanderService : ICommanderService
    {
        protected IJSRuntime _jsRuntime;

        public CommanderService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Init()
        {
            await _jsRuntime.InvokeAsync<string>("DansUI.commander.listen", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public void CommanderTriggered()
        {
            // TODO: do the trigger stuff here
        }
    }
}
