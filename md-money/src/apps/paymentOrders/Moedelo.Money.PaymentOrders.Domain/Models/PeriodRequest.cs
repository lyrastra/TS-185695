using System;

namespace Moedelo.Money.PaymentOrders.Domain.Models
{
    public class PeriodRequest
    {
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}