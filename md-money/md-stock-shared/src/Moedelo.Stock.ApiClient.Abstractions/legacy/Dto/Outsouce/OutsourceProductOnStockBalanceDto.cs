using System;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Outsouce
{
    public class OutsourceProductOnStockBalanceDto
    {
        public long StockId { get; set; }

        public long ProductId { get; set; }

        public DateTime Date { get; set; }

        public decimal Balance { get; set; }
    }
}