namespace Sanchez.DansUI.Models
{
    public class DraggableState<T>
    {
        public string TypeFullName { get; set; }

        public T State { get; set; }

        public DraggableState() { }
        public DraggableState(T state)
        {
            TypeFullName = typeof(T).FullName;
            State = state;
        }
    }
}
