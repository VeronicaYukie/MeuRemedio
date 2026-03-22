using AppLembreteMedicacao.Views; // onde estará CronogramaPage

namespace AppLembreteMedicacao;

public partial class AppShell : Shell
{
public AppShell()
{ InitializeComponent();

// Registrar rota para CronogramaPage
Routing.RegisterRoute(nameof(CronogramaPage), typeof(CronogramaPage));
   
 }
}