using AppLembreteMedicacao.Models;

namespace AppLembreteMedicacao.Views;

public partial class AdicionarHorarioPage : ContentPage
{
    public int MedicamentoId { get; set; }

    public event EventHandler<Cronograma> HorarioAdicionado; // para notificar o CronogramaPage

    public AdicionarHorarioPage(int medicamentoId)
    {
        InitializeComponent();
        MedicamentoId = medicamentoId;
    }

    private void Cancelar_Clicked(object sender, EventArgs e)
    {
        CloseModal();
    }

    private async void Adicionar_Clicked(object sender, EventArgs e)
    {
        string hora = timePicker.Time.ToString(@"hh\:mm");
        string frequencia = pickerFrequencia.SelectedItem?.ToString() ?? "Diária";

        var novoCronograma = new Cronograma
        {
            MedicamentoId = MedicamentoId,
            Hora = hora,
            Frequencia = frequencia,
            Ativo = 1
        };

        await App.Banco.InsertCronograma(novoCronograma);

        HorarioAdicionado?.Invoke(this, novoCronograma); // notifica a página principal

        await DisplayAlert("Sucesso", $"Horário {hora} ({frequencia}) adicionado!", "OK");

        CloseModal();
    }

    private async void CloseModal()
    {
        await Navigation.PopModalAsync();
    }
}