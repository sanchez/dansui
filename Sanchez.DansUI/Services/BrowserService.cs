using Microsoft.JSInterop;

using Sanchez.DansUI.IServices;
using Sanchez.DansUI.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Services
{
    public class BrowserService : IBrowserService
    {
        protected bool _hasInit = false;
        protected IJSRuntime _jsRuntime;

        protected List<Func<Task>> _onReadyActions = new();

        public BrowserService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Init()
        {
            var currentDimensions = await _jsRuntime.InvokeAsync<BrowserDimensions>("DansUI.window.currentSize");
            _onResize = new(currentDimensions);
            await _jsRuntime.InvokeAsync<string>("DansUI.window.resize", DotNetObjectReference.Create(this));

            await Task.WhenAll(_onReadyActions.Select(x => x()));
            _hasInit = true;
        }

        public void OnReady(Func<Task> onReady)
        {
            if (_hasInit)
                Task.Run(onReady);
            else
                _onReadyActions.Add(onReady);
        }

        public void OnReady(Action onReady)
        {
            OnReady(() =>
            {
                onReady();
                return Task.CompletedTask;
            });
        }

        protected BehaviorSubject<BrowserDimensions> _onResize;
        public IObservable<BrowserDimensions> OnResize => _onResize;

        [JSInvokable]
        public void UpdateBrowserDimensions(double width, double height)
        {
            _onResize.OnNext(new BrowserDimensions(width, height));
        }
    }
}
