using System;
using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.Payments.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Billing.Kafka.Abstractions.Payments
{
    public interface IPaymentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentEventReaderBuilder OnPaymentSuccessSwitch(Func<PaymentEventData, Task> handler);
    }
}