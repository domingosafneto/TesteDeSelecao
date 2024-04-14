namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public Movimento() 
        { 
            IdContaCorrente = "";
            TipoMovimento = "";
        
        }

        public int IdMovimento { get; set; }

        public string IdContaCorrente { get; set; }

        public DateTime DataMovimento { get; set; }

        public string TipoMovimento { get; set; }

        public decimal Valor { get; set; }
    }
}
