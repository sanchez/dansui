using System;
using Sanchez.DansUI.Models;

namespace Sanchez.DansUI.Components.Overlay
{
    public interface IPopperService
    {
        IObservable<TRes> Push<T, TRes>()
            where T : IPopper<TRes>
            where TRes : class;

        IObservable<PopperFrame> DisplayStack();
    }
}