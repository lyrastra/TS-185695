namespace Moedelo.AccountingV2.Dto.Payments
{
    public class BalanceByWorkerDto
    {
        public decimal Balance { get; set; }

        public int AccountCode { get; set; }

        public int WorkerId { get; set; }
    }
}