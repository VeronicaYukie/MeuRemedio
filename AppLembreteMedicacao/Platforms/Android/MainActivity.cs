using Android.App;
using Android.Content.PM;
using Android.OS;               // adicionado 07/04
using Plugin.LocalNotification; // adicionado 07/04

namespace AppLembreteMedicacao
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, 
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
    ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity // adicionado 07/04
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Adicione isso para processar o clique na notificação 07/04
            LocalNotificationCenter.CreateNotificationChannel();
        }
    }
}
