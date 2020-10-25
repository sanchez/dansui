using System;

namespace Sanchez.DansUI.State
{
    public class DefaultState<T>
    {
        public DefaultState(T defaultValue)
        {
            _value = defaultValue;
        }

        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                {
                    _value = value;
                    NotifyStateChanged();
                }
            }
        }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
