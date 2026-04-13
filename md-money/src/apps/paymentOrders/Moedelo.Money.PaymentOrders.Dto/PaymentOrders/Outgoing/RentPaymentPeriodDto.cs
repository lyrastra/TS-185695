using System;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing
{
    public class RentPaymentPeriodDto
    {
        public long PaymentBaseId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int RentalPaymentItemId { get; set; }
        public decimal PaymentSum { get; set; }
    }
}
