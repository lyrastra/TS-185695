using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.FinancialAssistance.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.FinancialAssistance
{
    /// <summary>
    /// ПКО - "Финансовая помощь от учредителя". Чтение событий
    /// </summary>
    public interface IFinancialAssistanceEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IFinancialAssistanceEventReaderBuilder OnCreated(Func<FinancialAssistanceCreated, Task> onEvent);

        IFinancialAssistanceEventReaderBuilder OnUpdated(Func<FinancialAssistanceUpdated, Task> onEvent);

        IFinancialAssistanceEventReaderBuilder OnDeleted(Func<FinancialAssistanceDeleted, Task> onEvent);
    }
}
