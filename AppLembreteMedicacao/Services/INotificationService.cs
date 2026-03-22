namespace AppLembreteMedicacao.Services;

public interface INotificationService
{
    void AgendarNotificacao(int id, string titulo, string mensagem, DateTime dataHora);
    void CancelarNotificacao(int id);
}
