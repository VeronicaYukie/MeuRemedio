using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppLembreteMedicacao.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nome { get; set; }

        [NotNull, Unique]
        public string Email { get; set; }

        [NotNull]
        public string SenhaHash { get; set; }

        public int Ativo { get; set; } = 1;
    }
}