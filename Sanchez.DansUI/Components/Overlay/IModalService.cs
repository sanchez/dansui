using Sanchez.DansUI.Models;
using System;

namespace Sanchez.DansUI.Components.Overlay
{
    public interface IModalService
    {
        IObservable<TRes> Push<T, TRes>()
            where T : IModal<TRes>
            where TRes : class;

        IObservable<ModalFrame> DisplayStack();
    }
}
