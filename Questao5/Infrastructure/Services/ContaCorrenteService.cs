using Microsoft.EntityFrameworkCore;
using Questao5.Data;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services
{
    public class ContaCorrenteService
    {
        private readonly Questao5DbContext _context;

        public ContaCorrenteService(Questao5DbContext context)
        {
            _context = context;
        }

        public async Task MovimentarContaCorrente(string idContaCorrente, string tipoMovimento, decimal valor)
        {
            // Verificar se a conta corrente está ativa
            var contaCorrente = await _context.ContasCorrentes.FindAsync(idContaCorrente);
            if (contaCorrente == null || contaCorrente.Ativo == 0)
            {
                throw new Exception("Conta corrente não encontrada ou inativa.");
            }

            // Verificar se o tipo de movimento é válido
            if (tipoMovimento != "C" && tipoMovimento != "D")
            {
                throw new Exception("Tipo de movimento inválido.");
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

        public async Task<decimal> ObterSaldoContaCorrente(string idContaCorrente)
        {
            var saldo = await _context.Movimentos
                .Where(m => m.IdContaCorrente == idContaCorrente)
                .SumAsync(m => m.TipoMovimento == "C" ? m.Valor : -m.Valor);

            return saldo;
        }

    }
}
