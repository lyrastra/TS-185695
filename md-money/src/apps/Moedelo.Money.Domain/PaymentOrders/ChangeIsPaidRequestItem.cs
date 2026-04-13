using System;

namespace Moedelo.Money.Domain.PaymentOrders
{
    public class ChangeIsPaidRequestItem
    {
        public long DocumentBaseId { get; set; }
        
        public DateTime? Date { get; set; }
        
        public bool? IsPaid { get; set; }
        
        public string PaymentNumber { get; set; }
        
        public string PayerSettlementNumber { get; set; }
    }
}