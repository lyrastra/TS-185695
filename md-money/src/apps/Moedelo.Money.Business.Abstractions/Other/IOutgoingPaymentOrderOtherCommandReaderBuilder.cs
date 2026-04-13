using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Other
{
    public interface IOutgoingPaymentOrderOtherCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IOutgoingPaymentOrderOtherCommandReaderBuilder OnImport(Func<ImportOtherOutgoing, Task> onCommand);
        IOutgoingPaymentOrderOtherCommandReaderBuilder OnImportDuplicate(Func<ImportOtherOutgoingDuplicate, Task> onCommand);
        IOutgoingPaymentOrderOtherCommandReaderBuilder OnImportWithMissingContractor(Func<ImportOtherOutgoingWithMissingContragent, Task> onCommand);
    }
}
