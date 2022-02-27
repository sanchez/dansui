using System;
using System.Collections.Generic;
using System.Threading;

namespace Sanchez.DansUI.Components.TreeView
{
    public interface ITreeItem<TChildren>
        where TChildren : ITreeItem<TChildren>
    {
        bool IsExpanded { get; set; }

        IObservable<ICollection<TChildren>> GetChildren(CancellationToken cToken);
    }
}
