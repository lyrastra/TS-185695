using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Other
{
    public interface IIncomingPaymentOrderOtherCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IIncomingPaymentOrderOtherCommandReaderBuilder OnImport(Func<ImportOtherIncoming, Task> onCommand);
        IIncomingPaymentOrderOtherCommandReaderBuilder OnImportDuplicate(Func<ImportOtherIncomingDuplicate, Task> onCommand);
        IIncomingPaymentOrderOtherCommandReaderBuilder OnImportWithMissingContractor(Func<ImportOtherIncomingWithMissingContragent, Task> onCommand);
    }
}
