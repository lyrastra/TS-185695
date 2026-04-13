using System.Collections.Generic;
using Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Events
{
    public class GoodsAndServicesAdvanceStatementCreatedMessage : BaseAdvanceStatementMessage
    {
        public IReadOnlyCollection<GoodsAndServicesItem> Items { get; set; }
    }
}