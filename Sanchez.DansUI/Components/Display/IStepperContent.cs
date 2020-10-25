using System;

namespace Sanchez.DansUI.Components.Display
{
    public interface IStepperContent<T>
    {
        Action<T> OnNext { get; set; }
    }
}
