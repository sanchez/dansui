using Sanchez.DansUI.Models;

using System;
using System.Collections.Generic;

namespace Sanchez.DansUI.Components.Overlay
{
    public interface IModalService
    {
        IObservable<TRes> Push<T, TRes>(IDictionary<string, object> parameters = null)
            where T : IModal<TRes>
            where TRes : class;

        IObservable<ModalFrame> DisplayStack();
    }
}
