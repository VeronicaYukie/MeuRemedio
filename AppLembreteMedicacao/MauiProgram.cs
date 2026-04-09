using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;  //Adicionado em 07/04
using System.Globalization; 

namespace AppLembreteMedicacao
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // Defina a cultura para Português do Brasil
            var culturaBr = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = culturaBr;
            CultureInfo.DefaultThreadCurrentUICulture = culturaBr;

            var builder = MauiApp.CreateBuilder(); builder
            .UseMauiApp<App>()
            .UseLocalNotification() //Adicionado em 07/04
            .ConfigureFonts(fonts =>
     {

         fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
         fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

     });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
