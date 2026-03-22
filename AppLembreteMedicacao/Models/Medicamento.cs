using SQLite;

namespace AppLembreteMedicacao.Models
{
    public class Medicamento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Adicione o "= string.Empty;" ao final de cada string
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Dosagem { get; set; } = string.Empty;
        public string DataInicio { get; set; } = string.Empty;
        public string DataFim { get; set; } = string.Empty;

        public int Ativo { get; set; } = 1;

        [Ignore]
        public List<Cronograma> Horarios { get; set; }
    }
}