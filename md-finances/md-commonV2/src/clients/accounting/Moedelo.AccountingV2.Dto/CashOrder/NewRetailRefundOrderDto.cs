using System;

namespace Moedelo.AccountingV2.Dto.CashOrder
{
    public class NewRetailRefundOrderDto
    {
        public long CashierId { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public int KontragentId { get; set; }
    }
}