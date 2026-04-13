using System;

namespace Moedelo.Money.Domain.PaymentOrders.Duplicates
{
    public class PaymentOrderDuplicateMergeRequest
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
    }
}