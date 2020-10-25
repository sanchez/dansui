using Sanchez.DansUI.Models;

using System;
using System.Threading.Tasks;

namespace Sanchez.DansUI.IServices
{
    public interface IBrowserService
    {
        Task Init();
        void OnReady(Func<Task> onReady);
        void OnReady(Action onReady);

        IObservable<BrowserDimensions> OnResize { get; }
    }
}
