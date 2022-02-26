using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Components.TreeView
{
    public interface ITreeItem<TChildren>
        where TChildren : ITreeItem<TChildren>
    {
        bool IsExpanded { get; set; }

        Task<ICollection<TChildren>> GetChildren(CancellationToken cToken);
    }
}
