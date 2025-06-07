using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseAlunos.ViewModel
{
    public class PatchAlunoViewModel
    {
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Plano { get; set; }  // "Mensal", etc.
        public string? FormaPagamento { get; set; } // "Pix", etc.
        public string? Logradouro { get; set; }
        public int? NumeroLogradouro { get; set; } // <-- Aqui precisa ser int?
        public string? DataNascimento { get; set; } // yyyy-MM-dd
        public decimal? ValorPago { get; set; }

    }
}