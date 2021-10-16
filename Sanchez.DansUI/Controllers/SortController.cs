using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

namespace Sanchez.DansUI.Controllers
{
    public interface ISortController<T> : IDisposable
    {
        SortAlgorithm<T> CurrentAlgorithm { get; }
        IObservable<SortAlgorithm<T>> ListenCurrentAlgorithm();

        void SetAlgorithm(SortAlgorithm<T> algo);

        ICollection<T> SortContent(ICollection<T> items);
    }

    public class SortComparer<T> : IComparer<T>
    {
        protected readonly bool _isDescending;
        protected readonly Func<T, IComparable> _comparer;

        public SortComparer(bool isDescending, Func<T, IComparable> comparer)
        {
            _isDescending = isDescending;
            _comparer = comparer;
        }

        int CompareNoDirection(T? x, T? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            var xItem = _comparer(x);
            var yItem = _comparer(y);

            if (xItem == null && yItem == null) return 0;
            if (xItem == null) return -1;
            if (yItem == null) return 1;

            return xItem.CompareTo(yItem);
        }

        public int Compare(T? x, T? y)
        {
            var res = CompareNoDirection(x, y);

            if (_isDescending)
                return res * -1;
            return res;
        }
    }

    public class SortAlgorithm<T>
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsDescending { get; set; }
        public Comparison<T> Comparer { get; set; }

        public SortAlgorithm() { }
        public SortAlgorithm(string name, bool isDescending, Func<T, IComparable> selector)
        {
            Key = name;
            Name = name;
            IsDescending = isDescending;
            Comparer = new SortComparer<T>(isDescending, selector).Compare;
        }
    }

    public class SortController<T> : ISortController<T>
    {
        protected BehaviorSubject<SortAlgorithm<T>> _currentAlgorithm;

        public SortController(SortAlgorithm<T> initialSort)
        {
            _currentAlgorithm = new(initialSort);
        }

        public SortController() : this(null) { }

        public ICollection<T> SortContent(ICollection<T> items)
        {
            if (_currentAlgorithm.Value == null) return items;

            var listItems = items.ToList();
            listItems.Sort(_currentAlgorithm.Value.Comparer);

            return listItems;
        }

        public SortAlgorithm<T> CurrentAlgorithm => _currentAlgorithm.Value;
        public IObservable<SortAlgorithm<T>> ListenCurrentAlgorithm() => _currentAlgorithm;

        public void SetAlgorithm(SortAlgorithm<T> algo)
        {
            _currentAlgorithm.OnNext(algo);
        }

        public void Dispose()
        {
            _currentAlgorithm.Dispose();
        }
    }
}
