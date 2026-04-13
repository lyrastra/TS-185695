namespace Moedelo.Money.PaymentOrders.DataAccess.WorkerPayments.DbModels
{
    class WorkerPaymentDbModel
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public long DocumentBaseId { get; set; }
        public int WorkerId { get; set; }
        public decimal PaymentSum { get; set; }
        public short TakeInTaxationSystem { get; set; }
    }
}
