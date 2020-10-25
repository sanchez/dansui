using System;

namespace Sanchez.DansUI.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableCellSuffixAttribute : Attribute
    {
        public TableCellSuffixAttribute(Type suffix)
        {
            Suffix = suffix;
        }

        public Type Suffix { get; }
    }
}
