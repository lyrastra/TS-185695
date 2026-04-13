using System.Collections.Generic;
using Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Events
{
    public class BusinessTripAdvanceStatementCreatedMessage : BaseAdvanceStatementMessage
    {
        public IReadOnlyCollection<BusinessTripItem> Items { get; set; }
    }
}