using AppLembreteMedicacao.Models;
using AppLembreteMedicacao.Views;
using Plugin.LocalNotification;

namespace AppLembreteMedicacao;

public partial class MainPage : ContentPage
{
public MainPage()
 
{ InitializeComponent();

// Zoom ao tocar no botão de cronograma
btnCronograma.Pressed += async (s, e) => await btnCronograma.ScaleTo(1.2, 100);
btnCronograma.Released += async (s, e) => await btnCronograma.ScaleTo(1, 100);
    
 }

// Salvar remédio
private async void AoClicarSalvar(object sender, EventArgs e)
{
  if (string.IsNullOrWhiteSpace(entNome.Text))

{ await DisplayAlert("Atenção", "Digite o nome do remédio.", "OK"); return; }

  var novoRemedio = new Medicamento
{
  Nome = entNome.Text,
  Dosagem = entDose.Text,
  DataInicio = dtInicio.Date.ToString("dd/MM/yyyy"),
  DataFim = dtFim.Date.ToString("dd/MM/yyyy"),
  Ativo = 1 };

  await App.Banco.InsertMedicamento(novoRemedio);

  await DisplayAlert("Sucesso!", $"{novoRemedio.Nome} cadastrado.", "OK");

  entNome.Text = string.Empty;
  entDose.Text = string.Empty;

      
 // Notificação de teste
 var notification = new NotificationRequest
{
  NotificationId = 1,
  Title = "Remédio cadastrado",
  Description = $"O remédio {novoRemedio.Nome} foi salvo!",
  Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Now.AddSeconds(3) }
 
 };
    await LocalNotificationCenter.Current.Show(notification); }

 // Abrir cronograma
 private async void AoClicarCronograma(object sender, EventArgs e)
 {
  var ultimoRemedio = await App.Banco.GetUltimoMedicamento();
  if (ultimoRemedio != null)
 { await Navigation.PushAsync(new CronogramaPage(ultimoRemedio.Id)); }
   else
   
  { await DisplayAlert("Atenção", "Cadastre um remédio antes de criar o cronograma.", "OK");
   
  }
 }
}