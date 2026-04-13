using System;

namespace Moedelo.AccountingV2.Dto.PurchaseInfo
{
    public class PurchaseInfoDto
    {
        public int DocumentBaseId { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal Price { get; set; }
    }
}
