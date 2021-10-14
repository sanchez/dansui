using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Sanchez.DansUI.Controllers
{
    public class SortController<T> : IObservable<SortAction<T>>, IDisposable
    {
        protected SortAction<T> _lastValue;
        protected BehaviorSubject<SortAction<T>> _subject;

        public SortController(SortAction<T> initialComparer)
        {
            _subject = new(initialComparer);
            _lastValue = initialComparer;
        }

        public SortController(string key, bool wasDescending, IComparer<T> comparer)
        {
            var action = new SortAction<T>()
            {
                Comparer = comparer,
                PropertyName = key,
                WasDescending = wasDescending
            };

            _subject = new(action);
            _lastValue = action;
        }

        public void Emit(SortAction<T> newComparer)
        {
            _lastValue = newComparer;
            _subject.OnNext(newComparer);
        }

        public SortAction<T> LastValue => _lastValue;

        public IDisposable Subscribe(IObserver<SortAction<T>> observer)
        {
            return _subject.Subscribe(observer);
        }

        public void Dispose()
        {
            _subject.Dispose();
        }
    }
}
