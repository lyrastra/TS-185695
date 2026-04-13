using System;

namespace Moedelo.Docs.Dto.PurchaseInfo
{
    public class PurchaseInfoRequestDto
    {
        public long RequestId { get; set; }

        public int KontragentId { get; set; }

        public long StockProductId { get; set; }

        public string ProductName { get; set; }
    }
}