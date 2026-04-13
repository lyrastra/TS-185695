using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ActualizeFromImport;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ChangeIsPaidFromIntegration;
using System.Threading.Tasks;
using System;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Commands
{
    public interface IPaymentOrderOutgoingCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IPaymentOrderOutgoingCommandReaderBuilder OnActualize(Func<ActualizeFromImport, Task> onCommand);
        
        IPaymentOrderOutgoingCommandReaderBuilder OnChangeIsPaid(Func<ChangeIsPaidFromIntegrationItem, Task> onCommand);
    }
}