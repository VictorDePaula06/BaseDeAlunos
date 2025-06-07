using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BaseAlunos.ViewModel.Enuns;


namespace BaseAlunos.ViewModel
{
    public class CreatAlunoViewModel
    {
        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? Telefone { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? DataNascimento { get; set; }

        [Required]
        public string? Logradouro { get; set; }

        [Required]
        public int NumeroLogradouro { get; set; }

        [Required]
        public string? DataUltimoPagamento { get; set; }

        [Required]
        public FormaPagamento FormaPagamento { get; set; }

        [Required]
        public decimal ValorPago {get; set; }


        [Required]
        public TipoPlano Plano { get; set; }

        public DateTime GetDataNascimento()
        {
            if (!DateTime.TryParseExact(DataNascimento, "dd/MM/yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.None, out DateTime data))
                throw new Exception("Data de nascimento invalida");
            if (data >= DateTime.Today)
                throw new Exception("Data de nascimento nao pode ser hoje ou no futuro.");

            return data;
        }

        public int CalcularIdade(DateTime nascimento)
        {
            var hoje = DateTime.Today;
            int idade = hoje.Year - nascimento.Year;
            if (nascimento > hoje.AddYears(-idade)) idade--;

            return idade;

        }

        public DateTime GetDataUltimoPagamento()
        {
            if (!DateTime.TryParseExact(DataUltimoPagamento, "dd/MM/yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.None, out DateTime data))
                throw new Exception("Data de Pagamento invalida");
            if (data > DateTime.Today)
                throw new Exception("Data de pagamento nao pode ser no futuro.");

            return data;
        }

        public DateTime CalcularProximoPagamento(DateTime ultimoPagamento)
        {
            return ultimoPagamento.AddMonths((int)Plano);
        }

      
    }
}