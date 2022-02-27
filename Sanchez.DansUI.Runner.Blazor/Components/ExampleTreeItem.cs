using Sanchez.DansUI.Components.TreeView;

using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Runner.Blazor.Components
{
    public class ExampleTreeItem : ITreeItem<ExampleTreeItem>
    {
        public Guid Id { get; } = Guid.NewGuid();
        public bool IsExpanded { get; set; }

        public int Depth { get; set; }

        ICollection<ExampleTreeItem> GenerateChildren()
        {
            Random rnd = new();
            ICollection<ExampleTreeItem> children = new List<ExampleTreeItem>();
            int numberKids = rnd.Next(0, 10);
            for (var i = 0; i <= numberKids; i++)
            {
                children.Add(new ExampleTreeItem()
                {
                    Depth = Depth + 1
                });
            }
            return children;
        }

        public IObservable<ICollection<ExampleTreeItem>> GetChildren(CancellationToken cToken)
        {
            return Observable.Create<ICollection<ExampleTreeItem>>(async (observer, cToken) =>
            {
                Random rnd = new();
                if (Depth > 5)
                {
                    observer.OnNext(new List<ExampleTreeItem>());
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(rnd.Next(0, 3)), cToken);
                    observer.OnNext(GenerateChildren());

                    await Task.Delay(TimeSpan.FromMinutes(2), cToken);
                    observer.OnNext(GenerateChildren());
                }

                return () =>
                {
                    // On cancel, do nothing
                };
            });
        }
    }
}
