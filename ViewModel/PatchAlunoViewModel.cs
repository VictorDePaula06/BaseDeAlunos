using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseAlunos.ViewModel
{
    public class PatchAlunoViewModel
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string DataNascimento { get; set; }
        public string Logradouro { get; set; }
        public int NumeroLogradouro { get; set; }

        /// <summary>
        /// Plano (Mensal, Trimestral, Anual)
        /// </summary>
        public string Plano { get; set; }
        /// <summary>
        /// Forma de pagamento (Pix, Dinheiro, Cartao)
        /// </summary>
        public string FormaPagamento { get; set; }
    }
}