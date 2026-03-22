using Android.App;
using Android.Runtime;
using Android.OS;

namespace AppLembreteMedicacao
{
    [Application]
    public class MainApplication : MauiApplication
    {
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
    : base(handle, ownership)
    {
    }

     protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override void OnCreate()
  { base.OnCreate(); CriarCanalNotificacao(); }

     void CriarCanalNotificacao()
    {
     if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
     
     { var channel = new NotificationChannel(
      "medicacao_channel","Lembrete de Medicamentos",NotificationImportance.High
       
     );

     var manager = (NotificationManager)GetSystemService(NotificationService);
     manager.CreateNotificationChannel(channel);
      
     }
     }
}
}