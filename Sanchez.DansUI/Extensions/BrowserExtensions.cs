using Microsoft.JSInterop;

using Sanchez.DansUI.Models;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Sanchez.DansUI.Extensions
{
    public static class BrowserExtensions
    {
        public static ValueTask<BrowserDimensions> GetWindowSize(this IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<BrowserDimensions>("DansUI.window.currentSize");
        }

        public static ValueTask<BrowserDimensions> GetPageSize(this IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<BrowserDimensions>("DansUI.page.currentSize");
        }

        public static async ValueTask DownloadFile(this IJSRuntime jsRuntime, string fileName, Stream stream)
        {
            using var streamRef = new DotNetStreamReference(stream);
            await jsRuntime.InvokeVoidAsync("DansUI.page.downloadFileFromStream", fileName, streamRef);
        }

        public static ValueTask TriggerFileDownload(this IJSRuntime jsRuntime, string fileName, string url)
        {
            return jsRuntime.InvokeVoidAsync("DansUI.page.triggerFileDownload", fileName, url);
        }

        public static async ValueTask<TimeSpan> GetBrowserOffset(this IJSRuntime jsRuntime)
        {
            try
            {
                int hoursOffset = await jsRuntime.InvokeAsync<int>("DansUI.datetime.timeZoneOffset");
                return TimeSpan.FromHours(hoursOffset);
            }
            catch (Exception ex)
            {
                return TimeSpan.Zero;
            }
        }
    }
}
