using System;
using Microsoft.AspNetCore.Components;
using Sanchez.DansUI.Icons;

namespace Sanchez.DansUI.Components.Display
{
    public class Step<T, TRes> : IStepItem
        where T : IStepperContent<TRes>, IComponent
    {
        public Step(IconType icon, string title, string subTitle, Action<TRes> onNext)
        {
            Icon = icon;
            Title = title;
            SubTitle = subTitle;
            OnNext = onNext;
        }

        public Step(IconType icon, string title, string subTitle) : this(icon, title, subTitle, x => { }) { }
        public Step(IconType icon, string title) : this(icon, title, null) { }
        public Step(string title) : this(IconType.UNKNOWN, title) { }

        public Step() { }

        public IconType Icon { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public Action<TRes> OnNext { get; set; }

        RenderFragment IStepItem.Render()
        {
            return builder =>
            {
                builder.OpenComponent<T>(0);
                builder.AddAttribute(1, nameof(IStepperContent<TRes>.OnNext), OnNext);
                builder.CloseComponent();
            };
        }
    }
}
