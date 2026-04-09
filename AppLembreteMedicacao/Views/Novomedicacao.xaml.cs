namespace AppLembreteMedicacao.Views;
using Plugin.LocalNotification; //07/04
using AppLembreteMedicacao.Models; // 07/04

public partial class Novomedicacao : ContentPage
{
    public Novomedicacao()
    {
        InitializeComponent();
    }

    async void OnSalvarClicked(object sender, EventArgs e)
    {
        // objeto com os dados da tela
        var novoMed = new Medicamento
        {
            Nome = txtNome.Text, 
            Dosagem = txtDosagem.Text,
            IntervaloHoras = int.Parse(txtIntervalo.Text),

            // TIMEZONE: Pegamos a data/hora local e convertemos para UTC para o banco
            DataInicio = DateTime.Now.ToUniversalTime()
        };

        // 2. Salvamos no Banco de Dados 
        // await App.Database.SaveMedicamentoAsync(novoMed);
        await App.Banco.InsertMedicamento(novoMed);

        // 3. AGENDAMENTO DA NOTIFICAÇÃO
        var request = new NotificationRequest
        {
            NotificationId = novoMed.Id,
            Title = "Hora do seu Remédio!",
            Description = $"Tomar: {novoMed.Nome} - {novoMed.Dosagem}",
            Schedule = new NotificationRequestSchedule
            {
                // O Plugin cuida da conversão de volta para o horário do celular
                NotifyTime = novoMed.DataInicio.ToLocalTime(),
                RepeatType = NotificationRepeat.TimeInterval,
                NotifyRepeatInterval = TimeSpan.FromHours(novoMed.IntervaloHoras)
            }
        };

        await LocalNotificationCenter.Current.Show(request);

        await DisplayAlert("Sucesso", "Lembrete configurado!", "OK");
        await Navigation.PopAsync();
    }

    private Medicamento _medicamentoParaEdicao;

    // Construtor para EDITAR (preenche os campos com os dados do banco)
    public Novomedicacao(Medicamento med)
    {
        InitializeComponent();
        _medicamentoParaEdicao = med;

        // Preenche a tela com os valores atuais para você poder mudar
        txtNome.Text = med.Nome;
        txtDosagem.Text = med.Dosagem;
        // Se você tiver um campo para intervalo, preencha-o aqui também
    }
}