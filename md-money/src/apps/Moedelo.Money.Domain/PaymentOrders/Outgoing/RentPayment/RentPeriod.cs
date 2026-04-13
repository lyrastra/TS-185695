namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment
{
    public class RentPeriod
    {
        public int RentalPaymentItemId { get; set; }

        public decimal PaymentSum { get; set; }
        
        public string Description { get; set; }

        public RentPeriodType PaymentType { get; set; }

        public decimal? PaymentRequiredSum { get; set; }
    }
}
