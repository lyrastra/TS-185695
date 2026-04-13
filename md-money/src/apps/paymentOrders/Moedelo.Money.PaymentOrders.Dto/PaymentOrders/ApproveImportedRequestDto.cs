using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders
{
    public class ApproveImportedRequestDto
    {
        public int? SettlementAccountId { get; set; }

        public DateTime? Skipline { get; set; }
    }
}
