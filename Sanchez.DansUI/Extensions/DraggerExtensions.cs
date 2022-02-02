using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Extensions
{
    public static class DraggerExtensions
    {
        public class CallbackHandler
        {
            public CallbackHandler()
            {
            }

            [JSInvokable]
            public void HandleCall(object something)
            {
                // TODO: Do something here
            }
        }

        static DotNetObjectReference<CallbackHandler> CreateCallbacker()
        {
            return DotNetObjectReference.Create(new CallbackHandler());
        }

        public static ValueTask BindDraggable<T>(this IJSRuntime jsRuntime, ElementReference elementReference, T item)
        {
            return jsRuntime.InvokeVoidAsync("DansUI.dragger.bindDraggable", elementReference, item);
        }

        public static ValueTask BindDroppable<T>(this IJSRuntime jsRuntime, ElementReference elementReference, Func<T, bool> canDrop)
        {
            return jsRuntime.InvokeVoidAsync("DansUI.dragger.bindDroppable", elementReference, CreateCallbacker(), CreateCallbacker());
        }
    }
}
