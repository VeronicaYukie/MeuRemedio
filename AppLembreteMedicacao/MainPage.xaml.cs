using AppLembreteMedicacao.Models;
using AppLembreteMedicacao.Views;
using Plugin.LocalNotification;

namespace AppLembreteMedicacao;

public partial class MainPage : ContentPage
{
    private Medicamento _medicamentoParaEdicao;
    public MainPage()

    {
        InitializeComponent();

        // Zoom ao tocar no botão de cronograma
        btnCronograma.Pressed += async (s, e) => await btnCronograma.ScaleTo(1.2, 100);
        btnCronograma.Released += async (s, e) => await btnCronograma.ScaleTo(1, 100);

    }

    // Salvar remédio
    private async void AoClicarSalvar(object sender, EventArgs e)
    {
        // 1. Validação básica: não salva se o nome estiver vazio
        if (string.IsNullOrWhiteSpace(entNome.Text))
        {
            await DisplayAlert("Erro", "Por favor, preencha o nome do remédio.", "OK");
            return;
        }

        try
        {
            if (_medicamentoParaEdicao == null)
            {
                // --- BLOCO 1: NOVO REMÉDIO ---
                var novo = new Medicamento
                {
                    Nome = entNome.Text,
                    Dosagem = entDose.Text,
                    DataInicio = dtInicio.Date,
                    DataFim = dtFim.Date,
                    Ativo = 1
                };

                await App.Banco.InsertMedicamento(novo);

                // Notificação para novo cadastro
                var notifNovo = new NotificationRequest
                {
                    NotificationId = 1,
                    Title = "Remédio Cadastrado",
                    Description = $"O lembrete para {novo.Nome} foi criado!",
                    Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Now.AddSeconds(2) }
                };
                await LocalNotificationCenter.Current.Show(notifNovo);
            }
            else
            {
                // --- BLOCO 2: ATUALIZAR EXISTENTE ---
                _medicamentoParaEdicao.Nome = entNome.Text;
                _medicamentoParaEdicao.Dosagem = entDose.Text;
                _medicamentoParaEdicao.DataInicio = dtInicio.Date;
                _medicamentoParaEdicao.DataFim = dtFim.Date;

                await App.Banco.UpdateMedicamento(_medicamentoParaEdicao);

                // Notificação para atualização
                var notifEdit = new NotificationRequest
                {
                    NotificationId = 2,
                    Title = "Remédio Atualizado",
                    Description = $"As alterações em {_medicamentoParaEdicao.Nome} foram salvas!",
                    Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Now.AddSeconds(2) }
                };
                await LocalNotificationCenter.Current.Show(notifEdit);
            }

            // 3. Fecha a tela e volta para a lista após salvar
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Não foi possível salvar: " + ex.Message, "OK");
        }
    }

    // Abrir cronograma
    private async void AoClicarCronograma(object sender, EventArgs e)
    {
        var ultimoRemedio = await App.Banco.GetUltimoMedicamento();
        if (ultimoRemedio != null)
        { await Navigation.PushAsync(new CronogramaPage(ultimoRemedio.Id)); }
        else

        {
            await DisplayAlert("Atenção", "Cadastre um remédio antes de criar o cronograma.", "OK");

        }
    }

    private async void AoClicarNovoRemedio(object sender, EventArgs e)
    {
        // Este comando abre a tela que você criou
        await Navigation.PushAsync(new Novomedicacao());
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            // 1. Busca a lista de remédios que você salvou no Banco
            var lista = await App.Banco.GetMedicamentos();

            // 2. Coloca essa lista dentro do componente visual que você criou
            if (lista != null)
            {
                listaMedicamentos.ItemsSource = lista;
            }
        }
        catch (Exception ex)
        {
            // Caso ocorra algum erro na leitura do banco
            System.Diagnostics.Debug.WriteLine($"Erro ao carregar lista: {ex.Message}");
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var med = (sender as Button).CommandParameter as Medicamento;

        bool confirm = await DisplayAlert("Excluir", $"Deseja apagar {med.Nome}?", "Sim", "Não");

        if (confirm)
        {
            // Aqui você usa o ID que o seu método pede
            await App.Banco.DeleteMedicamento(med.Id);

            // Atualiza a lista na tela
            OnAppearing();
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {

    }

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // 1. Busca os remédios salvos no banco SQLite configurado ontem
        var lista = await App.Banco.GetMedicamentos();

        if (lista == null || lista.Count == 0)
        {
            await DisplayAlert("Prontuário", "Você ainda não tem remédios cadastrados.", "OK");
            return;
        }

        // 2. Monta o texto do prontuário formatado
        string prontuario = $"📋 MEU PRONTUÁRIO MÉDICO - {DateTime.Now:dd/MM/yyyy}\n\n";
        foreach (var m in lista)
        {
            prontuario += $"💊 {m.Nome} ({m.Dosagem})\n";
        }

        // 3. Abre a opção de compartilhar do celular (WhatsApp, E-mail, etc)
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Title = "Compartilhar Prontuário",
            Text = prontuario,
            Uri = "App Meu Remédio"
        });
    }
}
