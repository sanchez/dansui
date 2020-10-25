using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData;

namespace Sanchez.DansUI.Controllers
{
    public static class SortControllerExtensions
    {
        public static IObservable<IChangeSet<T>> BindSortController<T>(this IObservable<IChangeSet<T>> source, SortController<T> sortController)
        {
            return source
                .Sort(sortController.Select(x => x.Comparer));
        }

        public static IObservable<ISortedChangeSet<T, TKey>> BindSortController<T, TKey>(this IObservable<IChangeSet<T, TKey>> source, SortController<T> sortController) where TKey : notnull
        {
            return source
                .Sort(sortController.Select(x => x.Comparer));
        }
    }

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
