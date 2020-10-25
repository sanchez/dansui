using Microsoft.AspNetCore.Components;
using System;

namespace Sanchez.DansUI.Components.Table
{
    public class ReflectionTableHeader<T>
    {
        public bool IsVisible { get; set; }
        public string Name { get; set; }
        public Func<T, object> Selector { get; set; }
        public string SortSelector { get; set; }

        public Type CellPrefix { get; set; }
        public RenderFragment RenderedCellPrefix => builder =>
        {
            builder.OpenComponent(0, CellPrefix);
            builder.CloseComponent();
        };

        public Type CellSuffix { get; set; }
        public RenderFragment RenderedCellSuffix => builder =>
        {
            builder.OpenComponent(0, CellSuffix);
            builder.CloseComponent();
        };
    }
}
