using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Sanchez.DansUI.Extensions
{
    public static class DraggerExtensions
    {
        public static ValueTask BindDraggable<T>(this IJSRuntime jsRuntime, ElementReference elementReference, T item)
        {
            return jsRuntime.InvokeVoidAsync("DansUI.dragger.bindDraggable", elementReference, item);
        }
    }
}
