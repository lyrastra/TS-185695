namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders
{
    public class RentPeriodDto
    {
        public int RentalPaymentItemId { get; set; }
        public decimal PaymentSum { get; set; }
        public decimal? PaymentRequiredSum { get; set; }
    }
}
