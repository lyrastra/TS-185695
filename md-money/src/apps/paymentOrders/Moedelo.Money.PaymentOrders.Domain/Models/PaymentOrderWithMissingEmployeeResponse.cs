using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Domain.Models
{
    public class PaymentOrderWithMissingEmployeeResponse
    {
        public long DocumentBaseId { get; set; }
        public OperationType OperationType { get; set; }
        public string WorkerName { get; set; }
        public string WorkerInn { get; set; }
        public string WorkerSettlementAccount { get; set; }
    }
}