using System;

namespace Sanchez.DansUI.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableCellPrefixAttribute : Attribute
    {
        public TableCellPrefixAttribute(Type prefix)
        {
            Prefix = prefix;
        }

        public Type Prefix { get; }
    }
}
