using System;
using System.Collections.Generic;

namespace Sanchez.DansUI.Models
{
    public class ModalFrame
    {
        public Guid Id { get; set; }
        public Type Type { get; set; }
        public IDictionary<string, object> Parameters { get; set; }
        public Action<object> OnCompleted { get; set; }
        public Action OnClosed { get; set; }
    }
}
