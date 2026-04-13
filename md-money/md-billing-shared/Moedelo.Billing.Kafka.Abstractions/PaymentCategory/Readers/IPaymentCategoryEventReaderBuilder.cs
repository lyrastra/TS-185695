using System;
using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.PaymentCategory.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Billing.Kafka.Abstractions.PaymentCategory.Readers;

public interface IPaymentCategoryEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
{
    IPaymentCategoryEventReaderBuilder OnNotesOnPayments(Func<PaymentCategoryEvent, Task> onEvent);
}