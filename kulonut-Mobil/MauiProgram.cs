using Android.Graphics.Drawables;
using CommunityToolkit.Maui;
using kulonut_Mobil.API;
using kulonut_Mobil.DependencyInjection;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using kulonut_Mobil.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Net;

namespace kulonut_Mobil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
				.ConfigureMauiHandlers(handlers =>
				{
#if ANDROID
					EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
					{
						handler.PlatformView.Background = null;
					});
					DatePickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
					{
						var nativeView = handler.PlatformView;
						nativeView.Background = null;
						nativeView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
					});
#endif
				});

			builder.Services
                .AddInfrastructure()
				.AddServices()
				.AddPages()
				.AddViewModels();

#if DEBUG
			builder.Logging.AddDebug();
        #endif
			return builder.Build();
        }
    }
}
