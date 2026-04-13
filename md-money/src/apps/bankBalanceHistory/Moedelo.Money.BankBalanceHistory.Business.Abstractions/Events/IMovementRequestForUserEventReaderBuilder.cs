using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.ImportForUser.Events;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.Events
{
    public interface IMovementRequestForUserEventReaderBuilder: IMoedeloEntityEventKafkaTopicReaderBuilder 
    {
        IMovementRequestForUserEventReaderBuilder OnImportForUserEvent(Func<ImportForUserKafkaEvent, Task> onEvent);
    }
}