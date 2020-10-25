using Microsoft.AspNetCore.Components;
using Sanchez.DansUI.Components.Overlay;
using Sanchez.DansUI.Helpers;
using Sanchez.DansUI.Models;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Sanchez.DansUI.Services
{
    public class ToastService : IToastService
    {
        protected ConcurrentLinkedList<ToastFrame> _toastList = new();
        protected BehaviorSubject<IEnumerable<ToastFrame>> _currentToastList = new(new List<ToastFrame>());

        public IObservable<IEnumerable<ToastFrame>> DisplayStack()
        {
            return _currentToastList;
        }

        protected void HandleRemoveToast(Guid id)
        {
            _toastList.Remove(x => x.Id == id);
            _currentToastList.OnNext(_toastList);
        }

        public IObservable<TRes> Push<TRes>(ToastSeverity toastSeverity, string title, string message, Func<Action<TRes>, RenderFragment> actionFactory)
        {
            return Observable.Create<TRes>(observer =>
            {
                var frameId = Guid.NewGuid();

                void handleCompleted(TRes item)
                {
                    HandleRemoveToast(frameId);

                    observer.OnNext(item);
                    observer.OnCompleted();
                }

                void handleExit()
                {
                    HandleRemoveToast(frameId);

                    observer.OnNext(default);
                    observer.OnCompleted();
                }

                var frame = new ToastFrame()
                {
                    Id = frameId,
                    ToastSeverity = toastSeverity,
                    Title = title,
                    Message = message,
                    Actions = actionFactory(handleCompleted),
                    OnClosed = handleExit
                };
                _toastList.Add(frame);
                _currentToastList.OnNext(_toastList);

                void cleanup()
                {
                    HandleRemoveToast(frameId);
                }

                return cleanup;
            });
        }

        public IObservable<Unit> Push(ToastSeverity toastSeverity, string title, string message)
        {
            return Push<Unit>(toastSeverity, title, message, cb => null);
        }
    }
}
