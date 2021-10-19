using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Sanchez.DansUI.Controllers
{
    public interface IPageController : IDisposable
    {
        int CurrentPage { get; }
        IObservable<int> ListenCurrentPage();

        int MaxPage { get; }
        IObservable<int> ListenMaxPage();

        void SetPage(int pageNo);
    }

    public interface IPageController<T> : IPageController
    {
        ICollection<T> PageContent(ICollection<T> items);
    }

    public class PageController<T> : IPageController<T>
    {
        protected BehaviorSubject<int> _currentPage;
        protected BehaviorSubject<int> _maxPage;
        protected int _itemsPerPage;

        public PageController(int itemsPerPage, int currentPage, int maxPage)
        {
            _itemsPerPage = itemsPerPage;
            _currentPage = new(currentPage);
            _maxPage = new(maxPage);
        }
        public PageController(int itemsPerPage, int maxPage) : this(itemsPerPage, 1, maxPage) { }
        public PageController(int itemsPerPage) : this(itemsPerPage, -1, -1) { }

        public int CurrentPage => _currentPage.Value;
        public IObservable<int> ListenCurrentPage() => _currentPage;

        public int MaxPage => _maxPage.Value;
        public IObservable<int> ListenMaxPage() => _maxPage;

        public void SetPage(int pageNo)
        {
            if (_maxPage.Value == -1)
            {
                if (pageNo > 0)
                {
                    _currentPage.OnNext(pageNo);
                }
            }
            else
            {
                if (pageNo > _maxPage.Value)
                    _currentPage.OnNext(pageNo);
                else if (pageNo < 1)
                    _currentPage.OnNext(1);
                else
                    _currentPage.OnNext(pageNo);
            }
        }

        public ICollection<T> PageContent(ICollection<T> items)
        {
            var numPages = (int)Math.Ceiling(items.Count / (double)_itemsPerPage);
            if (numPages != _maxPage.Value)
                _maxPage.OnNext(numPages);

            if (_currentPage.Value == -1)
                _currentPage.OnNext(1);
            else if (_currentPage.Value > _maxPage.Value)
                _currentPage.OnNext(_maxPage.Value);

            var skip = (_currentPage.Value - 1) * _itemsPerPage;
            var pagedItems = items.Skip(skip).Take(_itemsPerPage).ToList();
            return pagedItems;
        }

        public void Dispose()
        {
            _currentPage.Dispose();
            _maxPage.Dispose();
        }
    }
}
