namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public class PrimitiveWrapper<T>
    {
        public PrimitiveWrapper(T val)
        {
            Value = val;
        }

        public T Value { get; set; }
    }
}
