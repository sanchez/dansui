using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using Sanchez.DansUI.Components.Form;

using System.Threading.Tasks;

namespace Sanchez.DansUI.Extensions
{
    public static class ControlsExtensions
    {
        public static ValueTask Focus(this IJSRuntime jsRuntime, ElementReference elementReference)
        {
            return jsRuntime.InvokeVoidAsync("DansUI.controls.focus", elementReference);
        }

        public static ValueTask Focus(this IJSRuntime jsRuntime, IControllableField field) => jsRuntime.Focus(field.Field);

        public static ValueTask Blur(this IJSRuntime jsRuntime, ElementReference elementReference)
        {
            return jsRuntime.InvokeVoidAsync("DansUI.controls.blur", elementReference);
        }

        public static ValueTask Blur(this IJSRuntime jsRuntime, IControllableField field) => jsRuntime.Blur(field.Field);
    }
}
