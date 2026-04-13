namespace Moedelo.Money.PaymentOrders.Domain.Models
{
    public class RentPeriod
    {
        public int Id { get; set; }
        public int RentalPaymentItemId { get; set; }
        public decimal PaymentSum { get; set; }
        public decimal? PaymentRequiredSum { get; set; }
    }
}
