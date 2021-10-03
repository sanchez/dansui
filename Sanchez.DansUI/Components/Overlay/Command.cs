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

        public Command() { }

        public Command(IconType icon, string name, Action onExecute) : this()
        {
            Icon = icon;
            Name = name;
            OnExecute = onExecute;
        }

        public Command(IconType icon, string name, string description, Action onExecute) : this(icon, name, onExecute)
        {
            Description = description;
        }
    }
}
