namespace Moedelo.Money.PaymentOrders.Dto
{
    public class WorkerChargePaymentDto
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public string WorkerName { get; set; }

        public decimal Sum { get; set; }
    }
}
