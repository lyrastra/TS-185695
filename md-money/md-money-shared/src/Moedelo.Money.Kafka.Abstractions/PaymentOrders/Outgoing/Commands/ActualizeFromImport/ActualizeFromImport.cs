using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ActualizeFromImport
{
    public class ActualizeFromImport : IEntityCommandData
    {
        public IReadOnlyCollection<ActualizeFromImportItem> Items { get; set; }
    }
}
