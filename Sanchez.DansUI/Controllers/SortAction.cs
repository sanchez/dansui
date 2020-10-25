using System;
using System.Collections.Generic;

namespace Sanchez.DansUI.Controllers
{
    public class SortAction<T>
    {
        public bool WasDescending { get; set; }
        public string PropertyName { get; set; }
        public IComparer<T> Comparer { get; set; }
    }
}
