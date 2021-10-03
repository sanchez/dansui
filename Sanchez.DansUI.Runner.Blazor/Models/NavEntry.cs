using Microsoft.AspNetCore.Components;

using Sanchez.DansUI.Icons;

namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public record NavEntry(RenderFragment Nav, string Url, IconType Icon, string Title);
}
