using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.LoanObtaining.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.LoanObtaining
{
    /// <summary>
    /// ПКО - "Получение займа или кредита". Чтение событий
    /// </summary>
    public interface ILoanObtainingEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ILoanObtainingEventReaderBuilder OnCreated(Func<LoanObtainingCreated, Task> onEvent);

        ILoanObtainingEventReaderBuilder OnUpdated(Func<LoanObtainingUpdated, Task> onEvent);

        ILoanObtainingEventReaderBuilder OnDeleted(Func<LoanObtainingDeleted, Task> onEvent);
    }
}
