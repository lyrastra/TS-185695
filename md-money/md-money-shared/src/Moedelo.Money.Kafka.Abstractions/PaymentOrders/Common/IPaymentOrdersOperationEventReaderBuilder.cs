using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common.Events;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common
{
    public interface IPaymentOrdersOperationEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentOrdersOperationEventReaderBuilder OnTypeChanged(Func<OperationTypeChanged, Task> onEvent);
    }
}
