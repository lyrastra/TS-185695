using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Ukds.Commands
{
    public class UkdUpdateRefundPayment : IEntityCommandData
    {
        public long DocumentBaseId { get; set; }
    }
}