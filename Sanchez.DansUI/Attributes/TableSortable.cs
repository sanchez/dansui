using System;

namespace Sanchez.DansUI.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableSortable : Attribute
    {
        public TableSortable()
        {
            Selector = null;
        }

        public TableSortable(string selector)
        {
            Selector = selector;
        }

        public string Selector { get; }
    }
}
