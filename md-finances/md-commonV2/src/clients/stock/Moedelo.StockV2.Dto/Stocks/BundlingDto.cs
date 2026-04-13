using System;

namespace Moedelo.StockV2.Dto.Stocks
{
    public class BundlingDto
    {
        public int Id { get; set; }

        public long? DocumentBaseId { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DocumentDate { get; set; }

        public int WorkerId { get; set; }

        public long BundleId { get; set; }

        public decimal Count { get; set; }

        public long StockId { get; set; }
    }
}
