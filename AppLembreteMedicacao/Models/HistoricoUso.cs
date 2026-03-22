using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppLembreteMedicacao.Models
{
    public class HistoricoUso
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int MedicamentoId { get; set; }

        public int CronogramaId { get; set; }

        public string DataUso { get; set; }

        public int Tomado { get; set; } = 0;

        public string DataConfirmacao { get; set; }
        public bool Notificado { get; set; }
    }
}
