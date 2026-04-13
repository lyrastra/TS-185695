using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IUnifiedBudgetaryPaymentEventReaderBuilder OnCreated(Func<UnifiedBudgetaryPaymentCreated, Task> onEvent);
        IUnifiedBudgetaryPaymentEventReaderBuilder OnUpdated(Func<UnifiedBudgetaryPaymentUpdated, Task> onEvent);
        IUnifiedBudgetaryPaymentEventReaderBuilder OnDeleted(Func<UnifiedBudgetaryPaymentDeleted, Task> onEvent);

        IUnifiedBudgetaryPaymentEventReaderBuilder OnProvideRequired(Func<UnifiedBudgetaryPaymentProvideRequired, Task> onEvent);
        IUnifiedBudgetaryPaymentEventReaderBuilder OnUpdateAfterAccountingStatementCreated(Func<UnifiedBudgetaryPaymentUpdateAfterAccountingStatementCreated, Task> onEvent);

    }
}