using SQLite;

namespace AppLembreteMedicacao.Models
{
    public class Medicamento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Dosagem { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; } // Mudou-se string para DataTime  07/04
        public DateTime? DataFim { get; set; } // Mudou-se string para DataTime, o '?' permite que seja nulo (para remédios de uso contínuo) 07/04
        public int IntervaloHoras { get; set; } // Se o usuário digitar '8', o app avisará de 8 em 8 horas 07/04
        public int Ativo { get; set; } = 1;

        [Ignore]
        public List<Cronograma> Horarios { get; set; } = new(); // acrescentou-se new() 07/04

        // Propriedade auxiliar para facilitar a exibição na tela (converte UTC -> Local) 07/04
        [Ignore]
        public string HoraFormatada => DataInicio.ToLocalTime().ToString("HH:mm");

    }
}