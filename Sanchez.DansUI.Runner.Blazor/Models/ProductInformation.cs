using System;
using Sanchez.DansUI.Icons;

namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public record ProductInformation(IconType Icon, string DisplayName, ProductType ProductType);
}
