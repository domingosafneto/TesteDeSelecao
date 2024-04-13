using Microsoft.EntityFrameworkCore;
using Questao5.Domain.Entities;

namespace Questao5.Data
{
    public class Questao5DbContext : DbContext
    {
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<ContaCorrente> ContasCorrentes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("localhost");
        }
    }
}
