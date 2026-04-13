using System;
using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.Bills.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Billing.Kafka.Abstractions.Bills.Readers
{
    public interface IInvoiceBillEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IInvoiceBillEventReaderBuilder OnBillInvoiced(Func<BillInvoiced, Task> onEvent);
        IInvoiceBillEventReaderBuilder OnBillInvoicingFailed(Func<BillInvoicingFailed, Task> onEvent);
    }
}
