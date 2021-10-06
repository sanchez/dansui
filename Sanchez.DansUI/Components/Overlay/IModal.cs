using System;
using System.Collections.Generic;

namespace Sanchez.DansUI.Components.Overlay
{
    public interface IModal<T>
    {
        IDictionary<string, object> Parameters { get; set; }
        Action OnExit { get; set; }
        Action<T> OnCompleted { get; set; }
    }
}
