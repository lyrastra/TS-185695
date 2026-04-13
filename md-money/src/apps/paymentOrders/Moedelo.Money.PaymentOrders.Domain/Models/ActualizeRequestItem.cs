using System;

namespace Moedelo.Money.PaymentOrders.Domain.Models
{
    public class ActualizeRequestItem
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
    }
}
