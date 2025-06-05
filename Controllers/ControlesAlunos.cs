using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BaseAlunos.Data;
using BaseAlunos.Migrations;
using BaseAlunos.Models;
using BaseAlunos.ViewModel;
using BaseAlunos.ViewModel.Enuns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace BaseAlunos.Controllers
{
    [ApiController]
    [Route("api/v1/alunos")]
    public class ControlesAlunos : ControllerBase
    {
        private readonly AppDbContext context;

        public ControlesAlunos(AppDbContext context)
        {
            this.context = context;
        }




        // método para listar os contatos cadastrados
        [HttpGet]
        [Route("Listar Alunos")]
        public async Task<IActionResult> GetAtivosAsync(
            [FromServices] AppDbContext context)
        {
            var aluno = await context.Alunos // nome da tabela Alunos
            .Where(aluno => aluno.Ativo)
            .AsNoTracking()
            .ToListAsync();

            var resultado = aluno.Select(RetornoAlunos.FromModel);
            return Ok(resultado);
        }


        // método para listar os contatos inativos
        [HttpGet]
        [Route("Listar Alunos Inativos")]
        public async Task<IActionResult> GetInativosAsync(
       [FromServices] AppDbContext context)
        {
            var aluno = await context.Alunos // nome da tabela Alunos
            .Where(aluno => !aluno.Ativo)
            .AsNoTracking()
            .ToListAsync();

            var resultado = aluno.Select(RetornoAlunos.FromModel);
            return Ok(resultado);
        }


        //método para ver detalhes dos dados do aluno selecionado pelo Id 
        [HttpGet]
        [Route("Listar Alunos detalhados/{Id}")]
        public async Task<IActionResult> GetAtivosDetalhadosAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int Id)
        {
            var aluno = await context.Alunos // nome da tabela Pessoas
            .Where(aluno => aluno.Ativo)
            .AsNoTracking()
            .FirstOrDefaultAsync(aluno => aluno.Id == Id && aluno.Ativo);

            if (aluno == null)
                return NotFound(new { mensagem = "Aluno nao encontrado." });

            var resultado = RetornoDetalhesAlunos.FromModel(aluno);
            return Ok(resultado);
        }

        //método para cadastar um aluno
        [HttpPost]
        [Route("Cadastrar um Aluno")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreatAlunoViewModel model)

        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var DataNascimento = model.GetDataNascimento();
                var idade = model.CalcularIdade(DataNascimento);
                var dataUltimoPagamento = model.GetDataUltimoPagamento();
                var dataProximoPagamento = model.CalcularProximoPagamento(dataUltimoPagamento);

                var aluno = new ModeloAluno
                {
                    Nome = model.Nome,
                    Telefone = model.Telefone,
                    Email = model.Email,
                    DataNascimento = DataNascimento,
                    Idade = idade,
                    Logradouro = model.Logradouro,
                    NumeroLogradouro = model.NumeroLogradouro,
                    DataUltimoPagamento = dataUltimoPagamento,
                    DataProximoPagamento = dataProximoPagamento,
                    FormaPagamento = model.FormaPagamento,
                    Plano = model.Plano,
                    Ativo = true

                };

                await context.Alunos.AddAsync(aluno);
                await context.SaveChangesAsync();
                return Created($"Versao1.0/Pessoas/{aluno.Id}", new
                {
                    mensagem = $"Aluno {aluno.Nome} cadastrado com sucesso",
                    aluno
                }
                );
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //método para editar o aluno cadastrado e ativo
        [HttpPatch("{Id}")]
        public async Task<IActionResult> PatchAsync(
            [FromRoute] int Id,
            [FromBody] PatchAlunoViewModel model,
            [FromServices] AppDbContext context)
        {
            var aluno = await context.Alunos.FirstOrDefaultAsync(a => a.Id == Id);

            if (aluno == null)
                return NotFound(new { mensagem = "Aluno não encontrado." });

            // Só atualiza se foi enviado
            if (!string.IsNullOrWhiteSpace(model.Nome))
                aluno.Nome = model.Nome;

            if (!string.IsNullOrWhiteSpace(model.Telefone))
                aluno.Telefone = model.Telefone;

            if (!string.IsNullOrWhiteSpace(model.Email))
                aluno.Email = model.Email;

            if (!string.IsNullOrWhiteSpace(model.FormaPagamento))
            {
                if (Enum.TryParse<FormaPagamento>(model.FormaPagamento, true, out var formaEnum) &&
                    Enum.IsDefined(typeof(FormaPagamento), formaEnum))
                {
                    aluno.FormaPagamento = formaEnum;
                }
                else
                {
                    return BadRequest("Forma de pagamento inválida. Use: Pix, Dinheiro ou Cartao.");
                }
            }

            if (!string.IsNullOrWhiteSpace(model.Plano))
            {
                if (Enum.TryParse<TipoPlano>(model.Plano, true, out var planoEnum) &&
                    Enum.IsDefined(typeof(TipoPlano), planoEnum))
                {
                    aluno.Plano = planoEnum;
                }
                else
                {
                    return BadRequest("Plano inválido. Use: Mensal, Trimestral ou Anual.");
                }
            }

            await context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = $"Aluno {aluno.Nome} atualizado com sucesso.",
                aluno
            });
        }



        //método para inativar um aluno cadastrado
        [HttpPut]
        [Route("Inativar Aluno/{Id}")]
        public async Task<IActionResult> DesativarAlunoAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int Id)

        {
            var aluno = await context.Alunos.FirstOrDefaultAsync(aluno => aluno.Id == Id && aluno.Ativo);
            if (aluno == null)
                return NotFound("Aluno nao encontrado");
            aluno.Ativo = false;

            context.Alunos.Update(aluno);
            await context.SaveChangesAsync();

            return Ok($"Aluno {aluno.Nome} Desativado com sucesso");


        }

        //método para ativar um aluno que foi inativado
        [HttpPut]
        [Route("Ativar Aluno/{Id}")]
        public async Task<IActionResult> AtivarAlunoAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int Id)

        {
            var aluno = await context.Alunos.FirstOrDefaultAsync(aluno => aluno.Id == Id && !aluno.Ativo);
            if (aluno == null)
                return NotFound("Aluno nao encontrado");
            aluno.Ativo = true;

            context.Alunos.Update(aluno);
            await context.SaveChangesAsync();

            return Ok($"Aluno {aluno.Nome} Ativado com sucesso");


        }

        //método para deletar do banco um aluno cadastrado e ativo
        [HttpDelete]
        [Route("Deletar Aluno/{Id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int Id)

        {
            var aluno = await context.Alunos.FirstOrDefaultAsync(aluno => aluno.Id == Id && aluno.Ativo);
            try
            {
                context.Alunos.Remove(aluno);
                await context.SaveChangesAsync();

                return Ok($"Aluno {aluno.Nome} deletado com sucesso");

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar dados do aluno: {ex.Message}");
            }
        }




    }
}