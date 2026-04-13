using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.PaymentTransactions.Events
{
    public class PaymentTransactionLinkToPaymentChangeEvent : IEntityEventData
    {
        public int PaymentHistoryId { get; set; }

        public IReadOnlyCollection<PaymentTransactionEventData> PaymentTransactions { get; set; }
    }
}
