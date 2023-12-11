namespace Questao5.Models
{
    public class Movimento
    {
        public int IdMovimento { get; set; }

        public int IdContaCorrente { get; set; }

        public DateTime DataMovimento { get; set; }

        public string TipoMovimento { get; set; }

        public decimal Valor {  get; set; }
    }
}
