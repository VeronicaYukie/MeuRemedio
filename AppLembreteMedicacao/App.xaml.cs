using AppLembreteMedicacao.Helpers;

namespace AppLembreteMedicacao;

public partial class App : Application
{
public static SQLiteDatabaseHelper Banco { get; private set; }

    public App()
    {
        InitializeComponent();

        // habilita a barra azul/roxa no topo com os menus 09/04
        MainPage = new NavigationPage(new MainPage());
    
        // 1. Configura o Banco de Dados 07/04
        if (Banco == null)
        {
            string caminho = Path.Combine(FileSystem.AppDataDirectory, "remedios.db3");
            Banco = new SQLiteDatabaseHelper(caminho);
        }
        // 2. Define a página inicial com suporte a navegação (ESSENCIAL) 07/04
        MainPage = new NavigationPage(new MainPage());
    }
}