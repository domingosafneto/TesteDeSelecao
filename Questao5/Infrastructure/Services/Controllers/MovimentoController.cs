using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questao5.Data;
using Questao5.Domain.Entities;
using Questao5.Domain.Response;
using System.Resources;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentoController : ControllerBase
    {
        private readonly ContaCorrenteService _contaCorrenteService;
        
        public MovimentoController(Questao5DbContext context, ContaCorrenteService contaCorrenteService)
        {
            _contaCorrenteService = contaCorrenteService;            
        }

        [HttpPost("movimentar")]
        public async Task<IActionResult> MovimentarContaCorrente(string idContaCorrente, string tipoMovimento, decimal valor, string chaveIdempotencia)
        {
            try
            {
                await _contaCorrenteService.MovimentarContaCorrente(idContaCorrente, tipoMovimento, valor, chaveIdempotencia);
                return Ok("Movimentação da conta corrente realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao movimentar a conta corrente: {ex.Message}");
            }
        }


        [HttpGet("{idContaCorrente}/saldo")]
        public async Task<IActionResult> GetSaldoContaCorrente(string idContaCorrente)
        {
            try
            {
                decimal saldo = await _contaCorrenteService.GetSaldoContaCorrente(idContaCorrente);

                string nomeTitular = await _contaCorrenteService.GetNomeTitularContaCorrente(idContaCorrente);
                int numeroConta = await _contaCorrenteService.GetNumeroContaCorrente(idContaCorrente);

                if (saldo == 0)
                {
                    return Ok(new SaldoContaCorrenteResponse
                    {
                        Saldo = 0   
                    });
                }

                var response = new SaldoContaCorrenteResponse
                {
                    Saldo = saldo,
                    NumeroContaCorrente = numeroConta,
                    DataHoraRequisicao = DateTime.Now,
                    TitularContaCorrente = nomeTitular,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro obter saldo da conta corrente: {ex.Message}");
            }
        }
    }
}
