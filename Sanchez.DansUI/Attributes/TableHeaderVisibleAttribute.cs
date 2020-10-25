using System;

namespace Sanchez.DansUI.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableHeaderVisibleAttribute : Attribute
    {
        public TableHeaderVisibleAttribute(bool isVisible)
        {
            IsVisible = isVisible;
        }

        public bool IsVisible { get; }
    }
}
