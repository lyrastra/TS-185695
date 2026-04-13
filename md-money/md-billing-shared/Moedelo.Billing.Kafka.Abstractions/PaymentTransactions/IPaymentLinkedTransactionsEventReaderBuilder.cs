using System;
using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.PaymentTransactions.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Billing.Kafka.Abstractions.PaymentTransactions
{
    public interface IPaymentLinkedTransactionsEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentLinkedTransactionsEventReaderBuilder OnLinkToPaymentChange(Func<PaymentTransactionLinkToPaymentChangeEvent, Task> onEvent);
    }
}
