using Microsoft.JSInterop;

using Sanchez.DansUI.Extensions;

using System;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Models
{
    public class UserState
    {
        readonly IJSRuntime _jsRuntime;

        public UserState(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        TimeSpan _userOffset = TimeSpan.Zero;
        public virtual async ValueTask<TimeSpan> GetUserOffset()
        {
            if (_userOffset == TimeSpan.Zero)
                _userOffset = await _jsRuntime.GetBrowserOffset();

            return _userOffset;
        }
    }
}
