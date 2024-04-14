namespace Questao5.Domain.Response
{
    public class SaldoContaCorrenteResponse
    {
        public decimal Saldo { get; set; }
        public DateTime DataHoraRequisicao { get; set; }
        public int NumeroContaCorrente { get; set; }
        public string? TitularContaCorrente { get; set; }
    }
}
