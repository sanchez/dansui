using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Threading.Tasks;

namespace Sanchez.DansUI.Extensions
{
    public static class InputExtensions
    {
        public static ValueTask<IJSObjectReference> BindFileInput(this IJSRuntime jsRuntime, ElementReference dropZone, ElementReference inputFile)
        {
            return jsRuntime.InvokeAsync<IJSObjectReference>("DansUI.input.bindFileInput", dropZone, inputFile);
        }
    }
}
