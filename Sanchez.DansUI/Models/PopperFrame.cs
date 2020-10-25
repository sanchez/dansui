using System;

namespace Sanchez.DansUI.Models
{
    public class PopperFrame
    {
        public Guid Id { get; set; }
        public Type Type { get; set; }
        public Action<object> OnCompleted { get; set; }
        public Action OnClosed { get; set; }
    }
}
