using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest
{
    public interface IAccrualOfInterestCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IAccrualOfInterestCommandReaderBuilder OnImport(Func<ImportAccrualOfInterest, Task> onCommand);
        IAccrualOfInterestCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateAccrualOfInterest, Task> onCommand);
    }
}