using DynamicData;
using System;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace Sanchez.DansUI.Controllers
{
    public static class PageControllerExtensions
    {
        public static IObservable<IChangeSet<T>> BindPageController<T>(this IObservable<IChangeSet<T>> source, PageController pageController)
        {
            return source
                .Page(pageController)
                .Do(x =>
                {
                    pageController.MaxPages = x.Response.Pages;
                    pageController.CurrentPage = x.Response.Page;
                });
        }

        public static IObservable<IChangeSet<T, TKey>> BindPageController<T, TKey>(this IObservable<ISortedChangeSet<T, TKey>> source, PageController pageController) where TKey : notnull
        {
            return source
                .Page(pageController)
                .Do(x =>
                {
                    pageController.MaxPages = x.Response.Pages;
                    pageController.CurrentPage = x.Response.Page;
                });
        }
    }

    public class PageController : IObservable<IPageRequest>, IDisposable
    {
        protected int _pageSize;
        protected int _currentPage = 1;
        protected int _maxPages = -1;

        protected BehaviorSubject<IPageRequest> _subject;

        public PageController(int pageSize)
        {
            _pageSize = pageSize;
            _subject = new(new PageRequest(_currentPage, pageSize));
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    _subject.OnNext(new PageRequest(_currentPage, _pageSize));
                }
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    _subject.OnNext(new PageRequest(_currentPage, _pageSize));
                }
            }
        }

        public int MaxPages
        {
            get => _maxPages;
            set
            {
                _maxPages = value;
            }
        }

        public IDisposable Subscribe(IObserver<IPageRequest> observer)
        {
            return _subject.Subscribe(observer);
        }

        public void Dispose()
        {
            _subject.Dispose();
        }
    }
}
