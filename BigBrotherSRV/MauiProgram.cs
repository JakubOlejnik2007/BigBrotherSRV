using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
#if WINDOWS
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace BigBrotherSRV
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-ExtraLight.ttf", "RobotoExtraLight");
                    fonts.AddFont("Roboto-Light.ttf", "RobotoLight");
                    fonts.AddFont("Roboto-Regular.ttf", "Roboto");
                    fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");

                });

#if WINDOWS
            //builder.ConfigureLifecycleEvents(events =>
            //{
            //    events.AddWindows(w =>
            //    {
            //        w.OnWindowCreated(window =>
            //        {
            //            // Removes MAUI's own white title bar strip
            //            window.ExtendsContentIntoTitleBar = false;
            //            window.SystemBackdrop = null;

            //            // Color the native Windows title bar black
            //            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            //            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            //            var appWindow = AppWindow.GetFromWindowId(windowId);

            //            if (AppWindowTitleBar.IsCustomizationSupported())
            //            {
            //                var titleBar = appWindow.TitleBar;
            //                titleBar.ExtendsContentIntoTitleBar = false;

            //                titleBar.BackgroundColor         = Microsoft.UI.Colors.Black;
            //                titleBar.ForegroundColor         = Microsoft.UI.Colors.White;
            //                titleBar.InactiveBackgroundColor = Microsoft.UI.Colors.Black;
            //                titleBar.InactiveForegroundColor = Microsoft.UI.Colors.Gray;

            //                titleBar.ButtonBackgroundColor          = Microsoft.UI.Colors.Black;
            //                titleBar.ButtonForegroundColor          = Microsoft.UI.Colors.White;
            //                titleBar.ButtonInactiveBackgroundColor  = Microsoft.UI.Colors.Black;
            //                titleBar.ButtonInactiveForegroundColor  = Microsoft.UI.Colors.Gray;
            //                titleBar.ButtonHoverBackgroundColor     = Microsoft.UI.Colors.DarkGray;
            //                titleBar.ButtonHoverForegroundColor     = Microsoft.UI.Colors.White;
            //                titleBar.ButtonPressedBackgroundColor   = Microsoft.UI.Colors.Gray;
            //                titleBar.ButtonPressedForegroundColor   = Microsoft.UI.Colors.White;
            //            }
            //        });
            //    });
            //});
#endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}