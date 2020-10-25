using Microsoft.AspNetCore.Components;
using Sanchez.DansUI.Models;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace Sanchez.DansUI.Components.Overlay
{
    public interface IToastService
    {
        IObservable<TRes> Push<TRes>(ToastSeverity toastSeverity, string title, string message, Func<Action<TRes>, RenderFragment> actionFactory);
        IObservable<Unit> Push(ToastSeverity toastSeverity, string title, string message);

        IObservable<IEnumerable<ToastFrame>> DisplayStack();
    }
}
