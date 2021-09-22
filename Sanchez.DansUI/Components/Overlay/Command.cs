using Sanchez.DansUI.Icons;

using System;

namespace Sanchez.DansUI.Components.Overlay
{
    public class Command
    {
        public IconType Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Action OnExecute { get; set; }
    }
}
