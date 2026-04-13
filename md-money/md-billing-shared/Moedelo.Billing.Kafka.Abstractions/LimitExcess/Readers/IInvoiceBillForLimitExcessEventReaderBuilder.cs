using System;
using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.LimitExcess.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Billing.Kafka.Abstractions.LimitExcess.Readers
{
    public interface IInvoiceBillForLimitExcessEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IInvoiceBillForLimitExcessEventReaderBuilder OnBillInvoiced(Func<BillInvoiced, Task> onEvent);
        IInvoiceBillForLimitExcessEventReaderBuilder OnBillInvoicingFailed(Func<BillInvoicingFailed, Task> onEvent);
    }
}
