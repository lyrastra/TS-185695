using System;

namespace Moedelo.Docs.Dto.PurchaseInfo
{
    public class PurchaseInfoDto
    {
        public long RequestId { get; set; }

        public long DocumentBaseId { get; set; }

        public DateTime DocumentDate { get; set; }

        public decimal Price { get; set; }
    }
}