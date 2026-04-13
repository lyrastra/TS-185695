using System;

namespace Moedelo.Money.Domain.PaymentOrders.Private
{
    public class PeriodRequest
    {
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}