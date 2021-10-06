using Sanchez.DansUI.Components.Overlay;
using Sanchez.DansUI.Models;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Sanchez.DansUI.Services
{
    public class ModalService : IModalService
    {
        protected ConcurrentStack<ModalFrame> _modalStack = new();
        protected BehaviorSubject<ModalFrame> _currentModal = new(null);

        protected void HandleStack()
        {
            if (_modalStack.TryPeek(out var currentFrame))
            {
                if (currentFrame.Id != _currentModal.Value?.Id)
                    _currentModal.OnNext(currentFrame);
            }
            else
            {
                if (_currentModal.Value != null)
                    _currentModal.OnNext(null);
            }
        }

        protected void HandleRemoveModal(Guid id)
        {
            if (_modalStack.TryPop(out var oldFrame))
            {
                if (oldFrame.Id != id)
                    throw new Exception("Double frame popped out");
            }

            HandleStack();
        }

        public IObservable<ModalFrame> DisplayStack()
        {
            return _currentModal;
        }

        public IObservable<TRes> Push<T, TRes>(IDictionary<string, object> parameters = null)
            where T : IModal<TRes>
            where TRes : class
        {
            return Observable.Create<TRes>(observer =>
            {
                var frameId = Guid.NewGuid();
                var hasBeenRemoved = false;

                void handleCompleted(object item)
                {
                    if (_currentModal.Value?.Id != frameId) return;
                    HandleRemoveModal(frameId);
                    hasBeenRemoved = true;

                    observer.OnNext(item as TRes);
                    observer.OnCompleted();
                }

                void handleExit()
                {
                    if (_currentModal.Value?.Id != frameId) return;
                    HandleRemoveModal(frameId);
                    hasBeenRemoved = true;

                    observer.OnCompleted();
                }

                var frame = new ModalFrame()
                {
                    Id = frameId,
                    Type = typeof(T),
                    Parameters = parameters ?? new Dictionary<string, object>(),
                    OnCompleted = handleCompleted,
                    OnClosed = handleExit
                };

                _modalStack.Push(frame);
                HandleStack();

                void cleanup()
                {
                    if (!hasBeenRemoved)
                    {
                        var newStack = new ConcurrentStack<ModalFrame>();

                        foreach (var item in _modalStack)
                        {
                            if (item.Id == frameId) continue;
                            newStack.Push(item);
                        }

                        _modalStack = newStack;
                    }
                }

                return cleanup;
            });
        }
    }
}
