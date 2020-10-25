using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Sanchez.DansUI.Components.Form
{
    public record DropDownEntry<T>(T Item, Action OnSelect);
}
