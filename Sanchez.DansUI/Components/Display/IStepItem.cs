using System;
using Microsoft.AspNetCore.Components;
using Sanchez.DansUI.Icons;

namespace Sanchez.DansUI.Components.Display
{
    public interface IStepItem
    {
        IconType Icon { get; set; }
        string Title { get; set; }
        string SubTitle { get; set; }

        RenderFragment Render();
    }
}
