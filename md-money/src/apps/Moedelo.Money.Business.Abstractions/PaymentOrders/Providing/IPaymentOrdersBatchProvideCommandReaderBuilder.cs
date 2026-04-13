using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Providing
{
    public interface IPaymentOrdersBatchProvideCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IPaymentOrdersBatchProvideCommandReaderBuilder OnProvide(Func<PaymentOrdersBatchProvideCommand, Task> onCommand);
    }
}