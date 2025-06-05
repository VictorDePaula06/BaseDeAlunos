using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseAlunos.ViewModel.Enuns;

namespace BaseAlunos.Models
{
    public class ModeloAluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }   
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; } // calculo a partir da data de nascimento
        public string Logradouro { get; set; }
        public int NumeroLogradouro { get; set; }
        public DateTime DataUltimoPagamento { get; set; }
        public DateTime DataProximoPagamento { get; set; } // calculo a partir da data do ultimo pagamento
        public FormaPagamento FormaPagamento { get; set; } // enum
        public TipoPlano Plano { get; set; } // enum
        public bool Ativo { get; set; } // aluno ja vai ser cadastrado como ativo

        
    }
}