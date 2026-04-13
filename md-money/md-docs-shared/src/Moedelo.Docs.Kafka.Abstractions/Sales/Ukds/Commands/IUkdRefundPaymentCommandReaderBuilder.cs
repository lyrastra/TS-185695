using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Ukds.Commands
{
    public interface IUkdRefundPaymentCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IUkdRefundPaymentCommandReaderBuilder OnUpdate(Func<UkdUpdateRefundPayment, Task> onCommand);
    }
}