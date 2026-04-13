using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Bills.Events
{
    public sealed class SalesBillPaymentMessage
    {
        public long DocumentBaseId { get; set; }

        public decimal PaidSum { get; set; }

        public PaidStatus PaidStatus { get; set; }
    }
}
