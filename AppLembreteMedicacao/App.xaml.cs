using AppLembreteMedicacao.Helpers;

namespace AppLembreteMedicacao;

public partial class App : Application
{
public static SQLiteDatabaseHelper Banco { get; private set; }

public App()
{ InitializeComponent();

 if (Banco == null)
 { string caminho = Path.Combine(FileSystem.AppDataDirectory, "remedios.db3");
   Banco = new SQLiteDatabaseHelper(caminho); } 
   MainPage = new NavigationPage(new MainPage());
    
  }
}