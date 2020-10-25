using System;
using Microsoft.AspNetCore.Components;

namespace Sanchez.DansUI.Components.Overlay
{
    public interface IPopper<T>
    {
        Action OnExit { get; set; }
        Action<T> OnCompleted { get; set; }
    }
}
