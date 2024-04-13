using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questao5.Data;
using Questao5.Domain.Entities;

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
        public async Task<IActionResult> MovimentarContaCorrente(string idContaCorrente, string tipoMovimento, decimal valor)
        {
            try
            {
                await _contaCorrenteService.MovimentarContaCorrente(idContaCorrente, tipoMovimento, valor);
                return Ok("Movimentação da conta corrente realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao movimentar a conta corrente: {ex.Message}");
            }
        }

    }
}
