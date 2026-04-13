using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Kafka.Abstractions.InvoicePaymentOrder.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.BankIntegrations.Kafka.Abstractions.InvoicePaymentOrder
{
    public interface IInvoicePaymentOrderEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IInvoicePaymentOrderEventReaderBuilder OnChanged(Func<InvoicePaymentOrderChangedEventData, Task> onEvent);
    }
}