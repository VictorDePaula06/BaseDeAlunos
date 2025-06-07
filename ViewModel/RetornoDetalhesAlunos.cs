using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseAlunos.Models;
using BaseAlunos.ViewModel.Enuns;

namespace BaseAlunos.ViewModel
{
    public class RetornoDetalhesAlunos
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; } // calculo a partir da data de nascimento
        public string? Logradouro { get; set; }
        public int NumeroLogradouro { get; set; }
        public DateTime DataUltimoPagamento { get; set; }
        public DateTime DataProximoPagamento { get; set; } // calculo a partir da data do ultimo pagamento
        public string? FormaPagamento { get; set; } // enum
        public string? Plano { get; set; } // enum
        public decimal ValorPago { get; set; }




        public static RetornoDetalhesAlunos FromModel(ModeloAluno modeloAluno)
        {
            return new RetornoDetalhesAlunos
            {
                Id = modeloAluno.Id,
                Nome = modeloAluno.Nome,
                Telefone = modeloAluno.Telefone,
                Email = modeloAluno.Email,
                DataNascimento = modeloAluno.DataNascimento,
                Idade = modeloAluno.Idade,
                Logradouro = modeloAluno.Logradouro,
                NumeroLogradouro = modeloAluno.NumeroLogradouro,
                DataUltimoPagamento = modeloAluno.DataUltimoPagamento,
                DataProximoPagamento = modeloAluno.DataUltimoPagamento.AddMonths((int)modeloAluno.Plano),
                FormaPagamento = modeloAluno.FormaPagamento.ToString(),
                Plano = modeloAluno.Plano.ToString(),
                ValorPago = modeloAluno.ValorPago
            };

        }


    }
}