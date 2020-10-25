using System;

namespace Sanchez.DansUI.Components.Overlay
{
    public interface IModal<T>
    {
        Action OnExit { get; set; }
        Action<T> OnCompleted { get; set; }
    }
}
