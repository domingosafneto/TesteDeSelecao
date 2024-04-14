using Microsoft.EntityFrameworkCore;
using Questao5.Data;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Services.Controllers;
using System.Resources;

namespace Questao5.Infrastructure.Services
{
    public class ContaCorrenteService
    {
        private readonly Questao5DbContext _context;
        private readonly ResourceManager _resourceManager;

        public ContaCorrenteService(Questao5DbContext context)
        {
            _context = context;
            _resourceManager = new ResourceManager("Questao5.Resources.MensagensErro", typeof(MovimentoController).Assembly);
        }

        public async Task MovimentarContaCorrente(string idContaCorrente, string tipoMovimento, decimal valor)
        {
            var contaCorrente = await _context.ContasCorrentes.FindAsync(idContaCorrente);
            
            if (contaCorrente == null)  
            {
                throw new Exception(_resourceManager.GetString("INVALID_ACCOUNT"));
            }

            if (contaCorrente.Ativo == 0) 
            {
                throw new Exception(_resourceManager.GetString("INACTIVE_ACCOUNT"));
            }

            if (tipoMovimento != "C" && tipoMovimento != "D")
            {
                throw new Exception(_resourceManager.GetString("INVALID_TYPE"));
            }

            if (valor < 0)  
            {
                throw new Exception(_resourceManager.GetString("INVALID_VALUE"));
            }

            var movimento = new Movimento
            {
                IdMovimento = await GerarNovoIdMovimento(),
                IdContaCorrente = idContaCorrente,
                DataMovimento = DateTime.Now,
                TipoMovimento = tipoMovimento,
                Valor = valor
            };

            _context.Movimentos.Add(movimento);
            await _context.SaveChangesAsync();
        }

        private async Task<int> GerarNovoIdMovimento()
        {
            var ultimoId = await _context.Movimentos.OrderByDescending(m => m.IdMovimento).Select(m => m.IdMovimento).FirstOrDefaultAsync();
            return ultimoId + 1;
        }

        public async Task<decimal> GetSaldoContaCorrente(string idContaCorrente)
        {
            var contaCorrente = await _context.ContasCorrentes.FindAsync(idContaCorrente);

            if (contaCorrente == null)
            {
                throw new Exception(_resourceManager.GetString("INVALID_ACCOUNT"));
            }

            if (contaCorrente.Ativo == 0)
            {
                throw new Exception(_resourceManager.GetString("INACTIVE_ACCOUNT"));
            }

            var saldo = await _context.Movimentos
                .Where(m => m.IdContaCorrente == idContaCorrente)
                .SumAsync(m => m.TipoMovimento == "C" ? m.Valor : -m.Valor);

            return saldo;
        }

        // retorna o nome do titular da conta corrente
        public async Task<string> GetNomeTitularContaCorrente(string idContaCorrente)
        {
            var contaCorrente = await _context.ContasCorrentes.FindAsync(idContaCorrente);
            if (contaCorrente == null)
            {
                throw new Exception("Conta corrente não encontrada.");
            }

            return contaCorrente.Nome;
        }

        // pega o núnero de uma conta corrente
        public async Task<int> GetNumeroContaCorrente(string idContaCorrente)
        {
            var contaCorrente = await _context.ContasCorrentes.FindAsync(idContaCorrente);
            if (contaCorrente == null)
            {
                throw new Exception("Conta corrente não encontrada.");
            }

            return contaCorrente.Numero;
        }

    }
}
