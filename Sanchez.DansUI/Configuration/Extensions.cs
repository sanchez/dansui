using Microsoft.Extensions.DependencyInjection;

using Sanchez.DansUI.Components.Overlay;
using Sanchez.DansUI.IServices;
using Sanchez.DansUI.Services;
using Sanchez.DansUI.Theming;

namespace Sanchez.DansUI.Configuration
{
    public static class Extensions
    {
        public static void AddDansUI(this IServiceCollection services, bool isClientSide)
        {
            services.AddScoped<ThemeLoader>();
            services.AddScoped<IBrowserService, BrowserService>();
            services.AddScoped<IPopperService, PopperService>();
            services.AddScoped<IModalService, ModalService>();
            services.AddScoped<IToastService, ToastService>();
            services.AddScoped<ICommanderService, CommanderService>();
        }

        public static void AddDansUI(this IServiceCollection services, UITheme defaultTheme, bool isClientSide)
        {
            services.AddScoped<UITheme>(x => defaultTheme);
            services.AddDansUI(isClientSide);
        }
    }
}
