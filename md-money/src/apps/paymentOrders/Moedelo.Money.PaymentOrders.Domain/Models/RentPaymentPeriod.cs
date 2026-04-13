using System;

namespace Moedelo.Money.PaymentOrders.Domain.Models
{
    public class RentPaymentPeriod
    {
        public long PaymentBaseId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int RentalPaymentItemId { get; set; }
        public decimal PaymentSum { get; set; }
    }
}
