using Sanchez.DansUI.Components.Overlay;
using Sanchez.DansUI.Models;
using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Sanchez.DansUI.Services
{
    public class PopperService : IPopperService
    {
        protected ConcurrentStack<PopperFrame> _popperStack = new();
        protected BehaviorSubject<PopperFrame> _currentPopper = new(null);

        protected void HandleStack()
        {
            if (_popperStack.TryPeek(out var currentFrame))
            {
                if (currentFrame.Id != _currentPopper.Value?.Id)
                {
                    _currentPopper.OnNext(currentFrame);
                }
            }
            else
            {
                if (_currentPopper.Value != null)
                {
                    _currentPopper.OnNext(null);
                }
            }
        }

        protected void HandleRemovePopper(Guid id)
        {
            if (_popperStack.TryPop(out var oldFrame))
            {
                if (oldFrame.Id != id)
                {
                    throw new Exception("Double frame popped out");
                }
            }

            HandleStack();
        }

        public IObservable<PopperFrame> DisplayStack()
        {
            return _currentPopper;
        }

        public IObservable<TRes> Push<T, TRes>()
            where T : IPopper<TRes>
            where TRes : class
        {
            return Observable.Create<TRes>(observer =>
            {
                var frameId = Guid.NewGuid();
                var hasBeenRemoved = false;

                void handleCompleted(object item)
                {
                    if (_currentPopper.Value?.Id != frameId) return;
                    HandleRemovePopper(frameId);
                    hasBeenRemoved = true;

                    observer.OnNext(item as TRes);
                    observer.OnCompleted();
                }

                void handleExit()
                {
                    if (_currentPopper.Value?.Id != frameId) return;
                    HandleRemovePopper(frameId);
                    hasBeenRemoved = true;

                    observer.OnCompleted();
                }

                var frame = new PopperFrame()
                {
                    Id = frameId,
                    Type = typeof(T),
                    OnCompleted = handleCompleted,
                    OnClosed = handleExit
                };

                _popperStack.Push(frame);
                HandleStack();

                void cleanup()
                {
                    if (!hasBeenRemoved)
                    {
                        var newStack = new ConcurrentStack<PopperFrame>();

                        foreach (var item in _popperStack)
                        {
                            if (item.Id == frameId) continue;
                            newStack.Push(item);
                        }

                        _popperStack = newStack;
                    }
                }
                return cleanup;
            });
        }
    }
}
