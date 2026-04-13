using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class CurrencyInvoiceNdsUnifiedPaymentResponseDto
    {
        public long DocumentBaseId { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Number { get; set; }
        
        public decimal Sum { get; set; }
    }
}