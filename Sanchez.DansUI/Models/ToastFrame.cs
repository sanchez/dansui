using Microsoft.AspNetCore.Components;
using Sanchez.DansUI.Components.Overlay;
using System;

namespace Sanchez.DansUI.Models
{
    public class ToastFrame
    {
        public Guid Id { get; set; }

        public ToastSeverity ToastSeverity { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public RenderFragment Actions { get; set; }

        public Action OnClosed { get; set; }
    }
}
