using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.Commands.ActualizeFromImport
{
    public class ActualizeFromImport : IEntityCommandData
    {
        public IReadOnlyCollection<ActualizeFromImportItem> Items { get; set; }
    }
}
