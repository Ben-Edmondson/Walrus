using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WalrusClassLibrary.Auth;

namespace WalrusFront
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
                });
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


            var connectionStuff = new B2CConnectionDetails()
            {
                ClientId = builder.Configuration["Authentication:ClientId"],
            };
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LandingPage>();
            builder.Services.AddSingleton(new AuthenticationService(connectionStuff));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
