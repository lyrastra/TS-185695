namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.ClosedPeriods
{
    public class StockProductBalanceDto
    {
        public long ProductId { get; set; }

        public long StockId { get; set; }

        public string ProductName { get; set; }

        public decimal Balance { get; set; }

        public bool IsMediationProduct { get; set; }

        public string DivisionName { get; set; }

        public int DivisionId { get; set; }
    }
}
