using System;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.CurrencyPayments.Models
{
    public class PeriodRequestDto
    {
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}