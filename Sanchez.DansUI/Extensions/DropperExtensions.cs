using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using Sanchez.DansUI.Models;

using System.Threading.Tasks;

namespace Sanchez.DansUI.Extensions
{
    public static class DropperExtensions
    {
        public static ValueTask<BoundingRect> GetBoundingBox(this IJSRuntime jsRuntime, ElementReference elementReference)
        {
            return jsRuntime.InvokeAsync<BoundingRect>("DansUI.dropper.getBoundingBox", elementReference);
        }
    }
}
