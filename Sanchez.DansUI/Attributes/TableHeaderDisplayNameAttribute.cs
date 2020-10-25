using System;

namespace Sanchez.DansUI.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableHeaderDisplayNameAttribute : Attribute
    {
        public TableHeaderDisplayNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
