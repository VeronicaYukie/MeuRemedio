using Plugin.LocalNotification;
using AppLembreteMedicacao.Models;
using System.Threading.Tasks;

namespace AppLembreteMedicacao.Views;

[QueryProperty(nameof(MedicamentoId), "medicamentoId")]
public partial class CronogramaPage : ContentPage
{
public int MedicamentoId { get; set; }


public CronogramaPage(int medicamentoId)
{
  InitializeComponent();
  MedicamentoId = medicamentoId;

 // Zoom no botão adicionar horário
 btnAdicionarHorario.Pressed += async (s, e) => await btnAdicionarHorario.ScaleTo(1.2, 100);
 btnAdicionarHorario.Released += async (s, e) => await btnAdicionarHorario.ScaleTo(1, 100);
 
}
    
 protected override async void OnAppearing()
{ base.OnAppearing(); await CarregarCronogramas(); }

 private async Task CarregarCronogramas()
 { var cronogramas = await App.Banco.GetCronogramaPorMedicamento(MedicamentoId);
   listaCronogramas.ItemsSource = cronogramas; }



 // Adicionar horário com popup e confirmação
 private async void AoClicarAdicionarHorario(object sender, EventArgs e)
{
  // Solicita hora
  string hora = await DisplayPromptAsync(
  "Adicionar Horário", "Digite a hora do remédio (HH:mm):",
  placeholder: "08:00",
  maxLength: 5,
  keyboard: Keyboard.Text 
  
  );
  
  if (string.IsNullOrWhiteSpace(hora)) return;

  // Solicita frequência
  string frequencia = await DisplayPromptAsync(
  "Frequência","Digite a frequência (diária/semanal):",
   initialValue: "diária",
   keyboard: Keyboard.Text
     
   );
       
   if (string.IsNullOrWhiteSpace(frequencia)) return;

  //  Confirmação antes de salvar
  bool confirmar = await DisplayAlert(
   "Confirmar Horário",
   $"Deseja adicionar o horário {hora} ({frequencia})?",
   "Confirmar ✔️",
   "Cancelar ❌"

   );
       
   if (!confirmar) return;

   //  Cria e salva no banco
  var novoCronograma = new Cronograma
  {
  MedicamentoId = MedicamentoId, Hora = hora,Frequencia = frequencia,Ativo = 1 };
  await App.Banco.InsertCronograma(novoCronograma);

  //  Atualiza a lista
  await CarregarCronogramas();
  await DisplayAlert("Sucesso", $"Horário {hora} ({frequencia}) adicionado!", "OK");
  }

  // Remover horário com botão de confirmação
  private async void AoClicarRemoverHorario(object sender, EventArgs e)
  {
   
   if (sender is not Button button) return;
   int cronogramaId = (int)button.CommandParameter;

   bool confirmar = await DisplayAlert(
   "Confirmação",
   "Deseja realmente remover este horário?",
   "Sim",
   "Não"
       
    );
        
    if (!confirmar) return;
    await App.Banco.DeleteCronograma(cronogramaId);
    await CarregarCronogramas(); // Atualiza a lista
   
    }

}