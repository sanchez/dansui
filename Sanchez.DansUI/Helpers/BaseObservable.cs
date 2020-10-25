using System;
using System.Collections.Generic;

namespace Sanchez.DansUI.Helpers
{
    public class BaseObservable<T> : IObservable<T>
    {
        protected List<IObserver<T>> _observers = new();

        protected virtual void OnSubscribe(IObserver<T> observer)
        {
        }

        protected virtual void Emit(T value)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            OnSubscribe(observer);

            return new Unsubscriber(_observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<T>> _observers;
            private IObserver<T> _observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
