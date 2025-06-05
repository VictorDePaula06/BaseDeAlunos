using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseAlunos.Models;

namespace BaseAlunos.ViewModel
{
    public class RetornoAlunos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataUltimoPagamento { get; set; }
        public DateTime DataProximoPagamento { get; set; }


        public static RetornoAlunos FromModel(ModeloAluno modeloAluno)
        {
            return new RetornoAlunos
            {
                Id = modeloAluno.Id,
                Nome = modeloAluno.Nome,
                Telefone = modeloAluno.Telefone,
                Idade = modeloAluno.Idade,
                DataUltimoPagamento = modeloAluno.DataUltimoPagamento,
                DataProximoPagamento = modeloAluno.DataUltimoPagamento.AddMonths((int)modeloAluno.Plano),

            };
        }
    }
}