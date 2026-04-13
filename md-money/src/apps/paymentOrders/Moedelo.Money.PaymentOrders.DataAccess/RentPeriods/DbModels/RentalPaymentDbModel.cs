namespace Moedelo.Money.PaymentOrders.DataAccess.RentPeriods.DbModels
{
    internal sealed class RentalPaymentDbModel
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public long PaymentBaseId { get; set; }
        public int RentalPaymentItemId { get; set; }
        public decimal PaymentSum { get; set; }
    }
}
