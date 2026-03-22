using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppLembreteMedicacao.Models
{
    internal class Lembrete
    {
    [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CronogramaId { get; set; }

        public DateTime DataHoraProgramada { get; set; }

        public bool Enviada { get; set; }

    }
}
