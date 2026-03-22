using SQLite;

namespace AppLembreteMedicacao.Models
{
    public class Cronograma
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int MedicamentoId { get; set; }

        public string Hora { get; set; }

        public string Frequencia { get; set; }//semanal, diaria

        public int IntervaloHoras { get; set; }

        public int Ativo { get; set; } = 1;
    }
}