using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Threading;

namespace Sanchez.DansUI.Helpers
{
    public class ConcurrentLinkedList<T> : IEnumerable<T>
    {
        protected LinkedList<T> _list = new();
        protected SemaphoreSlim _semaphore = new(1, 1);

        public void Add(T item)
        {
            _semaphore.Wait();
            using var resourcer = Disposable.Create(() =>
            {
                _semaphore.Release();
            });

            _list.AddLast(item);
        }

        public void Remove(T item)
        {
            _semaphore.Wait();
            using var resourcer = Disposable.Create(() =>
            {
                _semaphore.Release();
            });

            _list.Remove(item);
        }

        public void Remove(Func<T, bool> predicate)
        {
            if (_list.Count == 0) return;

            _semaphore.Wait();
            using var resourcer = Disposable.Create(() =>
            {
                _semaphore.Release();
            });

            var toBeRemovedNodes = new List<LinkedListNode<T>>();
            var currentPos = _list.First;

            if (currentPos == null) return;

            do
            {
                if (predicate(currentPos.Value))
                    toBeRemovedNodes.Add(currentPos);
            }
            while ((currentPos = currentPos.Next) != null);

            foreach (var item in toBeRemovedNodes)
                _list.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
