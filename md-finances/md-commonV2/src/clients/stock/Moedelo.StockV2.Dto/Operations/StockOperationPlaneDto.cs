using System;
using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Operations
{
    public class StockOperationPlaneDto
    {
        public long OperationId { get; set; }

        public StockOperationTypeEnum Type { get; set; }

        public long ProductId { get; set; }

        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}