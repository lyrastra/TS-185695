using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Domain.Models
{
    public class WorkerPayment
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public decimal Sum { get; set; }
        public TaxationSystemType TaxationSystem { get; set; }
    }
}
